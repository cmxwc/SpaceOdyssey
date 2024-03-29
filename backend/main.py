from fastapi import FastAPI
import uvicorn
from base import database
from classes import *
import argparse
from deta import Deta
from typing import Optional, List, Any

# adding cors headers
from fastapi.middleware.cors import CORSMiddleware

parser = argparse.ArgumentParser(description='Process some integers.')
parser.add_argument('--db', type=str, default="deta",
                    help='select db')
args = parser.parse_args()

deta = Deta()
db = database(args.db)
app = FastAPI()

# adding cors urls
origins = [
    'http://127.0.0.1:5501',
    'http://127.0.0.1:5500',
    'http://localhost:54341/',
    'https://spaceodyssey-teacher-admin.netlify.app'
]
# add middleware
app.add_middleware(CORSMiddleware, 
                    allow_origins=origins, 
                    allow_credentials=True,
                    allow_methods=["*"],
                    allow_headers=["*"]
)


@app.get("/")
def read_root():
    return {"Hello": "Hello World"}


##### AUTHENTICATION REQUESTS ###############################
@app.post("/register_student", tags=['authentication'])
def register_student(data: User):
    authpath = "auth_student"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username == i['username']:
            return "Username already exists!"

    data_to_add = {
        "username": username,
        "password": password,
    }
    db.save_json(authpath, data_to_add)
    # users = deta.Base("auth_student")
    # users.put(data_to_add)
    # db.save_json("student_info", {"username": username})

    return "Successfully registered!"


@app.post("/login_student", tags=['authentication'])
def login_student(data: User):
    authpath = "auth_student"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username == i['username']:
            if password == i['password']:
                return "Successfully authenticated"
            else:
                return "Wrong password, try again!"

    return "Username does not exist!"


@app.post("/register_teacher", tags=['authentication'])
def register_teacher(data: User):
    authpath = "auth_teacher"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username == i['username']:
            return "Username already exists!"

    data_to_add = {
        "username": username,
        "password": password,
    }
    db.save_json(authpath, data_to_add)

    return "Successfully registered!"


@app.post("/login_teacher", tags=['authentication'])
def login_teacher(data: User):
    authpath = "auth_teacher"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username == i['username']:
            if password == i['password']:
                return "Successfully authenticated"
            else:
                return "Wrong password, try again!"

    return "Username does not exist!"


######### USER REQUESTS ###############################################
@app.get("/users", tags=['user'])
def get_all_users():
    authpath = "auth_student"
    data = db.load_json(authpath)
    authpath = "auth_teacher"
    data.extend(db.load_json(authpath))
    return data


@app.get("/get_teachers", tags=['user'])
def get_all_teachers():
    authpath = "auth_teacher"
    data = db.load_json(authpath)
    return data


@app.get("/get_students", tags=['user'])
def get_all_students():
    authpath = "auth_student"
    data = db.load_json(authpath)
    return data


@app.post("/add_userData", tags=['userData'])
async def add_userData(data: UserData):
    authpath = "userData"
    olddata = db.load_json(authpath)
    for i in olddata:
        if i['username'] == data.username:
            return "Failed: Username already registered previously"

    data_to_add = data.dict()
    db.save_json(authpath, data_to_add)
    return "UserData successfully registered"


@app.post("/update_userData_key", tags=['userData'])
async def update_userData_key(username: str, key: str, value: Any):
    """
    Updates a single field in an item in the userData schema
    """
    authpath = "userData"
    users = deta.Base(authpath)
    old_data = users.fetch().items
    found = False
    for i in old_data:
        if i['username'] == username:
            key_id = i['key']
            found = True
            break
    if not found:
        return "UserData Failed to Update - Username Not Found"
    item = users.get(key_id)
    field_type = type(item[key])
    if field_type == type(value):
        pass  # no need to convert, they already have the same type
    elif isinstance(item[key], int):
        value = int(value)
    elif isinstance(item[key], float):
        value = float(value)
    elif isinstance(item[key], str):
        value = str(value)
    else:
        raise TypeError("Cannot convert types")
    item[key] = value
    users.put(item)
    return "UserData successfully updated" 

@app.post("/update_userData", tags=['userData'])
async def update_userData(data: UserData):
    authpath = "userData"
    users = deta.Base(authpath)
    old_data = users.fetch().items
    found = False
    for i in old_data:
        if i['username'] == data.username:
            key_id = i['key']
            found = True
            break
    users.delete(key_id)
    users.put(data.dict())
    response = "UserData successfully updated" if found else "UserData Failed to Update - Username Not Found"
    return response

@app.get("/get_userData", tags=['userData'])
# @app.post("/get_userData", tags=['userData'])
async def get_userData(username: str):
    authpath = "userData"
    data = db.load_json(authpath)
    for i in data:
        if i['username'] == username:
            return i
    return "No User Found"


@app.get("/get_userData_all", tags=['userData'])
# @app.post("/get_userData_all", tags=['userData'])
async def get_userData_all():
    authpath = "userData"
    data = db.load_json(authpath)
    return data

############# QUESTIONS REQUESTS ################################################
@app.post("/add_question", tags=['question'])
async def add_question(data: Question):
    authpath = "questionData"
    olddata = db.load_json(authpath)
    for i in olddata:
        if i['questionSubject'] == data.questionSubject and i['year'] == data.year:
            if i['questionId'] == data.questionId:
                return "Failed: Question ID already exists"

    data_to_add = data.dict()
    db.save_json(authpath, data_to_add)
    return "Question successfully added"

@app.get("/get_question_all", tags=['question'])
async def get_question_all():
    authpath="questionData"
    data = db.load_json(authpath)
    return data

@app.get("/get_question_by_subject", tags=['question'])
async def get_question_by_id(subject: str):
    authpath = "questionData"
    data = db.load_json(authpath)
    result = []
    for i in data:
        if i['questionSubject'] == subject:
            result.append(i)
            
    if result == []:
        return "No question with subject {} found".format(subject)
    else:
        return result

@app.get("/get_question_by_subject_topic", tags=['question'])
async def get_question_by_subject_topic(subject: str, topic: int):
    authpath = "questionData"
    data = db.load_json(authpath)
    result = []
    for i in data:
        if i['questionSubject'] == subject and i['questionTopic'] == topic:
            result.append(i)
            
    if result == []:
        return "No question with subject {} and topic {} found".format(subject, topic)
    else:
        return result
    
@app.get("/get_question_by_subject_topic_difficulty", tags=['question'])
async def get_question_by_subject_topic_difficulty(subject: str, topic: int, difficulty: int, year: int):
    authpath = "questionData"
    data = db.load_json(authpath)
    result = []
    for i in data:
        if i['questionSubject'] == subject and i['questionTopic'] == topic and i['questionDifficulty'] == difficulty and i['year'] == year:
            result.append(i)
            
    if result == []:
        return "No question with subject {} and topic {}, difficulty {}, year {} found".format(subject, topic, difficulty, year)
    else:
        return result


@app.get("/get_question_by_subject_id_topic", tags=['question'])
async def get_question_by_id(subject: str, questionId: int, topic: int):
    authpath = "questionData"
    data = db.load_json(authpath)
    for i in data:
        if i['questionSubject'] == subject and i['questionId'] == questionId and i['questionTopic'] ==  topic :
            return i
    return "No question with subject {}, topic {} and id {} found".format(subject, topic, questionId)

@app.post("/delete_question_bank", tags=['question'])
async def delete_question_bank(subject: str, year: int):
    authpath = "questionData"
    users = deta.Base(authpath)
    old_data = users.fetch().items
    for i in old_data:
        if i["questionSubject"] == subject and i["year"] == year:
            users.delete(i["key"])

    return "Question bank for subject [{}], year {} deleted".format(subject, year)

######## GAME DATA REQUEST #######################################
def add_id_record(data):
    """
    Add the game id for game records
    """
    authpath = "gameRecordData"
    num_docs = len(list(db.load_json(authpath)))
    data["gameId"] = num_docs + 1
    return data

@app.post("/add_gamerecord", tags=['game data'])
async def add_gamerecord(data: GameRecord):
    authpath = "gameRecordData"
    data_to_add = data.dict()
    data_to_add = add_id_record(data_to_add)
    db.save_json(authpath, data_to_add)
    return "New game record added!"

@app.get("/get_gamerecord", tags=['game data'])
async def get_gamerecord():
    authpath="gameRecordData"
    data = db.load_json(authpath)
    return data

@app.get("/get_gamerecord_user", tags=['game data'])
async def get_gamerecord(username: str):
    authpath="gameRecordData"
    data = db.load_json(authpath)
    result = []
    for i in data:
        if i["username"] == username:
            result.append(i)
    result = sorted(result, key=lambda x: x["gameId"], reverse=True)
    return result

@app.post("/add_question_battle_record", tags=['game data'])
async def add_question_battle_record(data: QuestionBattleRecord):
    authpath = "questionRecordData"
    data_to_add = data.dict()
    data_to_add = add_id_record(data_to_add)
    db.save_json(authpath, data_to_add)
    return "New question battle record added!"

@app.post("/get_question_battle_record", tags=['game data'])
async def get_question_battle_record():
    authpath="questionRecordData"
    data = db.load_json(authpath)
    return data

# @app.post("/get_avg_subject", tags=['game data'])
# async def get_stats():
#     authpath1="questionRecordData"
#     authpath2="gameRecordData"
#     qns = deta.Base(authpath1)
#     games = deta.Base(authpath2)
#     for i in games:
#         for j in qns:
#         i["subject"] = qns.get(i["gameId"])
#     return data



# @app.post("/update_question_bank", tags=['question'])
# async def update_question_bank(data: List[Question]):
#     authpath = "questionData"
#     users = deta.Base(authpath)
#     for i in data:
#         i.dict()
#         users.put(i)

#     return "Question bank for subject updated"


######## SCORES REQUEST #######################################
@app.post("/add_highscore", tags=['scores'])
async def add_highscore(data: HighScores):
    authpath = "highscoreData"
    data_to_add = data.dict()
    users = deta.Base(authpath)
    old_data = users.fetch().items
    for i in old_data:
        if i["username"] == data.username and i["subject"] == data.subject:
            if i["score"] < data.score:
                users.delete(i["key"])
            else:
                return "No new highscore"
    users.put(data_to_add)
    return "New highscore successfully added"
    


@app.post("/get_highscore", tags=['scores'])
async def get_highscore(subject: str):
    authpath="highscoreData"
    result = []
    data = db.load_json(authpath)
    for i in data:
        if i["subject"] == subject:
            result.append(i)
    return result

######## ACHIEVEMENTS REQUEST #######################################
@app.post("/add_achievements", tags=['scores'])
async def update_userData(username: str, achievement: str):
    authpath = "achievementsData"
    users = deta.Base(authpath)
    old_data = users.fetch().items

    for i in old_data:
        if i["username"] == username:
            i[achievement] = True
            users.delete(i["key"])
            users.put(i)

            return "Updated"
    
    data_to_add = {"username": username, achievement: True}
    users.put(data_to_add)
    return "AchievementsData successfully updated"

@app.get("/get_achievements", tags=['scores'])
async def get_highscore(username: str):
    authpath="achievementsData"
    data = db.load_json(authpath)
    for i in data:
        if i["username"] == username:
            return i
    return "No achievements yet"

if __name__ == "__main__":
    host = "0.0.0.0"
    port = 8000
    print("="*10)
    print(f"View Documentation at - http://localhost:{port}/docs")
    print("="*10)
    uvicorn.run("main:app", host=host, port=port,
                log_level="info", reload=True)

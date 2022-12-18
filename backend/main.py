from fastapi import FastAPI
import uvicorn
from base import database
from classes import *

# adding cors headers
from fastapi.middleware.cors import CORSMiddleware


db = database('Space_DB')
app = FastAPI()

# adding cors urls
origins = [
    'http://127.0.0.1:5500',
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


# AUTHENTICATION REQUESTS
@app.post("/register_student", tags=['authentication'])
def register_student(data: User):
    authpath = "auth_student"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username in i['username']:
            return "Username already exists!"

    data_to_add = {
        "username": username,
        "password": password,
    }
    db.save_json(authpath, data_to_add)
    db.save_json("student_info", {"username": username})

    return "User successfully registered"


@app.post("/login_student", tags=['authentication'])
def login_student(data: User):
    authpath = "auth_student"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username in i['username']:
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
        if username in i['username']:
            return "Username already exists!"

    data_to_add = {
        "username": username,
        "password": password,
    }
    db.save_json(authpath, data_to_add)

    return "Successfully registered!", data_to_add


@app.get("/login_teacher", tags=['authentication'])
def login_teacher(data: User):
    authpath = "auth_teacher"
    username = data.username
    password = data.password

    data = db.load_json(authpath)
    for i in data:
        if username in i['username']:
            if password == i['password']:
                return "Successfully logged in!"
            else:
                return "Wrong password, try again!"

    return "Username does not exist!"


# USER REQUESTS
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


@app.post("/update_userData", tags=['userData'])
async def update_userData(data: UserData):
    authpath = "userData"
    data_to_add = data.dict()
    olddata = db.load_json(authpath)
    for idx, i in enumerate(olddata):
        if i['username'] == data.username:
            olddata[idx].lastLoginDay = data_to_add.lastLoginDay

            db.update_json(authpath, olddata)
            return "UserData successfully updated"
    return "UserData Failed to Update - Username Not Found"

@app.post("/update_userData_login", tags=['userData'])
async def update_userData(data: UserDataLogin):
    authpath = "userData"
    data_to_add = data.dict()
    olddata = db.load_json(authpath)
    for idx, i in enumerate(olddata):
        if i['username'] == data.username:
            olddata[idx]["lastLoginDay"] = data_to_add["lastLoginDay"]

            db.update_json(authpath, olddata)
            return "UserData successfully updated"
    return "UserData Failed to Update - Username Not Found"


@app.get("/get_userData", tags=['userData'])
@app.post("/get_userData", tags=['userData'])
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

# QUESTIONS REQUESTS
@app.post("/add_question", tags=['question'])
async def add_question(data: Question):
    authpath = "questionData"
    olddata = db.load_json(authpath)
    for i in olddata:
        if i['questionSubject'] == data.questionSubject:
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

if __name__ == "__main__":
    host = "0.0.0.0"
    port = 8000
    uvicorn.run("main:app", host=host, port=port,
                log_level="info", reload=True)

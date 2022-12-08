from fastapi import FastAPI
import uvicorn
from base import database
from classes import *

db = database('Space_DB')
app = FastAPI()


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

    return "Successfully registered!", data_to_add


@app.get("/login_student", tags=['authentication'])
def login_student(data: User):
    authpath = "auth_student"
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


@app.post("/add_student_info", tags=['user'])
def add_student_info(data: UserInfo):
    authpath = "student_info"
    data_to_add = data.dict()

    data = db.load_json(authpath)
    # return data[0]['username']
    for index, i in enumerate(data):
        if data_to_add['username'] == i['username']:
            data[index] = data_to_add
            db.update_json(authpath, data)
            return "Successfully updated", data

    return "Failed to update, username not found!"


@app.get("/get_student_info", tags=['user'])
def get_student_info():
    authpath = "student_info"
    data = db.load_json(authpath)
    return data


if __name__ == "__main__":
    host = "0.0.0.0"
    port = 8000
    uvicorn.run("main:app", host=host, port=port,
                log_level="info", reload=True)
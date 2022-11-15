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
        else:
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
        else:
            return "Username does not exist!"

# USER REQUESTS


@app.get("/users", tags=['user'])
async def get_all_users():
    authpath = "auth_student"
    data = db.load_json(authpath)
    authpath = "auth_teacher"
    data += db.load_json(authpath)
    return data


if __name__ == "__main__":
    host = "0.0.0.0"
    port = 8000
    uvicorn.run("main:app", host=host, port=port,
                log_level="info", reload=True)

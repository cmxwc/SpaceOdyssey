from cgi import print_exception
from fastapi import FastAPI, status, HTTPException
from pydantic import BaseModel
from typing import Optional, List
from database import SessionLocal
import uvicorn
import json
import models
from classes import *

app = FastAPI()

db = SessionLocal()


@app.get('/users', response_model=List[User], status_code=200)
def get_all_users():
    users = db.query(models.User).all()

    return users


@app.post('/users', response_model=User,
          status_code=status.HTTP_201_CREATED)
def register_user(user: User):
    db_item = db.query(models.User).filter(
        models.User.username == user.username).first()

    if db_item is not None:
        raise HTTPException(status_code=400, detail="User already exists")

    new_user = models.User(
        username=user.username,
        password=user.password,
        name=user.name
    )

    db.add(new_user)
    db.commit()

    return new_user


if __name__ == "__main__":
    host = "0.0.0.0"
    port = 8000
    uvicorn.run("main:app", host=host, port=port,
                log_level="info", reload=True)


'''
@app.get('/items', response_model=List[Item], status_code=200)
def get_all_items():
    items = db.query(models.Item).all()

    return items


@app.get('/items/{item_id}', response_model=Item, status_code=status.HTTP_200_OK)
def get_an_item(item_id: int):
    item = db.query(models.Item).filter(models.Item.id == item_id).first()
    return item


@app.post('/items', response_model=Item,
          status_code=status.HTTP_201_CREATED)
def create_an_item(item: Item):
    db_item = db.query(models.Item).filter(
        models.Item.name == item.name).first()

    if db_item is not None:
        raise HTTPException(status_code=400, detail="Item already exists")

    new_item = models.Item(
        name=item.name,
        price=item.price,
        description=item.description,
        on_offer=item.on_offer
    )

    db.add(new_item)
    db.commit()

    return new_item


@app.put('/item/{item_id}', response_model=Item, status_code=status.HTTP_200_OK)
def update_an_item(item_id: int, item: Item):
    item_to_update = db.query(models.Item).filter(
        models.Item.id == item_id).first()
    item_to_update.name = item.name
    item_to_update.price = item.price
    item_to_update.description = item.description
    item_to_update.on_offer = item.on_offer

    db.commit()
    return item_to_update


@app.delete('/item/{item_id}')
def delete_item(item_id: int):
    item_to_delete = db.query(models.Item).filter(
        models.Item.id == item_id).first()

    if item_to_delete is None:
        raise HTTPException(
            status_code=status.HTTP_404_NOT_FOUND, detail="Resource not found")

    db.delete(item_to_delete)
    db.commit()

    return item_to_delete
'''

# @app.get('/')
# def index():
#     return {"message": "Hello World"}


# @app.get('/greet/{name}')
# def greet_name(name: str):
#     return {"greeting": f"Hello {name}"}


# @app.get('/greet')
# def greet_optional_name(name: Optional[str] = "user"):  # default is user
#     return {"message": f"Hello {name}"}


# @app.put('/item/{item_id}')
# def update_item(item_id: int, item: Item):
#     return {"name": item.name,
#             "description": item.description,
#             "price": item.price,
#             "on_offer": item.on_offer}

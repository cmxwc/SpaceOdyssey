from fastapi.testclient import TestClient
import sys
import os
sys.path.insert(1, os.path.join(sys.path[0], '..'))
from main import app
import main
from base import database

main.db = database("TEST") 


client = TestClient(app)

def test_register_teacher():
  response = client.post("/register_teacher", json={
     "username": "string",
     "password": "string"
  }
  )
  assert response.status_code == 200


def test_login_teacher():
  response = client.post("/login_teacher", json={
    "username": "string",
    "password": "string"
  }
  )
  assert response.status_code == 200
    
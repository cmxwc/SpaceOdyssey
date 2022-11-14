from pydantic import BaseModel
from typing import Optional, List


class BaseModel(BaseModel):
    # https://stackoverflow.com/questions/69504352/fastapi-get-request-results-in-typeerror-value-is-not-a-valid-dict
    # SQLAlchemy does not return a dictionary, which is what pydantic expects by default, so need to include this
    class Config:
        orm_mode = True


class User(BaseModel):
    username: str
    password: str
    name: str


class UserInfo(BaseModel):
    username: str
    name: str
    userCharacter: int
    highestScore: int


class Question(BaseModel):
    questionId: int
    questionSubject: str
    questionTopic: str
    questionText: str
    questionAnsIndex: int
    questionAnsText: List[str]
    questionDifficulty: str

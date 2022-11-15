from pydantic import BaseModel
from typing import Optional, List


class User(BaseModel):
    username: str
    password: str


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

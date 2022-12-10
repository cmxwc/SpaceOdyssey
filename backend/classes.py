from pydantic import BaseModel
from typing import Optional, List


class User(BaseModel):
    username: str
    password: str


class UserData(BaseModel):
    username: str
    classNumber: int
    highestScore: int
    numOfGamesCompleted: int
    levelsUnlocked: List[int]
    subjectsTaken: List[str]
    lastLoginDay: str


class Question(BaseModel):
    questionId: int
    questionSubject: str
    questionTopic: int
    questionText: str
    questionAnsIndex: int
    questionAnsText: List[str]
    questionDifficulty: str

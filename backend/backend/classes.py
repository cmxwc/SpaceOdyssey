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
    subjectsTaken: List[str]
    lastLoginDay: str

class GameRecord(BaseModel):
    gameId: Optional[int] = None
    username: str
    score: int
    numberCorrect: int
    questionSubject: str
    questionTopic: int
    weakestLearningObj: str
    dateOfGame: str
    completed: bool
    timeTaken: float

class QuestionBattleRecord(BaseModel):
    username: str
    questionSubject: str
    questionId: int
    gameId: Optional[int] = None
    correct: bool

class Question(BaseModel):
    questionId: int
    questionSubject: str
    questionTopic: int
    questionDifficulty: int
    questionText: str
    questionAns: int
    option1: str
    option2: str
    option3: str
    option4: str
    questionLearningObj: str

class UserDataLogin(BaseModel):
    username: str
    lastLoginDay: str

class HighScores(BaseModel):
    username: str
    subject: str
    score: int

class Achievements(BaseModel):
    username: str
    achievementIdx: int 
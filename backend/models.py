from database import Base
from sqlalchemy import String, Boolean, Integer, Column, Text, ARRAY


class User(Base):
    __tablename__ = 'user'
    username = Column(String(255), nullable=False,
                      unique=True, primary_key=True)
    password = Column(String(255), nullable=False)
    name = Column(String(255), nullable=False)

    # def __repr__(self):
    #     return f"<Item name={self.username} price={self.password}>"


class UserInfo(Base):
    __tablename__ = 'user info'
    username = Column(String(255), nullable=False,
                      unique=True, primary_key=True)
    name = Column(String(255), nullable=False)
    userCharacter = Column(Integer, nullable=False)
    highestScore = Column(Integer)


class Question(Base):
    __tablename__ = 'question'
    questionId = Column(Integer, nullable=False, primary_key=True)
    questionSubject = Column(String(255), nullable=False)
    questionTopic = Column(String(255), nullable=False)
    questionText = Column(String(255), nullable=False)
    questionAnsIndex = Column(Integer, nullable=False)
    questionAnsText = Column(ARRAY(String))
    questionDifficulty = Column(String(255), nullable=False)

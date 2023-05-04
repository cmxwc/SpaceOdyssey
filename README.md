# SpaceOdyssey

<!-- TABLE OF CONTENTS -->
<details>
  <summary>Table of Contents</summary>
  <ol>
    <li>
      <a href="#about-the-project">About The Project</a>
      <ul>
        <li><a href="#built-with">Built With</a></li>
      </ul>
    </li>
    <li>
      <a href="#getting-started">Getting Started</a>
    </li>
    <li><a href="#usage">Usage</a></li>
  </ol>
</details>



<!-- ABOUT THE PROJECT -->
## About The Project
The key aim of this project is to develop a game that seeks to gamify learning while incorporating learning analytics. This project consists of two applications – Space Odyssey, a game for students, and a teacher web application, for teachers to manage learning content. 
This project directory contains the files used to build the frontend Space Odyssey game and teacher web application, and the backend development.

![overview]

### Space Odyssey

Space Odyssey is a space-themed game where students can visit different planets in various galaxies. As they free-roam the map, they will encounter enemies whom they must battle. The battle involves answering questions related to different academic topics which are set by teachers. Space Odyssey has multiple features including a leader board, achievements tracking and a review function for students to learn from previous mistakes.

### Teacher Web Application
The teacher web application allows teachers to modify and set questions for the game. At the same time, learning analytics is performed on user data from the game and visualised on the teacher web application, so teachers can track the performance of students.


### Built With

Space Odyssey Game:
* [![Unity][Unity]][Unity-url]

Backend:
* [![FastAPI][FastAPI]][FastAPI-url]
* Deta Space



<!-- GETTING STARTED -->
## Getting Started

This is an example of how you may start playing the game. 
To get a local copy up and running simply download the `Space Odyssey.exe` file onto your local computer. Open the executable file and it is ready for playing!
* To start playing, open the executable file and register an account

The teacher web application can be accessed through here: https://spaceodyssey-teacher-admin.netlify.app/
* To login, an example user can be used
* Username: teacher, Password: string

If you would like to view the backend server through the FastAPI documentation, you may access it through this url: https://space_backend-1-f3793365.deta.app/docs



<!-- USAGE EXAMPLES -->
## Usage

The following screenshots show the user flow when playing the Space Odyssey game or using the Teacher Web Application:
![space1]
![space2]
![teacher]


<!-- MARKDOWN LINKS & IMAGES -->
[overview]: images/Overview.png
[space1]: images/SpaceOdyssey1.png
[space2]: images/SpaceOdyssey2.png
[teacher]: images/TeacherApp.png

[Unity]: https://img.shields.io/badge/Unity-000000?style=for-the-badge&logo=unity&logoColor=white
[Unity-url]: https://unity.com/
[FastAPI]: https://img.shields.io/badge/FastAPI-35495E?style=for-the-badge&logo=fastapi&logoColor=4FC08D
[FastAPI-url]: https://fastapi.tiangolo.com/

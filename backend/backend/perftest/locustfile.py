from locust import HttpUser, task, constant


class LoadTesting(HttpUser):
    wait_time = constant(8)

    # @task
    # def loadtest(self):
    #     self.client.get("/get_Leaderboard")
    # @task
    # def loadtest1(self):
    #     self.client.get("/get_question_filtered?world=string&section=string&limit=1")
    @task
    def loadtest2(self):
        self.client.post("/login_student", json={"username": "string","password": "string"})
    @task
    def loadtest3(self):
        self.client.post("/login_teacher", json={"username": "string","password": "string"})
    # @task
    # def loadtest4(self):
    #     self.client.get("/get_userData", params={"username":"str"})
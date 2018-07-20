from locust import HttpLocust, TaskSet, task


class WebsiteTasks(TaskSet):
    @task
    def index(self):
        self.client.get("/repositories/lumyslinski/app_projects")


class WebsiteUser(HttpLocust):
    task_set = WebsiteTasks
    host = 'http://127.0.0.1:8080'
    min_wait = 5000
    max_wait = 15000
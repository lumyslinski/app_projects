from flask          import Flask
from flask_restful  import Resource, Api
from GitHubApi      import GitHubApi

class SimpleText(Resource):
    def get(self):
        return 'You can query only in this form: /repositories/<owner>/<repositoryName>'

app         = Flask(__name__)
api         = Api(app)
api.add_resource(SimpleText, '/')
api.add_resource(GitHubApi, '/repositories/<string:owner>/<string:repositoryName>')
from flask import Flask
from flask_restful import Resource, Api
from GitHubApi import GitHubApi

urlmin = '/repositories/<string:owner>/<string:repository>'
urlmax = urlmin + '/<string:token>'


class SimpleText(Resource):
    def get(self):
        return {'message': 'You can query only in this form: '+urlmax}


app = Flask(__name__)
api = Api(app)
api.add_resource(SimpleText, '/')
api.add_resource(GitHubApi, urlmin, urlmax)

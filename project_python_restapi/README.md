# About

Create a simple REST service which will return details of given Github repository. Details should
include:
- full name of repository
- description of repository
- git clone url
- number of stargazers
- date of creation (ISO 8601 format)

The API of the service might look as follows:
```
GET /repositories/{owner}/{repository-name}
{
"fullName": "...",
"description": "...",
"cloneUrl": "...",
"stars": 0,
"createdAt": "..."
}
```
GitHub API reference can be found at: https://developer.github.com/v3/
####Non functional requirements:
- should be able to serve 20 requests per second (assuming we have premium GitHub
account; simply put: application should not have obvious scaling bottlenecks)
- set of end-to-end tests that can be run easily
- good design and quality of code including documentation
- ready to deploy to production (if additional work is needed, please specify it)

It is okay to make tradeoffs or to simplify the solution as long as you leave a note describing
your thought process.
Your solution should be delivered as a private git repository on http://bitbucket.org. Create a pull Request from any branch to master. Grant read access to your repository to allegrotech (our bitbucket user) and add this user as a
reviewer to your Pull Request.

# Project
### Requirements to run this project:
- linux (tested on ubuntu) and 8080 unused port
- installed chrome
- installed [docker](https://docs.docker.com/install/linux/docker-ce/ubuntu/#set-up-the-repository)
- installed curl
- installed ab ( Apache HTTP server benchmarking tool: ``` sudo apt-get install apache2-utils ```)

### Problems:
- sometimes docker stucks with this error:
  >Error response from daemon: driver failed programming external connectivity on endpoint - 
  ``` docker stop $(docker ps -a -q); docker rm $(docker ps -a -q); docker volume rm $(docker volume ls -qf dangling=true) ```

### Structure:
- **app**:
    - api.token: it is a file with my secret token to query github api without restrictions
    - GitHubApi.py: it is a class wrapper for github api
    - MainApp.py: it is an implementation of flask server that starts rest api
    - Wsgi.py: it is a code for starting flask server from uWsgi server
- **conf**:
    - nginx.conf: it is a configuration file for nginx
    - requirements.txt: it is a configuration file for installing python packages when docker starts
    - supervisord.conf: it is a configuration file for app that starts all servers when linux is starting up in docker
    - wsgi.ini: it is a configuration file for uWsgi server
- **test**:
    - funkload: it is a directory of implemenation tests in funkload
    - locusTest.py: it is a simple test for locus package
    - unitTest.py: it is a unit test for testing querying github api and the response with required data  
- **Dockerfile**: file for running in docker
- **RunApp.sh**: it runs app in docker
- **RunAppTest.sh**: it runs app tests  
- #####Run.sh: it runs app in docker and app test, so it is a main start for the project
  
# Test results
There are two types of tests in this project:
1. Unit test to check working functionality (it is in test/unitTest.py)
2. Stress test to check 20 multiple requests at one time. I used ab (Apache HTTP server benchmarking tool)

### Result of stress tests:
First example is to stress test only when api is running. So I decided to run test with 1000 requests and 20 multiple requests at a time:
```
ab -n 1000 -c 20 http://localhost:8080/

Server Software:        nginx/1.14.0
Server Hostname:        localhost
Server Port:            8080

Document Path:          /
Document Length:        74 bytes

Concurrency Level:      20
Time taken for tests:   0.191 seconds
Complete requests:      1000
Failed requests:        0
Total transferred:      223000 bytes
HTML transferred:       74000 bytes
Requests per second:    5227.77 [#/sec] (mean)
Time per request:       3.826 [ms] (mean)
Time per request:       0.191 [ms] (mean, across all concurrent requests)
Transfer rate:          1138.47 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.3      0       2
Processing:     1    4   2.9      3      25
Waiting:        1    3   2.9      3      24
Total:          1    4   2.9      3      25

Percentage of the requests served within a certain time (ms)
  50%      3
  66%      4
  75%      4
  80%      4
  90%      5
  95%      7
  98%     20
  99%     22
 100%     25 (longest request)
```
Second example is to check when api is working with specified query that invokes github api. This is a fully working example of 50 requests and 20 multiple users at a time 
``` 
ab -n 50 -c 20 http://127.0.0.1:8080/repositories/lumyslinski/app_projects

Server Software:        nginx/1.14.0
Server Hostname:        127.0.0.1
Server Port:            8080

Document Path:          /repositories/lumyslinski/app_projects
Document Length:        224 bytes

Concurrency Level:      20
Time taken for tests:   10.560 seconds
Complete requests:      50
Failed requests:        0
Total transferred:      18700 bytes
HTML transferred:       11200 bytes
Requests per second:    4.73 [#/sec] (mean)
Time per request:       4224.021 [ms] (mean)
Time per request:       211.201 [ms] (mean, across all concurrent requests)
Transfer rate:          1.73 [Kbytes/sec] received

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:        0    0   0.2      0       1
Processing:   522 3010 938.3   3113    4725
Waiting:      522 3009 938.3   3113    4725
Total:        522 3010 938.2   3113    4725

Percentage of the requests served within a certain time (ms)
  50%   3113
  66%   3269
  75%   3680
  80%   3790
  90%   4191
  95%   4223
  98%   4725
  99%   4725
 100%   4725 (longest request)
```

# My steps to solve this project:
1.Get knowledge about github api:
 - Created user token in order to server 5000 requests per hour from github api. It is a safer method than providing username and password with api url
 - Example of querying github api and response: 
 
 ```
curl -H "Authorization: token 2e016c12ad016e068eac7116d982406f43c6fc7e" https://api.github.com/repos/lumyslinski/app_projects
{
  "id": 138361396,
  "node_id": "MDEwOlJlcG9zaXRvcnkxMzgzNjEzOTY=",
  "name": "app_projects",
  "full_name": "lumyslinski/app_projects",
  "owner": {
    "login": "lumyslinski",
    "id": 764448,
    "node_id": "MDQ6VXNlcjc2NDQ0OA==",
    "avatar_url": "https://avatars2.githubusercontent.com/u/764448?v=4",
    "gravatar_id": "",
    "url": "https://api.github.com/users/lumyslinski",
    "html_url": "https://github.com/lumyslinski",
    "followers_url": "https://api.github.com/users/lumyslinski/followers",
    "following_url": "https://api.github.com/users/lumyslinski/following{/other_user}",
    "gists_url": "https://api.github.com/users/lumyslinski/gists{/gist_id}",
    "starred_url": "https://api.github.com/users/lumyslinski/starred{/owner}{/repo}",
    "subscriptions_url": "https://api.github.com/users/lumyslinski/subscriptions",
    "organizations_url": "https://api.github.com/users/lumyslinski/orgs",
    "repos_url": "https://api.github.com/users/lumyslinski/repos",
    "events_url": "https://api.github.com/users/lumyslinski/events{/privacy}",
    "received_events_url": "https://api.github.com/users/lumyslinski/received_events",
    "type": "User",
    "site_admin": false
  },
  "private": false,
  "html_url": "https://github.com/lumyslinski/app_projects",
  "description": "This category contains mine software implementations",
  "fork": false,
  "url": "https://api.github.com/repos/lumyslinski/app_projects",
  "forks_url": "https://api.github.com/repos/lumyslinski/app_projects/forks",
  "keys_url": "https://api.github.com/repos/lumyslinski/app_projects/keys{/key_id}",
  "collaborators_url": "https://api.github.com/repos/lumyslinski/app_projects/collaborators{/collaborator}",
  "teams_url": "https://api.github.com/repos/lumyslinski/app_projects/teams",
  "hooks_url": "https://api.github.com/repos/lumyslinski/app_projects/hooks",
  "issue_events_url": "https://api.github.com/repos/lumyslinski/app_projects/issues/events{/number}",
  "events_url": "https://api.github.com/repos/lumyslinski/app_projects/events",
  "assignees_url": "https://api.github.com/repos/lumyslinski/app_projects/assignees{/user}",
  "branches_url": "https://api.github.com/repos/lumyslinski/app_projects/branches{/branch}",
  "tags_url": "https://api.github.com/repos/lumyslinski/app_projects/tags",
  "blobs_url": "https://api.github.com/repos/lumyslinski/app_projects/git/blobs{/sha}",
  "git_tags_url": "https://api.github.com/repos/lumyslinski/app_projects/git/tags{/sha}",
  "git_refs_url": "https://api.github.com/repos/lumyslinski/app_projects/git/refs{/sha}",
  "trees_url": "https://api.github.com/repos/lumyslinski/app_projects/git/trees{/sha}",
  "statuses_url": "https://api.github.com/repos/lumyslinski/app_projects/statuses/{sha}",
  "languages_url": "https://api.github.com/repos/lumyslinski/app_projects/languages",
  "stargazers_url": "https://api.github.com/repos/lumyslinski/app_projects/stargazers",
  "contributors_url": "https://api.github.com/repos/lumyslinski/app_projects/contributors",
  "subscribers_url": "https://api.github.com/repos/lumyslinski/app_projects/subscribers",
  "subscription_url": "https://api.github.com/repos/lumyslinski/app_projects/subscription",
  "commits_url": "https://api.github.com/repos/lumyslinski/app_projects/commits{/sha}",
  "git_commits_url": "https://api.github.com/repos/lumyslinski/app_projects/git/commits{/sha}",
  "comments_url": "https://api.github.com/repos/lumyslinski/app_projects/comments{/number}",
  "issue_comment_url": "https://api.github.com/repos/lumyslinski/app_projects/issues/comments{/number}",
  "contents_url": "https://api.github.com/repos/lumyslinski/app_projects/contents/{+path}",
  "compare_url": "https://api.github.com/repos/lumyslinski/app_projects/compare/{base}...{head}",
  "merges_url": "https://api.github.com/repos/lumyslinski/app_projects/merges",
  "archive_url": "https://api.github.com/repos/lumyslinski/app_projects/{archive_format}{/ref}",
  "downloads_url": "https://api.github.com/repos/lumyslinski/app_projects/downloads",
  "issues_url": "https://api.github.com/repos/lumyslinski/app_projects/issues{/number}",
  "pulls_url": "https://api.github.com/repos/lumyslinski/app_projects/pulls{/number}",
  "milestones_url": "https://api.github.com/repos/lumyslinski/app_projects/milestones{/number}",
  "notifications_url": "https://api.github.com/repos/lumyslinski/app_projects/notifications{?since,all,participating}",
  "labels_url": "https://api.github.com/repos/lumyslinski/app_projects/labels{/name}",
  "releases_url": "https://api.github.com/repos/lumyslinski/app_projects/releases{/id}",
  "deployments_url": "https://api.github.com/repos/lumyslinski/app_projects/deployments",
  "created_at": "2018-06-23T01:18:09Z",
  "updated_at": "2018-06-24T01:29:27Z",
  "pushed_at": "2018-06-24T01:29:26Z",
  "git_url": "git://github.com/lumyslinski/app_projects.git",
  "ssh_url": "git@github.com:lumyslinski/app_projects.git",
  "clone_url": "https://github.com/lumyslinski/app_projects.git",
  "svn_url": "https://github.com/lumyslinski/app_projects",
  "homepage": null,
  "size": 764,
  "stargazers_count": 0,
  "watchers_count": 0,
  "language": "C#",
  "has_issues": true,
  "has_projects": true,
  "has_downloads": true,
  "has_wiki": true,
  "has_pages": false,
  "forks_count": 0,
  "mirror_url": null,
  "archived": false,
  "open_issues_count": 0,
  "license": null,
  "forks": 0,
  "open_issues": 0,
  "watchers": 0,
  "default_branch": "master",
  "permissions": {
    "admin": true,
    "push": true,
    "pull": true
  },
  "allow_squash_merge": true,
  "allow_merge_commit": true,
  "allow_rebase_merge": true,
  "network_count": 0,
  "subscribers_count": 0
}
```

- Get interesting data and map them:
    - fullName 	  -> full_name
    - description -> description
    - cloneUrl    -> clone_url
    - stars       -> stargazers_count
    - createdAt   -> created_at (date format of created_at is already in ISO8601 format)

2.Write unit tests for quering github repo of mine and check results.

3.Then use this test to implement rest api:
- create GitHub class based on tests to load secret token and get result from calling  
- add rest api framework like Flask
    
4.Make it scalable and ready for production:
![Communication flow](communication_flow.png?raw=true "Communication flow")
- Flask has got only a developer server to debug app. On production it should be connected with uwsgi and nginx to scale and run with full performance. That's why I chose unix socket to bound uWsgi and nginx because it is a fastest method. 
- Added support of Docker to isolate this project and run out of the box whole application. I used as a base alpine linux which is the smallest Linux distribution.

5.Run some performance tests (20 requests per second):
- tried funkload, but there was a problem with running commands
- tried PycURL, but there was a problem with installing it
- tried ab (Apache HTTP server benchmarking tool)

# Final thoughts
This system could be enhanced with redis to cache requests and responses. Cache time should be reasonable due to refreshing star number.

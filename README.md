![Severino - logo][logo]

# Severino (api)

[![Build status](https://ci.appveyor.com/api/projects/status/cd7cd7fjrwqb7xwq/branch/master?svg=true)](https://ci.appveyor.com/project/jroliveira/severino-api/branch/master)
[![Build Status](https://travis-ci.org/jroliveira/severino-api.svg?branch=master)](https://travis-ci.org/jroliveira/severino-api)
[![CodeFactor](https://www.codefactor.io/repository/github/jroliveira/severino-api/badge)](https://www.codefactor.io/repository/github/jroliveira/severino-api)
[![Maintainability](https://api.codeclimate.com/v1/badges/64e3a65e97181f9fc737/maintainability)](https://codeclimate.com/github/jroliveira/severino-api/maintainability)
[![License: AGPL v3](https://img.shields.io/badge/License-AGPL%20v3-blue.svg)](LICENSE.txt)

Severino is a Security Token Service.

## Installing / Getting started

This step is to explain what you need to run this application without having to configure the whole dev environment, but if you want to develop for this application, I will explain this step better in the following steps.

### Minimum pre requirements

The minimum pre requirements you need to run this application is the [Docker Compose](https://docs.docker.com/compose/install/).  
After you install the Docker Compose, you need to run the commands below.

### Running application with Docker Compose

``` bash
# Clone this repository
$ git clone https://github.com/jroliveira/severino-api.git

# Go into the repository
$ cd severino-api

# Run the application
$ docker-compose up
```

### Api Reference / Monitoring

 - [Swagger UI](http://localhost:8080/)
 - [Graphana](http://localhost:3000/)
 - [Prometheus](http://localhost:9090/)
 - [Kibana](http://localhost:5601/)
 - [Elasticsearch](http://localhost:9200/)
 - [Jaeger UI](http://localhost:16686/)

## Developing

### Built With

 - [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/)
 - [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)
 - [ASP.NET Core](https://docs.microsoft.com/en-ca/aspnet/core/)
 - [Neo4j](https://neo4j.com/developer/)
 - [Docker Compose](https://docs.docker.com/compose/)
 - [Swagger](https://swagger.io/)
 - [Node.js](https://nodejs.org/en/)

### Pre requisites

Download and install:

 - [.NET Core SDK](https://www.microsoft.com/net/download)
 - [Neo4j](https://neo4j.com/download/)
 - [Docker Compose](https://docs.docker.com/compose/install/)
 - [Node.js](https://nodejs.org/en/download/)

### Setting up Dev

``` bash
# Clone this repository
$ git clone https://github.com/jroliveira/severino-api.git

# Go into the repository
$ cd severino-api

# Download node packages and install Cake
$ npm install
```

### Building

``` bash
$ dotnet cake
```

### Testing

``` bash
$ dotnet cake --target=Test
```

### Running

If you want to run with Docker Compose, you need to execute the command below:

``` bash
# Run the application
$ docker-compose up
```

or

If you want to run with dotnet, you need to run the command below:

``` bash
# Run dependencies
$ dotnet cake --target=Dependencies

# Run application
$ dotnet run --project ./src/Severino.WebApi/
```

### Deploying / Publishing

Manual

Using custom buildpack [dotnetcore-buildpack](https://github.com/jincod/dotnetcore-buildpack)

``` bash
$ heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack
```

or 

Automatic

[![Deploy][heroku_button]][heroku_template]

### Deploying / Publishing

``` bash
$ dotnet cake --target=Release
```

## Licensing

The code is available under the [AGPL-3.0 license](LICENSE.txt).

[logo]: docs/images/logo.png "Severino - logo"
[vs2017]: https://www.visualstudio.com/vs/whatsnew/
[docker_compose]: https://docs.docker.com/compose/
[heroku_button]: https://www.herokucdn.com/deploy/button.svg
[heroku_template]: https://heroku.com/deploy?template=https://github.com/jroliveira/severino-api

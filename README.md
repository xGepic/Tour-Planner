# Architecture & Design

The solution is divided into several components. A distinction is primarily made between individual layers and projects:

* **Tour-Planner-Service**: contains the required classes for the API.
* **Tour-Planner-Business**: contains the business logic.
* **Tour-Planner-DB**: contains the classes to connect to the database.
* **Tour-Planner-Model**: contains all the models (Tours and Tourlogs) as well as DTOs.
* **Tour-Planner-Tests**: contains the tests for the entire project.
* **Tour-Planner-UI**: contains the GUI as well as the related classes.

Each of these layers is separated by definition of the single responsibility principle and also includes a Global Usings File in which the complete using statements of the respective project are defined in order to keep the code clean.

# Features
* **Database**:Implemented with Npgsql and Postgres.
* **Logger**: Implemented with Log4Net.
* **Report Generation**: Implemented with itext7.
* **MapQuest API**: RESTful WebService for location-enabled geospatial solutions.

# Further notes

The latest versions of.NET have always been used during development. This means: 
* .NET 6 
* .NET Standard 2.1 
* C# 10

Of the C# 10 features, only file-scoped namespaces and global using statements were used.

# Designs, Failures and Selected Solutions

### Backend
The backend is mainly found in the `Tour-Planner-Service` `Tour-Planner-DB`. It was implemented with ASP.NET Core and consists of a Web API and a Postgres database behind it. For the Database Classes the Singleton Design Pattern has been used. The heart of the API consists of 2 controllers that provide the basic CRUD operations for both of the models.

### Models
The models are located in `Tour-Planner-Model` and hold all the classes and records for the data. Furthermore the DTOs are also saved here. For the DTOs Data Annoations have been used to further handle incoming requests.

### Frontend
-Alex

### Unit Test Design
In total 20 Unit Tests have been designed which can be found in `Tour-Planner-Tests`.

### Lessons Learned
We started by developing the Models and the Controllers in the Backend and let the application evolve from there.
**Backend**: We learned using and working with ASP.NET Core as well as multiple NuGet Packages as mentioned above. This however took some time at the beginning because a lot of research and planning was required at the start of the project.
**Frontend**: -Alex

## Time Spent
Roughly 60 h per Person.

## Link to Git
https://github.com/xGepic/Tour-Planner
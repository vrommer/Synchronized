# Synchronized
## General
A questions/answers management system in the spirit of stack overflow.  The Web Application of the system is created using MVC Core. The system uses EF Core as the ORM system. The security is implemented using ASP Core Identity. In order to run this on a mac or a linux machine some minor modifications should be made. The project adheres to OOP best practices including design patterns, seperation to micro services and more. The project also adheres to MVC Core best practices and EF Core best practices. In addition to the built in IoC container, the project leverages the use of StructureMap for dependency injection.
## How to run
Currently may run only on a windows machine, since EF is configured to work with MS SQL Server on a localhost.
* Clone
* Open in Visual Studio
* Make sure all npm dependencies in Synchronized.WebApp are met
* Run the project - A demo database should be created
## Capabilities
The system currently supports the following capabilities:
* User managemen - Signing In/Logging in
* Composition of Questions
* Composition of Answers
* Commenting on Questions/Answers
* Tagging and creation of tags
* Deletion of posts
* Rewards/Penalties
* Role based authorization
* Flagging of Questions/Answers
* Editing of Questions/Answers
* Voting for/against Questions/Answers
* Search in Questions - the main page search
* Viewing of users
* Viewing for each user the posts he/she contributed or contributed to
* Search in users
* Viewing all tags
* Searching in tags
* User notifications mechanism is implemented - no real email sender is currently configured
## Design patterns
* Repository
* Abstract Factory
* Decorator
* State
* Publish/Subscribe (Observer)
* Strategy

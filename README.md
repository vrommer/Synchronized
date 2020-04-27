# Synchronized
## General
A questions/answers management system in the spirit of stack overflow.  The Web Application of the system is created using MVC Core. The system uses EF Core as the ORM system. The security is implemented using ASP Core Identity.

[Watch demo!](http://ec2-3-10-116-37.eu-west-2.compute.amazonaws.com/)
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

# 📖 Minimalist Book Manager API - C# ASP.NET Core MVC Web API

## Introduction
Get the starter repository from  https://github.com/techreturners/lm-lab-csharp-book-manager-api. It provides some starter code to creating a Minimalist Book Manager API with synchronous API endpoints.

This is the developed version, where the assignment is to add User Story 5, deleting a book given its ID, and add error handling.

### Pre-Requisites
- C# / .NET 6
- NuGet

### Technologies & Dependencies
- ASP.NET Core MVC 6 (Web API Project)
- NUnit testing framework
- Moq
- Postman or Swagger
- MySql if not using InMemory DB, and 
- Entity Framework Mirgrations

### How to Get Started
- Fork this repo to your Github and then clone the forked version of this repo.

### Main Entry Point
- The Main Entry Point for the application is: [Program.cs](./BookManagerApi/Program.cs)

### Running the Unit Tests
- You can run the unit tests in Visual Studio, or you can go to your terminal and inside the root of this directory, run:

`dotnet test`

### Original Version

The features are:
- Get All Books
- Get a Book by ID
- Add a Book
- Update a Book

### This Version

📘 Task 1: Implement the following User Story with tests.

`User Story: As a user, I want to use the Book Manager API to delete a book using its ID`


📘 Extension Task: Added error handling when:

- Find a book using an ID that doesn't yet exist. 
- Delete a book using an ID that doesn't exist.
- Update an book using an ID that doesn't exist.

Still to do:
- Adding a book with an ID for a book that already exists.
- Adding a book for a genre that is not the following:  Thriller, Romance, Fantasy, Fiction, Education,

  

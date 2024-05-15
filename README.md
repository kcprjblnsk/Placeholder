# URLStatus IN PROGRESS

This repository contains a .NET WebAPI project built using Clean Architecture principles in ASP.NET. The project structure includes separate layers for Domain, Application, Infrastructure, and WebAPI. 

## Project Structure
- **Domain:** Contains the core domain logic and business rules.
- **Application:** Implements application logic and orchestrates interactions between different layers.
- **Infrastructure:** Houses infrastructure concerns such as data access, external services integration, etc.
- **WebAPI 8.0:** Serves as the startup project for the API.

## Current NuGet Packages
- SeriLog
- FluentValidation
- MediatR
- EntityFramework
- SwashBuckle (Swagger)

## Current Features
The current version of the project allows users to perform the following actions:
- Create an account
- Delete an account
- Verify user and account
- Create account validators (ensuring proper email and password formats)
- Login and Logout
- Implement Cookies
- Database caching
- JWT Token authentication
- Cross-Origin Resource Sharing (CORS) for enhanced project security
- AntiForgeryToken implementation for project security

## Database Structure
The database, created in MSSQL, contains the following tables:
- Accounts
- AccountUsers
- Users

These tables are structured as follows:
- An account can have multiple users with different permissions.
- A user can belong to multiple accounts.

## Frontend
An easy-to-use website is provided to test the current backend and database abilities.

### Tech Stack
- JavaScript
- Vue.js and Vuetify
- Nuxt.js
- Node.js

## YouTube Video Demo
[Link to YouTube video demonstration](https://www.youtube.com/watch?v=VY9EDWgiCeA)  <!-- Replace "#" with the actual YouTube link -->

## Photos
![Screenshot 1](Photos/screenshot1.png)
![Screenshot 2](Photos/screenshot2.png)




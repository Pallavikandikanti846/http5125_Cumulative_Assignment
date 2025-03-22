# http5125_Cumulative_Assignment

This repository contains a C# MVC application that connects to a MySQL database and displays database information on a webpage using MVC controllers. The application includes several API controllers that provide access to teacher, student, and course data.

## Project Structure

- **Models/SchoolDbContext.cs**: Contains the `DbContext` class to interact with the MySQL database using the connection string.
- **Controllers/TeacherAPIController.cs**: An API controller for accessing teacher information from the database.
- **Controllers/StudentAPIController.cs**: An API controller for accessing student information from the database.
- **Controllers/CourseAPIController.cs**: An API controller for accessing course information from the database.
- **Program.cs**: Contains the application configuration and startup logic for the ASP.NET Core application.

  ## Requirements

- **Visual Studio 2022 Editor or Visual Studio Code Editor**
- **PHP Adminer XAMPP or MAMP
- **MySQL Database**: Ensure you have a MySQL database set up with appropriate tables for `Students`, `Teachers`, and `Courses`.


# Cumulative Project
This project involves building a Minimum Viable Product (MVP) for managing teachers in a school database. The implementation is done using ASP.NET Core Web API and MVC to perform Create, Read, Update, and Delete (CRUD) operations on the tables of the provided School Database and includes additional features to enhance usability, error handling, and data management.

## Project Overview
This project implements the following:
1.  CRUD Functionality for Teachers, Students, and Courses.
2.  Integration of ASP.NET Core Web API and MVC to render dynamic, server-side web pages.
3.  Additional features to enhance usability, error handling, and data management.

## Project Structure
### Models
- Teacher.cs
- SchoolDbContext.cs
### Controllers
- TeacherAPIController.cs
- TeacherPageController.cs
- StudentAPIController.vs
- StudentPageController.cs
- CourseAPIController.cs
- CoursePageController.cs
### Views
#### TeacherPage
- List.cshtml
- Show.cshtml
- New.cshtml
- DeleteConfirm.cshtml
#### StudentPage
- List.cshtml
- Show.cshtml
- New.cshtml
- DeleteConfirm.cshtml
#### CoursePage
- List.cshtml
- Show.cshtml
- New.cshtml
- DeleteConfirm.cshtml
  
## Features
### CRUD Operations
1. Teachers Table
  - Create, Read, Update, and Delete teacher information.
2. Students Table
  - Create, Read, Update, and Delete student information.
3. Courses Table
  - Create, Read, Update, and Delete course information.
### Web API
  - Access teacher, student, and course data programmatically.
### Dynamic Web Pages
  - View lists and detailed information for teachers, students, and courses.
### Database Integration
  - Seamless connection to the PhpMyAdmin database using SchoolDbContext.

## Improvement Initiatives
Earn additional marks by implementing these features:

1. Error Handling
  - Prevent adding a teacher with invalid or duplicate EmployeeNumber.
  - Ensure HireDate is not in the future.
  - Handle cases where deletion is attempted on a non-existent teacher.

2. Enhancements
  - Add search filters for teachers, students, and courses.
  - Styling improvements for web pages.
  - Use of AJAX for real-time form validation during add/delete operations.

3. Extended Functionality
  - CRUD operations for Students and Courses.
  - Integration of TeacherWorkPhone field into teacher records.

## How to Run the Project

1. Clone the Repository
  - git clone <repository-url>
  - cd <repository-folder>
  
2. Set Up the Database
  - Ensure the SchoolDbContext is connected to the correct PhpMyAdmin instance.
    
4. Start the Application
  - Access the app at https://localhost:localhost.

## Submission Instructions
1. Push the project to a public repository.
2. Verify that all required files are included.
3. Submit the GitHub repository link.
4. Include a PDF with testing evidence.

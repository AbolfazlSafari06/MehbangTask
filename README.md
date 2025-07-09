# Mehbang Web API

Mehbang Web API is a .NET 8-based RESTful API designed to manage student data. It provides endpoints for creating, retrieving, updating, and deleting student records. The project is built with a clean architecture and follows best practices for API development.

## Features

- **Retrieve All Students**: Fetch a list of students with optional filters for `fullname` and `nationalCode`.
- **Retrieve a Student by ID**: Get detailed information about a specific student.
- **Create a New Student**: Add a new student record to the system.
- **Update an Existing Student**: Modify details of an existing student.
- **Delete a Student**: Remove a student record by ID.

## Technologies Used

- **.NET 8**: The latest version of the .NET framework.
- **ASP.NET Core**: For building the Web API.
- **Dependency Injection**: To manage service lifetimes and dependencies.
- **EF Core 8**: For ORM.
- **SQLite**: For Database.
- **Windoes Form (.NET 8)**: For Desktop App.

## Endpoints

### Students Controller

1. **GET** `/api/Students`
   - Retrieves all students with optional filters.
   - Query Parameters:
     - `fullname` (optional): Filter by full name.
     - `nationalCode` (optional): Filter by national code.

2. **GET** `/api/Students/{id}`
   - Retrieves a specific student by ID.

3. **POST** `/api/Students`
   - Creates a new student.
   - Request Body: `CreateStudentDTO`.

4. **PUT** `/api/Students`
   - Updates an existing student.
   - Request Body: `UpdateStudentDTO`.

5. **DELETE** `/api/Students/{id}`
   - Deletes a student by ID.

## Installation

1. Clone the repository:
2. Navigate to the project directory:
3. Build the project:
4. Run Mehbang.GUI.Desktop And Mehbang.WebAPI.Web:

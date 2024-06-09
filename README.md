# ProjectPulse
## Overview
**ProjectPulse** is an innovative application designed to streamline team workflows while working on projects. It enables users to manage projects and tasks within groups, facilitates easy communication, and helps in sharing responsibilities effectively.

## Features
- **Project Management**: Create and manage multiple projects.
- **Task Management**: Assign and track tasks within each project.
- **Team Collaboration**: Communicate with team members and share responsibilities.
- **User-Friendly Interface**: Blazor frontend for a seamless user experience.
## Tech Stack
- **Frontend**: Blazor
- **Backend**: .NET 8 Web API
- **Database**: MSSQL
## Getting Started
### Prerequisites
Before you begin, ensure you have the following installed on your machine:

- .NET 8 SDK
- SQL Server
- Visual Studio or any other preferred IDE
### Installation
1. Clone the repository:
   
   ```
   git clone https://github.com/your-username/ProjectPulse.git
   cd ProjectPulse
   ```
2. Set up the database:
   
   - Create a new database in SQL Server.
   - Update the connection string in appsettings.json with your database details.
     
     ```
     "ConnectionStrings": {
      "DefaultConnection": "Server=your_server_name;Database=ProjectPulseDB;User Id=your_username;Password=your_password;"
     }
     ```
3. Run the database migrations:
   
   ```
   dotnet ef database update
   ```
4. Build and run the project:
   - Run the API:

   
     ```
     cd ProjectPulse.API
     dotnet build
     dotnet run
     ```
   - Run the Frontend:

   
     ```
     cd ProjectPulse.Web
     dotnet build
     dotnet run
     ```
## Usage
### Creating a Project
1. Click on the **"Add New Project"** button.
2. Enter the project name and description.
3. Click **"Submit"** to create the project.
### Managing Tasks
1. Select a project from the project list.
2. Click on **"Add New Task"** to create a task.
3. Assign the task to a team member.
4. Track the progress of tasks in the project dashboard.
### Team Collaboration
1. Use the messaging feature to communicate with team members.
2. Share files and updates in real-time.
## License
This project is licensed under the **MIT** License. See the **LICENSE** file for details.

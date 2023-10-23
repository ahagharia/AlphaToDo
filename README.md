# AlphaToDo
You can also navigate to https://samplealphatodo.azurewebsites.net/ to check app, app and sql db is hosted on Azure

Prerequisites:
- go to root folder
- should have net7.0
- Should have two packages - Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.SqlServer
- Add your database string to appsettings.json
- Create initial tables using  
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet ef database update

Getting Started:
- dotnet run
- got to TaskDemoTest folder and run 'dotnet test' to run test cases
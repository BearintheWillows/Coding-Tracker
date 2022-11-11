

# Coding Tracker App
  

Console App using:

- SQLite
- ADO.Net
- CSharp
- Spectre.Console 
- Serilog

# Requriements:

  

- [x] Same base requirements as [[Habit_Tracker]]. Log time instead of quanitity.
- (x) Use a NuGet package to display data to console
- (x) Use seperate class files
- (x) Ensure correct Date format is used
- (x) Use config file to store database connection strings
- (x) Create CodingSession Model
- (x) Duration automatically calculated
- (x) Do not use anonymous object to read from database.


# Features:

### SQLite Database

- The App connects to a SQLite database and creates one if one is not available.

### CRUD

- When the app is opened a list of 6 options will be displayed.
1 - Add a new session
2 - View all sessions
3 - View a session
4 - Update a session
5 - Delete a session
6 - Exit

- All functions work. Errors handled and input validation implemented to maintain integrity of database.

- Parameters used to protect database from injection attacks.

  
## Comments

  This project was more of a challenged than the [[Habit_Tracker]]. Using both [[Serilog]] and [[Spectre.Console]] was eye opening in the power that using additional library brings but also the increase of complexity. 

 I found once the methods for the database were created, the UI side was easier and quicker to bring together
  

## Resources Used:

- ReadMe file based on [thags](https://github.com/thags/ConsoleTimeLogger/blob/master/README.md)

- Project based on the Habit-Logger project by [thecsharpacademy](https://www.thecsharpacademy.com/habit-tracker/)

- Discord community for bug finding

- Serilog NuGet Package - [NuGet Gallery | Serilog 2.12.1-dev-01587](https://www.nuget.org/packages/Serilog/2.12.1-dev-01587)

- Spectre Console - [Spectre.Console - Welcome! (spectreconsole.net)](https://spectreconsole.net/)

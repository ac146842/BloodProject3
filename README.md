# Blood Donation Web Application

A full-stack web application made using ASP.NET MVC and Entity Framework Core for potential blood donation centers manage donors, appointments, nurses, medical forms, and blood inventory.

______________________________________________

Table of Contents
[About](#about) |
[Built With](#built-with) |
[Login](#login) |
[Connection string](#connection-string) 

______________________________________________

## About

Blood donation centres often rely on physical folders and spreadsheets to track donor information, appointments, and blood inventory. This can lead to many problems such as misplaced records, duplicated data, limited access, and last but not least human error. It also isn't very practical in terms of scalability and maintenance.

This web application solves those problems by storing all data in a database with:
- Full CRUD (Create, Read, Update, Delete)
- Validation to prevent invalid data entry
- Search and pagination for large datasets
- Role-based authorisation
- Automatically calculated fields

______________________________________________

## Built With

| Visual Studio | IDE |
| ASP.NET Core MVC| Web framework |
| Entity Framework Core | ORM for database access |
| SQL Server | Database (LocalDB for development) |
| ASP.NET Core Identity | Authentication and authorisation |
| HTML | Front-end language |
| Bootstrap | Front-end styling and responsive layout |
| C# | Back-end language |
| Razor Views | Front-end templating |
| GitHub | Version control |

______________________________________________

## Login

Email: BDstaff@org.nz
Password: BloodDonation@123

This is the only account made by the initialiser!
Accounts created through the register page won't receive the admin role as it was an original idea that was removed later on

______________________________________________

## Connection string

1. Open appsettings.json in the root of the project.
2. Locate the ConnectionStrings section:
   "ConnectionStrings": {
  "BloodProject3DbContextConnection": "..."
}
3. Replace the text with your own SQL Server connection string found through
   - Go to the View menu at the top and select SQL Server Object Explorer
   - Expand the tree view to find your database (e.g., under (localdb)\MSSQLLocalDB or your server name).
   - Right-click on your database and select Properties
   -  From the Properties window, copy the connection string field
   -  Paste it into the connection string in the appsettings.json
4. Save the file and run the application
   (No changes are required if you're using LocalDB)

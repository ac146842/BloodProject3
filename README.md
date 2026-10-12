## Documentation

### What It Does

This is an admin-only web app for blood donation clinics. It lets staff manage donors, nurses, appointments, medical forms, questions, answers, blood types, inventory, and donated blood all from one place.

### How It Works

The app is built using ASP.NET Core MVC, which splits everything into three parts:

- **Model** — sets up the database structure and the validation rules
- **View** — what the user sees (HTML, CSS, Bootstrap, Razor)
- **Controller** — takes user input, talks to the database, and returns the right page

Entity Framework Core is used to talk to the SQL Server database with C# instead of writing SQL. This also protects against SQL injection.

Login and roles are handled by ASP.NET Core Identity. Only Admin users can access any of the CRUD pages.

### Key Requirements

- Must be logged in as Admin to use the app
- Full CRUD (Create, Read, Update, Delete) on all records
- Validation on every input (required fields, max lengths, correct formats)
- Expiry Date is automatically set to Collection Date + 42 days
- Search, pagination, and sorting on Index pages
- Delete confirmation modals
- Dropdowns used for enums and foreign keys to prevent mistakes
- Foreign keys link related tables together
- Responsive design for smaller screens

### System Requirements

- Visual Studio 2022
- .NET 6.0 SDK
- SQL Server LocalDB (comes with Visual Studio)
- Any modern browser (Chrome, Edge, Brave, Firefox)

### User Manual

**Logging In**

Use these credentials:
- Email: `BDstaff@org.nz`
- Password: `BloodDonation@123`

**Navigating**

The navbar is at the top of every page. Links: Appointments, Questions, Donors, Nurses, Medical Forms, Answers, Blood Types, Inventories, Donated Bloods. Click "Blood Donation" to go back to the Home page.

**Creating a Record**

Click a link in the navbar → click "Create New" → fill in all fields → click Create.

**Editing a Record**

Click a link in the navbar → click "Edit" on the record you want → change the fields → click Save.

**Deleting a Record**

Click a link in the navbar → click "Delete" on the record → confirm in the modal.

**Searching**

Type in the search bar and press Enter. Search only works with strings (like names and locations), not IDs or dates.

**Pagination**

10 records show per page. Use the Previous and Next buttons or click a page number.

**Dropdowns and Date Pickers**

Dropdowns are used for enums and foreign keys so you can't type the wrong thing. Date pickers are used for dates so you can't type an invalid date. Future dates are greyed out where they aren't allowed.

### Key Files

| File / Folder | What It Does |
|:---|:---|
| Controllers/ | Handles user input and database operations |
| Models/ | Defines database structure and validation |
| Views/ | All the UI pages |
| Data/BloodProject3DbContext.cs | Connects the app to the database |
| Data/DbInitialiser.cs | Seeds the database with default data |
| appsettings.json | Stores the connection string |
| Program.cs | Sets up services, authentication, and routing |

### Common Errors

| Error | What It Means |
|:---|:---|
| "X is required." | You left a required field empty |
| "X must be between Y and Z characters." | Your input is too short or too long |
| "X contains invalid characters." | You typed characters that aren't allowed |
| "Phone must be a valid NZ phone number." | Wrong phone number format |
| "ID does not exist." | The foreign key you entered doesn't match anything |
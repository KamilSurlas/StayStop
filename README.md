# StayStop 
An application created as a college project. Application is designed to the process of booking hotel rooms. Users can search for available rooms, view detailed descriptions and photos, and make reservations in a few simple steps.

## Features 
<li>Authenication and authorization</li>
<li>Searching and pagination</li>
<li>Editing account data</li>
<li>Hotels and rooms details</li>
<li>Hotels and rooms management (base on roles and resources authorzation)</li>
<li>Booking rooms</li>
<li>Admin panel for reservations management</li>
<li>User reservations history</li>
<li>Hotels' opinions</li>

## Run the app
In order to run the app follow this steps:
Clone the repo
```bash
git clone https://github.com/KamilSurlas/StayStop.git
```

Set up clients' app
```bash
cd StayStop.WebClient
npm install
npm serve --open
```
The clients' app should now be working


Create a database named: *StayStopDb* on your local Sql server. Then run a migration
```bash
dotnet ef migrations add Init
dotnet ef database update
```




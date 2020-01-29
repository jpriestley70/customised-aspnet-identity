# Customised ASP.NET Identity

A project that has customised ASP.NET Identity.

## Introduction

Using ASP.NET Identity "out of the box" is a bit limiting. This project was created to look at customising ASP.NET Identity, to allow for the addition of extra fields and to change the default database schema from dbo to something else.

## Getting Started

You will need a copy of Visual Studio 2019 with .NET Core 3.1 installed.  
The project uses Microsoft SQL Server. You can also use Azure SQL.  
The project was created with VS 2019 version 16.4.3  
You will need to modify appsettings.json and change AppDbConnection to point to your database.

## Project Breakdown

### GenericBrand.Data

This project contains all the data access code including ASP.NET Identity.  
I have separated ASP.NET Identity from the web application as an example for separating your projects. For example you may also want to use the data project in a Web API project which is separate from your Web App project.

### GenericBrand.Web.UI

Just a basic Web App with the Identity pages modified to use GenericBrand.Data project.

### Added Bonus

GenericBrand.Web.UI also contains code for creating a localised web application.  
The application is not fully localised but the basics are there. Including a TagHelper resx as a placeholder for items that will be translated from resource files.

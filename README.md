# VirtualDoctor
-------------------
Implementation of ambulance organizational system like a three-layered structure using WPF and MySQL Client in .NET Core.
Technologies:
- MySql Database using MySqlClient provider     for .NET applications
- WPF application (.NET) using C# and XAML .NET Framework 4.7.2
- .NET Core class libraries
- Automapper

System purpose:
VirtualDoctor introduces new digital ways for manipulating healthcare information, like personal health records, doctors, patients, ambulances with a list of doctors who work there, possible doctor's specializations and titles. Also, each place has a list of qualities, that can be used to relate disease with specific living conditions.

Users: Depending on the type of users, different features are enabled. Users are:
Doctors, Admins for places, ambulances, titles, specializations and doctors, Admins for health records, Patients

Details of implementation:
On the frontend, paging is enabled on tables. Also, there is an option for changing language (Serbian, English).
Also you can change themes. See more in folder [Documentation](./VirtualDoctor/AmbulanceSystem/Documentation/UserInstructions.md).  
 On the backend, systems like EntityFramework are not used. Instead, there is raw SQL that MySql provider for .NET executes on DB server. That way we get better performance and better control over SQL commands and generally, SQL language expressions. Not only that but Command Pattern is used with System.Reflection to lower the need for duplicating SQL expressions in code, and that greatly improved code reusability and system agility. It also uses services as a medium layer of three-layer structure to make code even more reusable. It needs to be refactored so that each layer has its own models for inter and intralayer communication, but in this phase, I chose to decrease instead code duplication. 
As DI is not supported in WPF applications, I used traditional Factory Methods for creating objects (for example Services). SOLID principles are followed. In the backend part, in need of code maintainability and reusability, I used different classes as ServiceExecutor for example and DbCommand, which are core and main classes of the whole implementation, each with Single Responsibility. See code for additional details or contact me on my email: marija.vanja.novakovic@gmail.com. 

For you to run the program:
First, the script for generating the database is in project AmbulanceDatabase, subfolder Scripts. Run it in some MySQL DBMS tool like MySQLWorkbench. ConnectionString is saved in Properties.settings file in Config subfolder of the same project. Change it. Some roles should be inserted in the database before using it. You can use the Insert script for that purpose. That are OrganizationalAdmin, PatientAdmin, DoctorAdmin, AccountsAdmin.

## Design
----------
One of the purposes of this project was to learn how to make a proper design. Although the design is not modern or nice, it has some qualities that are an important part of HCI, like user's customization (change theme or language), is intuitive, has paging etc. The themes that are supported are inspired by Harry Potter houses. 

![1](/1.PNG)
![2](/2.PNG)
![3](/3.PNG)
![4](/4.PNG)
![5](/5.PNG)
![6](/6.PNG)

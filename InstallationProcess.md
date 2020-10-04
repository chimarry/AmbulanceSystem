# Srpski
---------
### Instalacija
> Kako je aplikacija pisana korištenjem .NET Framework 4.7.2, potrebno je da ova ili novija verzija bude instalirana na Windows 10 operativnom sistemu. Aplikacija nije podržana na drugim platformama. Da biste provjerili da li imate na svom računaru instaliran .NET Framework 4.7.2, pratite instrukcije sa sljedećeg linka: [Da li imam .NET Framework 4.x na računaru?](https://www.windowscentral.com/how-quickly-check-net-framework-version-windows-10). Ukoliko nemate, možete ga instalirati prateći [Instaliraj .NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-web-installer)

Od razvojnih okruženja, potrebno je da imate [MySqlWorkbench](https://dev.mysql.com/downloads/workbench/) instaliran. U folderu /AmbulanceDatabase/Scripts se nalaze skripte koje treba da se izvrše. Jedna služi za kreiranje baze *CreateScript*, druga za popunjavanje *InsertAccountScript*.  
Prilikom instalacije MySQLWorkbencha, instaliran je i MySQL server, tako da će aplikacija moći da komunicira sa njim. Potrebno je da se podesi konekcioni string.   U folderu /AmbulanceSystem/Shared/Config postoji fajl Properties.settings fajl, i u tom fajlu se nalazi konekcioni string pod nazivom Ambulance.  
Promijeniti vrijednosti napisane velikim slovima tako da odgovaraju Vašoj konfiguraciji:  
*server = IP_ADRESA_SERVERA; port = MYSQL_PORT; database = ambulance; uid = KORISNIČKO_IME; password = LOZINKA;*   
Aplikaciju je moguće pokrenuti kroz razvojno okruženje [Visual Studio](https://visualstudio.microsoft.com/downloads/), ili direktnim kliktanjem na aplikaciju koja je objavljena u PublishedApplication folderu (ispratite korake za instalaciju aplikacije na sistem).
Projekat kog je moguće pokrenuti je AmbulanceSystem.

# English
-------------------------------------
## Installation
> Application runs on Windows 10 OS, precisely on .NET Framework 4.7.2 runtime environment. If you are not sure if you have .NET Framework 4.7.2 or higher, you can check by following steps on this link:  [Do I have .NET Framework 4.x on my computer? ](https://www.windowscentral.com/how-quickly-check-net-framework-version-windows-10). Or you can just download it from a site [Install .NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework/thank-you/net472-web-installer), and if it is already installed on your machine nothing will happen.

You can install [MySqlWorkbench](https://dev.mysql.com/downloads/workbench/). Script for generating database is in project AmbulanceDatabase, subfolder Scripts. Run it in some MySQL DBMS tool like MySQLWorkbench.. ConnectionString is saved in Properties.settings file in AmbulanceSystem/Shared/Config folder. Change the values written in uppercase so that your SQL configuration is synchronized with this connection string:
server = IP_ADDRESS_OF_SERVER; port = MYSQL_PORT; database = ambulance; uid = USERNAME; password = PASSWORD;  

The application can be run through [Visual Studio](https://visualstudio.microsoft.com/downloads/), where AmbulanceSystem is a startup project, or you can run it by running application in PublishedApplication folder (that way you will install the application on your Window 10 system).
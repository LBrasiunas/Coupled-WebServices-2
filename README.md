# Coupled-WebServices-2
2nd task for the Web services course. Running two web services in one single docker container and exchanging information between them.

## Web services used
* My own: https://github.com/LBrasiunas/CarService-WebService-1
* Other: https://github.com/VytenisKaj/LibraryWebService

## How to run with `docker-compose`
1. You need to have `Docker` installed on your system.
2. Pull the files to your desired location.
3. Open any terminal application.
4. Navigate to the pulled file location where the `docker-compose.yml` file is located.
5. Run `docker-compose up`.
6. Wait till all three services and their databases are up and running.
7.	1. Go to `http://localhost:5000/swagger/index.html` to access the CoupledWebservice (main).
	2. Go to `http://localhost:5001/swagger/index.html` to access the CarServiceWebservice.
	3. Go to `http://localhost:5002/swagger/index.html` to access the LibraryWebservice.

## Database migrations
* Command for adding a migration: 
`dotnet ef --startup-project CoupledWebService\CoupledServicesApi --project CoupledWebService\Infrastructure migrations add <migration_name>`
* Command for updating database locally:
`dotnet ef --startup-project CoupledWebService\CoupledServicesApi --project CoupledWebService\Infrastructure database update`
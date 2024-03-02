# Coupled-WebServices-2
2nd task for the Web services course. Running two web services in one single docker container and exchanging information between them.

## Web services used
* My own: https://github.com/LBrasiunas/CarService-WebService-1
* Other: https://github.com/VytenisKaj/LibraryWebService

## Database migrations
* Command for adding a migration: 
`dotnet ef --startup-project CoupledWebService\CoupledServicesApi --project CoupledWebService\Infrastructure migrations add <migration_name>`
* Command for updating database locally:
`dotnet ef --startup-project CoupledWebService\CoupledServicesApi --project CoupledWebService\Infrastructure database update`
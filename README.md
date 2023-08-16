[Documentation](https://github.com/gpreviatti/ProEventos/wiki)

[Project course fullstack com .Net + Angular](https://www.udemy.com/course/angular-dotnetcore-efcore/)


# How to Run

## Docker

execute the following command

`docker-compose up`

inside de backend project execute the following command

`dotnet ef database update -p src/ProEventos.Persistence/ -s src/ProEventos.API/ --connection "Host=localhost;Port=5432;Database=ProEventos;User ID=postgres;Password=admin"`

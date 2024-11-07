# MYSQL
docker run --name mysql-container -e MYSQL_ROOT_PASSWORD=rootpassword -e MYSQL_DATABASE=meudatabase -e MYSQL_USER=myuser -e MYSQL_PASSWORD=mypassword -p 3306:3306 -d mysql:latest

# POSTGRES
docker pull postgres
docker run -p 5432:5432 -e POSTGRES_PASSWORD=1234 postgres

# SQL SERVER
docker run --name sqlserver -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=1q2w3e4r@#$" -p 1433:1433 -d mcr.microsoft.com/mssql/server

# LIMPAR MIGRAÇÕES
dotnet ef migrations remove --project "./Sistema.Infraestrutura.Persistencia/Sistema.Infraestrutura.Persistencia.csproj" --startup-project "./Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj"

# ADICIONAR MIGRAÇÕES
dotnet ef migrations add InitialCreate --project "./Sistema.Infraestrutura.Persistencia/Sistema.Infraestrutura.Persistencia.csproj" --startup-project "./Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj"

# ATUALIZAR O BANCO DE DADOS
dotnet ef database update --project "./Sistema.Infraestrutura.Persistencia/Sistema.Infraestrutura.Persistencia.csproj" --startup-project "./Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj"


# BUILD
docker build -t sistema-presenca
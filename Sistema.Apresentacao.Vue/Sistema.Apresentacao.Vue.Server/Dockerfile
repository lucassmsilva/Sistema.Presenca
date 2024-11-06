#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/nuget.config", "Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/"]
COPY ["Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj", "Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/"]
COPY ["Sistema.Core.Aplicacao/Sistema.Core.Aplicacao.csproj", "Sistema.Core.Aplicacao/"]
COPY ["Sistema.Core.Dominio/Sistema.Core.Dominio.csproj", "Sistema.Core.Dominio/"]
COPY ["Sistema.Infraestrutura.Persistencia/Sistema.Infraestrutura.Persistencia.csproj", "Sistema.Infraestrutura.Persistencia/"]
COPY ["Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/sistema.apresentacao.vue.client.esproj", "Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/"]
RUN dotnet restore "./Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/./Sistema.Apresentacao.Vue.Server.csproj"
COPY . .
WORKDIR "/src/Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server"
RUN dotnet build "./Sistema.Apresentacao.Vue.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sistema.Apresentacao.Vue.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Sistema.Apresentacao.Vue.Server.dll"]
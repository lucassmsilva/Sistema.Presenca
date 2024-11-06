# Etapa base para o ASP.NET
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Etapa de build e configuração do Node.js
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Instalar Node.js
RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs

# Copiar os arquivos do projeto
COPY ["Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/nuget.config", "Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/"]
COPY ["Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj", "Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/"]
COPY ["Sistema.Core.Aplicacao/Sistema.Core.Aplicacao.csproj", "Sistema.Core.Aplicacao/"]
COPY ["Sistema.Core.Dominio/Sistema.Core.Dominio.csproj", "Sistema.Core.Dominio/"]
COPY ["Sistema.Infraestrutura.Persistencia/Sistema.Infraestrutura.Persistencia.csproj", "Sistema.Infraestrutura.Persistencia/"]
COPY ["Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/sistema.apresentacao.vue.client.esproj", "Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/"]
COPY . .

# Build do projeto Vue.js
WORKDIR "/src/Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client"
RUN npm ci
RUN npm run build

# Build do projeto .NET
WORKDIR "/src/Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server"
RUN dotnet restore "./Sistema.Apresentacao.Vue.Server.csproj"
RUN dotnet build "Sistema.Apresentacao.Vue.Server.csproj" -c Release -o /app/build

# Publicação
FROM build AS publish
RUN dotnet publish "Sistema.Apresentacao.Vue.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

RUN mkdir -p /https/
COPY ["./https/aspnetapp.pfx", "/https/"]

ENTRYPOINT ["dotnet", "Sistema.Apresentacao.Vue.Server.dll"]
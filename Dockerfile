# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN apt-get update && \
    apt-get install -y curl && \
    curl -fsSL https://deb.nodesource.com/setup_18.x | bash - && \
    apt-get install -y nodejs

COPY ["Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/nuget.config", "Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/"]
COPY ["Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj", "Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/"]
COPY ["Sistema.Core.Aplicacao/Sistema.Core.Aplicacao.csproj", "Sistema.Core.Aplicacao/"]
COPY ["Sistema.Core.Dominio/Sistema.Core.Dominio.csproj", "Sistema.Core.Dominio/"]
COPY ["Sistema.Infraestrutura.Persistencia/Sistema.Infraestrutura.Persistencia.csproj", "Sistema.Infraestrutura.Persistencia/"]
COPY ["Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/sistema.apresentacao.vue.client.esproj", "Sistema.Apresentacao.Vue/sistema.apresentacao.vue.client/"]
RUN dotnet restore "./Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server/Sistema.Apresentacao.Vue.Server.csproj"
COPY . .
WORKDIR "/src/Sistema.Apresentacao.Vue/Sistema.Apresentacao.Vue.Server"
RUN dotnet build "./Sistema.Apresentacao.Vue.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Sistema.Apresentacao.Vue.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Sistema.Apresentacao.Vue.Server.dll"]
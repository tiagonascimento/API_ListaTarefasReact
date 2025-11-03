# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#USER $APP_UID
WORKDIR /app
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/backend/ListaTarefas.API/ListaTarefas.API.csproj", "src/backend/ListaTarefas.API/"]
COPY ["src/shared/ListTarefas.Communication/ListTarefas.Communication.csproj", "src/shared/ListTarefas.Communication/"]
COPY ["src/backend/ListaTarefas.Aplication/ListaTarefas.Aplication.csproj", "src/backend/ListaTarefas.Aplication/"]
COPY ["src/shared/ListaTarefas.Exception/ListaTarefas.Exception.csproj", "src/shared/ListaTarefas.Exception/"]
COPY ["src/shared/ListaTarefas.Utils/ListaTarefas.Utils.csproj", "src/shared/ListaTarefas.Utils/"]
COPY ["src/backend/ListaTarefas.Domain/ListaTarefas.Domain.csproj", "src/backend/ListaTarefas.Domain/"]
COPY ["src/backend/ListaTarefas.Infra/ListaTarefas.Infra.csproj", "src/backend/ListaTarefas.Infra/"]
RUN dotnet restore "./src/backend/ListaTarefas.API/ListaTarefas.API.csproj"
COPY . .
WORKDIR "/src/src/backend/ListaTarefas.API"
RUN dotnet build "./ListaTarefas.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ListaTarefas.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ListaTarefas.API.dll"]
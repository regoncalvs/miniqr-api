# Define a imagem base
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

# Define a imagem de construção
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copia os arquivos do projeto e restaura as dependências
COPY ["MiniQr.API/MiniQr.API.csproj", "MiniQr.API/"]
RUN dotnet restore "MiniQr.API/MiniQr.API.csproj"

# Copia o código fonte e compila a aplicação
COPY . .
WORKDIR "/src/MiniQr.API"
RUN dotnet build "MiniQr.API.csproj" -c Release -o /app/build

# Publica a aplicação
FROM build AS publish
RUN dotnet publish "MiniQr.API.csproj" -c Release -o /app/publish

# Configura a imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MiniQr.API.dll"]

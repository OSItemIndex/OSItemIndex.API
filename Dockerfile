#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR .
COPY ["src/OSItemIndex.API/OSItemIndex.API.csproj", "src/OSItemIndex.API/"]
COPY ["src/OSItemIndex.Data/OsItemIndex.Data.csproj", "src/OSItemIndex.Data/"]
RUN dotnet restore "src/OSItemIndex.API/OSItemIndex.API.csproj"
COPY . .
WORKDIR "/src/OSItemIndex.API"
RUN dotnet build "OSItemIndex.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OSItemIndex.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OSItemIndex.API.dll"]

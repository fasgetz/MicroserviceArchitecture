#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ProfileService/ProfileService.csproj", "ProfileService/"]
COPY ["DataBaseLayer/DataBaseLayer.csproj", "DataBaseLayer/"]
RUN dotnet restore "ProfileService/ProfileService.csproj"
COPY . .
WORKDIR "/src/ProfileService"
RUN dotnet build "ProfileService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProfileService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProfileService.dll"]
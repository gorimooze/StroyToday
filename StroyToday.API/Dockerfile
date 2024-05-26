#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StroyToday.API/StroyToday.API.csproj", "StroyToday.API/"]
COPY ["StroyToday.Application/StroyToday.Application.csproj", "StroyToday.Application/"]
COPY ["StroyToday.Core/StroyToday.Core.csproj", "StroyToday.Core/"]
COPY ["StroyToday.Common/StroyToday.Common.csproj", "StroyToday.Common/"]
COPY ["StroyToday.DataAccess/StroyToday.DataAccess.csproj", "StroyToday.DataAccess/"]
RUN dotnet restore "./StroyToday.API/StroyToday.API.csproj"
COPY . .
WORKDIR "/src/StroyToday"
RUN dotnet build "./StroyToday.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StroyToday.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StroyToday.API.dll"]
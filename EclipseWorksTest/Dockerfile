# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5400
#EXPOSE 5401


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["EclipseWorksTest/EclipseWorksTest.csproj", "EclipseWorksTest/"]
COPY ["EclipseWorksTest.AppService/EclipseWorksTest.AppService.csproj", "EclipseWorksTest.AppService/"]
COPY ["EclipseWorksTest.CrossCutting/EclipseWorksTest.CrossCutting.csproj", "EclipseWorksTest.CrossCutting/"]
COPY ["EclipseWorksTest.Domain/EclipseWorksTest.Domain.csproj", "EclipseWorksTest.Domain/"]
COPY ["EclipseWorksTest.Infra.Data/EclipseWorksTest.Infra.Data.csproj", "EclipseWorksTest.Infra.Data/"]
RUN dotnet restore "./EclipseWorksTest/EclipseWorksTest.csproj"
COPY . .
WORKDIR "/src/EclipseWorksTest"
RUN dotnet build "./EclipseWorksTest.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./EclipseWorksTest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EclipseWorksTest.dll"]
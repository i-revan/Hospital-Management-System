#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Presentation/HospitalManagementSystem.API/HospitalManagementSystem.API.csproj", "src/Presentation/HospitalManagementSystem.API/"]
COPY ["src/Core/HospitalManagementSystem.Application/HospitalManagementSystem.Application.csproj", "src/Core/HospitalManagementSystem.Application/"]
COPY ["src/Core/HospitalManagementSystem.Domain/HospitalManagementSystem.Domain.csproj", "src/Core/HospitalManagementSystem.Domain/"]
COPY ["src/Infrastructure/HospitalManagementSystem.Infrastructure/HospitalManagementSystem.Infrastructure.csproj", "src/Infrastructure/HospitalManagementSystem.Infrastructure/"]
COPY ["src/Infrastructure/HospitalManagementSystem.Persistence/HospitalManagementSystem.Persistence.csproj", "src/Infrastructure/HospitalManagementSystem.Persistence/"]
RUN dotnet restore "./src/Presentation/HospitalManagementSystem.API/./HospitalManagementSystem.API.csproj"
COPY . .
WORKDIR "/src/src/Presentation/HospitalManagementSystem.API"
RUN dotnet build "./HospitalManagementSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HospitalManagementSystem.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HospitalManagementSystem.API.dll"]
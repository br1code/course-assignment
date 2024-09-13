# Use the official .NET 8 SDK image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the solution and restore dependencies
COPY CourseManagement.sln ./
COPY CourseManagement.API/*.csproj ./CourseManagement.API/
COPY CourseManagement.Application/*.csproj ./CourseManagement.Application/
COPY CourseManagement.Domain/*.csproj ./CourseManagement.Domain/
COPY CourseManagement.Infrastructure/*.csproj ./CourseManagement.Infrastructure/
COPY CourseManagement.Tests/*.csproj ./CourseManagement.Tests/

RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish CourseManagement.API/CourseManagement.API.csproj -c Release -o out

# Use the official .NET 8 runtime image for running the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port 80
EXPOSE 80

# Set environment variables (if needed)
ENV ASPNETCORE_URLS=http://+:80

# Start the application
ENTRYPOINT ["dotnet", "CourseManagement.API.dll"]

# building docker image
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 80   
COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
RUN dotnet restore src/IdentityService/IdentityService.csproj


# copy all .csproj files and restore as distinct layers.   Use of the same COPY command
# for every dockerfile in the project to take advantage of docker caching
# COPY Typist.sln Typist.sln
# COPY src/IdentityService/IdentityService.csproj src/IdentityService/IdentityService.csproj
# COPY src/TypistApi/TypistApi.csproj src/TypistApi/TypistApi.csproj
# COPY src/SearchService/SearchService.csproj src/SearchService/SearchService.csproj
# COPY src/GatewayService/GatewayService.csproj src/GatewayService/GatewayService.csproj
# COPY src/Contracts/Contracts.csproj src/Contracts/Contracts.csproj

# restore package deps 
# RUN dotnet restore Typist.sln

# Copy the app folders over
COPY src/IdentityService src/IdentityService
WORKDIR /app/src/IdentityService
RUN dotnet publish -c Release -o /app/src/out

# WORKDIR /app/src/IdentityService

# running the container
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .

ENTRYPOINT [ "dotnet", "IdentityService.dll" ]
FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app
EXPOSE 5227

COPY chatapp-webserver/WebSocketServer/WebSocketServer.csproj src/WebSocketServer/WebSocketServer.csproj 

RUN dotnet restore src/WebSocketServer/WebSocketServer.csproj

COPY chatapp-webserver/WebSocketServer src/WebSocketServer
WORKDIR /app/src/WebSocketServer
RUN dotnet publish "WebSocketServer.csproj" -c Release -o /app/src/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/src/out .
ENTRYPOINT [ "dotnet" , "WebSocketServer.dll" ]

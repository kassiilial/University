FROM mcr.microsoft.com/dotnet/sdk:5.0
ENV ASPNETCORE_ENVIRONMENT="development"
ENV DB_CONNECTION_STRING="Host=host.docker.internal;Port=5432;Database=usersdb;Username=postgres;Password=12345"
WORKDIR /myapp
COPY . .
WORKDIR /myapp/WebApplication
RUN dotnet restore
RUN dotnet build
ENTRYPOINT ["dotnet", "/myapp/WebApplication/bin/Debug/net5.0/WebApplication.dll"]


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src


COPY Dhrms.sln ./
COPY Dhrms.DataAccess/*.csproj ./Dhrms.DataAccess/
COPY Dhrms.WebService/*.csproj ./Dhrms.WebService/

RUN dotnet restore
COPY . .

WORKDIR /src/Dhrms.DataAccess
RUN dotnet build -c Release -o /app

WORKDIR /src/Dhrms.WebService
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
# The below is commented and the line after is used when in heroku
# ENTRYPOINT ["dotnet", "Dhrms.WebService.dll"]
# CMD ASPNETCORE_URLS=http://*:$PORT dotnet Dhrms.WebService.dll
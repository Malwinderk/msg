FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Assignment.LoginService/Assignment.LoginService.csproj", "Assignment.LoginService/"]
RUN dotnet restore "Assignment.LoginService/Assignment.LoginService.csproj"
COPY . .
WORKDIR "/src/Assignment.LoginService"
RUN dotnet build "Assignment.LoginService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Assignment.LoginService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Assignment.LoginService.dll"]
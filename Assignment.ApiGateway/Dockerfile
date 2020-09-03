FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Assignment.ApiGateway/Assignment.ApiGateway.csproj", "Assignment.ApiGateway/"]
RUN dotnet restore "Assignment.ApiGateway/Assignment.ApiGateway.csproj"
COPY . .
WORKDIR "/src/Assignment.ApiGateway"
RUN dotnet build "Assignment.ApiGateway.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Assignment.ApiGateway.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Assignment.ApiGateway.dll"]
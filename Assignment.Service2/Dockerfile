FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Assignment.Service2/Assignment.Service2.csproj", "Assignment.Service2/"]
RUN dotnet restore "Assignment.Service2/Assignment.Service2.csproj"
COPY . .
WORKDIR "/src/Assignment.Service2"
RUN dotnet build "Assignment.Service2.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Assignment.Service2.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Assignment.Service2.dll"]
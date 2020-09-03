FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Assignment.Service1/Assignment.Service1.csproj", "Assignment.Service1/"]
RUN dotnet restore "Assignment.Service1/Assignment.Service1.csproj"
COPY . .
WORKDIR "/src/Assignment.Service1"
RUN dotnet build "Assignment.Service1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Assignment.Service1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Assignment.Service1.dll"]
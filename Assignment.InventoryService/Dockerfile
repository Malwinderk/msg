FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Assignment.InventoryService/Assignment.InventoryService.csproj", "Assignment.InventoryService/"]
RUN dotnet restore "Assignment.InventoryService/Assignment.InventoryService.csproj"
COPY . .
WORKDIR "/src/Assignment.InventoryService"
RUN dotnet build "Assignment.InventoryService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Assignment.InventoryService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Assignment.InventoryService.dll"]
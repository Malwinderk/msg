FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["DeliverManagementService/DeliverManagementService.csproj", "DeliverManagementService/"]
RUN dotnet restore "DeliverManagementService/DeliverManagementService.csproj"
COPY . .
WORKDIR "/src/DeliverManagementService"
RUN dotnet build "DeliverManagementService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "DeliverManagementService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "DeliverManagementService.dll"]
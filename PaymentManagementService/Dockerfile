FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["PaymentManagementService/PaymentManagementService.csproj", "PaymentManagementService/"]
RUN dotnet restore "PaymentManagementService/PaymentManagementService.csproj"
COPY . .
WORKDIR "/src/PaymentManagementService"
RUN dotnet build "PaymentManagementService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PaymentManagementService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PaymentManagementService.dll"]
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TalentHub.NotificationService.Host/TalentHub.NotificationService.Host.csproj", "TalentHub.NotificationService.Host/"]
COPY ["TalentHub.NotificationService.Application/TalentHub.NotificationService.Application.csproj", "TalentHub.NotificationService.Application/"]
RUN dotnet restore "TalentHub.NotificationService.Host/TalentHub.NotificationService.Host.csproj"
COPY . .
WORKDIR "/src/TalentHub.NotificationService.Host"
RUN dotnet build "TalentHub.NotificationService.Host.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TalentHub.NotificationService.Host.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalentHub.NotificationService.Host.dll"]

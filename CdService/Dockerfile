FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["CdService/CdService.csproj", "CdService/"]
RUN dotnet restore "CdService/CdService.csproj"
COPY . .
WORKDIR "/src/CdService"
RUN dotnet build "CdService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CdService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "CdService.dll"]
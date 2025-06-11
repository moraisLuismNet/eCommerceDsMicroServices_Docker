FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ShoppingService/ShoppingService.csproj", "ShoppingService/"]
RUN dotnet restore "ShoppingService/ShoppingService.csproj"
COPY . .
WORKDIR "/src/ShoppingService"
RUN dotnet build "ShoppingService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingService.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "ShoppingService.dll"]
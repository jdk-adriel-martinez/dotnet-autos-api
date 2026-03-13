FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Autos.Api/Autos.Api.csproj", "Autos.Api/"]
RUN dotnet restore "Autos.Api/Autos.Api.csproj"

COPY . .
WORKDIR "/src/Autos.Api"
RUN dotnet publish "Autos.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Autos.Api.dll"]

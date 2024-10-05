FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["RequestEcho.Api/RequestEcho.Api.csproj", "RequestEcho.Api/"]
RUN dotnet restore "RequestEcho.Api/RequestEcho.Api.csproj"
COPY . .
WORKDIR "/src/RequestEcho.Api"
RUN dotnet build "RequestEcho.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RequestEcho.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RequestEcho.Api.dll"]

#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["prometheus-example/prometheus-example.csproj", "prometheus-example/"]
RUN dotnet restore "prometheus-example/prometheus-example.csproj"
COPY . .
WORKDIR "/src/prometheus-example"
RUN dotnet build "prometheus-example.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "prometheus-example.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 1234
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "prometheus-example.dll"]
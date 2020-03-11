#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1234

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["prometheus-example-host/prometheus-example-host.csproj", "prometheus-example-host/"]
RUN dotnet restore "prometheus-example-host/prometheus-example-host.csproj"
COPY . .
WORKDIR "/src/prometheus-example-host"
RUN dotnet build "prometheus-example-host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "prometheus-example-host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER root
RUN hostname localhost
RUN hostname
ENTRYPOINT ["dotnet", "prometheus-example-host.dll"]
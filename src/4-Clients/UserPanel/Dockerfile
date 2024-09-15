FROM nginx:alpine AS base
USER app
WORKDIR /usr/share/nginx/html
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/4-Clients/UserPanel/UserPanel.csproj", "src/4-Clients/UserPanel/"]
COPY ["src/1-BuildingBlocks/Web/Web.csproj", "src/1-BuildingBlocks/Web/"]
COPY ["src/1-BuildingBlocks/Contracts/Contracts.csproj", "src/1-BuildingBlocks/Contracts/"]

RUN dotnet restore "src/4-Clients/UserPanel/UserPanel.csproj"
COPY . .
RUN dotnet build "src/4-Clients/UserPanel/UserPanel.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/4-Clients/UserPanel/UserPanel.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY src/4-Clients/UserPanel/nginx.conf /etc/nginx/nginx.conf
CMD ["nginx", "-g", "daemon off;"]

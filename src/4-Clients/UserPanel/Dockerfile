FROM nginx:alpine AS base
COPY src/4-Clients/UserPanel/nginx.conf /etc/nginx/nginx.conf
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
COPY --from=publish /app/publish/wwwroot /usr/share/nginx/html
CMD ["nginx", "-g", "daemon off;"]

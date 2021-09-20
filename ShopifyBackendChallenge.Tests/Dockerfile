#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ShopifyBackendChallenge.Tests/ShopifyBackendChallenge.Tests.csproj", "ShopifyBackendChallenge.Tests/"]
COPY ["ShopifyBackendChallenge.Web/ShopifyBackendChallenge.Web.csproj", "ShopifyBackendChallenge.Web/"]
RUN dotnet restore "ShopifyBackendChallenge.Tests/ShopifyBackendChallenge.Tests.csproj"
COPY . .
WORKDIR "/src/ShopifyBackendChallenge.Tests"
RUN dotnet build "ShopifyBackendChallenge.Tests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShopifyBackendChallenge.Tests.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopifyBackendChallenge.Tests.dll"]
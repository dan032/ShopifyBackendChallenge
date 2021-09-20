#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ShopifyBackendChallenge.Web/ShopifyBackendChallenge.Web.csproj", "ShopifyBackendChallenge.Web/"]
RUN dotnet restore "ShopifyBackendChallenge.Web/ShopifyBackendChallenge.Web.csproj"

COPY ["ShopifyBackendChallenge.Tests/ShopifyBackendChallenge.Tests.csproj", "ShopifyBackendChallenge.Tests/"]
RUN dotnet restore "ShopifyBackendChallenge.Tests/ShopifyBackendChallenge.Tests.csproj"

COPY . .
WORKDIR "/src/ShopifyBackendChallenge.Tests"
RUN dotnet test "ShopifyBackendChallenge.Tests.csproj"

WORKDIR "/src/ShopifyBackendChallenge.Web"
RUN dotnet build "ShopifyBackendChallenge.Web.csproj" -c Release -o /app/build


RUN dotnet dev-certs https --trust
FROM build AS publish
RUN dotnet publish "ShopifyBackendChallenge.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShopifyBackendChallenge.Web.dll"]
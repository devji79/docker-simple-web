FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /usr/app

# Copy everything
COPY . ./
# Restore
RUN dotnet restore
# Build and publish a release
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /usr/app

COPY --from=build-env /usr/app/out .

ENTRYPOINT [ "dotnet", "SimpleWeb.API.dll"]
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

WORKDIR /src
COPY ["VniktiRdmBackend/VniktiRdmBackend/VniktiRdmBackend.csproj", "VniktiRdmBackend/"]
COPY ["VniktiRdmBackend/Data/Data.csproj", "Data/"]
RUN dotnet restore "VniktiRdmBackend/VniktiRdmBackend.csproj"

COPY . .
RUN ls
WORKDIR "/src/VniktiRdmBackend/VniktiRdmBackend"
RUN dotnet build "VniktiRdmBackend.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "VniktiRdmBackend.csproj" -c Release -o /app

# RUN curl -sL https://deb.nodesource.com/setup_10.x |  bash -
# RUN apt-get install -y nodejs

# WORKDIR "/app/wwwroot"
# RUN npm install

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "VniktiRdmBackend.dll"]
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["MusicStoreApp.csproj", "."]
RUN dotnet restore "MusicStoreApp.csproj"

COPY . .
RUN dotnet build "MusicStoreApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MusicStoreApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5099
ENV ASPNETCORE_URLS=http://+:5099

ENTRYPOINT ["dotnet", "MusicStoreApp.dll"]

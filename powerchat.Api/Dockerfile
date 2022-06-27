FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["powerchat.Api/powerchat.Api.csproj", "powerchat.Api/"]
RUN dotnet restore "powerchat.Api/powerchat.Api.csproj"
COPY . .
WORKDIR "/src/powerchat.Api"
RUN dotnet build "powerchat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "powerchat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "powerchat.Api.dll"]

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/powerchat.Api/powerchat.Api.csproj", "powerchat.Api/"]
COPY ["src/powerchat.Application/powerchat.Application.csproj", "powerchat.Application/"]
COPY ["src/powerchat.Shared/powerchat.Shared.csproj", "powerchat.Shared/"]
COPY ["src/powerchat.Infrastructure/powerchat.Infrastructure.csproj", "powerchat.Infrastructure/"]
RUN dotnet restore "powerchat.Api/powerchat.Api.csproj"
RUN dotnet restore "powerchat.Application/powerchat.Application.csproj"
RUN dotnet restore "powerchat.Shared/powerchat.Shared.csproj"
RUN dotnet restore "powerchat.Infrastructure/powerchat.Infrastructure.csproj"
COPY ./src .
WORKDIR "/src/powerchat.Api"
RUN dotnet build "powerchat.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "powerchat.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "powerchat.Api.dll"]
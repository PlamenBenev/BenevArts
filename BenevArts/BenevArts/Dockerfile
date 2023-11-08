#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BenevArts.Web/BenevArts.Web.csproj", "BenevArts.Web/"]
COPY ["BenevArts.Common/BenevArts.Common.csproj", "BenevArts.Common/"]
COPY ["BenevArts.Data/BenevArts.Data.csproj", "BenevArts.Data/"]
COPY ["BenevArts.Data.Models/BenevArts.Data.Models.csproj", "BenevArts.Data.Models/"]
COPY ["BenevArts.Services.Data/BenevArts.Services.Data.csproj", "BenevArts.Services.Data/"]
COPY ["BenevArts.Web.ViewModels/BenevArts.Web.ViewModels.csproj", "BenevArts.Web.ViewModels/"]
COPY ["BenevArts.Web.Infrastructure/BenevArts.Web.Infrastructure.csproj", "BenevArts.Web.Infrastructure/"]
RUN dotnet restore "BenevArts.Web/BenevArts.Web.csproj"
COPY . .
WORKDIR "/src/BenevArts.Web"
RUN dotnet build "BenevArts.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BenevArts.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BenevArts.Web.dll"]
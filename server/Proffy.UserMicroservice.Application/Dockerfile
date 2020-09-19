#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Proffy.UserMicroservice.Application/Proffy.UserMicroservice.Application.csproj", "Proffy.UserMicroservice.Application/"]
RUN dotnet restore "Proffy.UserMicroservice.Application/Proffy.UserMicroservice.Application.csproj"
COPY . .
WORKDIR "/src/Proffy.UserMicroservice.Application"
RUN dotnet build "Proffy.UserMicroservice.Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Proffy.UserMicroservice.Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Proffy.UserMicroservice.Application.dll"]
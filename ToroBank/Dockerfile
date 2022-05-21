#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ToroBank.WebApi/ToroBank.WebApi.csproj", "ToroBank.WebApi/"]
COPY ["ToroBank.Infrastructure/ToroBank.Infrastructure.Persistence.csproj", "ToroBank.Infrastructure/"]
COPY ["ToroBank.Application/ToroBank.Application.csproj", "ToroBank.Application/"]
COPY ["BankToro.Domain/ToroBank.Domain.csproj", "BankToro.Domain/"]
COPY ["ToroBank.Infrastructure.Shared/ToroBank.Infrastructure.Shared.csproj", "ToroBank.Infrastructure.Shared/"]
RUN dotnet restore "ToroBank.WebApi/ToroBank.WebApi.csproj"
COPY . .
WORKDIR "/src/ToroBank.WebApi"
RUN dotnet build "ToroBank.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToroBank.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToroBank.WebApi.dll","--environment:Development"]

#RUN chmod +x ./entrypoint.sh
#CMD /bin/bash ./entrypoint.sh
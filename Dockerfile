# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# คัดลอกไฟล์ csproj และ restore
COPY ["ProductCodeApi.Api/ProductCodeApi.Api.csproj", "ProductCodeApi.Api/"]
COPY ["ProductCodeApi.Application/ProductCodeApi.Application.csproj", "ProductCodeApi.Application/"]
COPY ["ProductCodeApi.Domain/ProductCodeApi.Domain.csproj", "ProductCodeApi.Domain/"]
COPY ["ProductCodeApi.Infrastructure/ProductCodeApi.Infrastructure.csproj", "ProductCodeApi.Infrastructure/"]
COPY ["ProductCodeApi.Tests/ProductCodeApi.Tests.csproj", "ProductCodeApi.Tests/"]

RUN dotnet restore "ProductCodeApi.Api/ProductCodeApi.Api.csproj"

# คัดลอกไฟล์ทั้งหมด
COPY . .
WORKDIR "/src/ProductCodeApi.Api"

# Publish แอป
RUN dotnet publish "ProductCodeApi.Api.csproj" -c Release -o /app/publish

# Stage 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
EXPOSE 443

ENTRYPOINT ["dotnet", "ProductCodeApi.Api.dll"]

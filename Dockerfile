# مرحله اول: build با .NET 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# مرحله دوم: اجرای پروژه با ASP.NET Core 8.0
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./
ENV ASPNETCORE_URLS=http://+:7002
EXPOSE 7002
ENTRYPOINT ["dotnet", "Shop.API.dll"]

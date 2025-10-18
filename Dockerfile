FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# فقط فایل‌های لازم رو کپی کن
COPY ["Shop.API/Shop.API.csproj", "Shop.API/"]
COPY . ./

RUN dotnet restore "Shop.API/Shop.API.csproj"
RUN dotnet publish "Shop.API/Shop.API.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out ./

# ✅ اجرای اپ روی پورت 10000 برای سازگاری با Render
ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "Shop.API.dll"]

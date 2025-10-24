# ======================================================================
# STAGE 1: Build (Xây dựng ứng dụng)
# Sử dụng .NET 8 SDK (bản đầy đủ) để build code
# ======================================================================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 1. Copy các file .csproj (project)
# Điều này tối ưu hóa Docker cache. Nếu file project không đổi, không cần restore lại.
# (Giả sử bạn chạy lệnh build từ thư mục gốc của solution)
COPY User.API/User.API.csproj User.API/
COPY User.Application/User.Application.csproj User.Application/
COPY User.Domain/User.Domain.csproj User.Domain/
COPY User.Infrastructure/User.Infrastructure.csproj User.Infrastructure/

# 2. Restore (Tải) các thư viện (NuGet packages)
# Chúng ta chỉ restore project API, nó sẽ tự động kéo theo các project phụ thuộc.
RUN dotnet restore "User.API/User.API.csproj"

# 3. Copy toàn bộ source code còn lại
COPY . .

# 4. Build và Publish project API ở chế độ Release
WORKDIR "/src/User.API"
RUN dotnet publish "User.API.csproj" -c Release -o /app/publish

# ======================================================================
# STAGE 2: Final (Chạy ứng dụng)
# Sử dụng .NET 8 ASP.NET Runtime (bản siêu nhẹ, bảo mật hơn)
# ======================================================================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Copy thành quả đã publish từ stage "build" sang
COPY --from=build /app/publish .

# Cấu hình cổng 9191 mặc định cho container
ENV ASPNETCORE_URLS=http://+:9191
EXPOSE 9191

# Lệnh để khởi chạy service của bạn
ENTRYPOINT ["dotnet", "User.API.dll"]
# DevAcademy

![CI](https://github.com/duongthinh03/DevAcademy/actions/workflows/ci.yml/badge.svg)

Nền tảng đào tạo lập trình đa người bán: khóa học, chấm code tự động, marketplace và ERP cho trung tâm đào tạo.
Dự án cá nhân để học sâu .NET 10, .NET Aspire, modular monolith và Domain-Driven Design.

## Trạng thái

Đang ở **giai đoạn 1**: khung dự án, gateway, CI. Tiếp theo là module Courses.

## Công nghệ

- .NET 10, ASP.NET Core Minimal API
- .NET Aspire (điều phối và dashboard khi phát triển)
- YARP (API Gateway)
- xUnit, GitHub Actions

## Cấu trúc thư mục

```
src/
  Aspire/          AppHost và ServiceDefaults
  Gateway/         API Gateway (YARP)
  Host/            Ứng dụng monolith nạp các module
  BuildingBlocks/  SharedKernel: lớp nền chung cho Domain
tests/             Test kiến trúc và test các module
```

## Chạy thử

Yêu cầu: .NET 10 SDK, Visual Studio 2026 hoặc `dotnet` CLI.

```
dotnet run --project src/Aspire/DevAcademy.AppHost
```

Mở dashboard Aspire theo link in ra trong terminal, rồi gọi `<địa chỉ gateway>/api/ping`.
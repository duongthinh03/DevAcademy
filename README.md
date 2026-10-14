# DevAcademy

![CI](https://github.com/duongthinh03/DevAcademy/actions/workflows/ci.yml/badge.svg)

Nền tảng đào tạo lập trình đa người bán: khóa học, chấm code tự động, marketplace và ERP cho trung tâm đào tạo.
Dự án cá nhân để học sâu .NET 10, .NET Aspire, modular monolith và Domain-Driven Design.

## Trạng thái

Đang ở **giai đoạn 1**: khung dự án, gateway, CI, ADR. Tiếp theo là module Courses.

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
docs/adr/          Các quyết định kiến trúc (ADR)
.github/workflows/ Pipeline CI
```

## Tài liệu kiến trúc

Các quyết định kiến trúc được ghi lại dưới dạng ADR trong [docs/adr](docs/adr):

- [0001 - Modular monolith](docs/adr/0001-modular-monolith.md)
- [0002 - PostgreSQL](docs/adr/0002-postgresql.md)
- [0003 - Cấu trúc code](docs/adr/0003-cau-truc-code.md)
- [0004 - .NET 10 LTS](docs/adr/0004-dotnet-10.md)
- [0005 - Trunk-based](docs/adr/0005-git-trunk-based.md)
- [0006 - API Gateway từ đầu](docs/adr/0006-gateway-tu-dau.md)
- [0007 - Xác thực bằng Keycloak](docs/adr/0007-xac-thuc.md)

## Chạy thử

Yêu cầu: .NET 10 SDK, Visual Studio 2026 hoặc `dotnet` CLI.

```
dotnet run --project src/Aspire/DevAcademy.AppHost
```

Mở dashboard Aspire theo link in ra trong terminal, rồi gọi `<địa chỉ gateway>/api/ping`.
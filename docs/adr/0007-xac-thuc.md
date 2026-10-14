# ADR-0007: Dùng Keycloak cho xác thực và phân quyền

- Trạng thái: Đã chấp nhận
- Ngày: 2026-10-13

## Bối cảnh
Hệ thống có năm vai trò (admin nền tảng, chủ trung tâm, giảng viên, trợ giảng, học viên), nhiều trung
tâm (multi-tenant), nhiều loại client (web Blazor, app MAUI) và sẽ tách thành nhiều service. Cơ chế
xác thực cần dùng chung cho tất cả và kiểm tra được token ở gateway.

## Các phương án
1. ASP.NET Core Identity: nhẹ, tích hợp sẵn, dễ bắt đầu; nhưng phải tự làm đăng nhập tập trung cho
   nhiều client và nhiều service, khó mở rộng khi tách service.
2. Keycloak: máy chủ danh tính riêng theo chuẩn OpenID Connect / OAuth 2.0; có sẵn quản lý người dùng,
   vai trò, đăng nhập một lần; Aspire hỗ trợ chạy Keycloak trong AppHost.

## Quyết định
Chọn Keycloak ngay từ giai đoạn 1. Các service và gateway chỉ kiểm tra token JWT do Keycloak cấp.
Keycloak dùng database PostgreSQL riêng, tách khỏi dữ liệu nghiệp vụ.

## Hệ quả
- Được: phân quyền nhiều tầng có sẵn; một cơ chế đăng nhập dùng chung cho web, app và mọi service;
  học được chuẩn OIDC dùng nhiều trong doanh nghiệp.
- Mất: nặng hơn (Java, cần giới hạn bộ nhớ, khoảng 512MB); nhiều cấu hình hơn (realm, client, role);
  thêm một thành phần phải vận hành và backup.
- Cần làm: thêm Keycloak vào AppHost và tạo realm DevAcademy khi làm module Identity.

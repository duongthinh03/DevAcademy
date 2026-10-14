# ADR-0002: Dùng PostgreSQL làm cơ sở dữ liệu chính

- Trạng thái: Đã chấp nhận
- Ngày: 2026-10-13

## Bối cảnh
Hệ thống cần một cơ sở dữ liệu quan hệ cho dữ liệu nghiệp vụ, sổ cái (cần giao dịch ACID), bài nộp
(tăng rất nhanh) và dữ liệu nhiều trung tâm (multi-tenant). Dự án cá nhân nên ưu tiên miễn phí,
chạy được trong container trên laptop, trên PC Ubuntu và sau này trên Kubernetes.

## Các phương án
1. SQL Server: tích hợp .NET rất tốt, công cụ mạnh; nhưng bản miễn phí (Express) giới hạn dung lượng
   mỗi database, bản đầy đủ tốn phí bản quyền.
2. MySQL: phổ biến, miễn phí; nhưng không có row-level security sẵn, kém linh hoạt hơn ở phân vùng
   bảng và kiểu dữ liệu nâng cao.
3. PostgreSQL: miễn phí, mã nguồn mở, chạy tốt với EF Core qua Npgsql; có row-level security,
   phân vùng bảng, JSONB, nhiều schema trong một database.

## Quyết định
Chọn PostgreSQL. Mỗi module dùng một schema riêng (courses, ordering, ledger...). Multi-tenant dùng
cột TenantId kết hợp row-level security. Hangfire cũng lưu job trong PostgreSQL để không phải
thêm hệ thống mới.

## Hệ quả
- Được: không tốn phí; một loại database cho hầu hết nhu cầu; có sẵn tính năng cho multi-tenant và
  bảng lớn; Aspire và Testcontainers đều hỗ trợ tốt.
- Mất: một số công cụ quen thuộc của hệ sinh thái SQL Server không dùng được; phải tự học vận hành
  PostgreSQL (backup, replication, tuning).
- Cần làm: thêm PostgreSQL vào AppHost khi làm module Courses; thiết lập backup khi đưa lên PC.

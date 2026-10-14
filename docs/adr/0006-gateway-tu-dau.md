# ADR-0006: Dựng API Gateway YARP ngay từ giai đoạn 1

- Trạng thái: Đã chấp nhận
- Ngày: 2026-10-13

## Bối cảnh
Giai đoạn 1 chỉ có một ứng dụng (monolith), nhưng giai đoạn 5 sẽ tách thành nhiều service. Nếu client
(Blazor, app MAUI) gọi thẳng vào ứng dụng, khi tách service phải sửa client để trỏ tới nhiều địa chỉ.

## Các phương án
1. Chưa dùng gateway, chỉ thêm khi tách service: ít việc lúc đầu, nhưng sau này phải sửa mọi client.
2. Đặt gateway YARP trước monolith ngay từ đầu: thêm một tiến trình, nhưng client chỉ biết một địa chỉ.

## Quyết định
Chọn phương án 2. Gateway DevAcademy.Gateway dùng YARP, định tuyến mọi request /api/... về Api và bỏ
tiền tố /api. Địa chỉ đích là https+http://api, được Aspire service discovery phân giải nhờ
.WithReference(api) trong AppHost. Gateway không chứa logic nghiệp vụ; giao tiếp nội bộ giữa các
service không đi qua gateway.

## Hệ quả
- Được: client chỉ biết một địa chỉ từ đầu; khi tách service chỉ đổi cấu hình định tuyến; là chỗ đặt
  xác thực tập trung và rate limit ở giai đoạn 4.
- Mất: thêm một tiến trình phải chạy và một chặng mạng cho mỗi request.
- Đã kiểm chứng: https://localhost:7150/api/ping trả JSON từ Api qua gateway (13/10/2026).

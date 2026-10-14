# ADR-0001: Bắt đầu bằng modular monolith, tách microservices sau

- Trạng thái: Đã chấp nhận
- Ngày: 2026-10-13

## Bối cảnh
Dự án làm một mình, dài hơi (1–2 năm). Nghiệp vụ còn chưa rõ: chưa biết chắc ranh giới
giữa khóa học, đơn hàng, sổ cái, chấm bài sẽ nằm ở đâu. Mục tiêu dài hạn vẫn muốn học
microservices, Kubernetes, message broker.

## Các phương án
1. Microservices ngay từ đầu: học hệ phân tán sớm, nhưng phải dựng hạ tầng phức tạp khi chưa có
   nghiệp vụ nào; cắt sai ranh giới service thì rất tốn công sửa.
2. Monolith thông thường: nhanh nhất lúc đầu, nhưng code dễ dính chùm, về sau không tách được.
3. Modular monolith: một ứng dụng, nhưng chia module nghiệp vụ với ranh giới rõ ràng.

## Quyết định
Chọn phương án 3. Mỗi module có Domain, Application, Infrastructure, Api, Contracts riêng; module
chỉ giao tiếp qua Contracts hoặc sự kiện; luật phụ thuộc được kiểm tra bằng test kiến trúc.
Chỉ tách microservices ở giai đoạn 5, khi ranh giới nghiệp vụ đã rõ.

## Hệ quả
- Được: phát triển nhanh ở giai đoạn đầu; một tiến trình, dễ chạy và debug; tách service sau chỉ
  là "nhấc" module ra.
- Mất: phải giữ kỷ luật về ranh giới module, vì monolith không tự ngăn việc gọi chéo; chưa học được
  các vấn đề của hệ phân tán cho tới giai đoạn 5.
- Cần làm: viết test kiến trúc kiểm tra luật phụ thuộc giữa các module (ADR-0003).

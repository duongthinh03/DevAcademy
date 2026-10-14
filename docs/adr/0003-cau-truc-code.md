# ADR-0003: Cấu trúc code theo module nghiệp vụ và vertical slice

- Trạng thái: Đã chấp nhận
- Ngày: 2026-10-13

## Bối cảnh
Theo ADR-0001, hệ thống là modular monolith và sẽ tách service ở giai đoạn 5. Cách tổ chức code
phải giữ được ranh giới module, để lúc tách chỉ cần "nhấc" module ra mà không phải viết lại.
Logic nghiệp vụ (hoa hồng, thuế, hoàn tiền, lương) cần được test kỹ và tách khỏi phần kỹ thuật.

## Các phương án
1. Clean Architecture chia theo lớp toàn cục (một project Domain, một Application, một Infrastructure
   cho cả hệ thống): quen thuộc, nhưng các module trộn lẫn trong cùng project, ranh giới dễ vỡ.
2. Chia theo module trước, bên trong mỗi module chia theo lớp; tầng Application chia theo tính năng
   (vertical slice).

## Quyết định
Chọn phương án 2. Mỗi module gồm các project Domain, Application, Infrastructure, Api, Contracts.
Application chia theo tính năng: mọi thứ của một use case (Command, Handler, Validator) nằm chung
một thư mục. Các luật bắt buộc:
- Module chỉ tham chiếu Contracts của module khác.
- Mỗi module có DbContext và schema riêng, không join chéo.
- Business logic chỉ nằm trong Domain.
- SharedKernel giữ thật mỏng.
- Luật phụ thuộc được kiểm tra bằng NetArchTest/ArchUnitNET trong CI.

## Hệ quả
- Được: ranh giới module rõ ràng và được kiểm tra tự động; tìm và sửa một use case rất nhanh;
  Domain test được mà không cần database; tách service sau gần như không đổi code Domain.
- Mất: nhiều project hơn; một số code bị lặp giữa các module vì không được dùng chung.
- Cần làm: viết luật kiến trúc đầu tiên khi tạo module Courses.

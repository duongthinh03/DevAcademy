# ADR-0005: Làm việc theo mô hình trunk-based

- Trạng thái: Đã chấp nhận
- Ngày: 2026-10-13

## Bối cảnh
Dự án làm một mình, repo public trên GitHub, có CI chạy build và test mỗi lần push. Cần một cách
dùng Git đơn giản, không tốn công quản lý nhánh nhưng vẫn giữ được nhánh chính luôn chạy được.

## Các phương án
1. Git Flow: nhiều nhánh cố định (develop, release, hotfix); hợp với nhóm lớn có lịch phát hành,
   nhưng quá nặng cho một người, dễ phát sinh merge phức tạp.
2. Trunk-based: làm trực tiếp trên main hoặc nhánh tính năng sống rất ngắn, commit nhỏ và thường xuyên.

## Quyết định
Chọn trunk-based:
- main là nhánh duy nhất sống lâu và phải luôn build xanh.
- Việc lớn hoặc thử nghiệm làm trên nhánh ngắn (vài ngày), merge về main qua pull request để CI kiểm tra.
- Commit nhỏ, message mô tả rõ việc đã làm.

## Hệ quả
- Được: đơn giản, ít xung đột; lịch sử commit dễ đọc; CI báo lỗi ngay sau mỗi lần push.
- Mất: phải tự giữ kỷ luật không đẩy code hỏng lên main; chưa luyện quy trình review của nhóm.
- Cần làm: khi CI báo đỏ thì sửa ngay trước khi làm việc khác.

using System;
using System.Collections.Generic;
using System.Text;

using DevAcademy.Courses.Domain;

namespace DevAcademy.Courses.UnitTests
{
    public class CourseTests
    {
        private static readonly Guid GiangVien = Guid.NewGuid();
        private static readonly DateTimeOffset BayGio = new(2026, 10, 14, 9, 0, 0, TimeSpan.Zero);

        private static Course TaoKhoaHocNhap() =>
            Course.Create(GiangVien, "Lập trình C# cơ bản", 499_000m).Value!;

        [Fact]
        public void Tao_khoa_hoc_hop_le_thi_o_trang_thai_nhap()
        {
            var ketQua = Course.Create(GiangVien, "  Lập trình C# cơ bản  ", 499_000m);

            Assert.True(ketQua.IsSuccess);
            Assert.Equal(CourseStatus.Draft, ketQua.Value!.Status);
            Assert.Equal("Lập trình C# cơ bản", ketQua.Value.Title);
        }

        [Fact]
        public void Khong_tao_duoc_khoa_hoc_khi_ten_trong()
        {
            var ketQua = Course.Create(GiangVien, "   ", 499_000m);

            Assert.True(ketQua.IsFailure);
        }

        [Fact]
        public void Khong_tao_duoc_khoa_hoc_khi_gia_am()
        {
            var ketQua = Course.Create(GiangVien, "Lập trình C# cơ bản", -1m);

            Assert.True(ketQua.IsFailure);
        }

        [Fact]
        public void Khong_tao_duoc_khoa_hoc_khi_gia_le_xu()
        {
            var ketQua = Course.Create(GiangVien, "Lập trình C# cơ bản", 499_000.5m);

            Assert.True(ketQua.IsFailure);
        }

        [Fact]
        public void Xuat_ban_that_bai_khi_chua_co_bai_hoc()
        {
            var khoaHoc = TaoKhoaHocNhap();

            var ketQua = khoaHoc.Publish(BayGio);

            Assert.True(ketQua.IsFailure);
            Assert.Equal(CourseStatus.Draft, khoaHoc.Status);
            Assert.Empty(khoaHoc.DomainEvents);
        }

        [Fact]
        public void Xuat_ban_thanh_cong_khi_co_bai_hoc_va_phat_su_kien()
        {
            var khoaHoc = TaoKhoaHocNhap();
            khoaHoc.AddLesson("Giới thiệu", 10);

            var ketQua = khoaHoc.Publish(BayGio);

            Assert.True(ketQua.IsSuccess);
            Assert.Equal(CourseStatus.Published, khoaHoc.Status);
            var suKien = Assert.IsType<CoursePublished>(Assert.Single(khoaHoc.DomainEvents));
            Assert.Equal(khoaHoc.Id, suKien.CourseId);
            Assert.Equal(BayGio, suKien.OccurredAt);
        }

        [Fact]
        public void Khong_xuat_ban_lai_khoa_hoc_da_xuat_ban()
        {
            var khoaHoc = TaoKhoaHocNhap();
            khoaHoc.AddLesson("Giới thiệu", 10);
            khoaHoc.Publish(BayGio);

            var ketQua = khoaHoc.Publish(BayGio);

            Assert.True(ketQua.IsFailure);
        }

        [Fact]
        public void Bai_hoc_duoc_danh_so_thu_tu_tang_dan()
        {
            var khoaHoc = TaoKhoaHocNhap();

            khoaHoc.AddLesson("Giới thiệu", 10);
            khoaHoc.AddLesson("Biến và kiểu dữ liệu", 25);

            Assert.Equal(new[] { 1, 2 }, khoaHoc.Lessons.Select(b => b.Order));
        }

        [Fact]
        public void Khong_them_bai_hoc_vao_khoa_hoc_da_luu_tru()
        {
            var khoaHoc = TaoKhoaHocNhap();
            khoaHoc.Archive();

            var ketQua = khoaHoc.AddLesson("Giới thiệu", 10);

            Assert.True(ketQua.IsFailure);
            Assert.Empty(khoaHoc.Lessons);
        }
    }
}

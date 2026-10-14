using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

using DevAcademy.SharedKernel;

namespace DevAcademy.Courses.Domain;

public sealed class Course : AggregateRoot
{
    public const int TitleMaxLength = 200;

    private readonly List<Lesson> _lessons = new();

    private Course(Guid instructorId, string title, decimal price)
    {
        InstructorId = instructorId;
        Title = title;
        Price = price;
        Status = CourseStatus.Draft;
    }

    public Guid InstructorId { get; }
    public string Title { get; private set; }
    public decimal Price { get; private set; }
    public CourseStatus Status { get; private set; }
    public IReadOnlyList<Lesson> Lessons => _lessons.AsReadOnly();

    public static Result<Course> Create(Guid instructorId, string title, decimal price)
    {
        if (instructorId == Guid.Empty)
            return Result<Course>.Fail("Khóa học phải có giảng viên.");

        if (string.IsNullOrWhiteSpace(title))
            return Result<Course>.Fail("Tên khóa học không được để trống.");

        if (title.Trim().Length > TitleMaxLength)
            return Result<Course>.Fail($"Tên khóa học tối đa {TitleMaxLength} ký tự.");

        if (price < 0)
            return Result<Course>.Fail("Giá khóa học không được âm.");

        if (price != decimal.Truncate(price))
            return Result<Course>.Fail("Giá khóa học phải là số đồng nguyên.");

        return Result<Course>.Ok(new Course(instructorId, title.Trim(), price));
    }

    public Result AddLesson(string title, int durationMinutes)
    {
        if (Status == CourseStatus.Archived)
            return Result.Fail("Không thể thêm bài học vào khóa học đã lưu trữ.");

        if (string.IsNullOrWhiteSpace(title))
            return Result.Fail("Tên bài học không được để trống.");

        if (durationMinutes <= 0)
            return Result.Fail("Thời lượng bài học phải lớn hơn 0 phút.");

        _lessons.Add(new Lesson(title.Trim(), durationMinutes, _lessons.Count + 1));
        return Result.Ok();
    }

    public Result Publish(DateTimeOffset now)
    {
        if (Status != CourseStatus.Draft)
            return Result.Fail("Chỉ khóa học nháp mới được xuất bản.");

        if (_lessons.Count == 0)
            return Result.Fail("Khóa học phải có ít nhất một bài học.");

        Status = CourseStatus.Published;
        Raise(new CoursePublished(Id, now));
        return Result.Ok();
    }

    public Result Archive()
    {
        if (Status == CourseStatus.Archived)
            return Result.Fail("Khóa học đã được lưu trữ.");

        Status = CourseStatus.Archived;
        return Result.Ok();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using DevAcademy.SharedKernel;

namespace DevAcademy.Courses.Domain;

public sealed class Lesson : Entity
{
    internal Lesson(string title, int durationMinutes, int order)
    {
        Title = title;
        DurationMinutes = durationMinutes;
        Order = order;
    }

    public string Title { get; }
    public int DurationMinutes { get; }
    public int Order { get; }
}

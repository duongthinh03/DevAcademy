using System;
using System.Collections.Generic;
using System.Text;
using DevAcademy.SharedKernel;

namespace DevAcademy.Courses.Domain;

public sealed record CoursePublished(Guid CourseId, DateTimeOffset OccurredAt) : IDomainEvent;
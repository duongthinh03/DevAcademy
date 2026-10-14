using System;
using System.Collections.Generic;
using System.Text;

namespace DevAcademy.SharedKernel
{
    public interface IDomainEvent
    {
        DateTimeOffset OccurredAt { get; }
    }
}

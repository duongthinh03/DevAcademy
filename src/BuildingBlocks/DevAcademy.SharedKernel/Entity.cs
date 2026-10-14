using System;
using System.Collections.Generic;
using System.Text;

namespace DevAcademy.SharedKernel;

public abstract class Entity
{
    public Guid Id { get; protected init; } = Guid.NewGuid();
}

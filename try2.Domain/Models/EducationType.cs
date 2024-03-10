using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class EducationType : Entity
{

    public string? Name { get; set; }

    public virtual ICollection<Expert> Experts { get; set; } = new List<Expert>();
}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class Project : Entity
{
    public string? Name { get; set; }

    //[JsonIgnore]
    public virtual ICollection<Version> Versions { get; set; } = new List<Version>();
}

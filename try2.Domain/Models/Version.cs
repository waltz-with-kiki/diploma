using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class Version : Entity
{

    public long? ProjectId { get; set; }

    public int? N { get; set; }

    public int? Nn { get; set; }

    public int? Nnn { get; set; }

    public string? Descr { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual Project? Project { get; set; }
}

using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class ImRequest : Entity
{

    public string? Name { get; set; }

    public long? OrderNumber { get; set; }

    public long? GroupId { get; set; }

    public virtual ImGroupRequest? Group { get; set; }

    public virtual ICollection<ImAnswer> ImAnswers { get; set; } = new List<ImAnswer>();
}

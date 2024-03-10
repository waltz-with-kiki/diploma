using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class ImGroupRequest : Entity
{

    public string? Name { get; set; }

    public int? OrderNumber { get; set; }

    public long? ParentId { get; set; }

    public virtual ICollection<ImRequest> ImRequests { get; set; } = new List<ImRequest>();

    public virtual ICollection<ImGroupRequest> InverseParent { get; set; } = new List<ImGroupRequest>();

    public virtual ImGroupRequest? Parent { get; set; }
}

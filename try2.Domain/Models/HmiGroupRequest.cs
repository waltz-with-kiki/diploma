using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class HmiGroupRequest : Entity
{

    public string? Name { get; set; }

    public int? OrderNumber { get; set; }

    public long? ParentId { get; set; }

    public virtual ICollection<HmiRequest> HmiRequests { get; set; } = new List<HmiRequest>();

    public virtual ICollection<HmiGroupRequest> InverseParent { get; set; } = new List<HmiGroupRequest>();

    public virtual HmiGroupRequest? Parent { get; set; }
}

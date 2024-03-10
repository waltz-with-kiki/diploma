using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class HmiRequest : Entity
{

    public string? Name { get; set; }

    public long? OrderNumber { get; set; }

    public long? GroupId { get; set; }

    public virtual HmiGroupRequest? Group { get; set; }

    public virtual ICollection<HmiAnswer> HmiAnswers { get; set; } = new List<HmiAnswer>();
}

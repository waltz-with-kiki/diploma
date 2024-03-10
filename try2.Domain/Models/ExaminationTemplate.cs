using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class ExaminationTemplate : Entity
{

    public string? Name { get; set; }

    public string? Descr { get; set; }

    public long? Qrim { get; set; }

    public long? Qrhmi { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();

    public virtual HmiQuestionnaire? QrhmiNavigation { get; set; }

    public virtual ImQuestionnaire? QrimNavigation { get; set; }
}

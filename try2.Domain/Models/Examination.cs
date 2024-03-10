using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class Examination : Entity
{

    public long? ExaminationTemplateId { get; set; }

    public long? ExpertId { get; set; }

    public long? VersionId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Comment { get; set; }

    public virtual ExaminationTemplate? ExaminationTemplate { get; set; }

    public virtual Expert? Expert { get; set; }

    public virtual ICollection<HmiAnswer> HmiAnswers { get; set; } = new List<HmiAnswer>();

    public virtual ICollection<HmiQuestionnareGeneralAnswer> HmiQuestionnareGeneralAnswers { get; set; } = new List<HmiQuestionnareGeneralAnswer>();

    public virtual ICollection<HmiSectionGeneralAnswer> HmiSectionGeneralAnswers { get; set; } = new List<HmiSectionGeneralAnswer>();

    public virtual ICollection<ImAnswer> ImAnswers { get; set; } = new List<ImAnswer>();

    public virtual ICollection<ImQuestionnareGeneralAnswer> ImQuestionnareGeneralAnswers { get; set; } = new List<ImQuestionnareGeneralAnswer>();

    public virtual ICollection<ImSectionGeneralAnswer> ImSectionGeneralAnswers { get; set; } = new List<ImSectionGeneralAnswer>();

    public virtual Version? Version { get; set; }
}

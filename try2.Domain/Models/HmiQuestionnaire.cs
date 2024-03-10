﻿using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class HmiQuestionnaire : Entity
{

    public string? Name { get; set; }

    public bool? GeneralRequest { get; set; }

    public virtual ICollection<ExaminationTemplate> ExaminationTemplates { get; set; } = new List<ExaminationTemplate>();

    public virtual ICollection<HmiAnswer> HmiAnswers { get; set; } = new List<HmiAnswer>();

    public virtual ICollection<HmiQuestionnareGeneralAnswer> HmiQuestionnareGeneralAnswers { get; set; } = new List<HmiQuestionnareGeneralAnswer>();

    public virtual ICollection<HmiSectionGeneralAnswer> HmiSectionGeneralAnswers { get; set; } = new List<HmiSectionGeneralAnswer>();
}

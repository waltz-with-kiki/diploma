﻿using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class HmiQuestionnareGeneralAnswer : Entity
{

    public long? QuestionnaireId { get; set; }

    public long? ExaminationId { get; set; }

    public double? Numeric { get; set; }

    public string? Comment { get; set; }

    public virtual Examination? Examination { get; set; }

    public virtual HmiQuestionnaire? Questionnaire { get; set; }
}

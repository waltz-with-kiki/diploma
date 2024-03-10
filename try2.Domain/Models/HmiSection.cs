﻿using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class HmiSection : Entity
{

    public string? Name { get; set; }

    public bool? GeneralRequest { get; set; }

    public virtual ICollection<HmiAnswer> HmiAnswers { get; set; } = new List<HmiAnswer>();

    public virtual ICollection<HmiSectionGeneralAnswer> HmiSectionGeneralAnswers { get; set; } = new List<HmiSectionGeneralAnswer>();
}

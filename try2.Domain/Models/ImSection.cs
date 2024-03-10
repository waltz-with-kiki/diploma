﻿using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class ImSection : Entity
{

    public string? Name { get; set; }

    public bool? GeneralRequest { get; set; }

    public virtual ICollection<ImAnswer> ImAnswers { get; set; } = new List<ImAnswer>();

    public virtual ICollection<ImSectionGeneralAnswer> ImSectionGeneralAnswers { get; set; } = new List<ImSectionGeneralAnswer>();
}
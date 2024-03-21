using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class ImSection : Entity
{

    public string? Name { get; set; }

    public bool? GeneralRequest { get; set; }

    public virtual ICollection<ImAnswer> ImAnswers { get; set; } = new List<ImAnswer>();

    [NotMapped]
    public virtual ICollection<ImRequestInSection> ImRequestInSections { get; set; } = new List<ImRequestInSection>();

    [NotMapped]
    public virtual ICollection<ImSectionInQuestionnaire> ImSectionInQuestionnaires { get; set; } = new List<ImSectionInQuestionnaire>();

    public virtual ICollection<ImSectionGeneralAnswer> ImSectionGeneralAnswers { get; set; } = new List<ImSectionGeneralAnswer>();
}

using System;
using System.Collections.Generic;

namespace try2.DAL.Models;

public partial class ImSectionInQuestionnaire
{
    public long? SectionId { get; set; }

    public long? QuestionnaireId { get; set; }

    public virtual ImQuestionnaire? Questionnaire { get; set; }

    public virtual ImSection? Section { get; set; }
}

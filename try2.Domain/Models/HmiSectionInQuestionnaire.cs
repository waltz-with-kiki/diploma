using System;
using System.Collections.Generic;

namespace try2.DAL.Models;

public partial class HmiSectionInQuestionnaire
{
    public long? SectionId { get; set; }

    public long? QuestionnaireId { get; set; }

    public virtual HmiQuestionnaire? Questionnaire { get; set; }

    public virtual HmiSection? Section { get; set; }
}

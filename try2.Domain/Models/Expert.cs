using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class Expert : Entity
{

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public int? BirthYear { get; set; }

    public int? ServiceYear { get; set; }

    public int? FlightHours { get; set; }

    public long? Education { get; set; }

    public int? PilotClass { get; set; }


    [NotMapped]
    public virtual ICollection<AircraftTypeForExpert>? AircraftTypeForExperts { get; set; }

    public virtual EducationType? EducationNavigation { get; set; }

    public virtual ICollection<Examination> Examinations { get; set; } = new List<Examination>();
}

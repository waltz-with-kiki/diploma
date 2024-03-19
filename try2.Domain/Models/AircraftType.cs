﻿using System;
using System.Collections.Generic;
using try2.Domain.Entities.Base;

namespace try2.DAL.Models;

public partial class AircraftType : Entity
{

    public string? Name { get; set; }

    public virtual ICollection<AircraftTypeForExpert>? AircraftTypeForExperts { get; set; }
}

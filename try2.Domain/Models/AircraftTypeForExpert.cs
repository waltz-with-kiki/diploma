using System;
using System.Collections.Generic;

namespace try2.DAL.Models;

public partial class AircraftTypeForExpert
{
    public long? AircraftTypeId { get; set; }

    public long? ExpertId { get; set; }

    public virtual AircraftType? AircraftType { get; set; }

    public virtual Expert? Expert { get; set; }
}

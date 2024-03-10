using System;
using System.Collections.Generic;

namespace try2.DAL.Models;

public partial class HmiRequestInSection
{
    public long? SectionId { get; set; }

    public long? RequestId { get; set; }

    public virtual HmiRequest? Request { get; set; }

    public virtual HmiSection? Section { get; set; }
}

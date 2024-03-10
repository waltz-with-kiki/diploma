using System;
using System.Collections.Generic;

namespace try2.DAL.Models;

public partial class ImRequestInSection
{
    public long? SectionId { get; set; }

    public long? RequestId { get; set; }

    public virtual ImRequest? Request { get; set; }

    public virtual ImSection? Section { get; set; }
}

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using try2.Domain.Models.Entities;

namespace try2.DAL.Configurations
{
    public class ProfileConfiguration : EntityConfiguration<Profile>
    {

        public override void Configure(EntityTypeBuilder<Profile> builder)
        {
            base.Configure(builder);

            builder.Property(e => e.NickName).HasMaxLength(32);
        }

    }
}

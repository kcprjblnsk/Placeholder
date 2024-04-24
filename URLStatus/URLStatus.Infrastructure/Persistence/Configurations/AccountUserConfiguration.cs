using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using URLStatus.Domain.Entities;

namespace URLStatus.Infrastructure.Persistence.Configurations
{
    public class AccountUserConfiguration : IEntityTypeConfiguration<AccountUser>
    {
        public void Configure(EntityTypeBuilder<AccountUser> builder)
        {
            builder
                .HasOne(p => p.Account)
                .WithMany(p => p.AccountUsers)
                .HasForeignKey(p => p.AccountId);           
            
            builder
                .HasOne(p => p.User)
                .WithMany(p => p.AccountUsers)
                .HasForeignKey(p => p.UserId);


            
        }
    }
}

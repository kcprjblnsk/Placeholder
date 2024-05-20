using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using URLStatus.Domain.Entities;

namespace URLStatus.Infrastructure.Persistence.Configurations
{
    public class MonitoredUrlConfiguration : IEntityTypeConfiguration<MonitoredUrl>
    {
        public void Configure(EntityTypeBuilder<MonitoredUrl> builder)
        {
            JsonSerializerOptions? serializerOptions = null;

            builder.Property(p => p.RuleSet)
                .HasColumnName("RuleSetJson")
                .HasConversion(
                    v => JsonSerializer.Serialize(v, serializerOptions),
                    v => JsonSerializer.Deserialize<ResultRuleSet>(v, serializerOptions) ?? new ResultRuleSet());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLStatus.Infrastructure.Auth
{
    public class JwtAuthenticationOptions
    {
        public string? Secret { get; set; } //password for en(de)cryption token
        public string? Issuer { get; set; } //wystawca
        public string? Audience { get; set; } // odbiorca
        public int ExpireInDays { get; set; } = 30;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLStatus.WebAPI.Application.Auth
{
    public class CookieSettings
    {
        public const string CookieName = "auth.token";
        public bool Secure { get; set; }
        public SameSiteMode SameSite = SameSiteMode.Lax;
    }
}

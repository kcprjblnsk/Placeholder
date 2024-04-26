﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLStatus.Application.Interfaces
{
    public interface IPasswordManager
    {
        string HashPassword(string password);
        bool VerifyPassword(string hash, string password);
    }
}

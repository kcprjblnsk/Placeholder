﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLStatus.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public List<FieldError> Errors { get; set; } = new List<FieldError>();

        public class FieldError
        {
            public required string FieldName { get; set; }
            public required string Error { get; set; }

        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Common.Exceptions
{
    public class ObjectCreationException : Exception
    {
        public ObjectCreationException(string message) : base(message)
        {
        }
    }
}

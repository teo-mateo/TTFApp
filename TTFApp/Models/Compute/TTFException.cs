using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTFApp.Models.Compute
{
    public class TTFException : Exception
    {
        public TTFException(string message)
            : base(message)
        {

        }
    }
}
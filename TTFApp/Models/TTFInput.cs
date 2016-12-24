using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TTFApp.Models
{
    public class TTFInput
    {
        public TTFInput(bool a, bool b, bool c, int d, int e, int f)
        {
            A = a; B = b; C = c;
            D = d; E = e; F = f;
        }
        public bool A { get; set; }
        public bool B { get; set; }
        public bool C { get; set; }
        public int D { get; set; }
        public int E { get; set; }
        public int F { get; set; }
    }
}

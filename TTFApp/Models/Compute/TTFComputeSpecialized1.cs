using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTFApp.Models.Compute
{
    public class TTFComputeSpecialized1 : TTFCompute
    {
        public TTFComputeSpecialized1()
            : base()
        {
            //add/overwrite specialized compute rule
            base.AddComputeRule(SRT.R, (d, e, f) => 2 * d + (d * e / 100.0m));
        }
    }
}

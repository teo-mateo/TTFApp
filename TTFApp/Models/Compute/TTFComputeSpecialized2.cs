using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTFApp.Models.Compute
{
    public class TTFComputeSpecialized2 : TTFCompute
    {
        public TTFComputeSpecialized2() : base()
        {
            //add/overwrite specialized compute rule
            base.AddComputeRule(SRT.S, (d, e, f) => f + d + (d * e / 100.0m));

            //add/overwrite specialized decision rule
            base.AddDecisionRule(SRT.T, (a, b, c) => a && b && !c);
            base.AddDecisionRule(SRT.S, (a, b, c) => a && !b && c);
        }
    }
}

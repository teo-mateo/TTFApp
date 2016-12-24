using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TTFApp.Models.Compute
{
    /// <summary>
    /// default implementation of the compute class
    /// </summary>
    public class TTFCompute : TTFComputeBase
    {
        public TTFCompute() : base()
        {
            base.AddDecisionRule(SRT.S, (a, b, c) => a && b && !c);
            base.AddDecisionRule(SRT.R, (a, b, c) => a && b && c);
            base.AddDecisionRule(SRT.T, (a, b, c) => !a && b && c);

            base.AddComputeRule(SRT.S, (d, e, f) => d + (d * e / 100.0m));
            base.AddComputeRule(SRT.R, (d, e, f) => d + (d * (e - f) / 100.0m));
            base.AddComputeRule(SRT.T, (d, e, f) => d - (d * f / 100.0m));
        }
    }
}

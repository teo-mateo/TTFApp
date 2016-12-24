using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TTFApp.Models.Decide;

namespace TTFApp.Models.Compute
{
    public abstract class TTFComputeBase : ITTFCompute
    {
        ABCDecision _abcDecision;
        protected ABCDecision AbcDecision { get; }

        private Dictionary<SRT, Func<decimal, decimal, decimal, decimal>> _computeRules;
        protected Dictionary<SRT, Func<decimal, decimal, decimal, decimal>> ComputeRules { get; }

        public TTFComputeBase()
        {
            _abcDecision = new ABCDecision();
            _computeRules = new Dictionary<Models.SRT, Func<decimal, decimal, decimal, decimal>>();
        }

        /// <summary>
        /// Add / overwrite a compute rule
        /// </summary>
        /// <param name="srt"></param>
        /// <param name="f"></param>
        protected void AddComputeRule(SRT srt, Func<decimal, decimal, decimal, decimal> f)
        {
            _computeRules[srt] = f;
        }

        /// <summary>
        /// Add / overwrite a decision rule
        /// </summary>
        /// <param name="srt"></param>
        /// <param name="e"></param>
        protected void AddDecisionRule(SRT srt, Expression<Func<bool, bool, bool, bool>> e)
        {
            _abcDecision.AddRule(srt, e);
        }

        /// <summary>
        /// Compute the result
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TTFResult Compute(TTFInput input)
        {
            if (input == null)
                throw new ArgumentException("missing TTF input argument");

            SRT x = _abcDecision.Decide(input.A, input.B, input.C);
            decimal y;
            if (_computeRules.ContainsKey(x))
            {
                y = _computeRules[x](input.D, input.E, input.F);
            }
            else
            {
                throw new TTFException("No rule defined for " + x);
            }

            return new Models.TTFResult() { X = x, Y = y };
        }
    }
}

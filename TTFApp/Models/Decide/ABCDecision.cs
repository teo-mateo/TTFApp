using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TTFApp.Models.Compute;

namespace TTFApp.Models.Decide
{
    /// <summary>
    /// This class holds the logic for making the decision based on the three boolean inputs
    /// </summary>
    public class ABCDecision
    {
        private List<ABCRule> _rules;

        public ABCDecision()
        {
            _rules = new List<ABCRule>();
        }

        /// <summary>
        /// this warrants a small discussion. 
        /// There are two possible interpretations of the 2nd specialization
        /// 1.  overwriting by result
        ///     i)  BASE(1) is replaced by SPECIALIZED(2) 
        ///     ii) BASE(3) is replaced by SPECIALIZED(1) 
        ///     
        /// 2.  overwriting by expression  
        ///     i)  BASE(1) is replaced by SPECIALIZED(1) because the expressions are the same
        ///     ii) A new rule is added, therefore we have two rules that could result in T. Depending on the case, this could be a feature. 
        ///     
        /// I chose to go with the 1st option. I hope this is what you wanted. 
        /// The second can very well be implemented by using ABCRule.ExpressionEquals, like this: 
        ///     int index = _rules.FindIndex(r=> r.ExpressionEquals(e));
        ///     
        /// </summary>
        /// <param name="srt"></param>
        /// <param name="e"></param>
        public void AddRule(SRT srt, Expression<Func<bool, bool, bool, bool>> e)
        {
            ABCRule rule = new ABCRule(srt, e);
            int index = _rules.FindIndex(r => r.Result == srt);
            if (index > -1)
                _rules[index] = rule;
            else
                _rules.Add(rule);
        }


        public SRT Decide(bool a, bool b, bool c)
        {
            var fitting = _rules.Where(r => r.Fits(a, b, c)).Select(r => r.Result).Distinct();
            if (fitting.Count() == 0)
                throw new TTFException("Panic, nuthin' fits.");
            else if (fitting.Count() == 1)
                return fitting.First();
            else
                throw new TTFException("Panic, more than one fits!");
        }

    }
}

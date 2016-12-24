using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TTFApp.Models.Decide
{
    /// <summary>
    /// Represents a single decision rule, such as: A && B && !C => X = S (copypasta from the requirements)
    /// It states that applying the left-side expression to the A,B,C inputs, S will be obtained.
    /// The expression that evaluates as a bool is stored as an Expression object.
    /// This allows us to compare two expressions; see comment below on comparisons
    /// </summary>
    public class ABCRule
    {
        private SRT _result;
        public SRT Result { get { return _result; } }

        private Expression<Func<bool, bool, bool, bool>> _expression;
        private Func<bool, bool, bool, bool> _f;
        public ABCRule(SRT srt, Expression<Func<bool, bool, bool, bool>> e)
        {
            _result = srt;
            _expression = e;
            _f = e.Compile();
        }

        /// <summary>
        /// </summary>
        /// <returns>true if the expression evaluates to true for the given inputs, false otherwise</returns>
        public bool Fits(bool a, bool b, bool c)
        {
            return _f(a, b, c);
        }

        public bool ExpressionEquals(Expression<Func<bool, bool, bool, bool>> e)
        {
            if (e == null)
                throw new ArgumentException();

            //simple expressions can support a simplistic comparison :)
            //more complex expression will give false negatives.
            //but as long as we express the a/b/c expression in a simple way, string comparison is sufficient.
            //otherwise, a custom expression comparer can be built. 
            //here's an example: https://github.com/lytico/db4o/blob/master/db4o.net/Db4objects.Db4o.Linq/Db4objects.Db4o.Linq/Expressions/ExpressionEqualityComparer.cs
            return _expression.ToString().Equals(e.ToString());
        }
    }
}

    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTFApp.Models.Compute
{
    public interface ITTFCompute
    {
        TTFResult Compute(TTFInput input);
    }
}

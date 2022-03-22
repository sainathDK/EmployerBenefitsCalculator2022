using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPCalcAPI.Rule.Interfaces
{
    public interface IGetCostOfBenefitPerPayCheckPerMemberTypeRule
    {
        decimal Compute(string memberTypeCd, int totalPayChecks);
    }
}

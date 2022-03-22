using BPCalcAPI.Rule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPCalcAPI.Rules
{
    public class GetCostOfBenefitPerPayCheckPerMemberTypeRule : IGetCostOfBenefitPerPayCheckPerMemberTypeRule
    {
        private const string EMPLOYEE_CD = "E";
        private const string DEPENDENT_SPOUSE = "DS";
        private const string DEPENDENT_CHILD = "DC";

        public decimal Compute(string memberTypeCd,int totalPayChecks)
        {
            decimal retVal;
            switch (memberTypeCd)
            {
                case EMPLOYEE_CD:
                    retVal = 1000m / totalPayChecks;
                    break;
                case DEPENDENT_SPOUSE:
                    retVal = 500m / totalPayChecks;
                    break;
                case DEPENDENT_CHILD:
                    retVal = 500m / totalPayChecks;
                    break;
                default:
                    throw new InvalidOperationException($"MemberTypeCode {memberTypeCd} not recognized.");
            }

            return retVal;
        }
    }
}

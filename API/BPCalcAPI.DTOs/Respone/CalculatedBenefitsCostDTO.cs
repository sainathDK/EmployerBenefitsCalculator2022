
using System.Collections.Generic;

namespace BPCalcAPI.Workflow.Interfaces.Response
{
    public class CalculatedBenefitsCostDTO
    {
        public List<List<MemberCostDto>> MemberCostDetails { get; set; }
    }
}

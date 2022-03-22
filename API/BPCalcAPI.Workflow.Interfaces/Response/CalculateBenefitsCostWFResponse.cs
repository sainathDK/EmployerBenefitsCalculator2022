using BPCalcAPI.Entities;
using System.Collections.Generic;

namespace BPCalcAPI.Workflow.Interfaces.Response
{
    public class CalculateBenefitsCostWFResponse
    {
        public List<List<MemberCost>> MemberCostDetails { get; set; }
    }
}


namespace BPCalcAPI.Workflow.Interfaces.Response
{
    public class MemberCostDto
    {
        public string MemberIdentifier { get; set; }
        public decimal CostToMemberPerPayCheck { get; set; }


        public decimal TotalCostOfBenefitForTheMember { get; set; }

        public decimal TotalPayPerCheckBeforeDeductions { set; get; }

        public decimal TotalPayPerYearBeforeDeductions { set; get; }

        public decimal TotalPayPerPayCheckAfterDeductions { set; get; }

        public decimal TotalPayPerYearAfterDeductions { get; set; }
    }
}

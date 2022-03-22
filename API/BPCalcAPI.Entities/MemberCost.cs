
namespace BPCalcAPI.Entities
{
    public class MemberCost
    {
        public string MemberIdentifier { get; set; }
        public decimal CostToMemberPerPayCheck { get; set; }
        
        public bool IsEmployee { get; set; }

        public decimal TotalCostOfBenefitForTheMember { get; set; }
        
        public decimal TotalPayPerCheckBeforeDeductions { set; get; }
        
        public decimal TotalPayPerYearBeforeDeductions { set; get; }
        
        public decimal TotalPayPerPayCheckAfterDeductions { set; get; }
        
        public decimal TotalPayPerYearAfterDeductions { get; set; }
    }

}

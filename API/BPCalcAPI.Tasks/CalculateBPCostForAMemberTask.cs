using BPCalcAPI.Entities;
using BPCalcAPI.Task.Interfaces;


namespace BPCalcAPI.Tasks
{
    public class CalculateBPCostForAMemberTask : ICalculateBPCostForAMemberTask
    {


        public MemberCost Execute(decimal costOfBPPerPayCheck, decimal totalDiscount,int totalPayChecksInAYear)
        {
            MemberCost mCost = new MemberCost();

            //finally compute the yearly amt and paychck amts for the person 
            mCost.CostToMemberPerPayCheck = costOfBPPerPayCheck - totalDiscount;
            mCost.TotalCostOfBenefitForTheMember = mCost.CostToMemberPerPayCheck * totalPayChecksInAYear;

            return mCost;
        }

    }
}

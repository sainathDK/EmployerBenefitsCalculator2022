using BPCalcAPI.Mapper.Interfaces;
using BPCalcAPI.Workflow.Interfaces.Response;
using System;
using System.Collections.Generic;

namespace BPCalcAPI.Mappers
{
    public class WorkFlowToExternReturnResponseMapper : IWorkFlowToExternReturnResponseMapper
    {
        public CalculatedBenefitsCostDTO MapFrom(CalculateBenefitsCostWFResponse srcData)
        {
            if (srcData is null) throw new ArgumentNullException(nameof(srcData));

            CalculatedBenefitsCostDTO retVal = new CalculatedBenefitsCostDTO();
            if (retVal.MemberCostDetails == null)
                retVal.MemberCostDetails = new List<List<MemberCostDto>>();

            foreach (var memberAndDependents in srcData.MemberCostDetails)
            {
                List<MemberCostDto> familyInst = new List<MemberCostDto>();

                foreach (var personInst in memberAndDependents)
                {
                    MemberCostDto targetmemberAndDependents = new MemberCostDto();
                    targetmemberAndDependents.MemberIdentifier = personInst.MemberIdentifier;
                    targetmemberAndDependents.CostToMemberPerPayCheck = Math.Round(personInst.CostToMemberPerPayCheck,2);
                    targetmemberAndDependents.TotalCostOfBenefitForTheMember = Math.Round(personInst.TotalCostOfBenefitForTheMember, 2);

                    targetmemberAndDependents.TotalPayPerCheckBeforeDeductions = Math.Round(personInst.TotalPayPerCheckBeforeDeductions,2);
                    targetmemberAndDependents.TotalPayPerPayCheckAfterDeductions = Math.Round(personInst.TotalPayPerPayCheckAfterDeductions,2);
                    targetmemberAndDependents.TotalPayPerYearAfterDeductions = Math.Round(personInst.TotalPayPerYearAfterDeductions,2);
                    targetmemberAndDependents.TotalPayPerYearBeforeDeductions = Math.Round(personInst.TotalPayPerYearBeforeDeductions,2);

                    familyInst.Add(targetmemberAndDependents);
                }
                retVal.MemberCostDetails.Add(familyInst);
            }


            return retVal;
        }
    }
}

using BPCalcApi.Entities.Interfaces;
using BPCalcAPI.Entities;
using BPCalcAPI.Rule.Interfaces;
using BPCalcAPI.Task.Interfaces;
using BPCalcAPI.Workflow.Interfaces;
using BPCalcAPI.Workflow.Interfaces.Request;
using BPCalcAPI.Workflow.Interfaces.Response;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BPCalcAPI.Workflows
{
    public class CalculateBenefitsCostWF : ICalculateBenefitsCostWF
    {
        private readonly IBpCalculationAttributes _bpCalcAttr;
        private readonly ICalculateBPCostForAMemberTask _bpCalcForMember;
        private readonly ICalcSummaryLevelAmtsForEmployeeTask _calcSummaryDataForEmployee;
        private readonly IGetCostOfBenefitPerPayCheckPerMemberTypeRule _getCostOfBPPerPayCheckRule;
        private readonly IDiscountForNameStarsWithARRule _nameDiscountRule;
        private readonly IBpCalculationAttributes _calcAttributes;

        //The instantiation of tasks and rules sholud be delayed by injecting a service factory 
        //The factory would be specific to this workflow i.e. a workflow will have corresponding factory for tasks and rules, as applicable
        public CalculateBenefitsCostWF(IBpCalculationAttributes bpCalcAttr,
            ICalculateBPCostForAMemberTask bpCalcForFamilyTask, 
            IBpCalculationAttributes calcAttributes,
            IGetCostOfBenefitPerPayCheckPerMemberTypeRule getBaseAmtRule,
            IDiscountForNameStarsWithARRule nameDiscountRule,
            ICalcSummaryLevelAmtsForEmployeeTask calcSummaryDataForEmployee)
        {

            if (bpCalcAttr is null) throw new ArgumentNullException(nameof(bpCalcAttr));
            if (calcAttributes is null) throw new ArgumentNullException(nameof(calcAttributes));
            if (nameDiscountRule is null) throw new ArgumentNullException(nameof(nameDiscountRule));
            if (getBaseAmtRule is null) throw new ArgumentNullException(nameof(getBaseAmtRule));
            if (bpCalcAttr is null) throw new ArgumentNullException(nameof(bpCalcAttr));
            if (bpCalcForFamilyTask is null) throw new ArgumentNullException(nameof(bpCalcForFamilyTask));
            if (calcSummaryDataForEmployee is null) throw new ArgumentNullException(nameof(calcSummaryDataForEmployee));

            _calcAttributes = calcAttributes;
            _nameDiscountRule = nameDiscountRule;
            _getCostOfBPPerPayCheckRule = getBaseAmtRule;
            _bpCalcAttr = bpCalcAttr;
            _bpCalcForMember = bpCalcForFamilyTask;
            _calcSummaryDataForEmployee = calcSummaryDataForEmployee;
        }
        public CalculateBenefitsCostWFResponse Execute(CalculateBenefitsCostWFRequest srcData)
        {


            CalculateBenefitsCostWFResponse retVal = new CalculateBenefitsCostWFResponse();
            if (retVal.MemberCostDetails == null) retVal.MemberCostDetails = new List<List<Entities.MemberCost>>();


            foreach (var familyInst in srcData.EmployeeAndFamilyList)
            {

                List<MemberCost> calcForEmployeeAndFamily = new List<MemberCost>();

                foreach (var personInst in familyInst)
                {
                    decimal costOfBPPerPayCheck = _getCostOfBPPerPayCheckRule.Compute(personInst.MemberTypeCode, _calcAttributes.TotalNumberOfPayChecksInAYear);
                    
                    List<decimal> discountAmts = new List<decimal>();
                    //There can be other discount amt related rules. I would then use a rule engine and not inject 10 rules into this workflow
                    var discountForFirstName = _nameDiscountRule.Compute(costOfBPPerPayCheck, personInst.FullName);
                    discountAmts.Add(discountForFirstName);
                    
                   //This should be a rule - IIsMemberTheEmployeeRule
                    bool isEmployee = personInst.MemberTypeCode.Equals("E", StringComparison.InvariantCultureIgnoreCase);

                    var memberCostResponse = _bpCalcForMember.Execute(costOfBPPerPayCheck, discountAmts.Sum(),
                        _calcAttributes.TotalNumberOfPayChecksInAYear);

                    if (memberCostResponse is not null)
                    {
                        memberCostResponse.MemberIdentifier = personInst.MemberIdentifier;
                        memberCostResponse.IsEmployee = isEmployee;
                    }

                    calcForEmployeeAndFamily.Add(memberCostResponse);
                }

                calcForEmployeeAndFamily = _calcSummaryDataForEmployee.Compute(calcForEmployeeAndFamily, 
                    _calcAttributes.AmtPaidForEmployeePerPayCheck, _calcAttributes.TotalNumberOfPayChecksInAYear);

                retVal.MemberCostDetails.Add(calcForEmployeeAndFamily);
    
            }



            return retVal;
        }
    }
}

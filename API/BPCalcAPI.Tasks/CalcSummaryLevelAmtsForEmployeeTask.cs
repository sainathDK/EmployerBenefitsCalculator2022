using BPCalcAPI.Entities;
using BPCalcAPI.Task.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BPCalcAPI.Tasks
{
    public class CalcSummaryLevelAmtsForEmployeeTask : ICalcSummaryLevelAmtsForEmployeeTask
    {
        public List<MemberCost> Compute(List<MemberCost> employeeAndFamily,decimal amtPerPayCheck,int totalPayChecks)
        {

            if (employeeAndFamily is null) throw new ArgumentNullException(nameof(employeeAndFamily));

            var retVal = employeeAndFamily;

            var targetMember = employeeAndFamily.FirstOrDefault(x => x.IsEmployee = true);

            targetMember.TotalPayPerCheckBeforeDeductions = amtPerPayCheck;
            targetMember.TotalPayPerYearBeforeDeductions = amtPerPayCheck * totalPayChecks;

            decimal? allDeductionsPerPayCheck = employeeAndFamily?.Sum(y => y.CostToMemberPerPayCheck);

            targetMember.TotalPayPerPayCheckAfterDeductions = amtPerPayCheck - allDeductionsPerPayCheck.GetValueOrDefault();
            
            targetMember.TotalPayPerYearAfterDeductions = 
                targetMember.TotalPayPerYearBeforeDeductions - (allDeductionsPerPayCheck.GetValueOrDefault() * totalPayChecks);

            return retVal;
        }
    }
}

using BPCalcAPI.Entities;
using System.Collections.Generic;

namespace BPCalcAPI.Task.Interfaces
{
    public interface ICalcSummaryLevelAmtsForEmployeeTask
    {
        List<MemberCost> Compute(List<MemberCost> employeeAndFamily, decimal amtPerPayCheck, int totalPayChecks);
    }
}

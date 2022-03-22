using BPCalcAPI.Entities;
using System;
using System.Collections.Generic;

namespace BPCalcAPI.Task.Interfaces
{
    public interface ICalculateBPCostForAMemberTask
    {
        MemberCost Execute(decimal costOfBPPerPayCheck, decimal totalDiscount, int totalPayChecksInAYear);
    }
}

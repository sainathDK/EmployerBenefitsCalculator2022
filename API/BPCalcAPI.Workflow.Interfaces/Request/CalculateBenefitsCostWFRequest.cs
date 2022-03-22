using BPCalcAPI.Entities;
using System;
using System.Collections.Generic;

namespace BPCalcAPI.Workflow.Interfaces.Request
{
    public class CalculateBenefitsCostWFRequest
    {
        public CalculateBenefitsCostWFRequest()
        {
            EmployeeAndFamilyList = new List<List<MemberInfo>>();
        }
        public List<List<MemberInfo>> EmployeeAndFamilyList { get; set; }
        public DateTime EffectiveDate { get; set; }
    }
}

using BPCalcAPI.Workflow.Interfaces.Request;
using BPCalcAPI.Workflow.Interfaces.Response;
using System;

namespace BPCalcAPI.Workflow.Interfaces
{
    public interface ICalculateBenefitsCostWF
    {
        CalculateBenefitsCostWFResponse Execute(CalculateBenefitsCostWFRequest srcData);
    }
}

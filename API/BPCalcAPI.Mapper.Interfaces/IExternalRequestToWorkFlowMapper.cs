using BPCalcAPI.DTOs.Request;
using BPCalcAPI.Workflow.Interfaces.Request;


namespace BPCalcAPI.Mapper.Interfaces
{
    public interface IExternalRequestToWorkFlowMapper
    {
        CalculateBenefitsCostWFRequest MapFrom(CalcRequestDto srcData);
    }
}

using BPCalcAPI.DTOs;
using BPCalcAPI.DTOs.Request;
using BPCalcAPI.Mapper.Interfaces;
using BPCalcAPI.Workflow.Interfaces;
using BPCalcAPI.Workflow.Interfaces.Response;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BenefitsPkgCalculatorAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BenefitPkgCalculator : ControllerBase
    {
        private readonly ICalculateBenefitsCostWF _bpCostCalculatorWf;
        private readonly IExternalRequestToWorkFlowMapper _exterReqToWfMapper;
        private readonly IWorkFlowToExternReturnResponseMapper _wFtoExternResponseMapper;

        public BenefitPkgCalculator(ICalculateBenefitsCostWF bpCostCalculatorWf,
            IExternalRequestToWorkFlowMapper exterReqToWfMapper,
            IWorkFlowToExternReturnResponseMapper wFtoExternResponseMapper)
        {

            if (bpCostCalculatorWf is null) throw new ArgumentNullException(nameof(bpCostCalculatorWf));
            if (exterReqToWfMapper is null) throw new ArgumentNullException(nameof(exterReqToWfMapper));

            _bpCostCalculatorWf = bpCostCalculatorWf;
            _exterReqToWfMapper = exterReqToWfMapper;
            _wFtoExternResponseMapper = wFtoExternResponseMapper;
        }

        [EnableCors("CustomPolicyForRazorApp")]
        [HttpPost]
        public CalculatedBenefitsCostDTO Get(CalcRequestDto requestData)
        {
            CalculatedBenefitsCostDTO retVal = null;

            var wfREq = _exterReqToWfMapper.MapFrom(requestData);
            var wfResponse = _bpCostCalculatorWf.Execute(wfREq);
            retVal = _wFtoExternResponseMapper.MapFrom(wfResponse);

            return retVal;
        }
    }
}

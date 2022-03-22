using BPCalcAPI.Workflow.Interfaces.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BPCalcAPI.Mapper.Interfaces
{
    public interface IWorkFlowToExternReturnResponseMapper
    {
        CalculatedBenefitsCostDTO MapFrom(CalculateBenefitsCostWFResponse srcData);
    }
}

using System.Collections.Generic;
namespace BPCalcAPI.DTOs.Request
{

    public class CalcRequestDto
    {
        public string EffectiveDateToCalculateFor { get; set; }
        public List<List<MemberDto>> Employees { get; set; }
    }
}

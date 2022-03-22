using System.ComponentModel.DataAnnotations;

namespace BPCalcAPI.DTOs
{
    public class MemberDto
    {
        [Required]
        public string MemberIdentifier { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string MemberTypeCode { get; set; }
    }
}

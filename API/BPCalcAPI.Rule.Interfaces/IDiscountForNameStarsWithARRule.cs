
namespace BPCalcAPI.Rule.Interfaces
{
    public interface IDiscountForNameStarsWithARRule
    {
        decimal Compute(decimal costAmt, string completeName);
    }
}

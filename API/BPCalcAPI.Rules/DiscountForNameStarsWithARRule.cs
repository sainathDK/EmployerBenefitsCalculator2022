using BPCalcAPI.Rule.Interfaces;
using System;

namespace BPCalcAPI.Rules
{
    /// <summary>
    /// This returns the amt (the discount) to be subtracted from the primary amount
    /// </summary>
    public class DiscountForNameStarsWithARRule : IDiscountForNameStarsWithARRule
    {
        public decimal Compute(decimal costAmt, string completeName)
        {
            decimal retVal = 0;

            if (!String.IsNullOrEmpty(completeName) && completeName.IndexOf("A", 0,StringComparison.InvariantCultureIgnoreCase) == 0)
            {
                retVal = (costAmt * (10m / 100m));
            }


            return retVal;
        }
    }
}

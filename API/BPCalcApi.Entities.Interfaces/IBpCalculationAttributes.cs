using System;

namespace BPCalcApi.Entities.Interfaces
{
    public interface IBpCalculationAttributes
    {
        int TotalNumberOfPayChecksInAYear { set; get; }

        decimal AmtPaidForEmployeePerPayCheck { set; get; }
    }
}

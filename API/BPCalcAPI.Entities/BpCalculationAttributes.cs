using BPCalcApi.Entities.Interfaces;

namespace BPCalcAPI.Entities
{


    public class BpCalculationAttributes : IBpCalculationAttributes
    {
        public BpCalculationAttributes()
        {
            //This should be set by reading from a storage
            //defined for a specific company or division within a company etc
            TotalNumberOfPayChecksInAYear = 26;
            AmtPaidForEmployeePerPayCheck = 2000m;
        }

        private int _totalNumberOfPayChecksInAYear;
        private decimal _amtPaidForEmployeePerPayCheck;

        public int TotalNumberOfPayChecksInAYear { get => _totalNumberOfPayChecksInAYear; set => _totalNumberOfPayChecksInAYear = value; }
        public decimal AmtPaidForEmployeePerPayCheck { get => _amtPaidForEmployeePerPayCheck; set => _amtPaidForEmployeePerPayCheck = value; }
    }
}

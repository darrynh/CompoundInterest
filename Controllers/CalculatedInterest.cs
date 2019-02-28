namespace CompoundInterest.Controllers
{
    public class CalculatedInterest
    {
        public CalculatedInterest()
        {
            Warning = "";
        }
        public string Warning { get; internal set; }
        public int NumberOfTimesCompounded { get; internal set; }
        public double PincipalAmount { get; internal set; }
        public double Rate { get; internal set; }
        public int TimeInYear { get; internal set; }
        public double InterestRate { get; internal set; }
        public double TotalAmount { get; internal set; }
        public double CalculatedTotal { get; internal set; }
        public double OriginalTotalInPence { get; internal set; }
    }
}
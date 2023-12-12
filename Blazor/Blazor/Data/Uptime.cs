namespace Blazor.Data
{
    public class Uptime
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public int PositiveChecks { get; set; }
        public int NegativeChecks { get; set; }
    }
}
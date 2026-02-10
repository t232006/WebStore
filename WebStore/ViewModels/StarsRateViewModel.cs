namespace WebStore.ViewModels
{
    public class StarsRateViewModel
    {
        public decimal AverageRate { get; set; }
        public byte? Rate { get; set; }
        public int VotesAmount { get; set; }
        public string UserID { get; set; }
        public int BlogID { get; set; }
    }
}

namespace WebApplication1.Models
{
    public class Equipment
    {
        public Guid Id { get; set; }
        public required string NameEq { get; set; }
        public required string Workshop { get; set; }
        public required string AcNumber { get; set; }
    }
}

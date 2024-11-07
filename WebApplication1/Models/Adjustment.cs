namespace WebApplication1.Models
{
    public class Adjustment
    {
        public Guid Id { get; set; }
        public Guid EquipmentId { get; set; }
        public string? NameEq { get; set; }
        public required string? Workshop { get; set; }
        public string? AcNumber { get; set; }
        public required string Status { get; set; }
        public required string TechnicianName { get; set; }
        public required string Comments { get; set; }

        public virtual Equipment? Equipment { get; set; }
        public virtual Worker? Worker { get; set; } // Навигационное свойство
    }
}


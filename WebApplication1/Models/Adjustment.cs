﻿namespace WebApplication1.Models
{
    public class Adjustment
    {
        public Guid Id { get; set; }
        public Guid EquipmentId { get; set; } // Связь с Equipment
        public required string NameEq { get; set; }
        public required string Workshop { get; set; }
        public required string AcNumber { get; set; }
        public required string Status { get; set; } // Статус выполнения работы
        public required string TechnicianName { get; set; } // ФИО наладчика
        public required string Comments { get; set; } // Комментарий наладчика }
    }
}
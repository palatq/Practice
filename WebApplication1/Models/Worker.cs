namespace WebApplication1.Models
{
    public class Worker
    {
        public Guid WorkerId { get; set; }
        public required string WorkerName { get; set; }
        public required string Surname { get; set; }
        public required string Patronymic { get; set; }
    }
}

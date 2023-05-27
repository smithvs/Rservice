namespace RService.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Record> Records { get; set; } = new List<Record>();
    }
}

namespace RService.Models
{
    public class Specialist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Information { get; set; }
        
        public int OfficeId { get; set; }
    }
}

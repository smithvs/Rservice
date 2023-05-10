namespace RService.Models
{
    public class Office
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public string Address { get; set; }
        public int OfficeTypeId { get; set; } 
        public bool IsActive { get; set; }
    }
}

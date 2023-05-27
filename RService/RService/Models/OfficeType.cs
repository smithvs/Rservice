namespace RService.Models
{
    public class OfficeType
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Office> Offices { get; set; }
    }
}

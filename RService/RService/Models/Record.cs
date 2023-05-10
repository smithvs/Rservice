namespace RService.Models
{
    public class Record
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int OfficeId { get; set; }
        public int SpecialistId { get; set; }
        public int? ClientId { get; set; }
        public DateTime Date { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        
        public string DescriptionClient { get; set; }
        public string DescriptionOffice { get; set; }

    }
}

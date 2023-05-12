using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface IOfficeRepository
    {
        public IEnumerable<Office> GetOffices();
        public Office GetOffice(int id);
        public Office AddOffice(Office office);
        public void DeleteOffice(int id);
        public Office PutOffice(Office office);
    }
}

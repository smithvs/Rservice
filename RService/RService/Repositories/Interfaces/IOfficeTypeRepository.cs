using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface IOfficeTypeRepository
    {
        public IEnumerable<OfficeType> GetOfficeTypes();
        public OfficeType GetOfficeType(int id);
        public OfficeType AddOfficeType(OfficeType officetype);
        public void DeleteOfficeType(int id);
        public OfficeType PutOfficeType(OfficeType officetype);
    }
}

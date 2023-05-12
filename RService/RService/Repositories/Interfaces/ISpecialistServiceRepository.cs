using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface ISpecialistServiceRepository
    {
        public IEnumerable<SpecialistService> GetSpecialistServices();
        public SpecialistService GetSpecialistService(int id);
        public SpecialistService AddSpecialistService(SpecialistService specialistservice);
        public void DeleteSpecialistService(int id);
        public SpecialistService PutSpecialistService(SpecialistService specialistservice);
    }
}

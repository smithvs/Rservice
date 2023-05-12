using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface ISpecialistRepository
    {
        public IEnumerable<Specialist> GetSpecialists();
        public Specialist GetSpecialist(int id);
        public Specialist AddSpecialist(Specialist specialist);
        public void DeleteSpecialist(int id);
        public Specialist PutSpecialist(Specialist specialist);
    }
}
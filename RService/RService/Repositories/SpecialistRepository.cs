using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class SpecialistRepository : ISpecialistRepository
    {
        private readonly ILogger<SpecialistController> _logger;
        private readonly List<Specialist> _specialist;

        public SpecialistRepository(ILogger<SpecialistController> logger)
        {
            _logger = logger;

            //MOOK
            _specialist = new List<Specialist>();
            _specialist.Add(new Specialist { Id = 1, Name = "Manikyr", Telephone = "79665555376", Information = "hgkg", OfficeId = 1 });
            _specialist.Add(new Specialist { Id = 2, Name = "Lekar", Telephone = "7955352345", Information = "aafdhsa", OfficeId = 2 });
            _specialist.Add(new Specialist { Id = 3, Name = "Superman" , Telephone = "79538765421", Information = "asdg", OfficeId = 3 });
        }

        public IEnumerable<Specialist> GetSpecialists()
        {
            return _specialist;
        }

        public Specialist GetSpecialist(int id)
        {
            return _specialist.FirstOrDefault(c => c.Id == id);
        }

        public Specialist AddSpecialist(Specialist specialist)
        {
            int id = _specialist.Max(x => x.Id) + 1;
            specialist.Id = id;
            _specialist.Add(specialist);
            return specialist;
        }

        public void DeleteSpecialist(int id)
        {
            Specialist specialist = GetSpecialist(id);
            if (specialist == null)
            {
                throw new Exception($"Специалист с Id={id} не найден");
            }
            _specialist.Remove(specialist);
        }

        public Specialist PutSpecialist(Specialist specialist)
        {
            Specialist specialistFromDB = GetSpecialist(specialist.Id);
            if (specialistFromDB == null)
            {
                throw new Exception($"Специалист с Id={specialist.Id} не найден");
            }
            specialistFromDB.Name = specialist.Name;
            specialistFromDB.Telephone = specialist.Telephone;
            specialistFromDB.Information = specialist.Information;
            specialistFromDB.OfficeId = specialist.OfficeId;
            return specialistFromDB;
        }
    }
}

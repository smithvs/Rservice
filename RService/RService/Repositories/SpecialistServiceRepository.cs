using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class SpecialistServiceRepository : ISpecialistServiceRepository
    {
        private readonly ILogger<SpecialistServiceRepository> _logger;
        private readonly List<SpecialistService> _specialistservices;

        public SpecialistServiceRepository(ILogger<SpecialistServiceRepository> logger)
        {
            _logger = logger;

            //MOOK
            _specialistservices = new List<SpecialistService>();
            _specialistservices.Add(new SpecialistService { Id = 1, SpecialistId = 1, ServiceId = 1 });
            _specialistservices.Add(new SpecialistService { Id = 2, SpecialistId = 2, ServiceId = 2 });
            _specialistservices.Add(new SpecialistService { Id = 3, SpecialistId = 3, ServiceId = 3 });
        }

        public IEnumerable<SpecialistService> GetSpecialistServices()
        {
            return _specialistservices;
        }

        public SpecialistService GetSpecialistService(int id)
        {
            return _specialistservices.FirstOrDefault(c => c.Id == id);
        }

        public SpecialistService AddSpecialistService(SpecialistService specialistservice)
        {
            int id = _specialistservices.Max(x => x.Id) + 1;
            specialistservice.Id = id;
            _specialistservices.Add(specialistservice);
            return specialistservice;
        }

        public void DeleteSpecialistService(int id)
        {
            SpecialistService specialistservice = GetSpecialistService(id);
            if (specialistservice == null)
            {
                throw new Exception($"СпециалистСервис с Id={id} не найден");
            }
            _specialistservices.Remove(specialistservice);
        }

        public SpecialistService PutSpecialistService(SpecialistService specialistservice)
        {
            SpecialistService specialistserviceFromDB = GetSpecialistService(specialistservice.Id);
            if (specialistserviceFromDB == null)
            {
                throw new Exception($"СпециалистСервис с Id={specialistservice.Id} не найден");
            }
            specialistserviceFromDB.SpecialistId = specialistservice.SpecialistId;
            specialistserviceFromDB.ServiceId = specialistservice.ServiceId;
            return specialistserviceFromDB;
        }
    }
}

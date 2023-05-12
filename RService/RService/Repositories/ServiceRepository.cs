using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ILogger<ServiceController> _logger;
        private readonly List<Service> _services;

        public ServiceRepository(ILogger<ServiceController> logger)
        {
            _logger = logger;

            //MOOK
            _services = new List<Service>(); 
            _services.Add(new Service { Id = 1, Name = "barbershop" });
            _services.Add(new Service { Id = 2, Name = "hospital" });
            _services.Add(new Service { Id = 3, Name = "beauty salon" });
        }

        public IEnumerable<Service> GetServices()
        {
            return _services;
        }

        public Service GetService(int id)
        {
            return _services.FirstOrDefault(c => c.Id == id);
        }

        public Service AddService(Service service)
        {
            int id = _services.Max(x => x.Id) + 1;
            service.Id = id;
            _services.Add(service);
            return service;
        }

        public void DeleteService(int id)
        {
            Service service = GetService(id);
            if (service == null)
            {
                throw new Exception($"Сервис с Id={id} не найден");
            }
            _services.Remove(service);
        }

        public Service PutService(Service service)
        {
            Service serviceFromDB = GetService(service.Id);
            if (serviceFromDB == null)
            {
                throw new Exception($"Сервис с Id={service.Id} не найден");
            }
            serviceFromDB.Name = service.Name;
            return serviceFromDB;
        }
    }
}

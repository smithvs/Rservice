using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class ServiceOfficeRepository : IServiceOfficeRepository
    {
        private readonly ILogger<ServiceOfficeController> _logger;
        private readonly List<ServiceOffice> _serviceoffices;

        public ServiceOfficeRepository(ILogger<ServiceOfficeController> logger)
        {
            _logger = logger;

            //MOOK
            _serviceoffices = new List<ServiceOffice>();
            _serviceoffices.Add(new ServiceOffice { Id = 1, ServiceId = 1, OfficeId = 1 });
            _serviceoffices.Add(new ServiceOffice { Id = 2, ServiceId = 2, OfficeId = 2 });
            _serviceoffices.Add(new ServiceOffice { Id = 3, ServiceId = 3 , OfficeId = 3 });
        }

        public IEnumerable<ServiceOffice> GetServiceOffices()
        {
            return _serviceoffices;
        }

        public ServiceOffice GetServiceOffice(int id)
        {
            return _serviceoffices.FirstOrDefault(c => c.Id == id);
        }

        public ServiceOffice AddServiceOffice(ServiceOffice serviceoffice)
        {
            int id = _serviceoffices.Max(x => x.Id) + 1;
            serviceoffice.Id = id;
            _serviceoffices.Add(serviceoffice);
            return serviceoffice;
        }

        public void DeleteServiceOffice(int id)
        {
            ServiceOffice serviceoffice = GetServiceOffice(id);
            if (serviceoffice == null)
            {
                throw new Exception($"СервисОффис с Id={id} не найден");
            }
            _serviceoffices.Remove(serviceoffice);
        }

        public ServiceOffice PutServiceOffice(ServiceOffice serviceoffice)
        {
            ServiceOffice serviceofficeFromDB = GetServiceOffice(serviceoffice.Id);
            if (serviceofficeFromDB == null)
            {
                throw new Exception($"СервисОффис с Id={serviceoffice.Id} не найден");
            }
            serviceofficeFromDB.OfficeId = serviceoffice.OfficeId;
            serviceofficeFromDB.ServiceId = serviceoffice.ServiceId;
            return serviceofficeFromDB;
        }
    }
}

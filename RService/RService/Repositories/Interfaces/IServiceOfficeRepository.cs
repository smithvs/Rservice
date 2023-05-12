using RService.Models;

namespace RService.Repositories.Interfaces
{ 
    public interface IServiceOfficeRepository
    {
        public IEnumerable<ServiceOffice> GetServiceOffices();
        public ServiceOffice GetServiceOffice(int id);
        public ServiceOffice AddServiceOffice(ServiceOffice serviceoffice);
        public void DeleteServiceOffice(int id);
        public ServiceOffice PutServiceOffice(ServiceOffice serviceoffice);
    }
}

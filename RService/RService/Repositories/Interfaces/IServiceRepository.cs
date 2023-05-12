using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        public IEnumerable<Service> GetServices();
        public Service GetService(int id);
        public Service AddService(Service service);
        public void DeleteService(int id);
        public Service PutService(Service service);
    }
}

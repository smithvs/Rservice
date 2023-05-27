using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetAsync();
        public Task<Client> GetAsync(int id);
        public Task<Client> GetAsync(string guid);
        public Task<Client> PostAsync(Client client);
        public Task DeleteAsync(int id);
        public Client PutClient(Client client);
        public Task<int?> FindAsync(string guid); 
    }
}

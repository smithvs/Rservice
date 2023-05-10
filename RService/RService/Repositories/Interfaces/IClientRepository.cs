using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public IEnumerable<Client> GetClients();
        public Client GetClient(int id);
        public Client AddClient(Client client);
        public void DeleteClient(int id);
        public Client PutClient(Client client);
    }
}

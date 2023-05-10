using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ILogger<ClientController> _logger;
        private readonly List<Client> _clients;

        public ClientRepository(ILogger<ClientController> logger) {
            _logger = logger;

            //MOOK
            _clients = new List<Client>();
            _clients.Add(new Client { Id = 1, Name = "Vasya", Telephone = "79231234561", GUID = "123" });
            _clients.Add(new Client { Id = 2, Name = "Petya", Telephone = "79138746345", GUID = "234" });
            _clients.Add(new Client { Id = 3, Name = "Natasha", Telephone = "790857813424", GUID = "345" });
        }

        public IEnumerable<Client> GetClients()
        {
            return _clients;
        }

        public Client GetClient(int id)
        {
            return _clients.FirstOrDefault(c => c.Id == id);
        }

        public Client AddClient(Client client)
        {
            int id = _clients.Max(x=>x.Id)+1;
            client.Id = id;
            _clients.Add(client);
            return client;
        }

        public void DeleteClient(int id)
        {
            Client client = GetClient(id);
            if (client == null) {
                throw new Exception($"Клиент с Id={id} не найден");
            }
            _clients.Remove(client);
        }

        public Client PutClient(Client client)
        {
            Client clientFromDB = GetClient(client.Id);
            if (clientFromDB == null)
            {
                throw new Exception($"Клиент с Id={client.Id} не найден");
            }
            clientFromDB.Name= client.Name;
            clientFromDB.Telephone= client.Telephone;
            clientFromDB.GUID= client.GUID;
            return clientFromDB;
        }
    }
}

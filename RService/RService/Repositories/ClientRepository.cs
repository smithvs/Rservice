using Microsoft.EntityFrameworkCore;
using RService.Data;
using RService.Models;
using RService.Repositories.Interfaces;
using System;

namespace RService.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly RServiceContext _context;

        public ClientRepository(RServiceContext context) { 
            _context = context;
        }

        public async Task<Client> PostAsync(Client client)
        {
            if (_context.Client == null)
            {
                throw new ArgumentNullException("SQL:Entity set 'RServiceContext.Client'  is null.");
            }
            _context.Client.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task DeleteAsync(int id)
        {
            if (_context.Client == null)
            {
                throw new Exception("Collection 'Client' not found");
            }
            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                throw new ArgumentException($"Client with 'id' = {id} in collection 'Client' not found");
            }

            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
        }

        public Client GetClient(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Client>> GetAsync()
        {
            if (_context.Client == null)
            {
                throw new Exception("Collection 'Client' not found");
            }
            return  await _context.Client.ToListAsync();
        }

        public Client PutClient(Client client)
        {
            throw new NotImplementedException();
        }

        public async Task<int?> FindAsync(string guid)
        {
            if (_context.Client == null)
            {
                throw new Exception("Collection 'Client' not found");
            }
            var client = await _context.Client.FirstOrDefaultAsync(x => x.GUID == guid);
            return client?.Id;
        }

        public async Task<Client> GetAsync(int id)
        {
            if (_context.Client == null)
            {
                throw new Exception("Collection 'Client' not found");
            }
            return await _context.Client.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Client> GetAsync(string guid)
        {
            if (_context.Client == null)
            {
                throw new Exception("Collection 'Client' not found");
            }
            return await _context.Client.FirstOrDefaultAsync(x => x.GUID == guid);
        }
    }
}

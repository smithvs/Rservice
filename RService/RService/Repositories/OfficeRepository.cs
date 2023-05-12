using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class OfficeRepository : IOfficeRepository
    {
        private readonly ILogger<OfficeRepository> _logger;
        private readonly List<Office> _offices;

        public OfficeRepository(ILogger<OfficeRepository> logger)
        {
            _logger = logger;

            //MOOK
            _offices = new List<Office>();
            _offices.Add(new Office { Id = 1, Name = "Pizza", Telephone = "79665553376", Address = "Velikyi Novgorod", OfficeTypeId = 1 });
            _offices.Add(new Office { Id = 2, Name = "Appel ", Telephone = "79437653342", Address = "Saint Petersburg", OfficeTypeId = 2 });
            _offices.Add(new Office { Id = 3, Name = "Pear", Telephone = "79864563254", Address = "Pskov", OfficeTypeId = 3 });
        }

        public IEnumerable<Office> GetOffices()
        {
            return _offices;
        }

        public Office GetOffice(int id)
        {
            return _offices.FirstOrDefault(c => c.Id == id);
        }

        public Office AddOffice(Office office)
        {
            int id = _offices.Max(x => x.Id) + 1;
            office.Id = id;
            _offices.Add(office);
            return office;
        }

        public void DeleteOffice(int id)
        {
            Office office = GetOffice(id);
            if (office == null)
            {
                throw new Exception($"Оффис с Id={id} не найден");
            }
            _offices.Remove(office);
        }

        public Office PutOffice(Office office)
        {
            Office officeFromDB = GetOffice(office.Id);
            if (officeFromDB == null)
            {
                throw new Exception($"Оффис с Id={office.Id} не найден");
            }
            officeFromDB.Name = office.Name;
            officeFromDB.Telephone = office.Telephone;
            officeFromDB.Address = office.Address;
            officeFromDB.OfficeTypeId = office.OfficeTypeId;
            officeFromDB.IsActive = office.IsActive;

            return officeFromDB;
        }
    }
}

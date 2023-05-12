using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class OfficeTypeRepository : IOfficeTypeRepository
    {
        private readonly ILogger<OfficeTypeRepository> _logger;
        private readonly List<OfficeType> _officetypes;

        public OfficeTypeRepository(ILogger<OfficeTypeRepository> logger)
        {
            _logger = logger;

            //MOOK
            _officetypes = new List<OfficeType>();
            _officetypes.Add(new OfficeType { Id = 1, Name = "Pizza1" });
            _officetypes.Add(new OfficeType { Id = 2, Name = "Appel2 "});
            _officetypes.Add(new OfficeType { Id = 3, Name = "Pear3"});
        }

        public IEnumerable<OfficeType> GetOfficeTypes()
        {
            return _officetypes;
        }

        public OfficeType GetOfficeType(int id)
        {
            return _officetypes.FirstOrDefault(c => c.Id == id);
        }

        public OfficeType AddOfficeType(OfficeType officetype)
        {
            int id = _officetypes.Max(x => x.Id) + 1;
            officetype.Id = id;
            _officetypes.Add(officetype);
            return officetype;
        }

        public void DeleteOfficeType(int id)
        {
            OfficeType officetype = GetOfficeType(id);
            if (officetype == null)
            {
                throw new Exception($"ОффисType с Id={id} не найден");
            }
            _officetypes.Remove(officetype);
        }

        public OfficeType PutOfficeType(OfficeType officetype)
        {
            OfficeType officetypeFromDB = GetOfficeType(officetype.Id);
            if (officetypeFromDB == null)
            {
                throw new Exception($"ОффисType с Id={officetype.Id} не найден");
            }
            officetypeFromDB.Name = officetype.Name;

            return officetypeFromDB;
        }
    }
}

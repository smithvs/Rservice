using RService.Models;

namespace RService.Repositories.Interfaces
{
    public interface IRecordRepository
    {
        public IEnumerable<Record> GetRecords();
        public Record GetRecord(int id);
        public Record AddRecord(Record record);
        public void DeleteRecord(int id);
        public Record PutRecord(Record record);
    }
}

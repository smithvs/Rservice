using RService.Controllers;
using RService.Models;
using RService.Repositories.Interfaces;

namespace RService.Repositories
{
    public class RecordRepository : IRecordRepository
    {
        private readonly ILogger<RecordController> _logger;
        private readonly List<Record> _records;

        public RecordRepository(ILogger<RecordController> logger)
        {
            _logger = logger;

            //MOOK
            _records = new List<Record>();
            _records.Add(new Record { Id = 1, ServiceId = 1, OfficeId = 1 , SpecialistId = 1 , ClientId = 1, Date = new DateTime(2023, 04, 29, 13, 30, 0) });
            _records.Add(new Record { Id = 2, ServiceId = 2, OfficeId = 2, SpecialistId = 2, ClientId = 2, Date = new DateTime(2023, 1, 1), TimeStart = new DateTime(10, 0, 0), TimeEnd = new DateTime(2022, 1, 1, 11, 0, 0) });
            _records.Add(new Record { Id = 3, ServiceId = 3, OfficeId = 3, SpecialistId = 3, ClientId = 3, Date = new DateTime(2023, 1, 1), TimeStart = new DateTime(20, 0, 0), TimeEnd = new DateTime(2021, 1, 1, 11, 0, 0) });
        }

        public IEnumerable<Record> GetRecords()
        {
            return _records;
        }

        public Record GetRecord(int id)
        {
            return _records.FirstOrDefault(c => c.Id == id);
        }

        public Record AddRecord(Record record)
        {
            int id = _records.Max(x => x.Id) + 1;
            record.Id = id;
            _records.Add(record);
            return record;
        }

        public void DeleteRecord(int id)
        {
            Record record = GetRecord(id);
            if (record == null)
            {
                throw new Exception($"Запись с Id={id} не найден");
            }
            _records.Remove(record);
        }

        public Record PutRecord(Record record)
        {
            Record recordFromDB = GetRecord(record.Id);
            if (recordFromDB == null)
            {
                throw new Exception($"Запись с Id={record.Id} не найден");
            }
            recordFromDB.OfficeId = record.OfficeId;
            recordFromDB.SpecialistId = record.SpecialistId;
            recordFromDB.ClientId = record.ClientId;
            recordFromDB.TimeStart = record.TimeStart;
            recordFromDB.TimeEnd = record.TimeEnd;
            recordFromDB.Date = record.Date;
            return recordFromDB;
        }
    }
}

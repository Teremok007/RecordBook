using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordBook.Models;

namespace RecordBook.DAL
{
    public interface IRecordRepository
    {
        List<Record> GetRecords();

        void CreateRecord(Record rec);

        void Update(Record rec);

        Record GetRecord(int id);
        void Delete(int id);
        void Delete(Record rec);
    }
}

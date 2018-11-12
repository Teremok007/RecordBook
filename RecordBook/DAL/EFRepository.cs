using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordBook.Models;

namespace RecordBook.DAL
{
    public class EFRepository : IRecordRepository
    {
        private GuestBookContext _context = new GuestBookContext();
        public void CreateRecord(Record rec)
        {
            Category category = new Category()
            {
                Name = "Samsung"
            };

            Product product = new Product()
            {
                Name = "A6+",
                Category = category
            };

            _context.Products.Add(product);
            _context.Records.Add(rec);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Record record = GetRecord(id);
            _context.Records.Remove(record);
            _context.SaveChanges();
        }

        public void Delete(Record rec)
        {
            throw new NotImplementedException();
        }

        public Record GetRecord(int id)
        {
            return _context.Records.First(r => r.Id == id);
        }

        public List<Record> GetRecords()
        {
            return _context.Records.ToList();
        }

        public void Update(Record rec)
        {
            Record dbRecord = GetRecord(rec.Id);
            dbRecord.Message = rec.Message;
            _context.SaveChanges();
        }
    }
}
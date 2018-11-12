using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RecordBook.Models;
using System.Data;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.Web.Hosting;

namespace RecordBook.DAL
{
    public class SqlCeRepository : IRecordRepository
    {        
        private  string _connString = string.Format("Data Source = {0}", HostingEnvironment.MapPath(@"~/App_Data/GB.sdf"));
        
        public void CreateRecord(Record rec)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connString))
            using (SqlCeCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    INSERT INTO Records([Message], [Author], [Date])
                    VALUES(@message, @author, @date)";

                command.Parameters.AddWithValue("message", rec.Message);
                command.Parameters.AddWithValue("author", rec.Author);
                command.Parameters.AddWithValue("date", rec.Date);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(Record rec)
        {
            this.Delete(rec.Id);
        }

        public Record GetRecord(int id)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connString))
            using (SqlCeCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    SELECT [Id], [Message], [Author], [Date]
                    FROM records
                    WHERE Id = @id";
                conn.Open();

                command.Parameters.AddWithValue("Id", id);
                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ParseRecord(reader);
                    }
                }
            }
            return null;
        }

        public List<Record> GetRecords()
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connString))
            using (SqlCeCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    SELECT [Id], [Message], [Author], [Date]
                    FROM Records";
                conn.Open();
                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    return ParserReader(reader);
                }
            }
        }

        private static List<Record> ParserReader(SqlCeDataReader reader)
        {
            List<Record> result = new List<Record>();
            while (reader.Read())
            {
                Record record = ParseRecord(reader);
                result.Add(record);
            }
            return result;
        }

        private static Record ParseRecord(SqlCeDataReader reader)
        {
            Record record = new Record()
            {
                Id = (int)reader["Id"],
                Message = (string)reader["Message"],
                Author = (string)reader["Author"],
                Date = (DateTime)reader["Date"]
            };
            return record;
        }

        public void Update(Record rec)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connString))
            using (SqlCeCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    UPDATE [Records]
                    SET	[Message] = @message
                    WHERE Id = @id";

                command.Parameters.AddWithValue("message", rec.Message);
                command.Parameters.AddWithValue("id", rec.Id);

                conn.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlCeConnection conn = new SqlCeConnection(_connString))
            using (SqlCeCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    DELETE FROM [Records]
                    WHERE Id = @id";
                command.Parameters.AddWithValue("id", id);
                conn.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
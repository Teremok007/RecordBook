using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using RecordBook.Models;

namespace RecordBook.DAL
{
    public class SqlRepository : IRecordRepository
    {
        private const string _connString = 
            "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=GuestBook;Integrated Security=True";

        public void CreateRecord(Record rec)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand command = conn.CreateCommand())
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

        public List<Record> GetRecords()
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            using(SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    SELECT [Id], [Message], [Author], [Date]
                    FROM records";
                conn.Open();
                using(SqlDataReader reader = command.ExecuteReader())
                {
                    return ParserReader(reader);
                }
            }
        }

        public Record GetRecord(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand command = conn.CreateCommand())
            {
                command.CommandText = @"
                    SELECT [Id], [Message], [Author], [Date]
                    FROM records
                    WHERE Id = @id";
                conn.Open();

                command.Parameters.AddWithValue("Id", id);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return ParseRecord(reader);
                    }
                }
            }
            return null;
        }

        private static List<Record> ParserReader(SqlDataReader reader)
        {
            List<Record> result = new List<Record>();
            while (reader.Read())
            {
                Record record = ParseRecord(reader);
                result.Add(record);
            }
            return result;
        }

        public void Update(Record rec)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand command = conn.CreateCommand())
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

        private static Record ParseRecord(SqlDataReader reader)
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

        public void Delete(Record rec)
        {
            Delete(rec.Id);
        }
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connString))
            using (SqlCommand command = conn.CreateCommand())
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generikus_Adatbaziskezelo_Osztaly
{
    public class DatabaseManager<T> where T : class
    {
        private List<T> records = new List<T>();

        protected int GetRecordId(T record)
        {
            return (int)record.GetType().GetProperty("Id").GetValue(record);
        }

        protected string GetRecordDetails(T record)
        {
            return record.ToString();
        }

        public void AddRecord(T record)
        {
            records.Add(record);
            Console.WriteLine($"Record added: {GetRecordDetails(record)}");
        }

        public void RemoveRecord(int id)
        {
            T record = records.FirstOrDefault(r => GetRecordId(r) == id);
            if (record != null)
            {
                records.Remove(record);
                Console.WriteLine($"Record with Id {id} removed.");
            }
            else
            {
                Console.WriteLine($"Record with Id {id} not found.");
            }
        }

        public T GetRecord(int id)
        {
            T record = records.FirstOrDefault(r => GetRecordId(r) == id);
            if (record != null)
            {
                Console.WriteLine($"Record found: {GetRecordDetails(record)}");
            }
            else
            {
                Console.WriteLine($"Record with Id {id} not found.");
            }
            return record;
        }

        public void PrintDatabase()
        {
            Console.WriteLine("Database contents:");
            foreach (var record in records)
            {
                Console.WriteLine(GetRecordDetails(record));
            }
        }
    }
}

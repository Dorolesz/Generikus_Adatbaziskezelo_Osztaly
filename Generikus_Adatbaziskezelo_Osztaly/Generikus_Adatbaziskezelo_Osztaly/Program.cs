using Generikus_Adatbaziskezelo_Osztaly;

class Program
{
    static void Main(string[] args)
    {
        var studentDb = new DatabaseManager<Student>();
        studentDb.AddRecord(new Student { Id = 1, Name = "Alice", Age = 20 });
        studentDb.AddRecord(new Student { Id = 2, Name = "Bob", Age = 22 });
        studentDb.PrintDatabase();
        studentDb.GetRecord(1);
        studentDb.RemoveRecord(1);
        studentDb.PrintDatabase();

        var bookDb = new DatabaseManager<Book>();
        bookDb.AddRecord(new Book { Id = 101, Title = "C# Programming", Author = "John Doe" });
        bookDb.PrintDatabase();

        var employeeDb = new DatabaseManager<Employee>();
        employeeDb.AddRecord(new Employee { Id = 201, Name = "Jane Smith", Position = "Manager" });
        employeeDb.PrintDatabase();
    }
}

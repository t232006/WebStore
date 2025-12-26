using WebStore.Models;

namespace WebStore.Data
{
    public static class TestData
    {
        public static readonly List<Employee> _employees = new List<Employee>
        {
            new() { Id = 1, Name = "Alice", Position = "Developer" , DateOfBirth=new DateTime(2002,12,12) },
            new() { Id = 2, Name = "Bob", Position = "Designer" , DateOfBirth=new DateTime(2001,11,11) },
            new() { Id = 3, Name = "Charlie", Position = "Manager", DateOfBirth=new DateTime(2000,10,10) }
        };
    }
}

using System.Diagnostics.CodeAnalysis;
using WebStore.Models;
using WebStore.Servises.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Mapping
{
    public static class EmplMapper
    {
        [return: NotNullIfNotNull("employee")]
        public static EmployeeViewModel? ToView(this Employee? e) => e is null ?
            null :
            new EmployeeViewModel
            {
                ID = e.ID,
                Name = e.Name,
                Position = e.Position,
                DateOfBirth = e.DateOfBirth,
            };
        [return: NotNullIfNotNull("employee")]
        public static Employee? FromView(this EmployeeViewModel? e) => e is null ?
            null :
            new Employee
            {
                ID = e.ID,
                Name = e.Name,
                Position = e.Position,
                DateOfBirth = e.DateOfBirth,
            };
        public static IEnumerable<EmployeeViewModel?> ToView(this IEnumerable<Employee?> empl) => 
            empl.Select(e => e.ToView());
        public static IEnumerable<Employee?> FromView(this IEnumerable<EmployeeViewModel?> empl) =>
            empl.Select(e => e.FromView());
    }
}

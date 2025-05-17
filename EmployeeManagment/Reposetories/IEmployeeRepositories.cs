using EmployeeManagment.Models;

namespace EmployeeManagment.Reposetories
{
    public interface IEmplyeeRepositories
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);

        Task AddEmployeAsync(Employee employee);
        Task UpdateEmployeeAsync(Employee employee);
        Task DeleteEmployeeAsync(int id);
    }
}

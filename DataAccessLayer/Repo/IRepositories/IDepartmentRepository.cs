using System.Collections.Generic;

namespace DataAccessLayer.Repo.IRepositories
{
    public interface IDepartmentRepository
    {
        int GetDepartmentIdByName(string departmentName);
        List<string> GetManagersByDepartmentId(int departmentId);
        }
}

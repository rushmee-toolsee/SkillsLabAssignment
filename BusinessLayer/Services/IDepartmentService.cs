using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public interface IDepartmentService
    {
        List<string> RetrieveManagersUsingDepartment(int id);
        int RetrieveDepartmentIdUsingName(string departmentName);
    }
}

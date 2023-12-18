using DataAccessLayer.Repo.IRepositories;
using System.Collections.Generic;

namespace BusinessLayer.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
       public  int RetrieveDepartmentIdUsingName(string departmentName)
        {
            return _departmentRepository.GetDepartmentIdByName( departmentName) ;
        }
        public List<string>  RetrieveManagersUsingDepartment(int id)
        {
            return _departmentRepository.GetManagersByDepartmentId(id);
        }
    }
}

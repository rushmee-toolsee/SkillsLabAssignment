using BusinessLayer.Services;
using BusinessLayer.Validators;
using DataAccessLayer.Common;
using DataAccessLayer.Repo;
using DataAccessLayer.Repo.ActualRepositories;
using DataAccessLayer.Repo.IRepositories;
using System.ComponentModel;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;


namespace SkillsLabAssignment
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            container.RegisterType<IDAL, DAL>();
            container.RegisterType<IAccountRepository, AccountRepository>();
            container.RegisterType<IAccountService, AccountService>();
            container.RegisterType<IDepartmentRepository, DepartmentRepository>();
            container.RegisterType<ITrainingRepository, TrainingRepository>();
            container.RegisterType<ILoginService, LoginService>();
            container.RegisterType<IRegisterService, RegisterService>();
            container.RegisterType<IDepartmentService, DepartmentService>();
            container.RegisterType<ITrainingApplicationService, TrainingApplicationService>();
            container.RegisterType<ITrainingApplicationRepository, TrainingApplicationRepository>();
            container.RegisterType<ITrainingService, TrainingService>();
            container.RegisterType<ILogger, Logger>();
            container.RegisterType<IRegistrationValidator, RegistrationValidator>();
            container.RegisterType<ILoginValidator, LoginValidator>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}
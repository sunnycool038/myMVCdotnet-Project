using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using SportsStore.Domain.Entities;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using SportsStore.WebUI.Infrastructure.Abstract;
using SportsStore.WebUI.Infrastructure.Concrete;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver:IDependencyResolver
    {
        private IKernel Kernel;
        public NinjectDependencyResolver(IKernel KernelParam)
        {
            Kernel = KernelParam;
            AddBidings();
        }
        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }
        public void AddBidings()
        {
            Kernel.Bind<IProductRepository>().To<EFProductRepository>();
            EmailSetting emailSetting = new EmailSetting()
            {
                WriteAsFile = bool.Parse(ConfigurationManager
                .AppSettings["Email.WriteAsFile"] ?? "false")
            };
            Kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("setting", emailSetting);
            Kernel.Bind<IAuthProvider>().To<FormsAuthProvider>();
            }
        }
    }

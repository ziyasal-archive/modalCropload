using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ModalCropload.Controllers;

namespace ModalCropload
{
    public class IoCConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(NewscastController).Assembly);



            IContainer container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
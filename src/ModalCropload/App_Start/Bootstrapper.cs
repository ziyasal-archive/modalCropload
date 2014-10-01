using System.Web.Routing;

namespace ModalCropload
{
    public class Bootstrapper
    {
        public static void Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            IoCConfig.Register();

            FluentValidationModelValidatorProvider.Configure(); 
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
        }
    }
}
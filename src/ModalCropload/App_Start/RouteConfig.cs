using System.Web.Mvc;
using System.Web.Routing;
using ModalCropload.Infrastructure;

namespace ModalCropload
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                 name: RouteNames.Default,
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Newscast", action = "Create", id = UrlParameter.Optional },
                 namespaces: new[] { "Web.UI.Controllers" }
             );
        }
    }
}

using System.Web.Http;
using Unity;
using Unity.Lifetime;

namespace ImageMege
{
    public static class WebApiConfig
    {

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();
            container.RegisterType<IPagedAlbumCollectionGenerator, PagedAlbumCollectionGenerator>(new HierarchicalLifetimeManager());
            container.RegisterType<IImageMerger, ImageMerger>(new HierarchicalLifetimeManager());
            container.RegisterType<IImageRepo, ImageRepo>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}

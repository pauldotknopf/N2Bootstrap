using System.Web.Mvc;
using N2.Engine;
using N2.Plugin;
using N2.Web.Mvc;
using N2Bootstrap.Library.Controllers;
using System.Web.Routing;
namespace N2Bootstrap.Library
{
    [AutoInitialize]
    public class Starter : IPluginInitializer
    {
        public void Initialize(IEngine engine)
        {
            RegisterRoutes(RouteTable.Routes, engine);
            RegisterControllerFactory(ControllerBuilder.Current, engine);
            RegisterViewEngines(ViewEngines.Engines);
        }

        public static void RegisterControllerFactory(ControllerBuilder controllerBuilder, IEngine engine)
        {
            // Registers controllers in the solution for dependency injection using the IoC container provided by N2
            engine.RegisterAllControllers();

            var controllerFactory = engine.Resolve<ControllerFactoryConfigurator>()
                .NotFound<StartPageController>(sc => sc.NotFound())
                .ControllerFactory;

            controllerBuilder.SetControllerFactory(controllerFactory);
        }

        public static void RegisterRoutes(RouteCollection routes, IEngine engine)
        {
            routes.MapContentRoute("Content", engine);
            routes.MapRoute("Css", "content/css", new { controller = "Common", action = "Styles" });
        }

        public static void RegisterViewEngines(ViewEngineCollection viewEngines)
        {
            viewEngines.RegisterThemeViewEngine<RazorViewEngine>("~/Bootstrap/Themes/");
            viewEngines.DecorateViewTemplateRegistration();
        }
    }
}
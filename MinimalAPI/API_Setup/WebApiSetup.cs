using System.Diagnostics.CodeAnalysis;

namespace LibraryManagementSystem.API_Setup
{
    [ExcludeFromCodeCoverage]
    public static class WebApiSetup
    {
        public static void AddWebApi(this IServiceCollection services, Type markerType)
        {
            services.RegisterImplementationsOf<IWebApi>(markerType);
            services.RegisterImplementationsOf<IWebApiAsync>(markerType);
        }

        public static async Task RegisterWebApisAsync(this WebApplication app)
        {

            using (var scope = app.Services.CreateScope())
            {
                var scopedProvider = scope.ServiceProvider;

                var webApis = scopedProvider.GetServices<IWebApi>();
                foreach (var webApi in webApis)
                {
                    webApi.Register(app);
                }

                var asyncWebApis = scopedProvider.GetServices<IWebApiAsync>();
                await Task.WhenAll(asyncWebApis.Select(x => x.RegisterAsync(app)));
            }
        }
    }
}

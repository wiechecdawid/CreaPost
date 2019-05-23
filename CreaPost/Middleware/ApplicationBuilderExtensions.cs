using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Microsoft.AspNetCore.Builder
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModues(
            this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");


            PhysicalFileProvider fileProider = new PhysicalFileProvider(path);

            var options = new StaticFileOptions();
            options.RequestPath = "/node_modules";
            options.FileProvider = fileProider;

            app.UseStaticFiles(options);

            return app;
        }
    }
}

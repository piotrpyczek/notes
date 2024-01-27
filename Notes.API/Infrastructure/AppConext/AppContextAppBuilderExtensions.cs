namespace Notes.API.Infrastructure.AppConext
{
    public static class AppContextAppBuilderExtensions
    {
        public static IApplicationBuilder UseAppContext(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AppContextMiddleware>();
        }
    }
}

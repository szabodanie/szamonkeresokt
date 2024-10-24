internal class Startup
{
    private static void Main(string[] args)
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogContext>(options =>
                options.UseSqlite("DataSource=blog.db"));
            services.AddControllers();
        }
    }
}
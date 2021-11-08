using temalabor2021.Models;

namespace temalabor2021.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context == null)
                return;
            context.Database.EnsureCreated();

        }
    }
}


using Microsoft.Extensions.Logging;

namespace CarService
{
    public class UserTypes
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public static class MauiProgram
    {
        public static List<UserTypes> _UserTypes = new List<UserTypes> {
            new UserTypes {Code ="C",Description="Müşteri" },
            new UserTypes {Code ="A",Description="Yetkili" },
            new UserTypes {Code ="R",Description="Normal" }
        };
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

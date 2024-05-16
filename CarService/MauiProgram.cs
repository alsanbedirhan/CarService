using Microsoft.Extensions.Logging;

namespace CarService
{
    public class clsCombo
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
    public static class MauiProgram
    {
        public static List<clsCombo> _UserTypes = new List<clsCombo> {
            new clsCombo {Code ="C",Description="Müşteri" },
            new clsCombo {Code ="A",Description="Yetkili" },
            new clsCombo {Code ="R",Description="Normal" }
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

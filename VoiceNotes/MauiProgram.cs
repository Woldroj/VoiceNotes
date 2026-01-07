using Microsoft.Maui.Hosting;
using VoiceNotes.ViewModels;
using VoiceNotes.Services;
using VoiceNotes;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>();

#if WINDOWS
        builder.Services.AddSingleton<IVoiceService, VoiceServiceWindows>();
        builder.Services.AddTransient<NoteDetailViewModel>();
#endif

        return builder.Build();
    }
}

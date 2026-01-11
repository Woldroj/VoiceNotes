using Microsoft.Maui.Hosting;
using VoiceNotes.ViewModels;
using VoiceNotes.Services;
using VoiceNotes.Views;
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
        builder.Services.AddSingleton<NotesStorageService>();
        builder.Services.AddTransient<NotesViewModel>();
        builder.Services.AddTransient<NotePage>();
#endif

        return builder.Build();
    }
}

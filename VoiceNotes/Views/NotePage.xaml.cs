using VoiceNotes.ViewModels;
using VoiceNotes.Services;
using Microsoft.Extensions.DependencyInjection;

namespace VoiceNotes.Views;

public partial class NotePage : ContentPage
{
    public NotePage()
    {
        InitializeComponent();
        Loaded += NotePage_Loaded;
    }

    private void NotePage_Loaded(object sender, EventArgs e)
    {
        var services = Handler.MauiContext.Services;
        var storageService = services.GetService<NotesStorageService>();
        var voiceService = services.GetService<IVoiceService>();

        var vm = new NotesViewModel(storageService, voiceService)
        {
            Navigation = Navigation
        };

        BindingContext = vm;
    }
}

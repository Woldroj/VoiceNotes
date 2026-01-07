using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VoiceNotes.Models;
using VoiceNotes.Services;
using VoiceNotes.Views;
using Microsoft.Maui.Controls;
using VoiceNotes.ViewModels;
using VoiceNotes;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace VoiceNotes.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();

        public ICommand NewNoteCommand { get; }

        public INavigation Navigation { get; set; }

        public NotesViewModel()
        {
            // Al ejecutar el comando, llama a OnNewNote()
            NewNoteCommand = new Command(async () => await OnNewNote());
        }

        private async Task OnNewNote()
        {
            // 1️⃣ Obtener el servicio de voz desde DI
            var voiceService = Microsoft.Maui.Controls.Application.Current.Handler.MauiContext.Services.GetService<IVoiceService>();

            // 2️⃣ Crear el ViewModel de la nota con la colección y el servicio
            var detailVM = new NoteDetailViewModel(Notes, voiceService)
            {
                Navigation = Navigation
            };

            // 3️⃣ Crear la página de detalle pasando el ViewModel
            var detailPage = new NoteDetailPage(detailVM);

            // 4️⃣ Navegar a la página
            await Navigation.PushAsync(detailPage);
        }
    }
}

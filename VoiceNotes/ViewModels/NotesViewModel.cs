using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VoiceNotes.Models;
using VoiceNotes.Services;
using VoiceNotes.Views;
using Microsoft.Maui.Controls;
using Microsoft.Extensions.DependencyInjection;

namespace VoiceNotes.ViewModels
{
    public class NotesViewModel : BaseViewModel
    {
        private readonly NotesStorageService _storageService;
        private readonly IVoiceService _voiceService;

        public ObservableCollection<Note> Notes { get; } = new ObservableCollection<Note>();

        private ObservableCollection<Note> _filteredNotes = new ObservableCollection<Note>();
        public ObservableCollection<Note> FilteredNotes
        {
            get => _filteredNotes;
            set { _filteredNotes = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                ApplyFilter();
            }
        }

        public ICommand NewNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand DeleteNoteCommand { get; }
        public ICommand VoiceSearchCommand { get; }

        public INavigation Navigation { get; set; }

        public NotesViewModel(NotesStorageService storageService, IVoiceService voiceService)
        {
            _storageService = storageService;
            _voiceService = voiceService;

            NewNoteCommand = new Command(async () => await OnNewNote());
            EditNoteCommand = new Command<Note>(async n => await OnEditNote(n));
            DeleteNoteCommand = new Command<Note>(async n => await OnDeleteNote(n));
            VoiceSearchCommand = new Command(async () => await OnVoiceSearch());

            Notes.CollectionChanged += (s, e) => ApplyFilter();

            _ = LoadNotesAsync();
        }

        private async Task LoadNotesAsync()
        {
            var savedNotes = await _storageService.LoadAsync();
            Notes.Clear();
            foreach (var note in savedNotes)
                Notes.Add(note);

            ApplyFilter();
        }

        private void ApplyFilter()
        {
            FilteredNotes.Clear();
            var text = SearchText?.ToLower() ?? "";
            foreach (var note in Notes)
            {
                if (string.IsNullOrEmpty(text) || note.Text.ToLower().Contains(text))
                    FilteredNotes.Add(note);
            }
        }

        private async Task OnNewNote()
        {
            var services = Application.Current.Handler.MauiContext.Services;
            var voiceService = services.GetService<IVoiceService>();

            var detailVM = new NoteDetailViewModel(Notes, voiceService, _storageService)
            {
                Navigation = Navigation
            };

            var page = new NoteDetailPage(detailVM);
            await Navigation.PushAsync(page);
        }

        private async Task OnEditNote(Note note)
        {
            if (note == null) return;

            var services = Application.Current.Handler.MauiContext.Services;
            var voiceService = services.GetService<IVoiceService>();

            var vm = new NoteDetailViewModel(Notes, voiceService, _storageService)
            {
                Navigation = Navigation
            };

            vm.LoadForEdit(note);

            var page = new NoteDetailPage(vm);
            await Navigation.PushAsync(page);
        }

        private async Task OnDeleteNote(Note note)
        {
            if (note == null) return;

            bool confirm = await Application.Current.MainPage.DisplayAlert(
                "Borrar nota",
                "¿Seguro que quieres borrar esta nota?",
                "Sí",
                "No");

            if (!confirm) return;

            Notes.Remove(note);
            await _storageService.SaveAsync(Notes);
        }

        private async Task OnVoiceSearch()
        {
            if (_voiceService == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "El servicio de voz no está disponible.",
                    "OK");
                return;
            }

            var result = await _voiceService.DictateAsync();

            if (!string.IsNullOrWhiteSpace(result))
                SearchText = result;
        }
    }
}

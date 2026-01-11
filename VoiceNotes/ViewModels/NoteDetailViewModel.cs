using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using VoiceNotes.Models;
using VoiceNotes.Services;
using Microsoft.Maui.Controls;

namespace VoiceNotes.ViewModels
{
    public class NoteDetailViewModel : BaseViewModel
    {
        private readonly IVoiceService _voiceService;
        private readonly NotesStorageService _storageService;
        private readonly ObservableCollection<Note> _notes;

        private Note _editingNote;

        public INavigation Navigation { get; set; }

        private string _text;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand DictateCommand { get; }

        // Crear nota
        public NoteDetailViewModel(
            ObservableCollection<Note> notes,
            IVoiceService voiceService,
            NotesStorageService storageService)
        {
            _notes = notes;
            _voiceService = voiceService;
            _storageService = storageService;

            SaveCommand = new Command(async () => await SaveAsync());

            DictateCommand = new Command(async () =>
            {
                try
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
                        Text += (string.IsNullOrWhiteSpace(Text) ? "" : " ") + result;
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Error",
                        $"Fallo al dictar: {ex.Message}",
                        "OK");
                }
            });
        }

        // Editar nota
        public void LoadForEdit(Note note)
        {
            _editingNote = note;
            Text = note.Text;
        }

        private async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Text))
                return;

            if (_editingNote == null)
            {
                // Nueva nota
                _notes.Add(new Note
                {
                    Text = Text,
                    Created = DateTime.Now
                });
            }
            else
            {
                // Editar nota existente
                _editingNote.Text = Text;
            }

            await _storageService.SaveAsync(_notes);

            if (Navigation != null)
                await Navigation.PopAsync();
        }
    }
}

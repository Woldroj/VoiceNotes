using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using VoiceNotes.Models;
using VoiceNotes.Services;

namespace VoiceNotes.ViewModels
{
    public class NoteDetailViewModel : BaseViewModel
    {
        private readonly IVoiceService _voiceService;
        private readonly ObservableCollection<Note> _notes;

        public INavigation Navigation { get; set; }

        private string _text;
        public string Text
        {
            get => _text;
            set { _text = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }
        public ICommand DictateCommand { get; }

        public NoteDetailViewModel(
            ObservableCollection<Note> notes,
            IVoiceService voiceService)
        {
            _notes = notes;
            _voiceService = voiceService;

            SaveCommand = new Command(async () =>
            {
                if (!string.IsNullOrWhiteSpace(Text))
                    _notes.Add(new Note { Text = Text, Created = DateTime.Now });

                await Navigation.PopAsync();
            });

            DictateCommand = new Command(async () =>
            {
                var result = await _voiceService.DictateAsync();

                if (!string.IsNullOrWhiteSpace(result))
                    Text += (string.IsNullOrWhiteSpace(Text) ? "" : " ") + result;
            });
        }
    }
}


using System.Collections.ObjectModel;
using VoiceNotes.Models;
using VoiceNotes.Services;
using VoiceNotes.ViewModels;

namespace VoiceNotes.Views;

public partial class NoteDetailPage : ContentPage
{
	public NoteDetailPage(NoteDetailViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}
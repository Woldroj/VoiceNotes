using VoiceNotes.ViewModels;

namespace VoiceNotes.Views;

public partial class NotePage : ContentPage
{
	public NotePage()
	{
		InitializeComponent();

		var vm = new NotesViewModel();
		vm.Navigation = Navigation;

		BindingContext = vm;
    }
}
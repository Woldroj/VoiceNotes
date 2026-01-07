using Microsoft.Extensions.DependencyInjection;
using VoiceNotes.Views;

namespace VoiceNotes
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage (new NotePage()); 
        }
    }
}
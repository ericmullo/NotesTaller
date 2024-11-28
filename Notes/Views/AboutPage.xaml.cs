using Microsoft.Maui.Controls;
using Microsoft.Maui.Essentials;

namespace Notes
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private async void LearnMore_Clicked(object sender, EventArgs e)
        {
            // Verificar si el BindingContext es del tipo Models.About
            if (BindingContext is Models.About about)
            {
                // Navegar a la URL especificada en el navegador del sistema
                await Launcher.Default.OpenAsync(about.MoreInfoUrl);
            }
        }
    }
}

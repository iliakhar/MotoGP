using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace motoGP.Views
{
    public partial class Request : Window
    {
        public Request()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace motoGP.Views
{
    public partial class RequestsResultsView : Window
    {
        public RequestsResultsView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

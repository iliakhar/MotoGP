using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace motoGP.Views
{
    public partial class RaceView : UserControl
    {
        public RaceView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

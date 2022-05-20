using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace motoGP.Views
{
    public partial class PilotView : UserControl
    {
        public PilotView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}

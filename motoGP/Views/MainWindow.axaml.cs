using Avalonia.Controls;
using motoGP.ViewModels;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;


namespace motoGP.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        
    }
}

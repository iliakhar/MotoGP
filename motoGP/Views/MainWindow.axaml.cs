using Avalonia.Controls;
using motoGP.ViewModels;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;


namespace motoGP.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }
        private async void RequestClick(object sender, RoutedEventArgs e)
        {
            string? tmpPattern = await new Request().ShowDialog<string?>((Window)this.VisualRoot);
        }
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            int ind = list.SelectedIndex;
            var context = this.DataContext as MainWindowViewModel;
            context.RowInd = 0;
            switch (ind)
            {
                case 0:
                    context.ChosenTable = new PilotViewModel(ref context.pilots);
                    break;
                case 1:
                    context.ChosenTable = new RaceViewModel(ref context.races);
                    break;

            }
        }
    }
}

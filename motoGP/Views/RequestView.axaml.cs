using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using motoGP.ViewModels;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using System.Collections.ObjectModel;
using ReactiveUI;

namespace motoGP.Views
{
    public partial class RequestView : UserControl
    {
        public RequestView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        private void ChangeSelect(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var context = this.DataContext as RequestViewModel;
            if(context != null)
                context.SelPr.Table = new ObservableCollection<string>(context.atributes[context.corespNames[combo.SelectedIndex]]);
            //context.SelectColInd = 0;
        }
        private void ChangeJoin(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var context = this.DataContext as RequestViewModel;
            if (context != null)
                context.JoinAtr = new ObservableCollection<string>(context.atributes[combo.SelectedIndex]);
            //context.SelectColInd = 0;
        }
        private void ChangeWhere(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var context = this.DataContext as RequestViewModel;
            if (context != null)
            {
                if (combo.SelectedIndex + 1 > context.ReqNames.Count) 
                    context.WhPr.WhereAtr = new ObservableCollection<string>(context.atributes[context.corespNames[combo.SelectedIndex - context.ReqNames.Count]]);
                else context.WhPr.WhereAtr = new ObservableCollection<string>(context.atributes[0]);
            }
                
            //context.SelectColInd = 0;
        }
        private void ChangeGroup(object sender, SelectionChangedEventArgs e)
        {
            var combo = sender as ComboBox;
            var context = this.DataContext as RequestViewModel;
            if (context != null)
                context.WhPr.GroupTb = new ObservableCollection<string>(context.atributes[context.corespNames[combo.SelectedIndex]]);
            //context.SelectColInd = 0;
        }
        private async void TapTap(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext as RequestViewModel;
            if (context != null && context.ReqListInd >= 0) 
                await new ShowReqTextView(context.StrReq[context.ReqListInd], context.ReqNames[context.ReqListInd]).ShowDialog<string?>((Window)this.VisualRoot);
        }
    }
}

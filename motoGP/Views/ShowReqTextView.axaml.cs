using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using motoGP.ViewModels;

namespace motoGP.Views
{
    public partial class ShowReqTextView : Window
    {
        public ShowReqTextView(string text, string name)
        {
            InitializeComponent();
            ReqName.Text = name;
            ReqText.Text = text;
            
        }
        public ShowReqTextView()
        {
            InitializeComponent();
            
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            ReqName = this.FindControl<global::Avalonia.Controls.TextBlock>("ReqName");
            ReqText = this.FindControl<global::Avalonia.Controls.TextBox>("ReqText");
        }
    }
}

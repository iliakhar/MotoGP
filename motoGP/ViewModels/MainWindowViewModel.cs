using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using motoGP.Models;
using ReactiveUI;
using System.Reactive;

namespace motoGP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public ObservableCollection<DefaultObj> Items { get; set; }
        //new DefaultObj(1, "a", "b"), new DefaultObj(2, "c", "d"), new DefaultObj(3, "e", "f")
        public MainWindowViewModel()
        {
            Items = new ObservableCollection<DefaultObj> { new DefaultObj("1", "a", "b"), new DefaultObj("2", "c", "d"), new DefaultObj("3", "e", "f") };
            //Items.Add(new DefaultObj(1, "a", "b"));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using motoGP.Models;
using ReactiveUI;
using System.Reactive;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Reactive.Linq;
using motoGP.Views;
using System.Data;

namespace motoGP.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        ViewModelBase content;
        private int maxHeight, minHeight, maxWidth, minWidth;
        public int Height { get; set; }
        public int Width { get; set; }
        public int MaxHeight
        {
            get { return maxHeight; }
            set {this.RaiseAndSetIfChanged(ref maxHeight, value); }
        }
        public int MinHeight
        {
            get { return minHeight; }
            set {this.RaiseAndSetIfChanged(ref minHeight, value);}
        }
        public int MaxWidth
        {
            get { return maxWidth; }
            set { this.RaiseAndSetIfChanged(ref maxWidth, value);}
        }
        public int MinWidth
        {
            get { return minWidth; }
            set { this.RaiseAndSetIfChanged(ref minWidth, value);}
        }

        ViewModelBase Content
        {
            get => content;
            set => this.RaiseAndSetIfChanged(ref content, value);
        }
        //public RequestViewModel reqWind { get; }
        public TablesViewModel mainTabWind { get; }
        public MainWindowViewModel()
        {
            mainTabWind = new TablesViewModel();
            MaxWidth = 2048;
            MinWidth = 700;
            MaxHeight = 2048;
            MinHeight = 500;
            Content = mainTabWind;
        }
        public void OpenReqWind(DataSet tables)
        {
            Width = MaxWidth = MinWidth = 850;
            Height = MaxHeight = MinHeight = 700;
            var vm = new RequestViewModel(mainTabWind.Tables, mainTabWind.StrReq);
            Observable.Merge(vm.Send)
                .Take(1)
                .Subscribe(msg =>
                {
                    if (msg != null)
                    {
                        mainTabWind.StrReq=msg;
                    }
                    Content = mainTabWind;
                    mainTabWind.CurrentTableIndex=-1;
                }
                );
            Content = vm;
        }
        public void OpenMainTablesWind()
        {
            MaxWidth = 2048;
            Width = MinWidth = 700;
            Height = MaxHeight = 2048;
            MinHeight = 500;
            Content = mainTabWind;
        }
    }
}

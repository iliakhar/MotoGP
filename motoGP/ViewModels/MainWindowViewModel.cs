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
        ViewModelBase chosenTable;
        public ViewModelBase ChosenTable
        {
            get => chosenTable;
            set => this.RaiseAndSetIfChanged(ref chosenTable, value);
        }
        public ObservableCollection<TBname> TableName { get; set; }
        public ObservableCollection<TabModel> Tabs { get; set; }
        private int tablesInd;
        private int rowInd;
        private int currTabInd;
        public int CurrTabInd
        {
            get => currTabInd;
            set { this.RaiseAndSetIfChanged(ref currTabInd, value); }
        }
        public int TablesInd
        {
            get => tablesInd;
            set { this.RaiseAndSetIfChanged(ref tablesInd, value); }
        }
        public int RowInd
        {
            get => rowInd;
            set { this.RaiseAndSetIfChanged(ref rowInd, value); }
        }
        public ObservableCollection<Pilot> pilots;
        public ObservableCollection<Race> races;
        //public ObservableCollection<DefaultObj> Items { get; set; }
        //new DefaultObj(1, "a", "b"), new DefaultObj(2, "c", "d"), new DefaultObj(3, "e", "f")
        public MainWindowViewModel()
        {
            TableName = new ObservableCollection<TBname> { new TBname("Pilot"), new TBname("Race")};
            var DB = new MotoGPContext();
            TablesInd = 0;
            pilots = new ObservableCollection<Pilot>(DB.Pilots);
            races = new ObservableCollection<Race>(DB.Races);
            ChosenTable = new PilotViewModel(ref pilots);
            RowInd = 0;
            //Items = new ObservableCollection<DefaultObj> { new DefaultObj("1", "a", "b"), new DefaultObj("2", "c", "d"), new DefaultObj("3", "e", "f") };
            //Items.Add(new DefaultObj(1, "a", "b"));
            AddRow = ReactiveCommand.Create(() => AddR());
            DelRow = ReactiveCommand.Create(() => Del());
            DelRequest = ReactiveCommand.Create(() => DelReq());
        }
        public ReactiveCommand<Unit, int> AddRow { get; }
        public ReactiveCommand<Unit, int> DelRow { get; }
        public ReactiveCommand<Unit, int> DelRequest { get; }
        private int AddR()
        {
            switch (TablesInd)
            {
                case 0: pilots.Add(new Pilot(pilots.Count + 1)); break;
                case 1: races.Add(new Race(races.Count + 1)); break;
            }
            return 0;
        }
        private int Del()
        {
            switch (TablesInd)
            {
                case 0: pilots.RemoveAt(RowInd); break;
                case 1: races.RemoveAt(RowInd); break;
            }
            return 0;
        }
        private int DelReq()
        {
            
            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using motoGP.Models;
using ReactiveUI;
using System.Reactive;
using System.Data;
using System.Data.SQLite;

namespace motoGP.ViewModels
{
    public class RequestResultsViewModel : ViewModelBase
    {
        
        public RequestResultsViewModel(DataSet tableSet, List<string> strRequests)
        {
            mainTables = tableSet;
            StrReq = strRequests;
            Send = ReactiveCommand.Create(() => StrReq);

            if (strRequests == null)
                strRequests = new List<string>();
            ReqNames = new ObservableCollection<string>();
            for (int i = 0; i < strRequests.Count; i++)
                ReqNames.Add("Request " + i.ToString());
        }
        public ReactiveCommand<Unit, List<string>> Send { get; set; }
        public DataSet mainTables;
        private DataSet tables;
        public DataSet Tables
        {
            get => tables;
            set => this.RaiseAndSetIfChanged(ref tables, value);
        }
        int tableInd;
        public int TableInd
        {
            get => tableInd;
            set => this.RaiseAndSetIfChanged(ref tableInd, value);
        }
        ObservableCollection<string> reqNames;
        public ObservableCollection<string> ReqNames
        {
            get => reqNames;
            set => this.RaiseAndSetIfChanged(ref reqNames, value);
        }
        private List<string> strReq;
        public List<string> StrReq
        {
            get => strReq;
            set => this.RaiseAndSetIfChanged(ref strReq, value);
        }
    }
}

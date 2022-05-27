using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace motoGP.Models
{
    public class SelectProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public SelectProperty()
        {
            TableInd = 0;
            Agreg = new ObservableCollection<string> { "", "AVG", "SUM", "MIN", "MAX", "COUNT" };
        }

        private int colInd, agrInd, agrTbInd, tableInd;
        ObservableCollection<string> table; 
        public ObservableCollection<string> Agreg { get; set; }
        public ObservableCollection<string> Table
        {
            get => table;
            set { table = value; NotifyPropertyChanged(); }
        }
        public int TableInd
        {
            get => tableInd;
            set { tableInd = value; NotifyPropertyChanged(); }
        }
        public int ColInd
        {
            get => colInd;
            set { colInd = value; NotifyPropertyChanged(); }
        }
        public int AgrInd
        {
            get => agrInd;
            set { agrInd = value; NotifyPropertyChanged(); }
        }
        public int AgrTbInd
        {
            get => agrTbInd;
            set { agrTbInd = value; NotifyPropertyChanged(); }
        }
     
    }
}

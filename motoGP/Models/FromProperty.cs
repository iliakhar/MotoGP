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
    public class FromProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public FromProperty()
        {
            TableInd = 0;
        }

        private int tableInd;
        public int TableInd
        {
            get => tableInd;
            set { tableInd = value; NotifyPropertyChanged(); }
        }
       
    }
}

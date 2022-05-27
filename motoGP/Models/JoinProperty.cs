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
    public class JoinProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public JoinProperty()
        {
            TableInd = 0;
        }

        private int atrIndFirst, atrIndSec, tableInd;
        public int TableInd
        {
            get => tableInd;
            set { tableInd = value; NotifyPropertyChanged(); }
        }
        public int AtrIndFirst
        {
            get => atrIndFirst;
            set { atrIndFirst = value; NotifyPropertyChanged(); }
        }
        public int AtrIndSec
        {
            get => atrIndSec;
            set { atrIndSec = value; NotifyPropertyChanged(); }
        }
       
    }
}

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
            SelectInd = 0;
        }

        private int selectInd, agrInd, agrTableInd, groupInd, tableInd;
        public int TableInd
        {
            get => tableInd;
            set { tableInd = value; NotifyPropertyChanged(); }
        }
        public int SelectInd
        {
            get => selectInd;
            set { selectInd = value; NotifyPropertyChanged(); }
        }
        public int AgrInd
        {
            get => agrInd;
            set { agrInd = value; NotifyPropertyChanged(); }
        }
        public int AgrTableInd
        {
            get => agrTableInd;
            set { agrTableInd = value; NotifyPropertyChanged(); }
        }
        public int GroupInd
        {
            get => groupInd;
            set { groupInd = value; NotifyPropertyChanged(); }
        }

        
    }
}

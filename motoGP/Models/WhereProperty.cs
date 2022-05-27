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
    public class WhereProperty : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public WhereProperty()
        {
            TableInd = 0;
            Signs = new ObservableCollection<string> { "", ">", "<", "=" };
            ConectWord = new ObservableCollection<string> { "", "OR", "AND" };
        }

        private int tableInd, atrInd, signInd, connectInd, groupInd, groupTbInd;
        string secOperand, text;
        ObservableCollection<string> signs, conectWord, groupTb, whereAtr;
        public string SecOperand
        {
            get => secOperand;
            set { secOperand = value; NotifyPropertyChanged(); }
        }
        public string Text
        {
            get => text;
            set { text = value; NotifyPropertyChanged(); }
        }
        public int TableInd
        {
            get => tableInd;
            set { tableInd = value; NotifyPropertyChanged(); }
        }
        public int AtrInd
        {
            get => atrInd;
            set { atrInd = value; NotifyPropertyChanged(); }
        }
        public int SignInd
        {
            get => signInd;
            set { signInd = value; NotifyPropertyChanged(); }
        }
        public int ConnectInd
        {
            get => connectInd;
            set { connectInd = value; NotifyPropertyChanged(); }
        }
        public int GroupInd
        {
            get => groupInd;
            set { groupInd = value; NotifyPropertyChanged(); }
        }
        public int GroupTbInd
        {
            get => groupTbInd;
            set { groupTbInd = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<string> Signs
        {
            get => signs;
            set { signs = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<string> ConectWord
        {
            get => conectWord;
            set { conectWord = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<string> GroupTb
        {
            get => groupTb;
            set { groupTb = value; NotifyPropertyChanged(); }
        }
        public ObservableCollection<string> WhereAtr
        {
            get => whereAtr;
            set { whereAtr = value; NotifyPropertyChanged(); }
        }
    }
}

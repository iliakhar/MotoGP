using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using motoGP.Models;
using ReactiveUI;
using System.Reactive;

namespace motoGP.ViewModels
{
    public class RaceViewModel : ViewModelBase
    {
        private ObservableCollection<Race> races;
        public RaceViewModel(ref ObservableCollection<Race> races)
        {
            Races = races;
        }

        public ObservableCollection<Race> Races
        {
            get
            {
                return races;
            }
            set
            {
                races = value;
            }
        }
    }
}

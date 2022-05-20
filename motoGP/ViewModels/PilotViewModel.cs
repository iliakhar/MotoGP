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
    public class PilotViewModel : ViewModelBase
    {
        private ObservableCollection<Pilot> pilots;
        public PilotViewModel(ref ObservableCollection<Pilot> pilots)
        {
            Pilots = pilots;
        }

        public ObservableCollection<Pilot> Pilots
        {
            get
            {
                return pilots;
            }
            set
            {
                pilots = value;
            }
        }
    }
}

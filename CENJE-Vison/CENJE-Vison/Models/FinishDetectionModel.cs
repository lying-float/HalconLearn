using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CENJE_Vison.Models
{
    public class FinishDetectionModel:ObservableObject
    {
        private int cellThreshold;

        public int CellThreshold
        {
            get { return cellThreshold; }
            set
            {
                cellThreshold = value;
                RaisePropertyChanged();
            }
        }

        private int pinErosion1;

        public int PinErosion1
        {
            get { return pinErosion1; }
            set
            {
                pinErosion1 = value;
                RaisePropertyChanged();
            }
        }

        private int pinErosion2;

        public int PinErosion2
        {
            get { return pinErosion2; }
            set
            {
                pinErosion2 = value;
                RaisePropertyChanged();
            }
        }

    }
}

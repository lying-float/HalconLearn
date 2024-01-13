using System;
using System.Collections.Generic;
using System.Configuration;
using GalaSoft.MvvmLight;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CENJE_Vison.Models.UserSection
{
    public class MainViewSection:ConfigurationSection,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public static MainViewSection Instance { get; } = (MainViewSection)App.Current.Cfg.GetSection("MainViewSection");

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [ConfigurationProperty("isOpenAllChecked", DefaultValue = false)]
        public bool isOpenAllChecked
        {
            get => (bool)this["isOpenAllChecked"];
            set { this["isOpenAllChecked"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("isOpenNegativeChecked", DefaultValue = false)]
        public bool IsOpenNegativeChecked
        {
            get => (bool)this["isOpenNegativeChecked"];
            set { this["isOpenNegativeChecked"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("isOpenPositiveChecked", DefaultValue = false)]
        public bool IsOpenPositiveChecked
        {
            get => (bool)this["isOpenPositiveChecked"];
            set { this["isOpenPositiveChecked"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("isOpenFinishChecked", DefaultValue = false)]
        public bool IsOpenFinishChecked
        {
            get => (bool)this["isOpenFinishChecked"];
            set { this["isOpenFinishChecked"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("realTimeMode", DefaultValue = false)]
        public bool RealTimeMode
        {
            get => (bool)this["realTimeMode"];
            set { this["realTimeMode"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("TestData", DefaultValue = false)]
        public bool TestData
        {
            get => (bool)this["TestData"];
            set { this["TestData"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("SaveNGData", DefaultValue = false)]
        public bool SaveNGData
        {
            get => (bool)this["SaveNGData"];
            set { this["SaveNGData"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("SaveOKData", DefaultValue = false)]
        public bool SaveOKData
        {
            get => (bool)this["SaveOKData"];
            set { this["SaveOKData"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("SaveOrData", DefaultValue = false)]
        public bool SaveOrData
        {
            get => (bool)this["SaveOrData"];
            set { this["SaveOrData"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("negativeFlower", DefaultValue = false)]
        public bool NegativeFlower
        {
            get => (bool)this["negativeFlower"];
            set { this["negativeFlower"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("negativeAngle", DefaultValue = false)]
        public bool NegativeAngle
        {
            get => (bool)this["negativeAngle"];
            set { this["negativeAngle"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("negativeL2", DefaultValue = false)]
        public bool NegativeL2
        {
            get => (bool)this["negativeL2"];
            set { this["negativeL2"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("negativeSplit", DefaultValue = false)]
        public bool NegativeSplit
        {
            get => (bool)this["negativeSplit"];
            set { this["negativeSplit"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("negativeNeedleHole", DefaultValue = false)]
        public bool NegativeNeedleHole
        {
            get => (bool)this["negativeNeedleHole"];
            set { this["negativeNeedleHole"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("negativeAll", DefaultValue = false)]
        public bool NegativeAll
        {
            get => (bool)this["negativeAll"];
            set { this["negativeAll"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveFlower", DefaultValue = false)]
        public bool PositiveFlower
        {
            get => (bool)this["positiveFlower"];
            set { this["positiveFlower"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveAngle", DefaultValue = false)]
        public bool PositiveAngle
        {
            get => (bool)this["positiveAngle"];
            set { this["positiveAngle"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveL2", DefaultValue = false)]
        public bool PositiveL2
        {
            get => (bool)this["positiveL2"];
            set { this["positiveL2"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveSplit", DefaultValue = false)]
        public bool PositiveSplit
        {
            get => (bool)this["positiveSplit"];
            set { this["positiveSplit"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveMoSplit", DefaultValue = false)]
        public bool PositiveMoSplit
        {
            get => (bool)this["positiveMoSplit"];
            set { this["positiveMoSplit"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveNeedleHole", DefaultValue = false)]
        public bool PositiveNeedleHole
        {
            get => (bool)this["positiveNeedleHole"];
            set { this["positiveNeedleHole"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("positiveAll", DefaultValue = false)]
        public bool PositiveAll
        {
            get => (bool)this["positiveAll"];
            set { this["positiveAll"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("productionHeight", DefaultValue = false)]
        public bool ProductionHeight
        {
            get => (bool)this["productionHeight"];
            set { this["productionHeight"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("productionDiameter", DefaultValue = false)]
        public bool ProductionDiameter
        {
            get => (bool)this["productionDiameter"];
            set { this["productionDiameter"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("pproductionfootDistanceExternal", DefaultValue = false)]
        public bool PproductionfootDistanceExternal
        {
            get => (bool)this["pproductionfootDistanceExternal"];
            set { this["pproductionfootDistanceExternal"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("productionfootOffset", DefaultValue = false)]
        public bool ProductionfootOffset
        {
            get => (bool)this["productionfootOffset"];
            set { this["productionfootOffset"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("productionAll", DefaultValue = false)]
        public bool ProductionAll
        {
            get => (bool)this["productionAll"];
            set { this["productionAll"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("StartAtuo", DefaultValue = false)]
        public bool StartAtuo
        {
            get => (bool)this["StartAtuo"];
            set { this["StartAtuo"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("ClearTotal", DefaultValue = false)]
        public bool ClearTotal
        {
            get => (bool)this["ClearTotal"];
            set { this["ClearTotal"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("ShowAlarm", DefaultValue = false)]
        public bool ShowAlarm
        {
            get => (bool)this["ShowAlarm"];
            set { this["ShowAlarm"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("pIntervalValue", DefaultValue = 0)]
        public int PIntervalValue
        {
            get => (int)this["pIntervalValue"];
            set { this["pIntervalValue"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("nIntervalValue", DefaultValue = 0)]
        public int NIntervalValue
        {
            get => (int)this["nIntervalValue"];
            set { this["nIntervalValue"] = value; OnPropertyChanged(); }
        }

        [ConfigurationProperty("formulaType", DefaultValue = "")]
        public string FormulaType
        {
            get => (string)this["formulaType"];
            set { this["formulaType"] = value; OnPropertyChanged(); }
        }
    }
}

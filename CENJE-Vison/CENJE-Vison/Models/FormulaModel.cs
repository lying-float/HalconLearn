using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CENJE_Vison.Models
{
    /// <summary>
    /// 该modle类定义了检测配方相关参数
    /// </summary>
    public class FormulaModel:ObservableObject
    {
        //配方型号
        private string _formulatype;

        public string FormulaType
        {
            get { return _formulatype; }
            set
            {
                _formulatype = value;
                RaisePropertyChanged();
            }
        }

        //花瓣数目
        private int _flowerCount;

        public int FlowerCount
        {
            get { return _flowerCount; }
            set
            {
                _flowerCount = value;
                RaisePropertyChanged();
            }
        }

        //良率报警
        private double _warming;

        public double Warming
        {
            get { return _warming; }
            set
            {
                _warming = value;
                RaisePropertyChanged();
            }
        }

        #region 负极参数
        //负极花瓣面积上限
        private double _AreaMaxN;

        public double AreaMaxN
        {
            get { return _AreaMaxN; }
            set
            {
                _AreaMaxN = value;
                RaisePropertyChanged();
            }
        }

        //负极花瓣面积下限
        private double _AreaMinN;

        public double AreaMinN
        {
            get { return _AreaMinN; }
            set
            {
                _AreaMinN = value;
                RaisePropertyChanged();
            }
        }

        //负极裂箔上限
        private double _foilCrackMaxN;

        public double FoilCrackMaxN
        {
            get { return _foilCrackMaxN; }
            set
            {
                _foilCrackMaxN = value;
                RaisePropertyChanged();
            }
        }

        //负极裂箔下限
        private double _foilCrackMinN;

        public double FoilCrackMinN
        {
            get { return _foilCrackMinN; }
            set
            {
                _foilCrackMinN = value;
                RaisePropertyChanged();
            }
        }

        //负极角度上限
        private double _angleMaxN;

        public double AngleMaxN
        {
            get { return _angleMaxN; }
            set
            {
                _angleMaxN = value;
                RaisePropertyChanged();
            }
        }

        //负极角度下限
        private double _angleMinN;

        public double AngleMinN
        {
            get { return _angleMinN; }
            set
            {
                _angleMinN = value;
                RaisePropertyChanged();
            }
        }

        //L2距离上限
        private double _L2MaxN;

        public double L2MaxN
        {
            get { return _L2MaxN; }
            set
            {
                _L2MaxN = value;
                RaisePropertyChanged();
            }
        }

        //L2距离下限
        private double _L2MinN;

        public double L2MinN
        {
            get { return _L2MinN; }
            set
            {
                _L2MinN = value;
                RaisePropertyChanged();
            }
        }

        //铆偏上限
        private double _rivetOffsetMaxN;

        public double RivetOffsrtMaxN
        {
            get { return _rivetOffsetMaxN; }
            set
            {
                _rivetOffsetMaxN = value;
                RaisePropertyChanged();
            }
        }

        //铆偏下限
        private double _rivetOffsetMinN;

        public double RivetOffsrtMinN
        {
            get { return _rivetOffsetMinN; }
            set
            {
                _rivetOffsetMinN = value;
                RaisePropertyChanged();
            }
        }

        //负极膜色
        private int _colorN;

        public int ColorN
        {
            get { return _colorN; }
            set
            {
                _colorN = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region 正极参数
        //正极花瓣面积上限
        private double _AreaMaxP;

        public double AreaMaxP
        {
            get { return _AreaMaxP; }
            set
            {
                _AreaMaxP = value;
                RaisePropertyChanged();
            }
        }

        //正极花瓣面积下限
        private double _AreaMinP;

        public double AreaMinP
        {
            get { return _AreaMinP; }
            set
            {
                _AreaMinP = value;
                RaisePropertyChanged();
            }
        }

        //正极裂箔上限
        private double _foilCrackMaxP;

        public double FoilCrackMaxP
        {
            get { return _foilCrackMaxP; }
            set
            {
                _foilCrackMaxP = value;
                RaisePropertyChanged();
            }
        }

        //正极裂箔下限
        private double _foilCrackMinP;

        public double FoilCrackMinP
        {
            get { return _foilCrackMinP; }
            set
            {
                _foilCrackMinP = value;
                RaisePropertyChanged();
            }
        }

        //正极角度上限
        private double _angleMaxP;

        public double AngleMaxP
        {
            get { return _angleMaxP; }
            set
            {
                _angleMaxP = value;
                RaisePropertyChanged();
            }
        }

        //正极角度下限
        private double _angleMinP;

        public double AngleMinP
        {
            get { return _angleMinP; }
            set
            {
                _angleMinP = value;
                RaisePropertyChanged();
            }
        }

        //L2距离上限
        private double _L2MaxP;

        public double L2MaxP
        {
            get { return _L2MaxP; }
            set
            {
                _L2MaxP = value;
                RaisePropertyChanged();
            }
        }

        //L2距离下限
        private double _L2MinP;

        public double L2MinP
        {
            get { return _L2MinP; }
            set
            {
                _L2MinP = value;
                RaisePropertyChanged();
            }
        }

        //铆偏上限
        private double _rivetOffsetMaxP;

        public double RivetOffsrtMaxP
        {
            get { return _rivetOffsetMaxP; }
            set
            {
                _rivetOffsetMaxP = value;
                RaisePropertyChanged();
            }
        }

        //铆偏下限
        private double _rivetOffsetMinP;

        public double RivetOffsrtMinP
        {
            get { return _rivetOffsetMinP; }
            set
            {
                _rivetOffsetMinP = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 芯包检测参数
        //高度上限
        private double  _heightMax;
        public double HeightMax
        {
            get { return _heightMax; }
            set
            {
                _heightMax = value;
                RaisePropertyChanged();
            }
        }

        //芯包高度下限
        private double _heightMin;
        public double HeightMin
        {
            get { return _heightMin; }
            set
            {
                _heightMin = value;
                RaisePropertyChanged();
            }
        }

        //芯包外径上限
        private double _ODMax;
        public double ODMax
        {
            get { return _ODMax; }
            set
            {
                _ODMax = value;
                RaisePropertyChanged();
            }
        }

        //芯包外径下限
        private double _ODMin;
        public double ODMin
        {
            get { return _ODMin; }
            set
            {
                _ODMin = value;
                RaisePropertyChanged();
            }
        }

        //脚距上限
        private double _pinPitchMax;
        public double PinPitchMax
        {
            get { return _pinPitchMax; }
            set
            {
                _pinPitchMax = value;
                RaisePropertyChanged();
            }
        }

        //脚距下限
        private double _pinPitchMin;
        public double PinPitchMin
        {
            get { return _pinPitchMin; }
            set
            {
                _pinPitchMin = value;
                RaisePropertyChanged();
            }
        }

        //高低脚上限
        private double _differenceOfHeightMax;
        public double DifferenceOfHeightMax
        {
            get { return _differenceOfHeightMax; }
            set
            {
                _differenceOfHeightMax = value;
                RaisePropertyChanged();
            }
        }

        //高低脚下限
        private double _differenceOfHeightMin;
        public double DifferenceOfHeightMin
        {
            get { return _differenceOfHeightMin; }
            set
            {
                _differenceOfHeightMin = value;
                RaisePropertyChanged();
            }
        }

        //上CP线上限
        private double _CPlineUpMax;
        public double CPlineUpMax
        {
            get { return _CPlineUpMax; }
            set
            {
                _CPlineUpMax = value;
                RaisePropertyChanged();
            }
        }

        //上CP线下限
        private double _CPlineUpMin;
        public double CPlineUpMin
        {
            get { return _CPlineUpMin; }
            set
            {
                _CPlineUpMin = value;
                RaisePropertyChanged();
            }
        }

        //下CP线上限
        private double _CPlineBottomMax;
        public double CPlineBottomMax
        {
            get { return _CPlineBottomMax; }
            set
            {
                _CPlineBottomMax = value;
                RaisePropertyChanged();
            }
        }

        //下CP线下限
        private double _CPlineBottomMin;
        public double CPlineBottomMin
        {
            get { return _CPlineBottomMin; }
            set
            {
                _CPlineBottomMin = value;
                RaisePropertyChanged();
            }
        }


        #endregion
    }
}

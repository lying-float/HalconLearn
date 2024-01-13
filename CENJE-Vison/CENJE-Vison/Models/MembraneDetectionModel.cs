using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CENJE_Vison.Models
{
    /// <summary>
    /// 该model类定义了图像检测相关参数
    /// </summary>
    public class MembraneDetectionModel:ObservableObject
    {
        //定位时膜的灰度值
        private int _membraneThreshold;

        public int MembraneThreshold
        {
            get { return _membraneThreshold; }
            set
            {
                _membraneThreshold = value;
                RaisePropertyChanged();
            }
        }

        private int _footThreshold;

        //定位针时最小灰度值
        public int FootThreshold
        {
            get { return _footThreshold; }
            set
            {
                _footThreshold = value;
                RaisePropertyChanged();
            }
        }

        //定位花瓣基准时最小灰度值
        private int _flowerThreshold;

        public int FlowerThrehold
        {
            get { return _flowerThreshold; }
            set
            {
                _flowerThreshold = value;
                RaisePropertyChanged();
            }
        }

        //定位L2最小灰度值
        private int _L2Threshold;

        public int L2Threshold
        {
            get { return _L2Threshold; }
            set
            {
                _L2Threshold = value;
                RaisePropertyChanged();
            }
        }

        //定位下膜基准线最小灰度值
        private int _membraneDownLineThreshold;

        public int MembraneDownLineThrehold
        {
            get { return _membraneDownLineThreshold; }
            set
            {
                _membraneDownLineThreshold = value;
                RaisePropertyChanged();
            }
        }

        //定位花瓣中心最大灰度值
        private int _flowerCenterThreshold;

        public int FlowerCenterThrehold
        {
            get { return _flowerCenterThreshold; }
            set
            {
                _flowerCenterThreshold = value;
                RaisePropertyChanged();
            }
        }

        //定位铝舌中心点最小灰度值
        private int _tongueThreshold;

        public int TongueThreshold
        {
            get { return _tongueThreshold; }
            set
            {
                _tongueThreshold = value;
                RaisePropertyChanged();
            }
        }

        //判定裂箔最小灰度值
        private int _splitThreshold;

        public int SplitThreshold
        {
            get { return _splitThreshold; }
            set
            {
                _splitThreshold = value;
                RaisePropertyChanged();
            }
        }

        //定位模板分数
        private double _locationScore;

        public double LocationScore
        {
            get { return _locationScore; }
            set
            {
                _locationScore = value;
                RaisePropertyChanged();
            }
        }

        //花瓣模板分数
        private double _flowerLocationScore;

        public double FlowerLocationScore
        {
            get { return _flowerLocationScore; }
            set
            {
                _flowerLocationScore = value;
                RaisePropertyChanged();
            }
        }


        //增益
        private double _gain;

        public double Gain
        {
            get { return _gain; }
            set
            {
                _gain = value;
                RaisePropertyChanged();
            }
        }

        //曝光时间
        private int _exposureTime;

        public int ExposureTime
        {
            get { return _exposureTime; }
            set
            {
                _exposureTime = value;
                RaisePropertyChanged();
            }
        }


    }
}

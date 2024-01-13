using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CENJE_Vison.Models
{
    //负极参数设置model
    public class CameraAndCalibrationModel:ObservableObject
    {
        #region 拍照相关参数
        //图像高度
        private double _imageHeight;
        public double ImageHeight
        {
            get { return _imageHeight; }
            set
            {
                _imageHeight = value;
                RaisePropertyChanged();
            }
        }

        //图像宽度度
        private double _imageWidth;
        public double ImageWidth
        {
            get { return _imageWidth; }
            set
            {
                _imageWidth = value;
                RaisePropertyChanged();
            }
        }

        //图像偏移X
        private double _offsetX;
        public double OffsetX
        {
            get { return _offsetX; }
            set
            {
                _offsetX = value;
                RaisePropertyChanged();
            }
        }

        //图像偏移Y
        private double _offsetY;
        public double OffsetY
        {
            get { return _offsetY; }
            set
            {
                _offsetY = value;
                RaisePropertyChanged();
            }
        }

        //FPS
        private double _fps;
        public double FPS
        {
            get { return _fps; }
            set
            {
                _fps = value;
                RaisePropertyChanged();
            }
        }

        //旋转角度
        private double _angel;
        public double Angel
        {
            get { return _angel; }
            set
            {
                _angel = value;
                RaisePropertyChanged();
            }
        }

        //碗光源亮度
        private double _light1;
        public double Light1
        {
            get { return _light1; }
            set
            {
                _light1 = value;
                RaisePropertyChanged();
            }
        }

        //同轴光亮度
        private double _light2;
        public double Light2
        {
            get { return _light2; }
            set
            {
                _light2 = value;
                RaisePropertyChanged();
            }
        }

        //光源持续时间
        private double _lightTime;
        public double LightTime
        {
            get { return _lightTime; }
            set
            {
                _lightTime = value;
                RaisePropertyChanged();
            }
        }

        //光源提前时间
        private double _lightAdvance;
        public double LightTimeAdvance
        {
            get { return _lightAdvance; }
            set
            {
                _lightAdvance = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 标定参数
        //标定值X
        private double _calibrationX;
        public double CalibrationX
        {
            get { return _calibrationX; }
            set
            {
                _calibrationX = value;
                RaisePropertyChanged();
            }
        }

        //标定值Y
        private double _calibrationY;
        public double CalibrationY
        {
            get { return _calibrationY; }
            set
            {
                _calibrationY = value;
                RaisePropertyChanged();
            }
        }

        //校准值X
        private double _adjustX;
        public double AdjustX
        {
            get { return _adjustX; }
            set
            {
                _adjustX = value;
                RaisePropertyChanged();
            }
        }

        //校准值Y
        private double _adjustY;
        public double AdjustY
        {
            get { return _adjustY; }
            set
            {
                _adjustY = value;
                RaisePropertyChanged();
            }
        }

        //
        private bool _calibrationMod;
        public bool CalibrationMod
        {
            get { return _calibrationMod; }
            set
            {
                _calibrationMod = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Roi
        #region 正负极
        //花瓣ROI
        private double[] flowerRoi;

        public double[] FlowerRoi
        {
            get { return flowerRoi; }
            set
            {
                flowerRoi = value;
                RaisePropertyChanged();
            }
        }

        //整体图像检测Roi
        private double[] pictureRoi;

        public double[] PictureRoi
        {
            get { return pictureRoi; }
            set
            {
                pictureRoi = value;
                RaisePropertyChanged();
            }
        }

        //箔下边缘线ROI
        private double[] lineRoi;

        public double[] LineRoi
        {
            get { return lineRoi; }
            set
            {
                lineRoi = value;
                RaisePropertyChanged();
            }
        }

        //针脚ROI
        private double[] pinRoi;

        public double[] PinRoi
        {
            get { return pinRoi; }
            set
            {
                pinRoi = value;
                RaisePropertyChanged();
            }
        }

        //L2ROI
        private double[] tongueRoi;

        public double[] TongueRoi
        {
            get { return tongueRoi; }
            set
            {
                tongueRoi = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 芯包成品Roi
        //暂时决定使用正负极的PinRoi和PictureRoi替代
        #endregion
        
        #endregion

    }
}

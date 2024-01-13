using CENJE_Vison.Common.HalconFunc;
using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENJE_Vison.Common
{
    /// <summary>
    /// 后台相机、标定、图像处理等参数设置结构体
    /// 目前按照MZ的UI模仿所写，部分参数后续版本如用不上则可以删去，亦可添加新参数
    /// </summary>
    public class DetectionSetting
    {
        //正极
        public struct PositiveDetection
        {
            //相机参数
            public double ImageHeight;
            public double ImageWidth;
            public double OffsetX;
            public double OffsetY;
            public double FPS;
            //public double Angel;
            public double Light1;
            //public double Light2;
            public double Gain;
            public double LightTime;
            //public double LightTimeAdvance;

            //标定参数
            public double CalibrationX;
            public double CalibrationY;
            public double AdjustX;
            public double AdjustY;
            public bool CalibrationMod;

            public List<HDrawingObjectInfo> HDrawingObjects;

            //检测参数
            public double MembraneThreshold;
            public double FootThreshold;
            public double FlowerThrehold;
            public double L2Threshold;
            public double MembraneDownLineThrehold;
            public double FlowerCenterThrehold;
            public double TongueThreshold;
            public double SplitThreshold;
            public double LocationScore;
            public double FlowerLocationScore;

            //Roi
            public double[] PictureRoi;
            public double[] FlowerRoi;
            public double[] PinRoi;
            public double[] TongueRoi;
            public double[] LineRoi;
        }

        //负极
        public struct NegativeDetection
        {
            //相机参数
            public double ImageHeight;
            public double ImageWidth;
            public double OffsetX;
            public double OffsetY;
            public double FPS;
            public double Angel;
            public double Light1;
            public double Light2;
            public double LightTime;
            public double LightTimeAdvance;
            public double Gain;
            public double ExposureTime;

            //标定参数
            public double CalibrationX;
            public double CalibrationY;
            public double AdjustX;
            public double AdjustY;
            public bool CalibrationMod;
            public List<HDrawingObjectInfo> HDrawingObjects;

            //检测参数
            public int ColorN;
            public double MembraneThreshold;
            public double FootThreshold;
            public double FlowerThrehold;
            public double L2Threshold;
            public double MembraneDownLineThrehold;
            public double FlowerCenterThrehold;
            public double TongueThreshold;
            public double SplitThreshold;
            public double LocationScore;
            public double FlowerLocationScore;

            //Roi
            public double[] PictureRoi;
            public double[] FlowerRoi;
            public double[] PinRoi;
            public double[] TongueRoi;
            public double[] LineRoi;
        }

        //成品
        public struct FinishDetection
        {
            //相机参数
            public double ImageHeight;
            public double ImageWidth;
            public double OffsetX;
            public double OffsetY;
            public double FPS;
            public double Angel;
            public double Light1;
            public double Gain;
            //public double Light2;
            public double LightTime;
            //public double LightTimeAdvance;

            //标定参数
            public double CalibrationX;
            public double CalibrationY;
            public double AdjustX;
            public double AdjustY;
            public bool CalibrationMod;
            public List<HDrawingObjectInfo> HDrawingObjects;

            //检测参数
            public int  CellThreshold;
            public int  PinErosion1;
            public int  PinErosion2;

            //Roi
            public double[] PictureRoi;
            public double[] PinRoi;
        }

    }
}

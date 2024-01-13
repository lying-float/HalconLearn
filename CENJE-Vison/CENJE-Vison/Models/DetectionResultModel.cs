using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace CENJE_Vison.Models
{
    /// <summary>
    /// 用于主窗口显示检测结果
    /// </summary>
    public class DetectionResultModel:ObservableObject
    {
        #region 负极
        private string[] negativeFlower1;

        public string[] NegativeFlower1
        {
            get { return negativeFlower1; }
            set
            {
                negativeFlower1 = value;
                RaisePropertyChanged();
            }
        }

        private string[] negativeFlower2;

        public string[] NegativeFlower2
        {
            get { return negativeFlower2; }
            set
            {
                negativeFlower2 = value;
                RaisePropertyChanged();
            }
        }

        private string[] negativeFlower3;

        public string[] NegativeFlower3
        {
            get { return negativeFlower3; }
            set
            {
                negativeFlower3 = value;
                RaisePropertyChanged();
            }
        }

        private string[] negativeFlower4;

        public string[] NegativeFlower4
        {
            get { return negativeFlower4; }
            set
            {
                negativeFlower4 = value;
                RaisePropertyChanged();
            }
        }

        private string[] negativeFlower5;

        public string[] NegativeFlower5
        {
            get { return negativeFlower5; }
            set
            {
                negativeFlower5 = value;
                RaisePropertyChanged();
            }
        }

        private string[] negativeFlower6;

        public string[] NegativeFlower6
        {
            get { return negativeFlower6; }
            set
            {
                negativeFlower6 = value;
                RaisePropertyChanged();
            }
        }

        private string negativeAngle;

        public string NegativeAngle
        {
            get { return negativeAngle; }
            set
            {
                negativeAngle = value;
                RaisePropertyChanged();
            }
        }

        private string negativeL2;

        public string NegativeL2
        {
            get { return negativeL2; }
            set
            {
                negativeL2 = value;
                RaisePropertyChanged();
            }
        }

        private string negativeFoilCrack;

        public string NegativeFoilCrack
        {
            get { return negativeFoilCrack; }
            set
            {
                negativeFoilCrack = value;
                RaisePropertyChanged();
            }
        }

        private string negativePinOffset;

        public string NegativePinOffset
        {
            get { return negativePinOffset; }
            set
            {
                negativePinOffset = value;
                RaisePropertyChanged();
            }
        }

        private int negativeFlowerNGNum;

        public int NegativeFlowerNGNum
        {
            get { return negativeFlowerNGNum; }
            set
            {
                negativeFlowerNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int negativeAngleNGNum;

        public int NegativeAngleNGNum
        {
            get { return negativeAngleNGNum; }
            set
            {
                negativeAngleNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int negativeL2NGNum;

        public int NegativeL2NGNum
        {
            get { return negativeL2NGNum; }
            set
            {
                negativeL2NGNum = value;
                RaisePropertyChanged();
            }
        }

        private int negativeFoilCrackNGNum;

        public int NegativeFoilCrackNGNum
        {
            get { return negativeFoilCrackNGNum; }
            set
            {
                negativeFoilCrackNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int negativePinOffsetNGNum;

        public int NegativePinOffsetNGNum
        {
            get { return negativePinOffsetNGNum; }
            set
            {
                negativePinOffsetNGNum = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region 正极
        private string[] positiveFlower1;

        public string[] PositiveFlower1
        {
            get { return positiveFlower1; }
            set
            {
                positiveFlower1 = value;
                RaisePropertyChanged();
            }
        }

        private string[] positiveFlower2;

        public string[] PositiveFlower2
        {
            get { return positiveFlower2; }
            set
            {
                positiveFlower2 = value;
                RaisePropertyChanged();
            }
        }

        private string[] positiveFlower3;

        public string[] PositiveFlower3
        {
            get { return positiveFlower3; }
            set
            {
                positiveFlower3 = value;
                RaisePropertyChanged();
            }
        }

        private string[] positiveFlower4;

        public string[] PositiveFlower4
        {
            get { return positiveFlower4; }
            set
            {
                positiveFlower4 = value;
                RaisePropertyChanged();
            }
        }

        private string[] positiveFlower5;

        public string[] PositiveFlower5
        {
            get { return positiveFlower5; }
            set
            {
                positiveFlower5 = value;
                RaisePropertyChanged();
            }
        }

        private string[] positiveFlower6;

        public string[] PositiveFlower6
        {
            get { return positiveFlower6; }
            set
            {
                positiveFlower6 = value;
                RaisePropertyChanged();
            }
        }

        private string positiveAngle;

        public string PositiveAngle
        {
            get { return positiveAngle; }
            set
            {
                positiveAngle = value;
                RaisePropertyChanged();
            }
        }

        private string positiveL2;

        public string PositiveL2
        {
            get { return positiveL2; }
            set
            {
                positiveL2 = value;
                RaisePropertyChanged();
            }
        }

        private string positiveFoilCrack;

        public string PositiveFoilCrack
        {
            get { return positiveFoilCrack; }
            set
            {
                positiveFoilCrack = value;
                RaisePropertyChanged();
            }
        }

        private string positivePinOffset;

        public string PositivePinOffset
        {
            get { return positivePinOffset; }
            set
            {
                positivePinOffset = value;
                RaisePropertyChanged();
            }
        }

        private int positiveFlowerNGNum;

        public int PositiveFlowerNGNum
        {
            get { return positiveFlowerNGNum; }
            set
            {
                positiveFlowerNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int positiveAngleNGNum;

        public int PositiveAngleNGNum
        {
            get { return positiveAngleNGNum; }
            set
            {
                positiveAngleNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int positiveL2NGNum;

        public int PositiveL2NGNum
        {
            get { return positiveL2NGNum; }
            set
            {
                positiveL2NGNum = value;
                RaisePropertyChanged();
            }
        }

        private int positiveFoilCrackNGNum;

        public int PositiveFoilCrackNGNum
        {
            get { return positiveFoilCrackNGNum; }
            set
            {
                positiveFoilCrackNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int positivePinOffsetNGNum;

        public int PositivePinOffsetNGNum
        {
            get { return positivePinOffsetNGNum; }
            set
            {
                positivePinOffsetNGNum = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region 成品
        private string productHeight;

        public string ProductHeight
        {
            get { return productHeight; }
            set
            {
                productHeight = value;
                RaisePropertyChanged();
            }
        }

        private string productDiameter;

        public string ProductDiameter
        {
            get { return productDiameter; }
            set
            {
                productDiameter = value;
                RaisePropertyChanged();
            }
        }

        private string productPinPitch;

        public string ProductPinPitch
        {
            get { return productPinPitch; }
            set
            {
                productPinPitch = value;
                RaisePropertyChanged();
            }
        }

        private string productDifferenceOfHeight;

        public string ProductDifferenceOfHeight
        {
            get { return productDifferenceOfHeight; }
            set
            {
                productDifferenceOfHeight = value;
                RaisePropertyChanged();
            }
        }


        private string productTopCPLine;

        public string ProductTopCPline
        {
            get { return productTopCPLine; }
            set
            {
                productTopCPLine = value;
                RaisePropertyChanged();
            }
        }

        private string productBottomCPLine;

        public string ProductBottomCPLine
        {
            get { return productBottomCPLine; }
            set
            {
                productBottomCPLine = value;
                RaisePropertyChanged();
            }
        }

        private int productHeightNGNum;

        public int ProductHeightNGNum
        {
            get { return productHeightNGNum; }
            set
            {
                productHeightNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int productDiameterNGNum;

        public int ProductDiameterNGNum
        {
            get { return productDiameterNGNum; }
            set
            {
                productDiameterNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int productPinPitchNGNum;

        public int ProductPinPitchNGNum
        {
            get { return productPinPitchNGNum; }
            set
            {
                productPinPitchNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int productDifferenceOfHeightNGNum;

        public int ProductDifferenceOfHeightNGNum
        {
            get { return productDifferenceOfHeightNGNum; }
            set
            {
                productDifferenceOfHeightNGNum = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 统计
        private int totalNum;

        public int TotalNum
        {
            get { return totalNum; }
            set
            {
                totalNum = value;
                RaisePropertyChanged();
            }
        }

        private int positiveDetectionCount;

        public int PositiveDetectionCount
        {
            get { return positiveDetectionCount; }
            set
            {
                positiveDetectionCount = value;
                RaisePropertyChanged();
            }
        }

        private int negativeDetectionCount;

        public int NegativeDetectionCount
        {
            get { return negativeDetectionCount; }
            set
            {
                negativeDetectionCount = value;
                RaisePropertyChanged();
            }
        }

        private int productDetectionCount;

        public int ProductDetectionCount
        {
            get { return productDetectionCount; }
            set
            {
                productDetectionCount = value;
                RaisePropertyChanged();
            }
        }

        private int positiveYield;

        public int PositiveYield
        {
            get { return positiveYield; }
            set
            {
                positiveYield = value;
                RaisePropertyChanged();
            }
        }

        private int negativeYield;

        public int NegativeYield
        {
            get { return negativeYield; }
            set
            {
                negativeYield = value;
                RaisePropertyChanged();
            }
        }

        private int productYield;

        public int ProductYield
        {
            get { return productYield; }
            set
            {
                productYield = value;
                RaisePropertyChanged();
            }
        }

        private int positiveNGCount;

        public int PositiveNGCount
        {
            get { return positiveNGCount; }
            set
            {
                positiveNGCount = value;
                RaisePropertyChanged();
            }
        }

        private int negativeNGCount;

        public int NegativeNGCount
        {
            get { return negativeNGCount; }
            set
            {
                negativeNGCount = value;
                RaisePropertyChanged();
            }
        }

        private int productNGCount;

        public int ProductNGCount
        {
            get { return productNGCount; }
            set
            {
                productNGCount = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using HalconDotNet;
using System.Configuration;

namespace CENJE_Vison.Models
{
    public class MainModel : ObservableObject
    {
        public string NseriesStr;//负极序列号
        public string PseriesStr;//正极序列号
        public string FseriesStr;//芯包成品序列号

        
        private bool _run_Flag;

        public bool Run_Flag
        {
            get => _run_Flag;
            set
            {
                _run_Flag = value;
                RaisePropertyChanged(() => Run_Flag);
            }
        }

        /// <summary>
        /// 负极图像
        /// </summary>
        private HImage nImage;
        
        public HImage NImage
        {
            get { return nImage; }
            set
            {
                nImage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极图像
        /// </summary>
        private HImage pImage;

        public HImage HImage
        {
            get { return pImage; }
            set
            {
                pImage = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 成品图像
        /// </summary>
        private HImage fImage;

        public HImage FImage
        {
            get { return fImage; }
            set
            {
                fImage = value;
                RaisePropertyChanged();
            }
        }

        #region 负极显示数据

        /// <summary>
        /// 四个花瓣面积
        /// </summary>
        private string area1N;

        public string Area1N
        {
            get { return area1N; }
            set
            {
                area1N = value;
                RaisePropertyChanged();
            }
        }

        private string area2N;

        public string Area2N
        {
            get { return area2N; }
            set
            {
                area2N = value;
                RaisePropertyChanged();
            }
        }

        private string area3N;

        public string Area3N
        {
            get { return area3N; }
            set
            {
                area3N = value;
                RaisePropertyChanged();
            }
        }

        private string area4N;

        public string Area4N
        {
            get { return area4N; }
            set
            {
                area4N = value;
                RaisePropertyChanged();
            }
        }

        private string area5N;

        public string Area5N
        {
            get { return area5N; }
            set
            {
                area5N = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极L2
        /// </summary>
        private string l2N;

        public string L2N
        {
            get { return l2N; }
            set
            {
                l2N = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极角度
        /// </summary>
        private string angleN;

        public string AngleN
        {
            get { return angleN; }
            set
            {
                angleN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极裂箔
        /// </summary>
        private int foilCrackN;

        public int FoilCrackN
        {
            get { return foilCrackN; }
            set
            {
                foilCrackN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极铆偏
        /// </summary>
        private string rivetOffsrtN;

        public string RivetOffsrtN
        {
            get { return rivetOffsrtN; }
            set
            {
                rivetOffsrtN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极花瓣NG个数
        /// </summary>
        private int flowerNGNumN;

        public int FlowerNGNumN
        {
            get { return flowerNGNumN; }
            set
            {
                flowerNGNumN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极L2NG
        /// </summary>
        private int l2NGNumN;

        public int L2NGNumN
        {
            get { return l2NGNumN; }
            set
            {
                l2NGNumN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极铆偏NG数目
        /// </summary>
        private int rivetOffestNGNumN;

        public int RivetOffestNGNumN
        {
            get { return rivetOffestNGNumN; }
            set
            {
                rivetOffestNGNumN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极裂箔NG数目
        /// </summary>
        private int foilCrackNGNumN;

        public int FoilCrackNGNumN
        {
            get { return foilCrackNGNumN; }
            set
            {
                foilCrackNGNumN = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 负极角度NG数目
        /// </summary>
        private int angleNGNumN;

        public int AngelNGNumN
        {
            get { return angleNGNumN; }
            set
            {
                angleNGNumN = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region 正极显示数据
        /// <summary>
        /// 四个花瓣面积
        /// </summary>
        private string area1P;

        public string Area1P
        {
            get { return area1P; }
            set
            {
                area1P = value;
                RaisePropertyChanged();
            }
        }

        private string area2P;

        public string Area2P
        {
            get { return area2P; }
            set
            {
                area2P = value;
                RaisePropertyChanged();
            }
        }

        private string area3P;

        public string Area3P
        {
            get { return area3P; }
            set
            {
                area3P = value;
                RaisePropertyChanged();
            }
        }

        private string area4P;

        public string Area4P
        {
            get { return area4P; }
            set
            {
                area4P = value;
                RaisePropertyChanged();
            }
        }

        private string area5P;

        public string Area5P
        {
            get { return area5P; }
            set
            {
                area5P = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极L2
        /// </summary>
        private string l2P;

        public string L2P
        {
            get { return l2P; }
            set
            {
                l2P = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极角度
        /// </summary>
        private string angleP;

        public string AngleP
        {
            get { return angleP; }
            set
            {
                angleP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极裂箔
        /// </summary>
        private int foilCrackP;

        public int FoilCrackP
        {
            get { return foilCrackP; }
            set
            {
                foilCrackP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极铆偏
        /// </summary>
        private string rivetOffsrtP;

        public string RivetOffsrtP
        {
            get { return rivetOffsrtP; }
            set
            {
                rivetOffsrtP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极花瓣NG个数
        /// </summary>
        private int flowerNGNumP;

        public int FlowerNGNumP
        {
            get { return flowerNGNumP; }
            set
            {
                flowerNGNumP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极L2NG
        /// </summary>
        private int l2NGNumP;

        public int L2NGNumP
        {
            get { return l2NGNumP; }
            set
            {
                l2NGNumP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极铆偏NG数目
        /// </summary>
        private int rivetOffestNGNumP;

        public int RivetOffestNGNumP
        {
            get { return rivetOffestNGNumP; }
            set
            {
                rivetOffestNGNumP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极裂箔NG数目
        /// </summary>
        private int foilCrackNGNumP;

        public int FoilCrackNGNumP
        {
            get { return foilCrackNGNumP; }
            set
            {
                foilCrackNGNumP = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 正极角度NG数目
        /// </summary>
        private int angleNGNumP;

        public int AngelNGNumP
        {
            get { return angleNGNumP; }
            set
            {
                angleNGNumP = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region 心苞成品显示数据
        /// <summary>
        /// 芯包高度
        /// </summary>
        private string height;

        public string Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 芯包宽度
        /// </summary>
        private string width;

        public string Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 脚距
        /// </summary>
        private string pinPitch;

        public string PinPitch
        {
            get { return pinPitch; }
            set
            {
                pinPitch = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 高低脚
        /// </summary>
        private string differenceOfHeight;

        public string DifferenceOfHeight
        {
            get { return differenceOfHeight; }
            set
            {
                differenceOfHeight = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 高度NG数目
        /// </summary>
        private int heightNGNum;

        public int HeightNGNum
        {
            get { return heightNGNum; }
            set
            {
                heightNGNum = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 宽度NG数目
        /// </summary>
        private int widthNGNum;

        public int WidthNGNum
        {
            get { return widthNGNum; }
            set
            {
                widthNGNum = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 脚距NG数目
        /// </summary>
        private int pinPitchNGNum;

        public int PinPitchNGNum
        {
            get { return pinPitchNGNum; }
            set
            {
                pinPitchNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int differenceOfHeightNGNum;

        /// <summary>
        /// 高低脚NG数目
        /// </summary>
        public int DifferenceOfHeightNGNum
        {
            get { return differenceOfHeightNGNum; }
            set
            {
                differenceOfHeightNGNum = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        #region 统计数据

        private int totalNGNum;

        public int TotalNGNum
        {
            get { return totalNGNum; }
            set
            {
                totalNGNum = value;
                RaisePropertyChanged();
            }
        }

        private int totalDetectionNumN;

        public int TotalDetectionNumN
        {
            get { return totalDetectionNumN; }
            set
            {
                totalDetectionNumN = value;
                RaisePropertyChanged();
            }
        }

        private int totalDetectionNumP;

        public int TotalDetectionNumP
        {
            get { return totalDetectionNumP; }
            set
            {
                totalDetectionNumP = value;
                RaisePropertyChanged();
            }
        }

        private int totalDetectionNumF;

        public int TotalDetectionNumF
        {
            get { return totalDetectionNumF; }
            set
            {
                totalDetectionNumF = value;
                RaisePropertyChanged();
            }
        }


        private int totalNGNumN;

        public int TotalNGNumN
        {
            get { return totalNGNumN; }
            set
            {
                totalNGNumN = value;
                RaisePropertyChanged();
            }
        }

        private int totalNGNumP;

        public int TotalNGNumP
        {
            get { return totalNGNumP; }
            set
            {
                totalNGNumP = value;
                RaisePropertyChanged();
            }
        }

        private int totalNGNumF;

        public int TotalNGNumF
        {
            get { return totalNGNumF; }
            set
            {
                totalNGNumF = value;
                RaisePropertyChanged();
            }
        }

        private float yieldN;

        public float YieldN
        {
            get { return yieldN; }
            set
            {
                yieldN = value;
                RaisePropertyChanged();
            }
        }

        private float yieldP;

        public float YieldP
        {
            get { return yieldP; }
            set
            {
                yieldP = value;
                RaisePropertyChanged();
            }
        }

        private float yieldF;

        public float YieldF
        {
            get { return yieldF; }
            set
            {
                yieldF = value;
                RaisePropertyChanged();
            }
        }

        #endregion
    }

}

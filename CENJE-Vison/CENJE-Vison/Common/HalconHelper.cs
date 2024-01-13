using System;
using System.Collections.Generic;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace CENJE_Vison.Common
{
    public class HalconHelper
    {
        /// <summary>
        /// 在halcon窗口显示图像、区域
        /// </summary>
        public void ShowImageWnd(HObject ho_image, HTuple hv_halconWindow)
        {
            HTuple nWidth = null, nHeight = null;
            if (ho_image.IsInitialized() && hv_halconWindow != null)
            {
                HOperatorSet.GetImageSize(ho_image, out nWidth, out nHeight);
                HOperatorSet.SetPart(hv_halconWindow, 0, 0, nHeight, nWidth);
                HOperatorSet.DispObj(ho_image, hv_halconWindow);
            }
            //nWidth.Dispose(); nHeight.Dispose();

        }

        /// <summary>
        /// 显示中心线程序
        /// </summary>
        public void ShowConterCross(HTuple hv_HalconWindow, HTuple nHeight, HTuple nWidth)
        {

            HObject ViewCenterCross;
            HTuple MaxLong;
            HOperatorSet.SetColor(hv_HalconWindow, "orange");
            if (nWidth > nHeight)
                MaxLong = nWidth;
            else
                MaxLong = nHeight;
            HOperatorSet.GenCrossContourXld(out ViewCenterCross, nHeight[0] / 2.0, nWidth[0] / 2.0, MaxLong, (HTuple)0);
            HOperatorSet.DispObj(ViewCenterCross, hv_HalconWindow);

        }

        /// <summary>
        /// 保存图像
        /// </summary>
        public void SaveImage(HObject ho_Image, string strPath, string strVision, int nSaveImageType = 1)
        {
            try
            {
                switch (nSaveImageType)
                {
                    case 0:
                        if (ho_Image.IsInitialized())
                        {
                            //strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff")}.jpg";
                            strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff  ")}" + $"{strVision}";
                            HOperatorSet.WriteImage(ho_Image, "jpeg", 255, strPath);
                        }
                        break;

                    case 1:
                        if (ho_Image.IsInitialized())
                        {
                            //strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff")}.bmp";
                            strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff  ")}" + $"{strVision}";
                            HOperatorSet.WriteImage(ho_Image, "bmp", 255, strPath);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {

            }



        }

        /// <summary>
        /// 窗口截图
        /// </summary>
        public void SaveNgImage(HTuple hv_HalconWindow, string strPath, string strVision, int nSaveImageType = 1)
        {
            HObject ho_Image = new HObject();
            HOperatorSet.GenEmptyObj(out ho_Image);
            try
            {
                HOperatorSet.DumpWindowImage(out ho_Image, hv_HalconWindow);
                switch (nSaveImageType)
                {
                    case 0:
                        if (hv_HalconWindow != null && strPath != null)
                        {
                            //strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff")}.jpg";
                            strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff  ")}" + $"{strVision}";
                            HOperatorSet.WriteImage(ho_Image, "jpeg", 255, strPath);
                        }
                        break;
                    case 1:
                        if (hv_HalconWindow != null && strPath != null)
                        {
                            //strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff")}.jpg";
                            strPath = strPath + $"\\{DateTime.Now.ToString("yy-MM-dd HH-mm-ss-ff  ")}" + $"{strVision}";
                            HOperatorSet.WriteImage(ho_Image, "bmp", 255, strPath);

                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception)
            {
            }
            ho_Image.Dispose();
        }

        /// <summary>
        /// 画ROI
        /// </summary>
        public void DrawRoi(HTuple hv_HalconWindow)
        {

        }

        

    }
}

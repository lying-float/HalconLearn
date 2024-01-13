using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace CENJE_Vison.Common
{
    public class HalFunc
    {
        public bool PositiveDetection(HObject ho_Image, HTuple hv_WindowHandler,MinMaxSetting.PositiveTolerance Setting)
        {
            #region 参数设置
            HTuple hv_mmpp = 0.013; //像素当量

            //检测上下限范围
            HTuple hv_areaMax = Setting.AreaMaxP;//面积
            HTuple hv_areaMin = Setting.AreaMinP;
            HTuple hv_L2Max = Setting.L2MaxP;//L2
            HTuple hv_L2Min = Setting.L2MinP;
            HTuple hv_AngelMax = Setting.AngleMaxP;//角度
            HTuple hv_AngelMin = Setting.AngleMinP;
            HTuple FoilCrackMax = Setting.FoilCrackMaxP;//箔裂
            HTuple FoilCrackMin = Setting.FoilCrackMinP;
            HTuple RivetOffsrtMax = Setting.RivetOffsrtMaxP;//铆偏
            HTuple RivetOffsrtMin = Setting.RivetOffsrtMinP;

            //划分roi区域
            //花瓣roi
            HTuple hv_FlowersRow = 369;
            HTuple hv_FlowersColumn = 569;
            HTuple hv_FlowersPhi = 0;
            HTuple hv_FlowersLength1 = 42;
            HTuple hv_FlowersLength2 = 110;
            HObject ho_FlowersRoi;HOperatorSet.GenEmptyObj(out ho_FlowersRoi);

            HOperatorSet.GenRectangle2(out ho_FlowersRoi, hv_FlowersRow, hv_FlowersColumn,
                hv_FlowersPhi, hv_FlowersLength1, hv_FlowersLength2);

            //L2位置ROI
            HTuple hv_RowL2 = 529;
            HTuple hv_ColumnL2 = 565;
            HTuple hv_PhiL2 = -0;
            HTuple hv_Length1L2 = 128.0;
            HTuple hv_Length2L2 = 40;

            //下边缘线ROI矩形参数
            HTuple hv_RowLine = 505;
            HTuple hv_ColumnLine = 573;
            HTuple hv_PhiLine = -0;
            HTuple hv_Length1Line = 370;
            HTuple hv_Length2Line = 35;

            //导针直线ROI
            HTuple hv_RowPin = 859;
            HTuple hv_ColumnPin = 578;
            HTuple hv_PhiPin = -0;
            HTuple hv_Length1Pin = 62;
            HTuple hv_Length2Pin = 152;

            //读取花瓣模板
            HTuple hv_FlowersModelID = new HTuple();
            HOperatorSet.ReadShapeModel("D:/CENJE-Vison/Model/FlowersModel.shm", out hv_FlowersModelID);
            HObject ho_FlowersModelContours = new HObject();
            HOperatorSet.GetShapeModelContours(out ho_FlowersModelContours, hv_FlowersModelID,
                1);
            #endregion

            #region 定位
            //查找花瓣位置
            HTuple hv_RowFind = new HTuple();
            HTuple hv_ColumnFind = new HTuple();
            HTuple hv_AngleFind = new HTuple();
            HTuple hv_Score = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.FindShapeModel(ho_Image, hv_FlowersModelID, -((new HTuple(15)).TupleRad()
                    ), (new HTuple(30)).TupleRad(), 0.5, 1, 0.5, "least_squares", 0, 0.9, out hv_RowFind,
                    out hv_ColumnFind, out hv_AngleFind, out hv_Score);
            }
            HTuple hv_HomMat2D=new HTuple();
            HOperatorSet.VectorAngleToRigid(hv_FlowersRow, hv_FlowersColumn, hv_FlowersPhi,
                hv_RowFind, hv_ColumnFind, hv_AngleFind, out hv_HomMat2D);
            HObject ho_RegionAffineTrans=new HObject();
            HOperatorSet.AffineTransRegion(ho_FlowersRoi, out ho_RegionAffineTrans, hv_HomMat2D,
                "nearest_neighbor");
            ho_FlowersRoi.Dispose();//释放未仿射变换的花瓣roi

            //roi仿射变换
            HTuple hv_PhiTransL = new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_PhiTransL = hv_PhiLine + (hv_AngleFind - hv_FlowersPhi);
            }
            HTuple hv_RowTransL=new HTuple();
            HTuple hv_ColTransL = new HTuple();
            HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowLine, hv_ColumnLine, out hv_RowTransL,
                out hv_ColTransL);
            hv_RowLine.Dispose(); hv_ColumnLine.Dispose();

            HTuple hv_PhiTransL2=new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_PhiTransL2 = hv_PhiL2 + (hv_AngleFind - hv_FlowersPhi);
            }

            HTuple hv_RowTransL2=new HTuple();
            HTuple hv_ColTransL2=new HTuple();
            HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowL2, hv_ColumnL2, out hv_RowTransL2,
                out hv_ColTransL2);
            hv_RowL2.Dispose(); hv_ColumnL2.Dispose();

            HTuple hv_PhiTransPin=new HTuple();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_PhiTransPin = hv_PhiPin + (hv_AngleFind - hv_FlowersPhi);
            }

            HTuple hv_RowTransP=new HTuple();
            HTuple hv_ColTransP=new HTuple();
            HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowPin, hv_ColumnPin, out hv_RowTransP,
                out hv_ColTransP);
            hv_RowPin.Dispose(); hv_ColumnPin.Dispose();
            #endregion

            #region 检测
            HObject ho_ImageFlowers;HOperatorSet.GenEmptyObj(out ho_ImageFlowers);
            HOperatorSet.ReduceDomain(ho_Image, ho_RegionAffineTrans, out ho_ImageFlowers
                );

            HObject ho_Region; HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.Threshold(ho_ImageFlowers, out ho_Region, 145, 255);

             HObject ho_RegionClosing; HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 4.5);

            HObject ho_RegionDifference; HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.Difference(ho_ImageFlowers, ho_RegionClosing, out ho_RegionDifference
                );

            HObject ho_RegionOpening; HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening, 4.5);
            ho_RegionDifference.Dispose();

            HObject ho_ConnectedRegions; HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
            ho_RegionOpening.Dispose();

            HObject ho_SelectedRegions; HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "circularity",
                "and", 0.7, 1);
            ho_ConnectedRegions.Dispose();

            HTuple hv_Number = new HTuple();
            HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);

            HObject ho_SortedRegions; HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "first_point",
                "true", "row");
            ho_SelectedRegions.Dispose();

            HTuple hv_Area = new HTuple();
            HTuple hv_Row = new HTuple();
            HTuple hv_Column = new HTuple();
            HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area, out hv_Row, out hv_Column);

            //生成十字线切割花瓣
            HObject ho_RegionLinesH; HOperatorSet.GenEmptyObj(out ho_RegionLinesH);
            HObject ho_RegionLinesV; HOperatorSet.GenEmptyObj(out ho_RegionLinesV);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.GenRegionLine(out ho_RegionLinesH, hv_Row, hv_Column - 35, hv_Row,
                    hv_Column + 35);
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.GenRegionLine(out ho_RegionLinesV, hv_Row - 35, hv_Column, hv_Row + 35,
                    hv_Column);
            }
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Connection(ho_RegionLinesH, out ExpTmpOutVar_0);
                ho_RegionLinesH.Dispose();
                ho_RegionLinesH = ExpTmpOutVar_0;
            }
            {
                HObject ExpTmpOutVar_0;
                HOperatorSet.Connection(ho_RegionLinesV, out ExpTmpOutVar_0);
                ho_RegionLinesV.Dispose();
                ho_RegionLinesV = ExpTmpOutVar_0;
            }
            //FlowerCenterColumn
            HTuple hv_FCC = new HTuple();
            //FlowerCenterRow
            HTuple hv_FCR = new HTuple();
            //花瓣1四朵花瓣面积
            HTuple hv_FlowerArea1 = new HTuple();
            hv_FlowerArea1[0] = 0;
            hv_FlowerArea1[1] = 0;
            hv_FlowerArea1[2] = 0;
            hv_FlowerArea1[3] = 0;
            //花瓣2四朵花瓣面积
            HTuple hv_FlowerArea2 = new HTuple();
            hv_FlowerArea2[0] = 0;
            hv_FlowerArea2[1] = 0;
            hv_FlowerArea2[2] = 0;
            hv_FlowerArea2[3] = 0;
            //花瓣3四朵花瓣面积
            HTuple hv_FlowerArea3 = new HTuple();
            hv_FlowerArea3[0] = 0;
            hv_FlowerArea3[1] = 0;
            hv_FlowerArea3[2] = 0;
            hv_FlowerArea3[3] = 0;
            HTuple end_val115 = hv_Number - 1;
            HTuple step_val115 = 1;
            for (HTuple hv_i = 0; hv_i.Continue(end_val115, step_val115); hv_i = hv_i.TupleAdd(step_val115))
            {
                HObject ho_Rectangle;HOperatorSet.GenEmptyObj(out ho_Rectangle);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row.TupleSelect(hv_i), hv_Column.TupleSelect(
                        hv_i), hv_FlowersPhi, hv_FlowersLength1 - 15, hv_FlowersLength2 / 3);
                }
                HObject ho_ImageFlower1; HOperatorSet.GenEmptyObj(out ho_ImageFlower1);
                HOperatorSet.ReduceDomain(ho_ImageFlowers, ho_Rectangle, out ho_ImageFlower1
                    );
                HObject ho_RegionFlower1; HOperatorSet.GenEmptyObj(out ho_RegionFlower1);
                HOperatorSet.Threshold(ho_ImageFlower1, out ho_RegionFlower1, 148, 255);
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.OpeningCircle(ho_RegionFlower1, out ExpTmpOutVar_0, 3.5);
                    ho_RegionFlower1.Dispose();
                    ho_RegionFlower1 = ExpTmpOutVar_0;
                }
                HObject ho_RegionDifference1; HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
                HOperatorSet.Difference(ho_RegionFlower1, ho_RegionLinesH, out ho_RegionDifference1
                    );
                {
                    HObject ExpTmpOutVar_0;
                    HOperatorSet.Difference(ho_RegionDifference1, ho_RegionLinesV, out ExpTmpOutVar_0
                        );
                    ho_RegionDifference1.Dispose();
                    ho_RegionDifference1 = ExpTmpOutVar_0;
                }
                HObject ho_SelectedRegions1; HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
                HOperatorSet.SelectShape(ho_RegionDifference1, out ho_SelectedRegions1, "area",
                    "and", 150, 99999);
                HObject ho_ConnectedRegions1; HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
                HOperatorSet.Connection(ho_SelectedRegions1, out ho_ConnectedRegions1);
                HObject ho_SortedRegionsFlower; HOperatorSet.GenEmptyObj(out ho_SortedRegionsFlower);
                HOperatorSet.SortRegion(ho_ConnectedRegions1, out ho_SortedRegionsFlower,
                    "character", "true", "row");

                HObject ho_ObjectSelected; HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
                HTuple hv_AreaF = null, hv_Row1 = null, hv_Column1 = null;
                for (HTuple hv_j = 0; (int)hv_j <= 3; hv_j = (int)hv_j + 1)
                {
                    switch (hv_i.I)
                    {
                        case 0:
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ObjectSelected.Dispose();
                                HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                    hv_j + 1);
                            }
                            hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                            HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                out hv_Column1);
                            if (hv_FlowerArea1 == null)
                                hv_FlowerArea1 = new HTuple();
                            hv_FlowerArea1[hv_j] = hv_AreaF;
                            break;
                        case 1:
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ObjectSelected.Dispose();
                                HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                    hv_j + 1);
                            }
                            hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                            HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                out hv_Column1);
                            if (hv_FlowerArea2 == null)
                                hv_FlowerArea2 = new HTuple();
                            hv_FlowerArea2[hv_j] = hv_AreaF;
                            break;
                        case 2:
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_ObjectSelected.Dispose();
                                HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                    hv_j + 1);
                            }
                            hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                            HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                out hv_Column1);
                            if (hv_FlowerArea3 == null)
                                hv_FlowerArea3 = new HTuple();
                            hv_FlowerArea3[hv_j] = hv_AreaF;
                            break;
                    }
                }
                hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                ho_ObjectSelected.Dispose(); ho_SortedRegionsFlower.Dispose();
            }

            //找箔边缘
            HTuple hv_MeasureHandle=new HTuple(),hv_Width=new HTuple(), hv_Height=new HTuple();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.GenMeasureRectangle2(hv_RowTransL, hv_ColTransL, hv_PhiTransL,
                hv_Length1Line, hv_Length2Line, hv_Width, hv_Height, "nearest_neighbor",
                out hv_MeasureHandle);
            HTuple hv_RowEdgeLine = new HTuple(), hv_ColumnEdgeLine = new HTuple(), hv_AmplitudeLine = new HTuple(),
                hv_DistanceLine = new HTuple();
            HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, 2, 20, "positive", "first",
                out hv_RowEdgeLine, out hv_ColumnEdgeLine, out hv_AmplitudeLine, out hv_DistanceLine);

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HOperatorSet.DispLine(hv_WindowHandle, hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                    )), hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())), hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()
                    )), hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos())));
            }
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                HObject ho_ContourLine; HOperatorSet.GenEmptyObj(out ho_ContourLine);
                HOperatorSet.GenContourPolygonXld(out ho_ContourLine, ((hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                    )))).TupleConcat(hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()))),
                    ((hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())))).TupleConcat(
                    hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos()))));
            }
            #endregion


            return true;
        }



        public bool NegativeDetection(HObject ho_image, HTuple hv_windowHandler, MinMaxSetting.NegativeTolerance Setting)
        {

            return true;
        }
        public bool FinishDetection(HObject ho_image, HTuple hv_windowHandler, MinMaxSetting.FinishTolerance Setting)
        {

            return true;
        }
    }
    
}

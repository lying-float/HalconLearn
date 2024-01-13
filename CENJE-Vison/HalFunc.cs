using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using CENJE_Vison.Properties;
using HalconDotNet;
using static System.Net.Mime.MediaTypeNames;
using static CENJE_Vison.Common.DetectionSetting;


namespace CENJE_Vison.Common
{
    public class HalFunc
    {
        public void HPositiveDetection(HObject ho_Image, HTuple hv_WindowHandler,MinMaxSetting.PositiveTolerance setting,DetectionSetting.PositiveDetection detection,out bool error)
        {
            error = false;
            if (!ho_Image.IsInitialized()||hv_WindowHandler==null) return;
            #region 
            #region 参数初始化
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject  ho_FlowersRoi, ho_FlowersModelContours;
            HObject ho_RegionAffineTrans = null, ho_ImageFlowers = null;
            HObject ho_Region = null, ho_RegionClosing = null, ho_RegionDifference = null;
            HObject ho_RegionOpening = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null, ho_SortedRegions = null;
            HObject ho_RegionLinesH = null, ho_RegionLinesV = null, ho_Rectangle = null;
            HObject ho_ImageFlower1 = null, ho_RegionFlower1 = null, ho_RegionDifference1 = null;
            HObject ho_SelectedRegions1 = null, ho_ConnectedRegions1 = null;
            HObject ho_SortedRegionsFlower = null, ho_ObjectSelected = null;
            HObject ho_RivetOffsetLines = null, ho_RivetOffsetContours = null;
            HObject ho_ContourLine = null, ho_RegionLeft = null, ho_RegionRight = null;
            HObject ho_RegionDown = null, ho_RegionPin = null;


            // Local control variables 

            HTuple hv_ImageFiles = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(); //hv_WindowHandle = new HTuple();
            HTuple hv_FlowersRow = new HTuple(), hv_FlowersColumn = new HTuple();
            HTuple hv_FlowersPhi = new HTuple(), hv_FlowersLength1 = new HTuple();
            HTuple hv_FlowersLength2 = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_mmpp = new HTuple(), hv_areaMax = new HTuple();
            HTuple hv_areaMin = new HTuple(), hv_L2Max = new HTuple();
            HTuple hv_L2Min = new HTuple(), hv_AngelMax = new HTuple();
            HTuple hv_AngelMin = new HTuple(), hv_RowL2 = new HTuple();
            HTuple hv_ColumnL2 = new HTuple(), hv_PhiL2 = new HTuple();
            HTuple hv_Length1L2 = new HTuple(), hv_Length2L2 = new HTuple();
            HTuple hv_RowLine = new HTuple(), hv_ColumnLine = new HTuple();
            HTuple hv_PhiLine = new HTuple(), hv_Length1Line = new HTuple();
            HTuple hv_Length2Line = new HTuple(), hv_RowPin = new HTuple();
            HTuple hv_ColumnPin = new HTuple(), hv_PhiPin = new HTuple();
            HTuple hv_Length1Pin = new HTuple(), hv_Length2Pin = new HTuple();
            HTuple hv_FlowersModelID = new HTuple(), hv_RowFind = new HTuple();
            HTuple hv_ColumnFind = new HTuple(), hv_AngleFind = new HTuple();
            HTuple hv_Score = new HTuple(), hv_HomMat2D = new HTuple();
            HTuple hv_Index = new HTuple(), hv_PhiTransL = new HTuple();
            HTuple hv_RowTransL = new HTuple(), hv_ColTransL = new HTuple();
            HTuple hv_PhiTransL2 = new HTuple(), hv_RowTransL2 = new HTuple();
            HTuple hv_ColTransL2 = new HTuple(), hv_PhiTransPin = new HTuple();
            HTuple hv_RowTransP = new HTuple(), hv_ColTransP = new HTuple();
            HTuple hv_Number = new HTuple(), hv_Area = new HTuple();
            HTuple hv_FCC = new HTuple(), hv_FCR = new HTuple(), hv_FlowerArea1 = new HTuple();
            HTuple hv_FlowerArea2 = new HTuple(), hv_FlowerArea3 = new HTuple();
            HTuple hv_FlowerArea4 = new HTuple(), hv_FlowerArea5 = new HTuple();
            HTuple hv_i = new HTuple(), hv_j = new HTuple(), hv_AreaF = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Ra = new HTuple(), hv_Rb = new HTuple(), hv_MeasureHandle = new HTuple();
            HTuple hv_RowEdgeLine = new HTuple(), hv_ColumnEdgeLine = new HTuple();
            HTuple hv_AmplitudeLine = new HTuple(), hv_DistanceLine = new HTuple();
            HTuple hv_MeasureHandleL = new HTuple(), hv_RowEdgeFirstL = new HTuple();
            HTuple hv_ColumnEdgeFirstL = new HTuple(), hv_AmplitudeFirstL = new HTuple();
            HTuple hv_RowEdgeSecondL = new HTuple(), hv_ColumnEdgeSecondL = new HTuple();
            HTuple hv_AmplitudeSecondL = new HTuple(), hv_IntraDistance1 = new HTuple();
            HTuple hv_InterDistance1 = new HTuple(), hv_RowEdgeD = new HTuple();
            HTuple hv_ColumnEdgeD = new HTuple(), hv_AmplitudeD = new HTuple();
            HTuple hv_DistanceD = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_mDistance = new HTuple(), hv_MeasureHandlePin = new HTuple();
            HTuple hv_RowEdgePin = new HTuple(), hv_ColumnEdgePin = new HTuple();
            HTuple hv_AmplitudePin = new HTuple(), hv_Distance1 = new HTuple();
            HTuple hv_RadPin = new HTuple(), hv_AnglePin = new HTuple();
            HTuple hv_DistanceRivetOffset1 = new HTuple(), hv_DistanceRivetOffset2 = new HTuple();
            HTuple hv_DistanceRivetOffset = new HTuple(), hv_NumberPetal = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_FlowersRoi);
            HOperatorSet.GenEmptyObj(out ho_FlowersModelContours);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageFlowers);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesH);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesV);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageFlower1);
            HOperatorSet.GenEmptyObj(out ho_RegionFlower1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsFlower);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RivetOffsetLines);
            HOperatorSet.GenEmptyObj(out ho_RivetOffsetContours);
            HOperatorSet.GenEmptyObj(out ho_ContourLine);
            HOperatorSet.GenEmptyObj(out ho_RegionLeft);
            HOperatorSet.GenEmptyObj(out ho_RegionRight);
            HOperatorSet.GenEmptyObj(out ho_RegionDown);
            HOperatorSet.GenEmptyObj(out ho_RegionPin);

            #endregion

            //HTuple hv_ErrorType = new HTuple();
            //if (hv_ErrorType == null)
            //{
            //    hv_ErrorType = new HTuple();
            //}
            //hv_ErrorType[0] = "定位失败";
            //hv_ErrorType[1] = "L2";
            //hv_ErrorType[2] = "角度";
            //hv_ErrorType[3] = "裂箔";
            //hv_ErrorType[4] = "铆偏";
            //hv_ErrorType[5] = "极花1";
            //hv_ErrorType[6] = "极花2";
            //hv_ErrorType[7] = "极花3";
            //hv_ErrorType[8] = "极花4";
            //hv_ErrorType[9] = "极花5";

            HOperatorSet.ClearWindow(hv_WindowHandler);
            HOperatorSet.SetLineWidth(hv_WindowHandler, 2);
            HOperatorSet.SetDraw(hv_WindowHandler, "margin");
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.SetWindowAttr("background_color", "black");
            //HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "visible", "", out hv_WindowHandler);
            HOperatorSet.SetPart(hv_WindowHandler, 0, 0, hv_Height, hv_Width);
            //HDevWindowStack.Push(hv_WindowHandler);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }

            #region 默认ROI参数与定位模板读取
            //划分roi区域
            //花瓣roi
            //draw_rectangle2 (WindowHandle, FlowersRow, FlowersColumn, FlowersPhi, FlowersLength1, FlowersLength2))
            hv_FlowersRow.Dispose();
            hv_FlowersRow = 369;
            hv_FlowersColumn.Dispose();
            hv_FlowersColumn = 569;
            hv_FlowersPhi.Dispose();
            hv_FlowersPhi = 0;
            hv_FlowersLength1.Dispose();
            hv_FlowersLength1 = 42;
            hv_FlowersLength2.Dispose();
            hv_FlowersLength2 = 110;
            ho_FlowersRoi.Dispose();
            HOperatorSet.GenRectangle2(out ho_FlowersRoi, hv_FlowersRow, hv_FlowersColumn,
                hv_FlowersPhi, hv_FlowersLength1, hv_FlowersLength2);
            //************************
            //计算像素当量
            //draw_rectangle2 (WindowHandle, Row, Column, Phi, Length1, Length2)
            //gen_measure_rectangle2 (Row, Column, Phi, Length1, Length2, Width, Height, 'nearest_neighbor', MeasureHandlemmpp)
            //measure_pairs (Image, MeasureHandlemmpp, 2, 30, 'all', 'all', RowEdgeFirst, ColumnEdgeFirst, AmplitudeFirst, RowEdgeSecond, ColumnEdgeSecond, AmplitudeSecond, IntraDistance, InterDistance)
            //mm := 2.7
            //mmpp := mm/IntraDistance
            hv_mmpp.Dispose();
            hv_mmpp = 0.013;
            //公差范围
            hv_areaMax.Dispose();
            hv_areaMax = setting.AreaMaxP;
            hv_areaMin.Dispose();
            hv_areaMin = setting.AreaMinP;
            hv_L2Max.Dispose();
            hv_L2Max = setting.L2MaxP;
            hv_L2Min.Dispose();
            hv_L2Min = setting.L2MinP;
            hv_AngelMax.Dispose();
            hv_AngelMax = setting.AngleMaxP;
            hv_AngelMin.Dispose();
            hv_AngelMin = setting.AngleMinP;
            //*************************
            //*************************
            //L2位置
            //L2位置ROI
            hv_RowL2.Dispose();
            hv_RowL2 = 529;
            hv_ColumnL2.Dispose();
            hv_ColumnL2 = 565;
            hv_PhiL2.Dispose();
            hv_PhiL2 = -0;
            hv_Length1L2.Dispose();
            hv_Length1L2 = 128.0;
            hv_Length2L2.Dispose();
            hv_Length2L2 = 40;
            //下边缘线ROI矩形参数
            hv_RowLine.Dispose();
            hv_RowLine = 505;
            hv_ColumnLine.Dispose();
            hv_ColumnLine = 573;
            hv_PhiLine.Dispose();
            hv_PhiLine = -0;
            hv_Length1Line.Dispose();
            hv_Length1Line = 370;
            hv_Length2Line.Dispose();
            hv_Length2Line = 35;
            //导针直线ROI
            //draw_rectangle2 (WindowHandle, RowPin, ColumnPin, PhiPin, Length1Pin, Length2Pin)
            hv_RowPin.Dispose();
            hv_RowPin = 859;
            hv_ColumnPin.Dispose();
            hv_ColumnPin = 578;
            hv_PhiPin.Dispose();
            hv_PhiPin = -0;
            hv_Length1Pin.Dispose();
            hv_Length1Pin = 62;
            hv_Length2Pin.Dispose();
            hv_Length2Pin = 152;

            //**************************
            //读取花瓣轮廓模板
            hv_FlowersModelID.Dispose();
            HOperatorSet.ReadShapeModel("D:/CENJE-Vison/Model/FlowersModel.shm", out hv_FlowersModelID);
            ho_FlowersModelContours.Dispose();
            HOperatorSet.GetShapeModelContours(out ho_FlowersModelContours, hv_FlowersModelID,
                1);

            #endregion

            try
            {
                #region 执行检测
                if (HDevWindowStack.IsOpen())
                {
                    //HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    //ho_Image.Dispose();
                    //HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                //查找花瓣位置
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowFind.Dispose(); hv_ColumnFind.Dispose(); hv_AngleFind.Dispose(); hv_Score.Dispose();
                    HOperatorSet.FindShapeModel(ho_Image, hv_FlowersModelID, -((new HTuple(15)).TupleRad()
                        ), (new HTuple(30)).TupleRad(), 0.6, 1, 0.5, "least_squares", 4, 0.8, out hv_RowFind,
                        out hv_ColumnFind, out hv_AngleFind, out hv_Score);
                }
                if (hv_Score<0.8)
                {
                    error = true;
                    disp_message(hv_WindowHandler, "定位失败", "image", 0, 0, "red", "false");
                    return;
                }
                hv_HomMat2D.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_FlowersRow, hv_FlowersColumn, hv_FlowersPhi,
                    hv_RowFind, hv_ColumnFind, hv_AngleFind, out hv_HomMat2D);
                ho_RegionAffineTrans.Dispose();
                HOperatorSet.AffineTransRegion(ho_FlowersRoi, out ho_RegionAffineTrans, hv_HomMat2D,
                    "nearest_neighbor");

                //roi仿射变换
                hv_PhiTransL.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PhiTransL = hv_PhiLine + (hv_AngleFind - hv_FlowersPhi);
                }
                hv_RowTransL.Dispose(); hv_ColTransL.Dispose();
                HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowLine, hv_ColumnLine, out hv_RowTransL,
                    out hv_ColTransL);
                hv_PhiTransL2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PhiTransL2 = hv_PhiL2 + (hv_AngleFind - hv_FlowersPhi);
                }
                hv_RowTransL2.Dispose(); hv_ColTransL2.Dispose();
                HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowL2, hv_ColumnL2, out hv_RowTransL2,
                    out hv_ColTransL2);
                hv_PhiTransPin.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PhiTransPin = hv_PhiPin + (hv_AngleFind - hv_FlowersPhi);
                }
                hv_RowTransP.Dispose(); hv_ColTransP.Dispose();
                HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowPin, hv_ColumnPin, out hv_RowTransP,
                    out hv_ColTransP);

                //花瓣检测
                ho_ImageFlowers.Dispose(); 
                HOperatorSet.ReduceDomain(ho_Image, ho_RegionAffineTrans, out ho_ImageFlowers
                    );
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageFlowers, out ho_Region, detection.FlowerThrehold, 255);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 4.5);//
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_ImageFlowers, ho_RegionClosing, out ho_RegionDifference
                    );
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening, 4.5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "circularity",
                    "and", 0.7, 1);
                hv_Number.Dispose();
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                ho_SortedRegions.Dispose();
                HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "first_point",
                    "true", "row");
                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();
                HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area, out hv_Row, out hv_Column);
                //生成十字线切割花瓣
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionLinesH.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLinesH, hv_Row, hv_Column - 38, hv_Row,
                        hv_Column + 38);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionLinesV.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLinesV, hv_Row - 38, hv_Column, hv_Row + 38,
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
                hv_FCC.Dispose();
                hv_FCC = new HTuple();
                //FlowerCenterRow
                hv_FCR.Dispose();
                hv_FCR = new HTuple();
                //花瓣1四朵花瓣面积
                hv_FlowerArea1.Dispose();
                hv_FlowerArea1 = new HTuple();
                hv_FlowerArea1[0] = 0;
                hv_FlowerArea1[1] = 0;
                hv_FlowerArea1[2] = 0;
                hv_FlowerArea1[3] = 0;
                //花瓣2四朵花瓣面积
                hv_FlowerArea2.Dispose();
                hv_FlowerArea2 = new HTuple();
                hv_FlowerArea2[0] = 0;
                hv_FlowerArea2[1] = 0;
                hv_FlowerArea2[2] = 0;
                hv_FlowerArea2[3] = 0;
                //花瓣3四朵花瓣面积
                hv_FlowerArea3.Dispose();
                hv_FlowerArea3 = new HTuple();
                hv_FlowerArea3[0] = 0;
                hv_FlowerArea3[1] = 0;
                hv_FlowerArea3[2] = 0;
                hv_FlowerArea3[3] = 0;
                //花瓣4四朵花瓣面积
                hv_FlowerArea4.Dispose();
                hv_FlowerArea4 = new HTuple();
                hv_FlowerArea4[0] = 0;
                hv_FlowerArea4[1] = 0;
                hv_FlowerArea4[2] = 0;
                hv_FlowerArea4[3] = 0;
                //花瓣5四朵花瓣面积
                hv_FlowerArea5.Dispose();
                hv_FlowerArea5 = new HTuple();
                hv_FlowerArea5[0] = 0;
                hv_FlowerArea5[1] = 0;
                hv_FlowerArea5[2] = 0;
                hv_FlowerArea5[3] = 0;
                HTuple end_val115 = hv_Number - 1;
                HTuple step_val115 = 1;
                for (hv_i = 0; hv_i.Continue(end_val115, step_val115); hv_i = hv_i.TupleAdd(step_val115))
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Rectangle.Dispose();
                        HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row.TupleSelect(hv_i), hv_Column.TupleSelect(
                            hv_i), hv_FlowersPhi, hv_FlowersLength1 - 15, hv_FlowersLength2 / 3);
                    }
                    ho_ImageFlower1.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageFlowers, ho_Rectangle, out ho_ImageFlower1
                        );
                    ho_RegionFlower1.Dispose();
                    HOperatorSet.Threshold(ho_ImageFlower1, out ho_RegionFlower1, detection.FlowerCenterThrehold, 255);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.OpeningCircle(ho_RegionFlower1, out ExpTmpOutVar_0, 3.5);
                        ho_RegionFlower1.Dispose();
                        ho_RegionFlower1 = ExpTmpOutVar_0;
                    }
                    ho_RegionDifference1.Dispose();
                    HOperatorSet.Difference(ho_RegionFlower1, ho_RegionLinesH, out ho_RegionDifference1
                        );
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Difference(ho_RegionDifference1, ho_RegionLinesV, out ExpTmpOutVar_0
                            );
                        ho_RegionDifference1.Dispose();
                        ho_RegionDifference1 = ExpTmpOutVar_0;
                    }
                    ho_SelectedRegions1.Dispose();
                    HOperatorSet.SelectShape(ho_RegionDifference1, out ho_SelectedRegions1, "area",
                        "and", 150, 99999);
                    ho_ConnectedRegions1.Dispose();
                    HOperatorSet.Connection(ho_SelectedRegions1, out ho_ConnectedRegions1);
                    hv_NumberPetal.Dispose();
                    HOperatorSet.CountObj(ho_ConnectedRegions1, out hv_NumberPetal);
                    if ((int)(new HTuple(hv_NumberPetal.TupleEqual(4))) != 0)
                    {
                        ho_SortedRegionsFlower.Dispose();
                        HOperatorSet.SortRegion(ho_ConnectedRegions1, out ho_SortedRegionsFlower,
                            "character", "true", "row");
                        for (hv_j = 0; (int)hv_j < hv_NumberPetal; hv_j = (int)hv_j + 1)
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
                                case 3:
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        ho_ObjectSelected.Dispose();
                                        HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                            hv_j + 1);
                                    }
                                    hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                                    HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                        out hv_Column1);
                                    if (hv_FlowerArea4 == null)
                                        hv_FlowerArea4 = new HTuple();
                                    hv_FlowerArea4[hv_j] = hv_AreaF;
                                    break;
                                case 4:
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        ho_ObjectSelected.Dispose();
                                        HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                            hv_j + 1);
                                    }
                                    hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                                    HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                        out hv_Column1);
                                    if (hv_FlowerArea5 == null)
                                        hv_FlowerArea5 = new HTuple();
                                    hv_FlowerArea5[hv_j] = hv_AreaF;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        disp_message(hv_WindowHandler, "花瓣检测失败，数目不为4",
                            "window", hv_Row1, hv_Column1, "black", "true");
                        return;
                    }
                }

                //生成铆偏直线
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RivetOffsetLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RivetOffsetLines, hv_FCR.TupleSelect(0),
                        hv_FCC.TupleSelect(0), hv_FCR.TupleSelect(1), hv_FCC.TupleSelect(1));
                }
                ho_RivetOffsetContours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_RivetOffsetLines, out ho_RivetOffsetContours,
                    "border");
                hv_Ra.Dispose(); hv_Rb.Dispose(); hv_Phi.Dispose();
                HOperatorSet.EllipticAxisPointsXld(ho_RivetOffsetContours, out hv_Ra, out hv_Rb,
                    out hv_Phi);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RivetOffsetLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RivetOffsetLines, (hv_FCR.TupleSelect(0)) + (200 * (hv_Phi.TupleSin()
                        )), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos())), (hv_FCR.TupleSelect(
                        1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(1)) + (200 * (hv_Phi.TupleCos()
                        )));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, (hv_FCR.TupleSelect(0)) + (200 * (hv_Phi.TupleSin()
                        )), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos())), (hv_FCR.TupleSelect(
                        1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(1)) + (200 * (hv_Phi.TupleCos()
                        )));
                }
                //找箔边缘
                hv_MeasureHandle.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowTransL, hv_ColTransL, hv_PhiTransL,
                    hv_Length1Line, hv_Length2Line, hv_Width, hv_Height, "nearest_neighbor",
                    out hv_MeasureHandle);
                hv_RowEdgeLine.Dispose(); hv_ColumnEdgeLine.Dispose(); hv_AmplitudeLine.Dispose(); hv_DistanceLine.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, 2, 20, "positive", "first",
                    out hv_RowEdgeLine, out hv_ColumnEdgeLine, out hv_AmplitudeLine, out hv_DistanceLine);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )), hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())), hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )), hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos())));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ContourLine.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourLine, ((hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )))).TupleConcat(hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()))),
                        ((hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())))).TupleConcat(
                        hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos()))));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ContourLine.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourLine, ((hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )))).TupleConcat(hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()))),
                        ((hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())))).TupleConcat(
                        hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos()))));
                }
                //找左右两侧边缘
                hv_MeasureHandleL.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowTransL2, hv_ColTransL2, hv_PhiTransL2,
                    hv_Length1L2, hv_Length2L2, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandleL);

                hv_RowEdgeFirstL.Dispose(); hv_ColumnEdgeFirstL.Dispose(); hv_AmplitudeFirstL.Dispose(); hv_RowEdgeSecondL.Dispose(); hv_ColumnEdgeSecondL.Dispose(); hv_AmplitudeSecondL.Dispose(); hv_IntraDistance1.Dispose(); hv_InterDistance1.Dispose();
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandleL, 3, 60, "positive", "all",
                    out hv_RowEdgeFirstL, out hv_ColumnEdgeFirstL, out hv_AmplitudeFirstL,
                    out hv_RowEdgeSecondL, out hv_ColumnEdgeSecondL, out hv_AmplitudeSecondL,
                    out hv_IntraDistance1, out hv_InterDistance1);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionLeft.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLeft, hv_RowEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionRight.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionRight, hv_RowEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }
                //找下边缘
                //gen_rectangle2 (rect, RowL, ColumnL, PhiL-1.57, Length1L-90, Length2L)
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_MeasureHandleL.Dispose();
                    HOperatorSet.GenMeasureRectangle2(hv_RowTransL2, hv_ColTransL2, hv_PhiTransL2 - 1.57,
                        hv_Length1L2 - 90, hv_Length2L2, hv_Width, hv_Height, "nearest_neighbor",
                        out hv_MeasureHandleL);
                }
                hv_RowEdgeD.Dispose(); hv_ColumnEdgeD.Dispose(); hv_AmplitudeD.Dispose(); hv_DistanceD.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandleL, 2, 30, "negative", "first",
                    out hv_RowEdgeD, out hv_ColumnEdgeD, out hv_AmplitudeD, out hv_DistanceD);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleCos())));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Distance.Dispose();
                    HOperatorSet.DistancePl(hv_RowEdgeD, hv_ColumnEdgeD, hv_RowEdgeLine - (hv_Length1Line * (hv_PhiTransL.TupleSin()
                        )), hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiTransL.TupleCos())), hv_RowEdgeLine + (hv_Length1Line * (hv_PhiTransL.TupleSin()
                        )), hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiTransL.TupleCos())), out hv_Distance);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionDown.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionDown, hv_RowEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleCos())));
                }
                hv_mDistance.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_mDistance = hv_Distance * hv_mmpp;
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteString(hv_WindowHandler, "L2: " + hv_mDistance);
                }

                //导针直线
                hv_MeasureHandlePin.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowTransP, hv_ColTransP, hv_PhiTransPin,
                    hv_Length1Pin, hv_Length2Pin, hv_Width, hv_Height, "nearest_neighbor",
                    out hv_MeasureHandlePin);
                hv_RowEdgePin.Dispose(); hv_ColumnEdgePin.Dispose(); hv_AmplitudePin.Dispose(); hv_Distance1.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandlePin, 1, 30, "positive", "first",
                    out hv_RowEdgePin, out hv_ColumnEdgePin, out hv_AmplitudePin, out hv_Distance1);
                if ((int)((new HTuple((new HTuple(hv_RowEdgePin.TupleLength())).TupleEqual(
                    1))).TupleAnd(new HTuple((new HTuple(hv_ColumnEdgePin.TupleLength())).TupleEqual(
                    1)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleSin())), hv_RowEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleSin())));
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_RegionPin.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionPin, hv_RowEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleSin())), hv_RowEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleSin())));
                    }
                }
                else
                {
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hv_WindowHandler, "red");
                    }
                    HOperatorSet.WriteString(hv_WindowHandler, "Can Not Find Pin!");
                }

                //计算角度
                if ((int)((new HTuple((new HTuple(hv_RowEdgePin.TupleLength())).TupleEqual(
                    1))).TupleAnd(new HTuple((new HTuple(hv_ColumnEdgePin.TupleLength())).TupleEqual(
                    1)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_RadPin.Dispose();
                        HOperatorSet.AngleLl(hv_RowEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())),
                            hv_ColumnEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                            )), hv_ColumnEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgePin - (hv_Length2Pin * (hv_PhiTransPin.TupleCos()
                            )), hv_ColumnEdgePin - (hv_Length2Pin * (hv_PhiTransPin.TupleSin())), hv_RowEdgePin + (hv_Length2Pin * (hv_PhiTransPin.TupleCos()
                            )), hv_ColumnEdgePin + (hv_Length2Pin * (hv_PhiTransPin.TupleSin())), out hv_RadPin);
                    }
                    hv_AnglePin.Dispose();
                    HOperatorSet.TupleDeg(hv_RadPin, out hv_AnglePin);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.WriteString(hv_WindowHandler, "角度: " + hv_AnglePin);
                    }
                }

                //铆偏计算
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DistanceRivetOffset1.Dispose();
                    HOperatorSet.DistancePl(hv_RowEdgeFirstL, hv_ColumnEdgeFirstL, (hv_FCR.TupleSelect(
                        0)) + (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos()
                        )), (hv_FCR.TupleSelect(1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(
                        1)) + (200 * (hv_Phi.TupleCos())), out hv_DistanceRivetOffset1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DistanceRivetOffset2.Dispose();
                    HOperatorSet.DistancePl(hv_RowEdgeSecondL, hv_ColumnEdgeSecondL, (hv_FCR.TupleSelect(
                        0)) + (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos()
                        )), (hv_FCR.TupleSelect(1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(
                        1)) + (200 * (hv_Phi.TupleCos())), out hv_DistanceRivetOffset2);
                }
                hv_DistanceRivetOffset.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DistanceRivetOffset = (((hv_DistanceRivetOffset1 - hv_DistanceRivetOffset2)).TupleAbs()
                        ) * hv_mmpp;
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteString(hv_WindowHandler, "铆偏差值: " + hv_DistanceRivetOffset);
                }

                //判断是否为良品
                HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
                set_display_font(hv_WindowHandler, 18, "mono", "true", "false");
                HTuple hv_DetectionIndex = new HTuple();
                HTuple hv_Color=new HTuple();
                HTuple hv = new HTuple();
                hv_DetectionIndex = 0;
                hv_Color = "green";
                for(int i = 0; i < hv_Number-1; i++)
                {
                    hv.Dispose();
                    switch (i)
                    {
                        case 0: hv = hv_FlowerArea1; break;
                        case 1: hv = hv_FlowerArea2; break;
                        case 2: hv = hv_FlowerArea3; break;
                        case 3: hv = hv_FlowerArea4; break;
                        case 4: hv = hv_FlowerArea5; break;
                        default: break;
                    }
                    if (setting.AreaMinP <= hv && hv <= setting.AreaMinP)
                    {
                        //HOperatorSet.SetColor(hv_WindowHandler, "green");
                        hv_Color = "green";
                    }
                    else
                    {
                        error = true;
                        hv_Color = "red";
                        HOperatorSet.SetColor(hv_WindowHandler, "red");
                        if (ho_SortedRegionsFlower.IsInitialized())
                        {
                            HOperatorSet.DispObj(ho_SortedRegionsFlower[i], hv_WindowHandler);
                        }
                        HOperatorSet.SetColor(hv_WindowHandler, "green");
                    }
                    hv_DetectionIndex++;
                    using (new HDevDisposeHelper())
                    {
                        disp_message(hv_WindowHandler, "花瓣面积："+i.ToString() + hv_FlowerArea1[0] + "," + hv_FlowerArea1[1] + ","
                            + hv_FlowerArea1[2] + "," + hv_FlowerArea1[3], "window", 30 * hv_DetectionIndex, 10, hv_Color,
                            "false");
                    }
                }
                hv.Dispose();//释放临时用于记录花瓣索引的变量
                HOperatorSet.DispObj(ho_ContourLine, hv_WindowHandler);
                if(setting.AngleMinP<=hv_AnglePin&&hv_AnglePin<=setting.AngleMaxP)
                {
                    hv_Color = "green";
                }
                else
                {
                    error = true;
                    hv_Color= "red";
                    HOperatorSet.SetColor(hv_WindowHandler, "red");
                    HOperatorSet.DispObj(ho_RegionPin, hv_WindowHandler);
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }

                hv_DetectionIndex++;
                disp_message(hv_WindowHandler, "角度：", "window", 30 * hv_DetectionIndex, 400, hv_Color,"false");

                if (setting.L2MinP <= hv_mDistance && hv_mDistance <= setting.L2MaxP)
                {
                    hv_Color = "green";
                }
                else
                {
                    error = true;
                    hv_Color = "red";
                    HOperatorSet.SetColor(hv_WindowHandler, "red");
                    
                }
                hv_DetectionIndex++;
                disp_message(hv_WindowHandler, "L2：", "window", 30 * hv_DetectionIndex, 400, hv_mDistance, "false");

                if(setting.RivetOffsetMinP<=hv_DistanceRivetOffset&&hv_DistanceRivetOffset <= setting.RivetOffsetMaxP)
                {
                    hv_Color = "green";
                }
                else
                {
                    error = true;
                    hv_Color = "red";
                    HOperatorSet.SetColor (hv_WindowHandler, "red");
                    HOperatorSet.DispObj(ho_RegionLeft, hv_WindowHandler);
                    HOperatorSet.DispObj(ho_RegionRight, hv_WindowHandler);
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }
                hv_DetectionIndex++;
                disp_message(hv_WindowHandler, "L2：", "window", 30 * hv_DetectionIndex, 400, hv_DistanceRivetOffset, "false");
                //HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
                #endregion
            }
            catch (HalconException ex)
            {
                #region 资源释放
                ho_Image.Dispose();
                ho_FlowersRoi.Dispose();
                ho_FlowersModelContours.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_ImageFlowers.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionLinesH.Dispose();
                ho_RegionLinesV.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageFlower1.Dispose();
                ho_RegionFlower1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SortedRegionsFlower.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RivetOffsetLines.Dispose();
                ho_RivetOffsetContours.Dispose();
                ho_ContourLine.Dispose();
                ho_RegionLeft.Dispose();
                ho_RegionRight.Dispose();
                ho_RegionDown.Dispose();
                ho_RegionPin.Dispose();

                hv_ImageFiles.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                //hv_WindowHandle.Dispose();
                hv_FlowersRow.Dispose();
                hv_FlowersColumn.Dispose();
                hv_FlowersPhi.Dispose();
                hv_FlowersLength1.Dispose();
                hv_FlowersLength2.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Phi.Dispose();
                hv_mmpp.Dispose();
                hv_areaMax.Dispose();
                hv_areaMin.Dispose();
                hv_L2Max.Dispose();
                hv_L2Min.Dispose();
                hv_AngelMax.Dispose();
                hv_AngelMin.Dispose();
                hv_RowL2.Dispose();
                hv_ColumnL2.Dispose();
                hv_PhiL2.Dispose();
                hv_Length1L2.Dispose();
                hv_Length2L2.Dispose();
                hv_RowLine.Dispose();
                hv_ColumnLine.Dispose();
                hv_PhiLine.Dispose();
                hv_Length1Line.Dispose();
                hv_Length2Line.Dispose();
                hv_RowPin.Dispose();
                hv_ColumnPin.Dispose();
                hv_PhiPin.Dispose();
                hv_Length1Pin.Dispose();
                hv_Length2Pin.Dispose();
                hv_FlowersModelID.Dispose();
                hv_RowFind.Dispose();
                hv_ColumnFind.Dispose();
                hv_AngleFind.Dispose();
                hv_Score.Dispose();
                hv_HomMat2D.Dispose();
                hv_Index.Dispose();
                hv_PhiTransL.Dispose();
                hv_RowTransL.Dispose();
                hv_ColTransL.Dispose();
                hv_PhiTransL2.Dispose();
                hv_RowTransL2.Dispose();
                hv_ColTransL2.Dispose();
                hv_PhiTransPin.Dispose();
                hv_RowTransP.Dispose();
                hv_ColTransP.Dispose();
                hv_Number.Dispose();
                hv_Area.Dispose();
                hv_FCC.Dispose();
                hv_FCR.Dispose();
                hv_FlowerArea1.Dispose();
                hv_FlowerArea2.Dispose();
                hv_FlowerArea3.Dispose();
                hv_FlowerArea4.Dispose();
                hv_FlowerArea5.Dispose();
                hv_i.Dispose();
                hv_j.Dispose();
                hv_AreaF.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Ra.Dispose();
                hv_Rb.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdgeLine.Dispose();
                hv_ColumnEdgeLine.Dispose();
                hv_AmplitudeLine.Dispose();
                hv_DistanceLine.Dispose();
                hv_MeasureHandleL.Dispose();
                hv_RowEdgeFirstL.Dispose();
                hv_ColumnEdgeFirstL.Dispose();
                hv_AmplitudeFirstL.Dispose();
                hv_RowEdgeSecondL.Dispose();
                hv_ColumnEdgeSecondL.Dispose();
                hv_AmplitudeSecondL.Dispose();
                hv_IntraDistance1.Dispose();
                hv_InterDistance1.Dispose();
                hv_RowEdgeD.Dispose();
                hv_ColumnEdgeD.Dispose();
                hv_AmplitudeD.Dispose();
                hv_DistanceD.Dispose();
                hv_Distance.Dispose();
                hv_mDistance.Dispose();
                hv_MeasureHandlePin.Dispose();
                hv_RowEdgePin.Dispose();
                hv_ColumnEdgePin.Dispose();
                hv_AmplitudePin.Dispose();
                hv_Distance1.Dispose();
                hv_RadPin.Dispose();
                hv_AnglePin.Dispose();
                hv_DistanceRivetOffset1.Dispose();
                hv_DistanceRivetOffset2.Dispose();
                hv_DistanceRivetOffset.Dispose();
                hv_NumberPetal.Dispose();
                #endregion
                throw ex;
            }
            finally
            {
                #region 资源释放
                ho_Image.Dispose();
                ho_FlowersRoi.Dispose();
                ho_FlowersModelContours.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_ImageFlowers.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionLinesH.Dispose();
                ho_RegionLinesV.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageFlower1.Dispose();
                ho_RegionFlower1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SortedRegionsFlower.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RivetOffsetLines.Dispose();
                ho_RivetOffsetContours.Dispose();
                ho_ContourLine.Dispose();
                ho_RegionLeft.Dispose();
                ho_RegionRight.Dispose();
                ho_RegionDown.Dispose();
                ho_RegionPin.Dispose();

                hv_ImageFiles.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                //hv_WindowHandle.Dispose();
                hv_FlowersRow.Dispose();
                hv_FlowersColumn.Dispose();
                hv_FlowersPhi.Dispose();
                hv_FlowersLength1.Dispose();
                hv_FlowersLength2.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Phi.Dispose();
                hv_mmpp.Dispose();
                hv_areaMax.Dispose();
                hv_areaMin.Dispose();
                hv_L2Max.Dispose();
                hv_L2Min.Dispose();
                hv_AngelMax.Dispose();
                hv_AngelMin.Dispose();
                hv_RowL2.Dispose();
                hv_ColumnL2.Dispose();
                hv_PhiL2.Dispose();
                hv_Length1L2.Dispose();
                hv_Length2L2.Dispose();
                hv_RowLine.Dispose();
                hv_ColumnLine.Dispose();
                hv_PhiLine.Dispose();
                hv_Length1Line.Dispose();
                hv_Length2Line.Dispose();
                hv_RowPin.Dispose();
                hv_ColumnPin.Dispose();
                hv_PhiPin.Dispose();
                hv_Length1Pin.Dispose();
                hv_Length2Pin.Dispose();
                hv_FlowersModelID.Dispose();
                hv_RowFind.Dispose();
                hv_ColumnFind.Dispose();
                hv_AngleFind.Dispose();
                hv_Score.Dispose();
                hv_HomMat2D.Dispose();
                hv_Index.Dispose();
                hv_PhiTransL.Dispose();
                hv_RowTransL.Dispose();
                hv_ColTransL.Dispose();
                hv_PhiTransL2.Dispose();
                hv_RowTransL2.Dispose();
                hv_ColTransL2.Dispose();
                hv_PhiTransPin.Dispose();
                hv_RowTransP.Dispose();
                hv_ColTransP.Dispose();
                hv_Number.Dispose();
                hv_Area.Dispose();
                hv_FCC.Dispose();
                hv_FCR.Dispose();
                hv_FlowerArea1.Dispose();
                hv_FlowerArea2.Dispose();
                hv_FlowerArea3.Dispose();
                hv_FlowerArea4.Dispose();
                hv_FlowerArea5.Dispose();
                hv_i.Dispose();
                hv_j.Dispose();
                hv_AreaF.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Ra.Dispose();
                hv_Rb.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdgeLine.Dispose();
                hv_ColumnEdgeLine.Dispose();
                hv_AmplitudeLine.Dispose();
                hv_DistanceLine.Dispose();
                hv_MeasureHandleL.Dispose();
                hv_RowEdgeFirstL.Dispose();
                hv_ColumnEdgeFirstL.Dispose();
                hv_AmplitudeFirstL.Dispose();
                hv_RowEdgeSecondL.Dispose();
                hv_ColumnEdgeSecondL.Dispose();
                hv_AmplitudeSecondL.Dispose();
                hv_IntraDistance1.Dispose();
                hv_InterDistance1.Dispose();
                hv_RowEdgeD.Dispose();
                hv_ColumnEdgeD.Dispose();
                hv_AmplitudeD.Dispose();
                hv_DistanceD.Dispose();
                hv_Distance.Dispose();
                hv_mDistance.Dispose();
                hv_MeasureHandlePin.Dispose();
                hv_RowEdgePin.Dispose();
                hv_ColumnEdgePin.Dispose();
                hv_AmplitudePin.Dispose();
                hv_Distance1.Dispose();
                hv_RadPin.Dispose();
                hv_AnglePin.Dispose();
                hv_DistanceRivetOffset1.Dispose();
                hv_DistanceRivetOffset2.Dispose();
                hv_DistanceRivetOffset.Dispose();
                hv_NumberPetal.Dispose();
                #endregion
            }


#endregion

        }

        public void HNegativeDetection(HObject ho_Image, HTuple hv_WindowHandler, MinMaxSetting.NegativeTolerance setting, DetectionSetting.NegativeDetection detection, out bool error)
        {
            error = false;
            if (!ho_Image.IsInitialized() || hv_WindowHandler == null) return;
            #region 
            #region 参数初始化
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            // Local iconic variables 
            HObject ho_FlowersRoi, ho_FlowersModelContours;
            HObject ho_RegionAffineTrans = null, ho_ImageFlowers = null;
            HObject ho_Region = null, ho_RegionClosing = null, ho_RegionDifference = null;
            HObject ho_RegionOpening = null, ho_ConnectedRegions = null;
            HObject ho_SelectedRegions = null, ho_SortedRegions = null;
            HObject ho_RegionLinesH = null, ho_RegionLinesV = null, ho_Rectangle = null;
            HObject ho_ImageFlower1 = null, ho_RegionFlower1 = null, ho_RegionDifference1 = null;
            HObject ho_SelectedRegions1 = null, ho_ConnectedRegions1 = null;
            HObject ho_SortedRegionsFlower = null, ho_ObjectSelected = null;
            HObject ho_RivetOffsetLines = null, ho_RivetOffsetContours = null;
            HObject ho_ContourLine = null, ho_RegionLeft = null, ho_RegionRight = null;
            HObject ho_RegionDown = null, ho_RegionPin = null;


            // Local control variables 

            HTuple hv_ImageFiles = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(); //hv_WindowHandle = new HTuple();
            HTuple hv_FlowersRow = new HTuple(), hv_FlowersColumn = new HTuple();
            HTuple hv_FlowersPhi = new HTuple(), hv_FlowersLength1 = new HTuple();
            HTuple hv_FlowersLength2 = new HTuple(), hv_Row = new HTuple();
            HTuple hv_Column = new HTuple(), hv_Phi = new HTuple();
            HTuple hv_mmpp = new HTuple(), hv_areaMax = new HTuple();
            HTuple hv_areaMin = new HTuple(), hv_L2Max = new HTuple();
            HTuple hv_L2Min = new HTuple(), hv_AngelMax = new HTuple();
            HTuple hv_AngelMin = new HTuple(), hv_RowL2 = new HTuple();
            HTuple hv_ColumnL2 = new HTuple(), hv_PhiL2 = new HTuple();
            HTuple hv_Length1L2 = new HTuple(), hv_Length2L2 = new HTuple();
            HTuple hv_RowLine = new HTuple(), hv_ColumnLine = new HTuple();
            HTuple hv_PhiLine = new HTuple(), hv_Length1Line = new HTuple();
            HTuple hv_Length2Line = new HTuple(), hv_RowPin = new HTuple();
            HTuple hv_ColumnPin = new HTuple(), hv_PhiPin = new HTuple();
            HTuple hv_Length1Pin = new HTuple(), hv_Length2Pin = new HTuple();
            HTuple hv_FlowersModelID = new HTuple(), hv_RowFind = new HTuple();
            HTuple hv_ColumnFind = new HTuple(), hv_AngleFind = new HTuple();
            HTuple hv_Score = new HTuple(), hv_HomMat2D = new HTuple();
            HTuple hv_Index = new HTuple(), hv_PhiTransL = new HTuple();
            HTuple hv_RowTransL = new HTuple(), hv_ColTransL = new HTuple();
            HTuple hv_PhiTransL2 = new HTuple(), hv_RowTransL2 = new HTuple();
            HTuple hv_ColTransL2 = new HTuple(), hv_PhiTransPin = new HTuple();
            HTuple hv_RowTransP = new HTuple(), hv_ColTransP = new HTuple();
            HTuple hv_Number = new HTuple(), hv_Area = new HTuple();
            HTuple hv_FCC = new HTuple(), hv_FCR = new HTuple(), hv_FlowerArea1 = new HTuple();
            HTuple hv_FlowerArea2 = new HTuple(), hv_FlowerArea3 = new HTuple();
            HTuple hv_FlowerArea4 = new HTuple(), hv_FlowerArea5 = new HTuple();
            HTuple hv_i = new HTuple(), hv_j = new HTuple(), hv_AreaF = new HTuple();
            HTuple hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Ra = new HTuple(), hv_Rb = new HTuple(), hv_MeasureHandle = new HTuple();
            HTuple hv_RowEdgeLine = new HTuple(), hv_ColumnEdgeLine = new HTuple();
            HTuple hv_AmplitudeLine = new HTuple(), hv_DistanceLine = new HTuple();
            HTuple hv_MeasureHandleL = new HTuple(), hv_RowEdgeFirstL = new HTuple();
            HTuple hv_ColumnEdgeFirstL = new HTuple(), hv_AmplitudeFirstL = new HTuple();
            HTuple hv_RowEdgeSecondL = new HTuple(), hv_ColumnEdgeSecondL = new HTuple();
            HTuple hv_AmplitudeSecondL = new HTuple(), hv_IntraDistance1 = new HTuple();
            HTuple hv_InterDistance1 = new HTuple(), hv_RowEdgeD = new HTuple();
            HTuple hv_ColumnEdgeD = new HTuple(), hv_AmplitudeD = new HTuple();
            HTuple hv_DistanceD = new HTuple(), hv_Distance = new HTuple();
            HTuple hv_mDistance = new HTuple(), hv_MeasureHandlePin = new HTuple();
            HTuple hv_RowEdgePin = new HTuple(), hv_ColumnEdgePin = new HTuple();
            HTuple hv_AmplitudePin = new HTuple(), hv_Distance1 = new HTuple();
            HTuple hv_RadPin = new HTuple(), hv_AnglePin = new HTuple();
            HTuple hv_DistanceRivetOffset1 = new HTuple(), hv_DistanceRivetOffset2 = new HTuple();
            HTuple hv_DistanceRivetOffset = new HTuple(), hv_NumberPetal = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_FlowersRoi);
            HOperatorSet.GenEmptyObj(out ho_FlowersModelContours);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans);
            HOperatorSet.GenEmptyObj(out ho_ImageFlowers);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesH);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesV);
            HOperatorSet.GenEmptyObj(out ho_Rectangle);
            HOperatorSet.GenEmptyObj(out ho_ImageFlower1);
            HOperatorSet.GenEmptyObj(out ho_RegionFlower1);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegionsFlower);
            HOperatorSet.GenEmptyObj(out ho_ObjectSelected);
            HOperatorSet.GenEmptyObj(out ho_RivetOffsetLines);
            HOperatorSet.GenEmptyObj(out ho_RivetOffsetContours);
            HOperatorSet.GenEmptyObj(out ho_ContourLine);
            HOperatorSet.GenEmptyObj(out ho_RegionLeft);
            HOperatorSet.GenEmptyObj(out ho_RegionRight);
            HOperatorSet.GenEmptyObj(out ho_RegionDown);
            HOperatorSet.GenEmptyObj(out ho_RegionPin);

            #endregion

            //HTuple hv_ErrorType = new HTuple();
            //if (hv_ErrorType == null)
            //{
            //    hv_ErrorType = new HTuple();
            //}
            //hv_ErrorType[0] = "定位失败";
            //hv_ErrorType[1] = "L2";
            //hv_ErrorType[2] = "角度";
            //hv_ErrorType[3] = "裂箔";
            //hv_ErrorType[4] = "铆偏";
            //hv_ErrorType[5] = "极花1";
            //hv_ErrorType[6] = "极花2";
            //hv_ErrorType[7] = "极花3";
            //hv_ErrorType[8] = "极花4";
            //hv_ErrorType[9] = "极花5";

            HOperatorSet.ClearWindow(hv_WindowHandler);
            HOperatorSet.SetLineWidth(hv_WindowHandler, 2);
            HOperatorSet.SetDraw(hv_WindowHandler, "margin");
            hv_Width.Dispose(); hv_Height.Dispose();
            HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
            HOperatorSet.SetWindowAttr("background_color", "black");
            //HOperatorSet.OpenWindow(0, 0, hv_Width, hv_Height, 0, "visible", "", out hv_WindowHandler);
            HOperatorSet.SetPart(hv_WindowHandler, 0, 0, hv_Height, hv_Width);
            //HDevWindowStack.Push(hv_WindowHandler);
            HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
            }

            #region 默认ROI参数与定位模板读取
            //划分roi区域
            //花瓣roi
            //draw_rectangle2 (WindowHandle, FlowersRow, FlowersColumn, FlowersPhi, FlowersLength1, FlowersLength2))
            hv_FlowersRow.Dispose();
            hv_FlowersRow = 369;
            hv_FlowersColumn.Dispose();
            hv_FlowersColumn = 569;
            hv_FlowersPhi.Dispose();
            hv_FlowersPhi = 0;
            hv_FlowersLength1.Dispose();
            hv_FlowersLength1 = 42;
            hv_FlowersLength2.Dispose();
            hv_FlowersLength2 = 110;
            ho_FlowersRoi.Dispose();
            HOperatorSet.GenRectangle2(out ho_FlowersRoi, hv_FlowersRow, hv_FlowersColumn,
                hv_FlowersPhi, hv_FlowersLength1, hv_FlowersLength2);
            //************************
            //计算像素当量
            //draw_rectangle2 (WindowHandle, Row, Column, Phi, Length1, Length2)
            //gen_measure_rectangle2 (Row, Column, Phi, Length1, Length2, Width, Height, 'nearest_neighbor', MeasureHandlemmpp)
            //measure_pairs (Image, MeasureHandlemmpp, 2, 30, 'all', 'all', RowEdgeFirst, ColumnEdgeFirst, AmplitudeFirst, RowEdgeSecond, ColumnEdgeSecond, AmplitudeSecond, IntraDistance, InterDistance)
            //mm := 2.7
            //mmpp := mm/IntraDistance
            hv_mmpp.Dispose();
            hv_mmpp = 0.013;
            //公差范围
            hv_areaMax.Dispose();
            hv_areaMax = setting.AreaMaxN;
            hv_areaMin.Dispose();
            hv_areaMin = setting.AreaMinN;
            hv_L2Max.Dispose();
            hv_L2Max = setting.L2MaxN;
            hv_L2Min.Dispose();
            hv_L2Min = setting.L2MinN;
            hv_AngelMax.Dispose();
            hv_AngelMax = setting.AngleMaxN;
            hv_AngelMin.Dispose();
            hv_AngelMin = setting.AngleMinN;
            //*************************
            //*************************
            //L2位置
            //L2位置ROI
            hv_RowL2.Dispose();
            hv_RowL2 = 529;
            hv_ColumnL2.Dispose();
            hv_ColumnL2 = 565;
            hv_PhiL2.Dispose();
            hv_PhiL2 = -0;
            hv_Length1L2.Dispose();
            hv_Length1L2 = 128.0;
            hv_Length2L2.Dispose();
            hv_Length2L2 = 40;
            //下边缘线ROI矩形参数
            hv_RowLine.Dispose();
            hv_RowLine = 505;
            hv_ColumnLine.Dispose();
            hv_ColumnLine = 573;
            hv_PhiLine.Dispose();
            hv_PhiLine = -0;
            hv_Length1Line.Dispose();
            hv_Length1Line = 370;
            hv_Length2Line.Dispose();
            hv_Length2Line = 35;
            //导针直线ROI
            //draw_rectangle2 (WindowHandle, RowPin, ColumnPin, PhiPin, Length1Pin, Length2Pin)
            hv_RowPin.Dispose();
            hv_RowPin = 859;
            hv_ColumnPin.Dispose();
            hv_ColumnPin = 578;
            hv_PhiPin.Dispose();
            hv_PhiPin = -0;
            hv_Length1Pin.Dispose();
            hv_Length1Pin = 62;
            hv_Length2Pin.Dispose();
            hv_Length2Pin = 152;

            //**************************
            //读取花瓣轮廓模板
            hv_FlowersModelID.Dispose();
            HOperatorSet.ReadShapeModel("D:/CENJE-Vison/Model/FlowersModel.shm", out hv_FlowersModelID);
            ho_FlowersModelContours.Dispose();
            HOperatorSet.GetShapeModelContours(out ho_FlowersModelContours, hv_FlowersModelID,
                1);

            #endregion

            try
            {
                #region 执行检测
                if (HDevWindowStack.IsOpen())
                {
                    //HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                }
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    //ho_Image.Dispose();
                    //HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                }
                //查找花瓣位置
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowFind.Dispose(); hv_ColumnFind.Dispose(); hv_AngleFind.Dispose(); hv_Score.Dispose();
                    HOperatorSet.FindShapeModel(ho_Image, hv_FlowersModelID, -((new HTuple(15)).TupleRad()
                        ), (new HTuple(30)).TupleRad(), 0.6, 1, 0.5, "least_squares", 4, 0.8, out hv_RowFind,
                        out hv_ColumnFind, out hv_AngleFind, out hv_Score);
                }
                if (hv_Score < 0.8)
                {
                    error = true;
                    disp_message(hv_WindowHandler, "定位失败", "image", 0, 0, "red", "false");
                    return;
                }
                hv_HomMat2D.Dispose();
                HOperatorSet.VectorAngleToRigid(hv_FlowersRow, hv_FlowersColumn, hv_FlowersPhi,
                    hv_RowFind, hv_ColumnFind, hv_AngleFind, out hv_HomMat2D);
                ho_RegionAffineTrans.Dispose();
                HOperatorSet.AffineTransRegion(ho_FlowersRoi, out ho_RegionAffineTrans, hv_HomMat2D,
                    "nearest_neighbor");

                //roi仿射变换
                hv_PhiTransL.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PhiTransL = hv_PhiLine + (hv_AngleFind - hv_FlowersPhi);
                }
                hv_RowTransL.Dispose(); hv_ColTransL.Dispose();
                HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowLine, hv_ColumnLine, out hv_RowTransL,
                    out hv_ColTransL);
                hv_PhiTransL2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PhiTransL2 = hv_PhiL2 + (hv_AngleFind - hv_FlowersPhi);
                }
                hv_RowTransL2.Dispose(); hv_ColTransL2.Dispose();
                HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowL2, hv_ColumnL2, out hv_RowTransL2,
                    out hv_ColTransL2);
                hv_PhiTransPin.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_PhiTransPin = hv_PhiPin + (hv_AngleFind - hv_FlowersPhi);
                }
                hv_RowTransP.Dispose(); hv_ColTransP.Dispose();
                HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowPin, hv_ColumnPin, out hv_RowTransP,
                    out hv_ColTransP);

                //花瓣检测
                ho_ImageFlowers.Dispose();
                HOperatorSet.ReduceDomain(ho_Image, ho_RegionAffineTrans, out ho_ImageFlowers
                    );
                ho_Region.Dispose();
                HOperatorSet.Threshold(ho_ImageFlowers, out ho_Region, detection.FlowerThrehold, 255);
                ho_RegionClosing.Dispose();
                HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 4.5);//
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_ImageFlowers, ho_RegionClosing, out ho_RegionDifference
                    );
                ho_RegionOpening.Dispose();
                HOperatorSet.OpeningCircle(ho_RegionDifference, out ho_RegionOpening, 4.5);
                ho_ConnectedRegions.Dispose();
                HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions);
                ho_SelectedRegions.Dispose();
                HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "circularity",
                    "and", 0.6, 1);
                hv_Number.Dispose();
                HOperatorSet.CountObj(ho_SelectedRegions, out hv_Number);
                ho_SortedRegions.Dispose();
                HOperatorSet.SortRegion(ho_SelectedRegions, out ho_SortedRegions, "first_point",
                    "true", "row");
                hv_Area.Dispose(); hv_Row.Dispose(); hv_Column.Dispose();
                HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area, out hv_Row, out hv_Column);
                //生成十字线切割花瓣
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionLinesH.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLinesH, hv_Row, hv_Column - 38, hv_Row,
                        hv_Column + 38);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionLinesV.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLinesV, hv_Row - 38, hv_Column, hv_Row + 38,
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
                hv_FCC.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_FCC = new HTuple();
                    hv_FCC = hv_FCC.TupleConcat(hv_Column.TupleSelect(
                        0));
                    hv_FCC = hv_FCC.TupleConcat(hv_Column.TupleSelect(hv_Number - 1));
                }
                //FlowerCenterRow
                hv_FCR.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_FCR = new HTuple();
                    hv_FCR = hv_FCR.TupleConcat(hv_Row.TupleSelect(
                        0));
                    hv_FCR = hv_FCR.TupleConcat(hv_Row.TupleSelect(hv_Number - 1));
                }
                //花瓣1四朵花瓣面积
                hv_FlowerArea1.Dispose();
                hv_FlowerArea1 = new HTuple();
                hv_FlowerArea1[0] = 0;
                hv_FlowerArea1[1] = 0;
                hv_FlowerArea1[2] = 0;
                hv_FlowerArea1[3] = 0;
                //花瓣2四朵花瓣面积
                hv_FlowerArea2.Dispose();
                hv_FlowerArea2 = new HTuple();
                hv_FlowerArea2[0] = 0;
                hv_FlowerArea2[1] = 0;
                hv_FlowerArea2[2] = 0;
                hv_FlowerArea2[3] = 0;
                //花瓣3四朵花瓣面积
                hv_FlowerArea3.Dispose();
                hv_FlowerArea3 = new HTuple();
                hv_FlowerArea3[0] = 0;
                hv_FlowerArea3[1] = 0;
                hv_FlowerArea3[2] = 0;
                hv_FlowerArea3[3] = 0;
                //花瓣4四朵花瓣面积
                hv_FlowerArea4.Dispose();
                hv_FlowerArea4 = new HTuple();
                hv_FlowerArea4[0] = 0;
                hv_FlowerArea4[1] = 0;
                hv_FlowerArea4[2] = 0;
                hv_FlowerArea4[3] = 0;
                //花瓣5四朵花瓣面积
                hv_FlowerArea5.Dispose();
                hv_FlowerArea5 = new HTuple();
                hv_FlowerArea5[0] = 0;
                hv_FlowerArea5[1] = 0;
                hv_FlowerArea5[2] = 0;
                hv_FlowerArea5[3] = 0;
                HTuple end_val115 = hv_Number - 1;
                HTuple step_val115 = 1;
                for (hv_i = 0; hv_i.Continue(end_val115, step_val115); hv_i = hv_i.TupleAdd(step_val115))
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Rectangle.Dispose();
                        HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row.TupleSelect(hv_i), hv_Column.TupleSelect(
                            hv_i), hv_FlowersPhi, hv_FlowersLength1 - 15, hv_FlowersLength2 / 3);
                    }
                    ho_ImageFlower1.Dispose();
                    HOperatorSet.ReduceDomain(ho_ImageFlowers, ho_Rectangle, out ho_ImageFlower1
                        );
                    ho_RegionFlower1.Dispose();
                    HOperatorSet.Threshold(ho_ImageFlower1, out ho_RegionFlower1, detection.FlowerCenterThrehold, 255);
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.OpeningCircle(ho_RegionFlower1, out ExpTmpOutVar_0, 3.5);
                        ho_RegionFlower1.Dispose();
                        ho_RegionFlower1 = ExpTmpOutVar_0;
                    }
                    ho_RegionDifference1.Dispose();
                    HOperatorSet.Difference(ho_RegionFlower1, ho_RegionLinesH, out ho_RegionDifference1
                        );
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.Difference(ho_RegionDifference1, ho_RegionLinesV, out ExpTmpOutVar_0
                            );
                        ho_RegionDifference1.Dispose();
                        ho_RegionDifference1 = ExpTmpOutVar_0;
                    }
                    ho_SelectedRegions1.Dispose();
                    HOperatorSet.SelectShape(ho_RegionDifference1, out ho_SelectedRegions1, "area",
                        "and", 150, 99999);
                    ho_ConnectedRegions1.Dispose();
                    HOperatorSet.Connection(ho_SelectedRegions1, out ho_ConnectedRegions1);
                    hv_NumberPetal.Dispose();
                    HOperatorSet.CountObj(ho_ConnectedRegions1, out hv_NumberPetal);
                    if ((int)(new HTuple(hv_NumberPetal.TupleEqual(4))) != 0)
                    {
                        ho_SortedRegionsFlower.Dispose();
                        HOperatorSet.SortRegion(ho_ConnectedRegions1, out ho_SortedRegionsFlower,
                            "character", "true", "row");
                        for (hv_j = 0; (int)hv_j <= 3; hv_j = (int)hv_j + 1)
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
                                case 3:
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        ho_ObjectSelected.Dispose();
                                        HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                            hv_j + 1);
                                    }
                                    hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                                    HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                        out hv_Column1);
                                    if (hv_FlowerArea4 == null)
                                        hv_FlowerArea4 = new HTuple();
                                    hv_FlowerArea4[hv_j] = hv_AreaF;
                                    break;
                                case 4:
                                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                    {
                                        ho_ObjectSelected.Dispose();
                                        HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                            hv_j + 1);
                                    }
                                    hv_AreaF.Dispose(); hv_Row1.Dispose(); hv_Column1.Dispose();
                                    HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1,
                                        out hv_Column1);
                                    if (hv_FlowerArea5 == null)
                                        hv_FlowerArea5 = new HTuple();
                                    hv_FlowerArea5[hv_j] = hv_AreaF;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        disp_message(hv_WindowHandler, "花瓣检测失败，数目不为4",
                            "window", hv_Row1, hv_Column1, "black", "true");
                        return;
                    }
                }

                //生成铆偏直线
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RivetOffsetLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RivetOffsetLines, hv_FCR.TupleSelect(0),
                        hv_FCC.TupleSelect(0), hv_FCR.TupleSelect(1), hv_FCC.TupleSelect(1));
                }
                ho_RivetOffsetContours.Dispose();
                HOperatorSet.GenContourRegionXld(ho_RivetOffsetLines, out ho_RivetOffsetContours,
                    "border");
                hv_Ra.Dispose(); hv_Rb.Dispose(); hv_Phi.Dispose();
                HOperatorSet.EllipticAxisPointsXld(ho_RivetOffsetContours, out hv_Ra, out hv_Rb,
                    out hv_Phi);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RivetOffsetLines.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RivetOffsetLines, (hv_FCR.TupleSelect(0)) + (200 * (hv_Phi.TupleSin()
                        )), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos())), (hv_FCR.TupleSelect(
                        1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(1)) + (200 * (hv_Phi.TupleCos()
                        )));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, (hv_FCR.TupleSelect(0)) + (200 * (hv_Phi.TupleSin()
                        )), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos())), (hv_FCR.TupleSelect(
                        1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(1)) + (200 * (hv_Phi.TupleCos()
                        )));
                }
                //找箔边缘
                hv_MeasureHandle.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowTransL, hv_ColTransL, hv_PhiTransL,
                    hv_Length1Line, hv_Length2Line, hv_Width, hv_Height, "nearest_neighbor",
                    out hv_MeasureHandle);
                hv_RowEdgeLine.Dispose(); hv_ColumnEdgeLine.Dispose(); hv_AmplitudeLine.Dispose(); hv_DistanceLine.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, 2, 20, "positive", "first",
                    out hv_RowEdgeLine, out hv_ColumnEdgeLine, out hv_AmplitudeLine, out hv_DistanceLine);
                if (HDevWindowStack.IsOpen())
                {
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )), hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())), hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )), hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos())));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ContourLine.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourLine, ((hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )))).TupleConcat(hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()))),
                        ((hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())))).TupleConcat(
                        hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos()))));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_ContourLine.Dispose();
                    HOperatorSet.GenContourPolygonXld(out ho_ContourLine, ((hv_RowEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleSin()
                        )))).TupleConcat(hv_RowEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleSin()))),
                        ((hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiLine.TupleCos())))).TupleConcat(
                        hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiLine.TupleCos()))));
                }
                //找左右两侧边缘
                hv_MeasureHandleL.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowTransL2, hv_ColTransL2, hv_PhiTransL2,
                    hv_Length1L2, hv_Length2L2, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandleL);

                hv_RowEdgeFirstL.Dispose(); hv_ColumnEdgeFirstL.Dispose(); hv_AmplitudeFirstL.Dispose(); hv_RowEdgeSecondL.Dispose(); hv_ColumnEdgeSecondL.Dispose(); hv_AmplitudeSecondL.Dispose(); hv_IntraDistance1.Dispose(); hv_InterDistance1.Dispose();
                HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandleL, 3, 60, "positive", "all",
                    out hv_RowEdgeFirstL, out hv_ColumnEdgeFirstL, out hv_AmplitudeFirstL,
                    out hv_RowEdgeSecondL, out hv_ColumnEdgeSecondL, out hv_AmplitudeSecondL,
                    out hv_IntraDistance1, out hv_InterDistance1);

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionLeft.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionLeft, hv_RowEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeFirstL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }

                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionRight.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionRight, hv_RowEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())), hv_RowEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleCos()
                        )), hv_ColumnEdgeSecondL + (hv_Length2L2 * (hv_PhiTransL2.TupleSin())));
                }
                //找下边缘
                //gen_rectangle2 (rect, RowL, ColumnL, PhiL-1.57, Length1L-90, Length2L)
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_MeasureHandleL.Dispose();
                    HOperatorSet.GenMeasureRectangle2(hv_RowTransL2, hv_ColTransL2, hv_PhiTransL2 - 1.57,
                        hv_Length1L2 - 90, hv_Length2L2, hv_Width, hv_Height, "nearest_neighbor",
                        out hv_MeasureHandleL);
                }
                hv_RowEdgeD.Dispose(); hv_ColumnEdgeD.Dispose(); hv_AmplitudeD.Dispose(); hv_DistanceD.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandleL, 2, 30, "negative", "first",
                    out hv_RowEdgeD, out hv_ColumnEdgeD, out hv_AmplitudeD, out hv_DistanceD);
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleCos())));
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Distance.Dispose();
                    HOperatorSet.DistancePl(hv_RowEdgeD, hv_ColumnEdgeD, hv_RowEdgeLine - (hv_Length1Line * (hv_PhiTransL.TupleSin()
                        )), hv_ColumnEdgeLine - (hv_Length1Line * (hv_PhiTransL.TupleCos())), hv_RowEdgeLine + (hv_Length1Line * (hv_PhiTransL.TupleSin()
                        )), hv_ColumnEdgeLine + (hv_Length1Line * (hv_PhiTransL.TupleCos())), out hv_Distance);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RegionDown.Dispose();
                    HOperatorSet.GenRegionLine(out ho_RegionDown, hv_RowEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                        )), hv_ColumnEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleCos())));
                }
                hv_mDistance.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_mDistance = hv_Distance * hv_mmpp;
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteString(hv_WindowHandler, "L2: " + hv_mDistance);
                }

                //导针直线
                hv_MeasureHandlePin.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowTransP, hv_ColTransP, hv_PhiTransPin,
                    hv_Length1Pin, hv_Length2Pin, hv_Width, hv_Height, "nearest_neighbor",
                    out hv_MeasureHandlePin);
                hv_RowEdgePin.Dispose(); hv_ColumnEdgePin.Dispose(); hv_AmplitudePin.Dispose(); hv_Distance1.Dispose();
                HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandlePin, 1, 30, "positive", "first",
                    out hv_RowEdgePin, out hv_ColumnEdgePin, out hv_AmplitudePin, out hv_Distance1);
                if ((int)((new HTuple((new HTuple(hv_RowEdgePin.TupleLength())).TupleEqual(
                    1))).TupleAnd(new HTuple((new HTuple(hv_ColumnEdgePin.TupleLength())).TupleEqual(
                    1)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleSin())), hv_RowEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleSin())));
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_RegionPin.Dispose();
                        HOperatorSet.GenRegionLine(out ho_RegionPin, hv_RowEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin - (hv_Length2Pin * (hv_PhiPin.TupleSin())), hv_RowEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleCos()
                            )), hv_ColumnEdgePin + (hv_Length2Pin * (hv_PhiPin.TupleSin())));
                    }
                }
                else
                {
                    if (HDevWindowStack.IsOpen())
                    {
                        HOperatorSet.SetColor(hv_WindowHandler, "red");
                    }
                    HOperatorSet.WriteString(hv_WindowHandler, "Can Not Find Pin!");
                }

                //计算角度
                if ((int)((new HTuple((new HTuple(hv_RowEdgePin.TupleLength())).TupleEqual(
                    1))).TupleAnd(new HTuple((new HTuple(hv_ColumnEdgePin.TupleLength())).TupleEqual(
                    1)))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_RadPin.Dispose();
                        HOperatorSet.AngleLl(hv_RowEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleSin())),
                            hv_ColumnEdgeD - (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleSin()
                            )), hv_ColumnEdgeD + (hv_Length2L2 * (hv_PhiTransL2.TupleCos())), hv_RowEdgePin - (hv_Length2Pin * (hv_PhiTransPin.TupleCos()
                            )), hv_ColumnEdgePin - (hv_Length2Pin * (hv_PhiTransPin.TupleSin())), hv_RowEdgePin + (hv_Length2Pin * (hv_PhiTransPin.TupleCos()
                            )), hv_ColumnEdgePin + (hv_Length2Pin * (hv_PhiTransPin.TupleSin())), out hv_RadPin);
                    }
                    hv_AnglePin.Dispose();
                    HOperatorSet.TupleDeg(hv_RadPin, out hv_AnglePin);
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        HOperatorSet.WriteString(hv_WindowHandler, "角度: " + hv_AnglePin);
                    }
                }

                //铆偏计算
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DistanceRivetOffset1.Dispose();
                    HOperatorSet.DistancePl(hv_RowEdgeFirstL, hv_ColumnEdgeFirstL, (hv_FCR.TupleSelect(
                        0)) + (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos()
                        )), (hv_FCR.TupleSelect(1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(
                        1)) + (200 * (hv_Phi.TupleCos())), out hv_DistanceRivetOffset1);
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DistanceRivetOffset2.Dispose();
                    HOperatorSet.DistancePl(hv_RowEdgeSecondL, hv_ColumnEdgeSecondL, (hv_FCR.TupleSelect(
                        0)) + (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(0)) - (200 * (hv_Phi.TupleCos()
                        )), (hv_FCR.TupleSelect(1)) - (200 * (hv_Phi.TupleSin())), (hv_FCC.TupleSelect(
                        1)) + (200 * (hv_Phi.TupleCos())), out hv_DistanceRivetOffset2);
                }
                hv_DistanceRivetOffset.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DistanceRivetOffset = (((hv_DistanceRivetOffset1 - hv_DistanceRivetOffset2)).TupleAbs()
                        ) * hv_mmpp;
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    HOperatorSet.WriteString(hv_WindowHandler, "铆偏差值: " + hv_DistanceRivetOffset);
                }

                //判断是否为良品
                HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
                set_display_font(hv_WindowHandler, 15, "mono", "true", "false");
                HTuple hv_DetectionIndex = new HTuple();
                HTuple hv_Color = new HTuple();
                HTuple hv = new HTuple();
                hv_DetectionIndex = 0;
                hv_Color = "green";
                for (int i = 0; i < hv_Number - 1; i++)
                {
                    hv.Dispose();
                    switch (i)
                    {
                        case 0: hv = hv_FlowerArea1; break;
                        case 1: hv = hv_FlowerArea2; break;
                        case 2: hv = hv_FlowerArea3; break;
                        case 3: hv = hv_FlowerArea4; break;
                        case 4: hv = hv_FlowerArea5; break;
                        default: break;
                    }
                    if (setting.AreaMinN <= hv && hv <= setting.AreaMaxN)
                    {
                        //HOperatorSet.SetColor(hv_WindowHandler, "green");
                        hv_Color = "green";
                        if (ho_SortedRegionsFlower.IsInitialized())
                        {
                            HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                             1);
                            HOperatorSet.DispObj(ho_ObjectSelected, hv_WindowHandler);
                        }
                    }
                    else
                    {
                        error = true;
                        hv_Color = "red";
                        HOperatorSet.SetColor(hv_WindowHandler, "red");
                        if (ho_SortedRegionsFlower.IsInitialized())
                        {
                            HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected,
                                             1);
                            HOperatorSet.DispObj(ho_ObjectSelected, hv_WindowHandler);
                        }
                        HOperatorSet.SetColor(hv_WindowHandler, "green");
                    }
                    hv_DetectionIndex++;
                    using (new HDevDisposeHelper())
                    {
                        disp_message(hv_WindowHandler, "花瓣面积：" + i.ToString() + hv_FlowerArea1[0] + "," + hv_FlowerArea1[1] + ","
                            + hv_FlowerArea1[2] + "," + hv_FlowerArea1[3], "window", 30 * hv_DetectionIndex, 10, hv_Color,
                            "false");
                    }
                }
                hv.Dispose();//释放临时用于记录花瓣索引的变量
                HOperatorSet.DispObj(ho_ContourLine, hv_WindowHandler);
                if (setting.AngleMinN <= hv_AnglePin && hv_AnglePin <= setting.AngleMaxN)
                {
                    hv_Color = "green";
                    HOperatorSet.DispObj(ho_RegionPin, hv_WindowHandler);
                }
                else
                {
                    error = true;
                    hv_Color = "red";
                    HOperatorSet.SetColor(hv_WindowHandler, "red");
                    HOperatorSet.DispObj(ho_RegionPin, hv_WindowHandler);
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }

                hv_DetectionIndex++;
                disp_message(hv_WindowHandler, "角度：", "window", 30 * hv_DetectionIndex, 400, hv_Color, "false");

                if (setting.L2MinN <= hv_mDistance && hv_mDistance <= setting.L2MaxN)
                {
                    hv_Color = "green";
                }
                else
                {
                    error = true;
                    hv_Color = "red";
                    HOperatorSet.SetColor(hv_WindowHandler, "red");

                }
                hv_DetectionIndex++;
                disp_message(hv_WindowHandler, "L2："+ hv_mDistance, "window", 30 * hv_DetectionIndex, 400, hv_Color, "false");

                if (setting.RivetOffsetMinN <= hv_DistanceRivetOffset && hv_DistanceRivetOffset <= setting.RivetOffsetMaxN)
                {
                    hv_Color = "green";
                    HOperatorSet.DispObj(ho_RegionLeft, hv_WindowHandler);
                    HOperatorSet.DispObj(ho_RegionRight, hv_WindowHandler);
                }
                else
                {
                    error = true;
                    hv_Color = "red";
                    HOperatorSet.SetColor(hv_WindowHandler, "red");
                    HOperatorSet.DispObj(ho_RegionLeft, hv_WindowHandler);
                    HOperatorSet.DispObj(ho_RegionRight, hv_WindowHandler);
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                }
                hv_DetectionIndex++;
                disp_message(hv_WindowHandler, "铆偏："+ hv_DistanceRivetOffset, "window", 30 * hv_DetectionIndex, 10, hv_Color, "false");
                //HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
                #endregion
            }
            catch (HalconException ex)
            {
                #region 资源释放
                ho_Image.Dispose();
                ho_FlowersRoi.Dispose();
                ho_FlowersModelContours.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_ImageFlowers.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionLinesH.Dispose();
                ho_RegionLinesV.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageFlower1.Dispose();
                ho_RegionFlower1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SortedRegionsFlower.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RivetOffsetLines.Dispose();
                ho_RivetOffsetContours.Dispose();
                ho_ContourLine.Dispose();
                ho_RegionLeft.Dispose();
                ho_RegionRight.Dispose();
                ho_RegionDown.Dispose();
                ho_RegionPin.Dispose();

                hv_ImageFiles.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                //hv_WindowHandle.Dispose();
                hv_FlowersRow.Dispose();
                hv_FlowersColumn.Dispose();
                hv_FlowersPhi.Dispose();
                hv_FlowersLength1.Dispose();
                hv_FlowersLength2.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Phi.Dispose();
                hv_mmpp.Dispose();
                hv_areaMax.Dispose();
                hv_areaMin.Dispose();
                hv_L2Max.Dispose();
                hv_L2Min.Dispose();
                hv_AngelMax.Dispose();
                hv_AngelMin.Dispose();
                hv_RowL2.Dispose();
                hv_ColumnL2.Dispose();
                hv_PhiL2.Dispose();
                hv_Length1L2.Dispose();
                hv_Length2L2.Dispose();
                hv_RowLine.Dispose();
                hv_ColumnLine.Dispose();
                hv_PhiLine.Dispose();
                hv_Length1Line.Dispose();
                hv_Length2Line.Dispose();
                hv_RowPin.Dispose();
                hv_ColumnPin.Dispose();
                hv_PhiPin.Dispose();
                hv_Length1Pin.Dispose();
                hv_Length2Pin.Dispose();
                hv_FlowersModelID.Dispose();
                hv_RowFind.Dispose();
                hv_ColumnFind.Dispose();
                hv_AngleFind.Dispose();
                hv_Score.Dispose();
                hv_HomMat2D.Dispose();
                hv_Index.Dispose();
                hv_PhiTransL.Dispose();
                hv_RowTransL.Dispose();
                hv_ColTransL.Dispose();
                hv_PhiTransL2.Dispose();
                hv_RowTransL2.Dispose();
                hv_ColTransL2.Dispose();
                hv_PhiTransPin.Dispose();
                hv_RowTransP.Dispose();
                hv_ColTransP.Dispose();
                hv_Number.Dispose();
                hv_Area.Dispose();
                hv_FCC.Dispose();
                hv_FCR.Dispose();
                hv_FlowerArea1.Dispose();
                hv_FlowerArea2.Dispose();
                hv_FlowerArea3.Dispose();
                hv_FlowerArea4.Dispose();
                hv_FlowerArea5.Dispose();
                hv_i.Dispose();
                hv_j.Dispose();
                hv_AreaF.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Ra.Dispose();
                hv_Rb.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdgeLine.Dispose();
                hv_ColumnEdgeLine.Dispose();
                hv_AmplitudeLine.Dispose();
                hv_DistanceLine.Dispose();
                hv_MeasureHandleL.Dispose();
                hv_RowEdgeFirstL.Dispose();
                hv_ColumnEdgeFirstL.Dispose();
                hv_AmplitudeFirstL.Dispose();
                hv_RowEdgeSecondL.Dispose();
                hv_ColumnEdgeSecondL.Dispose();
                hv_AmplitudeSecondL.Dispose();
                hv_IntraDistance1.Dispose();
                hv_InterDistance1.Dispose();
                hv_RowEdgeD.Dispose();
                hv_ColumnEdgeD.Dispose();
                hv_AmplitudeD.Dispose();
                hv_DistanceD.Dispose();
                hv_Distance.Dispose();
                hv_mDistance.Dispose();
                hv_MeasureHandlePin.Dispose();
                hv_RowEdgePin.Dispose();
                hv_ColumnEdgePin.Dispose();
                hv_AmplitudePin.Dispose();
                hv_Distance1.Dispose();
                hv_RadPin.Dispose();
                hv_AnglePin.Dispose();
                hv_DistanceRivetOffset1.Dispose();
                hv_DistanceRivetOffset2.Dispose();
                hv_DistanceRivetOffset.Dispose();
                hv_NumberPetal.Dispose();
                #endregion
                throw ex;
            }
            finally
            {
                #region 资源释放
                ho_Image.Dispose();
                ho_FlowersRoi.Dispose();
                ho_FlowersModelContours.Dispose();
                ho_RegionAffineTrans.Dispose();
                ho_ImageFlowers.Dispose();
                ho_Region.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionDifference.Dispose();
                ho_RegionOpening.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionLinesH.Dispose();
                ho_RegionLinesV.Dispose();
                ho_Rectangle.Dispose();
                ho_ImageFlower1.Dispose();
                ho_RegionFlower1.Dispose();
                ho_RegionDifference1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SortedRegionsFlower.Dispose();
                ho_ObjectSelected.Dispose();
                ho_RivetOffsetLines.Dispose();
                ho_RivetOffsetContours.Dispose();
                ho_ContourLine.Dispose();
                ho_RegionLeft.Dispose();
                ho_RegionRight.Dispose();
                ho_RegionDown.Dispose();
                ho_RegionPin.Dispose();

                hv_ImageFiles.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                //hv_WindowHandle.Dispose();
                hv_FlowersRow.Dispose();
                hv_FlowersColumn.Dispose();
                hv_FlowersPhi.Dispose();
                hv_FlowersLength1.Dispose();
                hv_FlowersLength2.Dispose();
                hv_Row.Dispose();
                hv_Column.Dispose();
                hv_Phi.Dispose();
                hv_mmpp.Dispose();
                hv_areaMax.Dispose();
                hv_areaMin.Dispose();
                hv_L2Max.Dispose();
                hv_L2Min.Dispose();
                hv_AngelMax.Dispose();
                hv_AngelMin.Dispose();
                hv_RowL2.Dispose();
                hv_ColumnL2.Dispose();
                hv_PhiL2.Dispose();
                hv_Length1L2.Dispose();
                hv_Length2L2.Dispose();
                hv_RowLine.Dispose();
                hv_ColumnLine.Dispose();
                hv_PhiLine.Dispose();
                hv_Length1Line.Dispose();
                hv_Length2Line.Dispose();
                hv_RowPin.Dispose();
                hv_ColumnPin.Dispose();
                hv_PhiPin.Dispose();
                hv_Length1Pin.Dispose();
                hv_Length2Pin.Dispose();
                hv_FlowersModelID.Dispose();
                hv_RowFind.Dispose();
                hv_ColumnFind.Dispose();
                hv_AngleFind.Dispose();
                hv_Score.Dispose();
                hv_HomMat2D.Dispose();
                hv_Index.Dispose();
                hv_PhiTransL.Dispose();
                hv_RowTransL.Dispose();
                hv_ColTransL.Dispose();
                hv_PhiTransL2.Dispose();
                hv_RowTransL2.Dispose();
                hv_ColTransL2.Dispose();
                hv_PhiTransPin.Dispose();
                hv_RowTransP.Dispose();
                hv_ColTransP.Dispose();
                hv_Number.Dispose();
                hv_Area.Dispose();
                hv_FCC.Dispose();
                hv_FCR.Dispose();
                hv_FlowerArea1.Dispose();
                hv_FlowerArea2.Dispose();
                hv_FlowerArea3.Dispose();
                hv_FlowerArea4.Dispose();
                hv_FlowerArea5.Dispose();
                hv_i.Dispose();
                hv_j.Dispose();
                hv_AreaF.Dispose();
                hv_Row1.Dispose();
                hv_Column1.Dispose();
                hv_Ra.Dispose();
                hv_Rb.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdgeLine.Dispose();
                hv_ColumnEdgeLine.Dispose();
                hv_AmplitudeLine.Dispose();
                hv_DistanceLine.Dispose();
                hv_MeasureHandleL.Dispose();
                hv_RowEdgeFirstL.Dispose();
                hv_ColumnEdgeFirstL.Dispose();
                hv_AmplitudeFirstL.Dispose();
                hv_RowEdgeSecondL.Dispose();
                hv_ColumnEdgeSecondL.Dispose();
                hv_AmplitudeSecondL.Dispose();
                hv_IntraDistance1.Dispose();
                hv_InterDistance1.Dispose();
                hv_RowEdgeD.Dispose();
                hv_ColumnEdgeD.Dispose();
                hv_AmplitudeD.Dispose();
                hv_DistanceD.Dispose();
                hv_Distance.Dispose();
                hv_mDistance.Dispose();
                hv_MeasureHandlePin.Dispose();
                hv_RowEdgePin.Dispose();
                hv_ColumnEdgePin.Dispose();
                hv_AmplitudePin.Dispose();
                hv_Distance1.Dispose();
                hv_RadPin.Dispose();
                hv_AnglePin.Dispose();
                hv_DistanceRivetOffset1.Dispose();
                hv_DistanceRivetOffset2.Dispose();
                hv_DistanceRivetOffset.Dispose();
                hv_NumberPetal.Dispose();
                #endregion
            }


            #endregion
        }
        
        public void HFinishDetection(HObject ho_Image, HTuple hv_WindowHandler, MinMaxSetting.FinishTolerance setting, DetectionSetting.FinishDetection detection, out bool error)
        {
            //HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
            error = false;
            if (!ho_Image.IsInitialized() || hv_WindowHandler == null) return;

            #region field
            // Local iconic variables 

            HObject ho_RectangleRoi, ho_RectangleRoi2;
            HObject ho_RegionDifference, ho_RectangleDRoi, ho_ModelContours;
            HObject ho_ContoursAffineTrans1 = null, ho_RoiDTrans = null;
            HObject ho_ImageReducedD = null, ho_RegionD = null, ho_ConnectedRegionsD = null;
            HObject ho_RegionOpeningD = null, ho_RegionLinePin1 = null;
            HObject ho_RegionLinePin2 = null, ho_SelectedRegions = null;

            // Local control variables 

            HTuple hv_ImageFiles = new HTuple(), hv_Width = new HTuple();
            HTuple hv_Height = new HTuple(), hv_WindowHandle = new HTuple();
            HTuple hv_RowRoi = new HTuple(), hv_ColumnRoi = new HTuple();
            HTuple hv_PhiRoi = new HTuple(), hv_Length1Roi = new HTuple();
            HTuple hv_Length2Roi = new HTuple(), hv_Length1Roi2 = new HTuple();
            HTuple hv_Length2Roi2 = new HTuple(), hv_RowP = new HTuple();
            HTuple hv_ColumnP = new HTuple(), hv_PhiP = new HTuple();
            HTuple hv_Length1P = new HTuple(), hv_Length2P = new HTuple();
            HTuple hv_RowD = new HTuple(), hv_ColumnD = new HTuple();
            HTuple hv_PhiD = new HTuple(), hv_Length1D = new HTuple();
            HTuple hv_Length2D = new HTuple(), hv_PinLenght = new HTuple();
            HTuple hv_RowTransP = new HTuple(), hv_ColTransP = new HTuple();
            HTuple hv_PhiTransP = new HTuple(), hv_FinishModelID = new HTuple();
            HTuple hv_Index = new HTuple(), hv_RowF = new HTuple();
            HTuple hv_ColumnF = new HTuple(), hv_AngleF = new HTuple();
            HTuple hv_Score1 = new HTuple(), hv_HomMat2DF = new HTuple();
            HTuple hv_RowTrans = new HTuple(), hv_ColTrans = new HTuple();
            HTuple hv_PhiTrans = new HTuple(), hv_MeasureHandle = new HTuple();
            HTuple hv_RowEdgeFirstH = new HTuple(), hv_ColumnEdgeFirstH = new HTuple();
            HTuple hv_AmplitudeFirstH = new HTuple(), hv_RowEdgeSecondH = new HTuple();
            HTuple hv_ColumnEdgeSecondH = new HTuple(), hv_AmplitudeSecondH = new HTuple();
            HTuple hv_IntraDistanceH = new HTuple(), hv_InterDistanceH = new HTuple();
            HTuple hv_DistanceH = new HTuple(), hv_MeasureHandle1 = new HTuple();
            HTuple hv_RowEdgeFirstW = new HTuple(), hv_ColumnEdgeFirstW = new HTuple();
            HTuple hv_AmplitudeFirstW = new HTuple(), hv_RowEdgeSecondW = new HTuple();
            HTuple hv_ColumnEdgeSecondW = new HTuple(), hv_AmplitudeSecondW = new HTuple();
            HTuple hv_IntraDistanceW = new HTuple(), hv_InterDistanceW = new HTuple();
            HTuple hv_DistanceW = new HTuple(), hv_NumberOfPin = new HTuple();
            HTuple hv_MeasureHandle2 = new HTuple(), hv_RowEdgeFirstP = new HTuple();
            HTuple hv_ColumnEdgeFirstP = new HTuple(), hv_AmplitudeFirstP = new HTuple();
            HTuple hv_RowEdgeSecondP = new HTuple(), hv_ColumnEdgeSecondP = new HTuple();
            HTuple hv_AmplitudeSecondP = new HTuple(), hv_IntraDistanceP = new HTuple();
            HTuple hv_InterDistanceP = new HTuple(), hv_RowPin1 = new HTuple();
            HTuple hv_RowPin2 = new HTuple(), hv_ColumnPin = new HTuple();
            HTuple hv_MinDistance = new HTuple(), hv_RowMinP1 = new HTuple();
            HTuple hv_ColumnMinP1 = new HTuple(), hv_RowMinP2 = new HTuple();
            HTuple hv_ColumnMinP2 = new HTuple(), hv_DistanceP = new HTuple();
            HTuple hv_DistancePinWidth = new HTuple(), hv_DistanceMin = new HTuple();
            HTuple hv_DistanceMax = new HTuple(), hv_DifferenceOfPin = new HTuple();
            #endregion

            try
            {
                // Stack for temporary objects 
                HObject[] OTemp = new HObject[20];

                // Initialize local and output iconic variables 
                HOperatorSet.GenEmptyObj(out ho_Image);
                HOperatorSet.GenEmptyObj(out ho_RectangleRoi);
                HOperatorSet.GenEmptyObj(out ho_RectangleRoi2);
                HOperatorSet.GenEmptyObj(out ho_RegionDifference);
                HOperatorSet.GenEmptyObj(out ho_RectangleDRoi);
                HOperatorSet.GenEmptyObj(out ho_ModelContours);
                HOperatorSet.GenEmptyObj(out ho_ContoursAffineTrans1);
                HOperatorSet.GenEmptyObj(out ho_RoiDTrans);
                HOperatorSet.GenEmptyObj(out ho_ImageReducedD);
                HOperatorSet.GenEmptyObj(out ho_RegionD);
                HOperatorSet.GenEmptyObj(out ho_ConnectedRegionsD);
                HOperatorSet.GenEmptyObj(out ho_RegionOpeningD);
                HOperatorSet.GenEmptyObj(out ho_RegionLinePin1);
                HOperatorSet.GenEmptyObj(out ho_RegionLinePin2);
                HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
                //Image Acquisition 01: Code generated by Image Acquisition 01
                HOperatorSet.ClearWindow(hv_WindowHandler);
                hv_ImageFiles.Dispose();
                HOperatorSet.ListFiles("C:/Users/W/Desktop/picture/finish", (new HTuple("files")).TupleConcat(
                    "follow_links"), out hv_ImageFiles);
                {
                    HTuple ExpTmpOutVar_0;
                    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
                        "ignore_case"), out ExpTmpOutVar_0);
                    hv_ImageFiles.Dispose();
                    hv_ImageFiles = ExpTmpOutVar_0;
                }

                //*创建图像定位模板
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_Image.Dispose();
                    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(0));
                }
                hv_Width.Dispose(); hv_Height.Dispose();
                HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
                HOperatorSet.SetPart(hv_WindowHandler, 0, 0, hv_Height, hv_Width);
                //dev_open_window(...);
                HOperatorSet.DispObj(ho_Image, hv_WindowHandler);
                HOperatorSet.SetDraw(hv_WindowHandler, "margin");
                HOperatorSet.SetColor(hv_WindowHandler, "green");
                //划分roi
                //draw_rectangle2 (WindowHandle, RowRoi, ColumnRoi, PhiRoi, Length1Roi, Length2Roi)
                hv_RowRoi.Dispose();
                hv_RowRoi = 629;
                hv_ColumnRoi.Dispose();
                hv_ColumnRoi = 941;
                hv_PhiRoi.Dispose();
                hv_PhiRoi = -0;
                hv_Length1Roi.Dispose();
                hv_Length1Roi = 244;
                hv_Length2Roi.Dispose();
                hv_Length2Roi = 244;

                ho_RectangleRoi.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleRoi, hv_RowRoi, hv_ColumnRoi, hv_PhiRoi,
                    hv_Length1Roi, hv_Length2Roi);
                //draw_rectangle2 (WindowHandle, RowRoi2, ColumnRoi2, PhiRoi2, Length1Roi2, Length2Roi2)
                hv_Length1Roi2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Length1Roi2 = hv_Length1Roi * 0.25;
                }
                hv_Length2Roi2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_Length2Roi2 = hv_Length2Roi * 0.65;
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RectangleRoi2.Dispose();
                    HOperatorSet.GenRectangle2(out ho_RectangleRoi2, hv_RowRoi, hv_ColumnRoi + 210,
                        hv_PhiRoi, hv_Length1Roi2, hv_Length2Roi2);
                }
                ho_RegionDifference.Dispose();
                HOperatorSet.Difference(ho_RectangleRoi, ho_RectangleRoi2, out ho_RegionDifference
                    );

                //高度检测框
                //draw_rectangle2 (WindowHandle, RowH, ColumnH, PhiH, Length1H, Length2H)
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RectangleRoi.Dispose();
                    HOperatorSet.GenRectangle2(out ho_RectangleRoi, hv_RowRoi, hv_ColumnRoi, hv_PhiRoi,
                        hv_Length1Roi + 50, hv_Length2Roi - 200);
                }

                //宽度检测框
                //draw_rectangle2 (WindowHandle, RowW, ColumnW, PhiW, Length1W, Length2W)
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    ho_RectangleRoi.Dispose();
                    HOperatorSet.GenRectangle2(out ho_RectangleRoi, hv_RowRoi, hv_ColumnRoi, hv_PhiRoi - 1.57,
                        hv_Length1Roi + 50, hv_Length2Roi - 150);
                }

                //计算像素当量
                //calculate_pixel_equivalent (Image, RowRoi, ColumnRoi, PhiRoi, Length1Roi, Length2Roi, Width, Height, MMPPV, MMPPH)

                //脚距检测框Roi
                //draw_rectangle2 (WindowHandle, RowP, ColumnP, PhiP, Length1P, Length2P)
                hv_RowP.Dispose();
                hv_RowP = 609;
                hv_ColumnP.Dispose();
                hv_ColumnP = 1233;
                hv_PhiP.Dispose();
                hv_PhiP = -1.57;
                hv_Length1P.Dispose();
                hv_Length1P = 244;
                hv_Length2P.Dispose();
                hv_Length2P = 32;

                //高低脚检测框Roi(difference of height)
                //draw_rectangle2 (WindowHandle, RowD, ColumnD, PhiD, Length1D, Length2D)
                hv_PinLenght.Dispose();
                hv_PinLenght = new HTuple();
                hv_PinLenght[0] = 0;
                hv_PinLenght[1] = 0;
                hv_RowD.Dispose();
                hv_RowD = 629;
                hv_ColumnD.Dispose();
                hv_ColumnD = 1301;
                hv_PhiD.Dispose();
                hv_PhiD = -1.57;
                hv_Length1D.Dispose();
                hv_Length1D = 228;
                hv_Length2D.Dispose();
                hv_Length2D = 112;
                ho_RectangleDRoi.Dispose();
                HOperatorSet.GenRectangle2(out ho_RectangleDRoi, hv_RowD, hv_ColumnD, hv_PhiD,
                    hv_Length1D, hv_Length2D);
                //gen_measure_rectangle2 (RowTransP, ColTransP+100, PhiTransP, Length1P, Length2P, Width, Height, 'nearest_neighbor', MeasureHandle3)
                //measure_pairs (Image, MeasureHandle3, 1, 30, 'all', 'all', RowEdgeFirst2, ColumnEdgeFirst2, AmplitudeFirst2, RowEdgeSecond2, ColumnEdgeSecond2, AmplitudeSecond2, IntraDistance2, InterDistance2)

                //芯包模板
                //gen_finish_shape_model (Image, WindowHandle, 'D:/CENJE-Vison/Model/FinishModel.shm')
                hv_FinishModelID.Dispose();
                HOperatorSet.ReadShapeModel("D:/CENJE-Vison/Model/FinishModel.shm", out hv_FinishModelID);
                ho_ModelContours.Dispose();
                HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_FinishModelID, 1);


                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_ImageFiles.TupleLength()
                    )) - 1); hv_Index = (int)hv_Index + 1)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        ho_Image.Dispose();
                        HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
                    }
                    HOperatorSet.SetColor(hv_WindowHandler, "green");
                    //产品定位
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_RowF.Dispose(); hv_ColumnF.Dispose(); hv_AngleF.Dispose(); hv_Score1.Dispose();
                        HOperatorSet.FindShapeModel(ho_Image, hv_FinishModelID, (new HTuple(-15)).TupleRad()
                            , (new HTuple(30)).TupleRad(), 0.5, 1, 0.5, "least_squares", 0, 0.9, out hv_RowF,
                            out hv_ColumnF, out hv_AngleF, out hv_Score1);
                    }
                    if ((int)(new HTuple(hv_Score1.TupleGreater(0.7))) != 0)
                    {
                        hv_HomMat2DF.Dispose();
                        HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RowF, hv_ColumnF, hv_AngleF,
                            out hv_HomMat2DF);
                        ho_ContoursAffineTrans1.Dispose();
                        HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffineTrans1,
                            hv_HomMat2DF);
                        hv_HomMat2DF.Dispose();
                        HOperatorSet.VectorAngleToRigid(hv_RowRoi, hv_ColumnRoi, 0, hv_RowF, hv_ColumnF,
                            hv_AngleF, out hv_HomMat2DF);
                        hv_RowTrans.Dispose(); hv_ColTrans.Dispose();
                        HOperatorSet.AffineTransPixel(hv_HomMat2DF, hv_RowRoi, hv_ColumnRoi, out hv_RowTrans,
                            out hv_ColTrans);
                        hv_PhiTrans.Dispose();
                        hv_PhiTrans = new HTuple(hv_AngleF);
                        hv_RowTransP.Dispose(); hv_ColTransP.Dispose();
                        HOperatorSet.AffineTransPixel(hv_HomMat2DF, hv_RowP, hv_ColumnP, out hv_RowTransP,
                            out hv_ColTransP);
                        hv_PhiTransP.Dispose();
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_PhiTransP = hv_PhiP + (hv_AngleF - hv_PhiRoi);
                        }
                        //affine_trans_pixel (HomMat2DF, RowTransD, ColTransD, RowTransD, ColTransD)
                        //PhiTransD := PhiD+(AngleF-PhiRoi)
                        ho_RoiDTrans.Dispose();
                        HOperatorSet.AffineTransRegion(ho_RectangleDRoi, out ho_RoiDTrans, hv_HomMat2DF,
                            "nearest_neighbor");

                        //************************
                        //高度检测
                        //************************
                        //gen_rectangle2 (Rectangle, RowTrans, ColTrans, PhiTrans, Length1Roi+50, Length2Roi-200)
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_MeasureHandle.Dispose();
                            HOperatorSet.GenMeasureRectangle2(hv_RowTrans, hv_ColTrans, hv_PhiTrans,
                                hv_Length1Roi + 50, hv_Length2Roi - 200, hv_Width, hv_Height, "nearest_neighbor",
                                out hv_MeasureHandle);
                        }
                        hv_RowEdgeFirstH.Dispose(); hv_ColumnEdgeFirstH.Dispose(); hv_AmplitudeFirstH.Dispose(); hv_RowEdgeSecondH.Dispose(); hv_ColumnEdgeSecondH.Dispose(); hv_AmplitudeSecondH.Dispose(); hv_IntraDistanceH.Dispose(); hv_InterDistanceH.Dispose();
                        HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle, 1, 30, "all", "all",
                            out hv_RowEdgeFirstH, out hv_ColumnEdgeFirstH, out hv_AmplitudeFirstH,
                            out hv_RowEdgeSecondH, out hv_ColumnEdgeSecondH, out hv_AmplitudeSecondH,
                            out hv_IntraDistanceH, out hv_InterDistanceH);
                        if ((int)((new HTuple((new HTuple(hv_RowEdgeFirstH.TupleLength())).TupleEqual(
                            1))).TupleAnd(new HTuple((new HTuple(hv_IntraDistanceH.TupleLength())).TupleEqual(
                            1)))) != 0)
                        {
                            //            DistanceH:=IntraDistanceH*MMPPH
                            if ((int)(new HTuple((new HTuple((new HTuple(4)).TupleLess(hv_DistanceH))).TupleLess(
                                6))) != 0)
                            {
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    disp_message(hv_WindowHandler, "高度：" + hv_DistanceH, "window",
                                        12, 12, "green", "false");
                                }
                            }
                            else
                            {
                                HOperatorSet.SetColor(hv_WindowHandler, "red");
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    disp_message(hv_WindowHandler, "高度：" + hv_DistanceH, "window",
                                        12, 12, "red", "false");
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeFirstH - (50 * (hv_PhiTrans.TupleCos()
                                    )), hv_ColumnEdgeFirstH - (50 * (hv_PhiTrans.TupleSin())), hv_RowEdgeFirstH + (50 * (hv_PhiTrans.TupleCos()
                                    )), hv_ColumnEdgeFirstH + (50 * (hv_PhiTrans.TupleSin())));
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeSecondH - (50 * (hv_PhiTrans.TupleCos()
                                    )), hv_ColumnEdgeSecondH - (50 * (hv_PhiTrans.TupleSin())), hv_RowEdgeSecondH + (50 * (hv_PhiTrans.TupleCos()
                                    )), hv_ColumnEdgeSecondH + (50 * (hv_PhiTrans.TupleSin())));
                            }
                            HOperatorSet.SetColor(hv_WindowHandler, "green");
                        }


                        //***********************
                        //宽度检测
                        //***********************
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_MeasureHandle1.Dispose();
                            HOperatorSet.GenMeasureRectangle2(hv_RowTrans, hv_ColTrans, hv_PhiTrans - 1.57,
                                hv_Length1Roi + 50, hv_Length2Roi - 150, hv_Width, hv_Height, "nearest_neighbor",
                                out hv_MeasureHandle1);
                        }
                        hv_RowEdgeFirstW.Dispose(); hv_ColumnEdgeFirstW.Dispose(); hv_AmplitudeFirstW.Dispose(); hv_RowEdgeSecondW.Dispose(); hv_ColumnEdgeSecondW.Dispose(); hv_AmplitudeSecondW.Dispose(); hv_IntraDistanceW.Dispose(); hv_InterDistanceW.Dispose();
                        HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle1, 1, 30, "all", "all",
                            out hv_RowEdgeFirstW, out hv_ColumnEdgeFirstW, out hv_AmplitudeFirstW,
                            out hv_RowEdgeSecondW, out hv_ColumnEdgeSecondW, out hv_AmplitudeSecondW,
                            out hv_IntraDistanceW, out hv_InterDistanceW);
                        if ((int)((new HTuple((new HTuple(hv_RowEdgeFirstW.TupleLength())).TupleEqual(
                            1))).TupleAnd(new HTuple((new HTuple(hv_IntraDistanceW.TupleLength())).TupleEqual(
                            1)))) != 0)
                        {
                            //            DistanceW:=IntraDistanceW*MMPPV
                            if ((int)(new HTuple((new HTuple((new HTuple(3)).TupleLess(hv_DistanceW))).TupleLess(
                                5))) != 0)
                            {
                                HOperatorSet.SetColor(hv_WindowHandler, "green");
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    disp_message(hv_WindowHandler, "宽度：" + hv_DistanceH, "window",
                                        24, 12, "green", "false");
                                }
                            }
                            else
                            {
                                HOperatorSet.SetColor(hv_WindowHandler, "red");
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    disp_message(hv_WindowHandler, "宽度：" + hv_DistanceH, "window",
                                        24, 12, "red", "false");
                                }
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeFirstW - (50 * (hv_PhiTrans.TupleSin()
                                    )), hv_ColumnEdgeFirstW - (50 * (hv_PhiTrans.TupleCos())), hv_RowEdgeFirstW + (50 * (hv_PhiTrans.TupleSin()
                                    )), hv_ColumnEdgeFirstW + (50 * (hv_PhiTrans.TupleCos())));
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                HOperatorSet.DispLine(hv_WindowHandler, hv_RowEdgeSecondW - (50 * (hv_PhiTrans.TupleSin()
                                    )), hv_ColumnEdgeSecondW - (50 * (hv_PhiTrans.TupleCos())), hv_RowEdgeSecondW + (50 * (hv_PhiTrans.TupleSin()
                                    )), hv_ColumnEdgeSecondW + (50 * (hv_PhiTrans.TupleCos())));
                            }
                            HOperatorSet.SetColor(hv_WindowHandler, "green");
                        }

                        //***************************
                        //脚距检测
                        //***************************
                        ho_ImageReducedD.Dispose();
                        HOperatorSet.ReduceDomain(ho_Image, ho_RoiDTrans, out ho_ImageReducedD);
                        //dev_clear_window ()
                        //dev_display (ImageReducedD)
                        ho_RegionD.Dispose();
                        HOperatorSet.Threshold(ho_ImageReducedD, out ho_RegionD, 0, 100);
                        ho_ConnectedRegionsD.Dispose();
                        HOperatorSet.Connection(ho_RegionD, out ho_ConnectedRegionsD);
                        ho_RegionOpeningD.Dispose();
                        HOperatorSet.OpeningCircle(ho_ConnectedRegionsD, out ho_RegionOpeningD, 25);
                        hv_NumberOfPin.Dispose();
                        HOperatorSet.CountObj(ho_RegionOpeningD, out hv_NumberOfPin);
                        if ((int)(new HTuple(hv_NumberOfPin.TupleEqual(2))) != 0)
                        {
                            hv_MeasureHandle2.Dispose();
                            HOperatorSet.GenMeasureRectangle2(hv_RowTransP, hv_ColTransP, hv_PhiTransP,
                                hv_Length1P, hv_Length2P, hv_Width, hv_Height, "nearest_neighbor",
                                out hv_MeasureHandle2);
                            hv_RowEdgeFirstP.Dispose(); hv_ColumnEdgeFirstP.Dispose(); hv_AmplitudeFirstP.Dispose(); hv_RowEdgeSecondP.Dispose(); hv_ColumnEdgeSecondP.Dispose(); hv_AmplitudeSecondP.Dispose(); hv_IntraDistanceP.Dispose(); hv_InterDistanceP.Dispose();
                            HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandle2, 1, 30, "all", "all",
                                out hv_RowEdgeFirstP, out hv_ColumnEdgeFirstP, out hv_AmplitudeFirstP,
                                out hv_RowEdgeSecondP, out hv_ColumnEdgeSecondP, out hv_AmplitudeSecondP,
                                out hv_IntraDistanceP, out hv_InterDistanceP);
                            hv_RowPin1.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_RowPin1 = ((hv_RowEdgeFirstP.TupleSelect(
                                    0)) + (hv_RowEdgeSecondP.TupleSelect(0))) / 2;
                            }
                            hv_RowPin2.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_RowPin2 = ((hv_RowEdgeFirstP.TupleSelect(
                                    1)) + (hv_RowEdgeSecondP.TupleSelect(1))) / 2;
                            }
                            hv_ColumnPin.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_ColumnPin = hv_ColumnEdgeFirstP.TupleSelect(
                                    0);
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_RegionLinePin1.Dispose();
                                HOperatorSet.GenRegionLine(out ho_RegionLinePin1, hv_RowPin1 - (70 * (hv_PhiTransP.TupleCos()
                                    )), hv_ColumnPin - (70 * (hv_PhiTransP.TupleSin())), hv_RowPin1 + (70 * (hv_PhiTransP.TupleCos()
                                    )), hv_ColumnPin + (70 * (hv_PhiTransP.TupleSin())));
                            }
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                ho_RegionLinePin2.Dispose();
                                HOperatorSet.GenRegionLine(out ho_RegionLinePin2, hv_RowPin2 - (70 * (hv_PhiTransP.TupleCos()
                                    )), hv_ColumnPin - (70 * (hv_PhiTransP.TupleSin())), hv_RowPin2 + (70 * (hv_PhiTransP.TupleCos()
                                    )), hv_ColumnPin + (70 * (hv_PhiTransP.TupleSin())));
                            }
                            hv_MinDistance.Dispose(); hv_RowMinP1.Dispose(); hv_ColumnMinP1.Dispose(); hv_RowMinP2.Dispose(); hv_ColumnMinP2.Dispose();
                            HOperatorSet.DistanceRrMin(ho_RegionLinePin1, ho_RegionLinePin2, out hv_MinDistance,
                                out hv_RowMinP1, out hv_ColumnMinP1, out hv_RowMinP2, out hv_ColumnMinP2);
                            //            DistanceP:=MinDistance*MMPPV
                            if ((int)((new HTuple((new HTuple(2)).TupleLess(hv_DistanceP))).TupleAnd(
                                new HTuple(hv_DistanceP.TupleLess(3.5)))) != 0)
                            {
                                HOperatorSet.SetColor(hv_WindowHandler, "green");
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    disp_message(hv_WindowHandler, "脚距：" + hv_DistanceP, "window",
                                        24, 12, "green", "false");
                                }
                            }
                            else
                            {
                                HOperatorSet.SetColor(hv_WindowHandler, "red");
                                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                                {
                                    disp_message(hv_WindowHandler, "脚距：" + hv_DistanceP, "window",
                                        24, 12, "red", "false");
                                }
                            }
                            HOperatorSet.DispLine(hv_WindowHandler, hv_RowMinP1, hv_ColumnPin,
                                hv_RowMinP2, hv_ColumnPin);
                            HOperatorSet.SetColor(hv_WindowHandler, "green");
                        }
                        else
                        {
                            disp_message(hv_WindowHandler, "缺脚", "window", 24, 12, "red",
                                "false");
                        }



                        //**************************
                        //高低脚检测
                        //**************************
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DistancePinWidth.Dispose();
                            HOperatorSet.DistancePp(hv_RowEdgeFirstP.TupleSelect(0), hv_ColumnEdgeFirstP.TupleSelect(
                                0), hv_RowEdgeSecondP.TupleSelect(0), hv_ColumnEdgeSecondP.TupleSelect(
                                0), out hv_DistancePinWidth);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            HObject ExpTmpOutVar_0;
                            HOperatorSet.OpeningCircle(ho_RegionOpeningD, out ExpTmpOutVar_0, (hv_DistancePinWidth / 2) - 5);
                            ho_RegionOpeningD.Dispose();
                            ho_RegionOpeningD = ExpTmpOutVar_0;
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_SelectedRegions.Dispose();
                            HOperatorSet.SelectShape(ho_RegionOpeningD, out ho_SelectedRegions, "row",
                                "and", 0, hv_RowF + 10);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DistanceMin.Dispose(); hv_DistanceMax.Dispose();
                            HOperatorSet.DistanceLr(ho_SelectedRegions, hv_RowEdgeSecondH - (50 * (hv_PhiTrans.TupleCos()
                                )), hv_ColumnEdgeSecondH - (50 * (hv_PhiTrans.TupleSin())), hv_RowEdgeSecondH + (50 * (hv_PhiTrans.TupleCos()
                                )), hv_ColumnEdgeSecondH + (50 * (hv_PhiTrans.TupleSin())), out hv_DistanceMin,
                                out hv_DistanceMax);
                        }
                        if (hv_PinLenght == null)
                            hv_PinLenght = new HTuple();
                        hv_PinLenght[0] = hv_DistanceMax;
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_SelectedRegions.Dispose();
                            HOperatorSet.SelectShape(ho_RegionOpeningD, out ho_SelectedRegions, "row",
                                "and", hv_RowF - 10, 1000);
                        }
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            hv_DistanceMin.Dispose(); hv_DistanceMax.Dispose();
                            HOperatorSet.DistanceLr(ho_SelectedRegions, hv_RowEdgeSecondH - (50 * (hv_PhiTrans.TupleCos()
                                )), hv_ColumnEdgeSecondH - (50 * (hv_PhiTrans.TupleSin())), hv_RowEdgeSecondH + (50 * (hv_PhiTrans.TupleCos()
                                )), hv_ColumnEdgeSecondH + (50 * (hv_PhiTrans.TupleSin())), out hv_DistanceMin,
                                out hv_DistanceMax);
                        }
                        if (hv_PinLenght == null)
                            hv_PinLenght = new HTuple();
                        hv_PinLenght[1] = hv_DistanceMax;
                        //        DifferenceOfPin:=MMPPH*(PinLenght[1]-PinLenght[0])
                        if ((int)(new HTuple(((hv_DifferenceOfPin.TupleAbs())).TupleLess(0.25))) != 0)
                        {
                            HOperatorSet.SetColor(hv_WindowHandler, "green");
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                disp_message(hv_WindowHandler, "高低脚：" + hv_DistanceP, "window",
                                    48, 12, "green", "false");
                            }
                        }
                        else
                        {
                            HOperatorSet.SetColor(hv_WindowHandler, "red");
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                disp_message(hv_WindowHandler, "高低脚：" + hv_DistanceP, "window",
                                    48, 12, "green", "false");
                            }
                        }
                        //write_string (WindowHandle, 'DifferenceOfPin: '+DifferenceOfPin)
                        HOperatorSet.DispObj(ho_RegionOpeningD, hv_WindowHandler);
                        HOperatorSet.SetColor(hv_WindowHandler, "green");
                    }
                    else
                    {
                        disp_message(hv_WindowHandler, "定位失败或空料！", "window",
                            12, 12, "red", "false");
                    }
                    HOperatorSet.ClearWindow(hv_WindowHandler);
                }
                #region
                ho_Image.Dispose();
                ho_RectangleRoi.Dispose();
                ho_RectangleRoi2.Dispose();
                ho_RegionDifference.Dispose();
                ho_RectangleDRoi.Dispose();
                ho_ModelContours.Dispose();
                ho_ContoursAffineTrans1.Dispose();
                ho_RoiDTrans.Dispose();
                ho_ImageReducedD.Dispose();
                ho_RegionD.Dispose();
                ho_ConnectedRegionsD.Dispose();
                ho_RegionOpeningD.Dispose();
                ho_RegionLinePin1.Dispose();
                ho_RegionLinePin2.Dispose();
                ho_SelectedRegions.Dispose();

                hv_ImageFiles.Dispose();
                hv_Width.Dispose();
                hv_Height.Dispose();
                hv_WindowHandle.Dispose();
                hv_RowRoi.Dispose();
                hv_ColumnRoi.Dispose();
                hv_PhiRoi.Dispose();
                hv_Length1Roi.Dispose();
                hv_Length2Roi.Dispose();
                hv_Length1Roi2.Dispose();
                hv_Length2Roi2.Dispose();
                hv_RowP.Dispose();
                hv_ColumnP.Dispose();
                hv_PhiP.Dispose();
                hv_Length1P.Dispose();
                hv_Length2P.Dispose();
                hv_RowD.Dispose();
                hv_ColumnD.Dispose();
                hv_PhiD.Dispose();
                hv_Length1D.Dispose();
                hv_Length2D.Dispose();
                hv_PinLenght.Dispose();
                hv_RowTransP.Dispose();
                hv_ColTransP.Dispose();
                hv_PhiTransP.Dispose();
                hv_FinishModelID.Dispose();
                hv_Index.Dispose();
                hv_RowF.Dispose();
                hv_ColumnF.Dispose();
                hv_AngleF.Dispose();
                hv_Score1.Dispose();
                hv_HomMat2DF.Dispose();
                hv_RowTrans.Dispose();
                hv_ColTrans.Dispose();
                hv_PhiTrans.Dispose();
                hv_MeasureHandle.Dispose();
                hv_RowEdgeFirstH.Dispose();
                hv_ColumnEdgeFirstH.Dispose();
                hv_AmplitudeFirstH.Dispose();
                hv_RowEdgeSecondH.Dispose();
                hv_ColumnEdgeSecondH.Dispose();
                hv_AmplitudeSecondH.Dispose();
                hv_IntraDistanceH.Dispose();
                hv_InterDistanceH.Dispose();
                hv_DistanceH.Dispose();
                hv_MeasureHandle1.Dispose();
                hv_RowEdgeFirstW.Dispose();
                hv_ColumnEdgeFirstW.Dispose();
                hv_AmplitudeFirstW.Dispose();
                hv_RowEdgeSecondW.Dispose();
                hv_ColumnEdgeSecondW.Dispose();
                hv_AmplitudeSecondW.Dispose();
                hv_IntraDistanceW.Dispose();
                hv_InterDistanceW.Dispose();
                hv_DistanceW.Dispose();
                hv_NumberOfPin.Dispose();
                hv_MeasureHandle2.Dispose();
                hv_RowEdgeFirstP.Dispose();
                hv_ColumnEdgeFirstP.Dispose();
                hv_AmplitudeFirstP.Dispose();
                hv_RowEdgeSecondP.Dispose();
                hv_ColumnEdgeSecondP.Dispose();
                hv_AmplitudeSecondP.Dispose();
                hv_IntraDistanceP.Dispose();
                hv_InterDistanceP.Dispose();
                hv_RowPin1.Dispose();
                hv_RowPin2.Dispose();
                hv_ColumnPin.Dispose();
                hv_MinDistance.Dispose();
                hv_RowMinP1.Dispose();
                hv_ColumnMinP1.Dispose();
                hv_RowMinP2.Dispose();
                hv_ColumnMinP2.Dispose();
                hv_DistanceP.Dispose();
                hv_DistancePinWidth.Dispose();
                hv_DistanceMin.Dispose();
                hv_DistanceMax.Dispose();
                hv_DifferenceOfPin.Dispose();
                #endregion
            }
            catch (HalconException ex)
            {
                throw ex;
            }

            #region
            ho_Image.Dispose();
            ho_RectangleRoi.Dispose();
            ho_RectangleRoi2.Dispose();
            ho_RegionDifference.Dispose();
            ho_RectangleDRoi.Dispose();
            ho_ModelContours.Dispose();
            ho_ContoursAffineTrans1.Dispose();
            ho_RoiDTrans.Dispose();
            ho_ImageReducedD.Dispose();
            ho_RegionD.Dispose();
            ho_ConnectedRegionsD.Dispose();
            ho_RegionOpeningD.Dispose();
            ho_RegionLinePin1.Dispose();
            ho_RegionLinePin2.Dispose();
            ho_SelectedRegions.Dispose();

            hv_ImageFiles.Dispose();
            hv_Width.Dispose();
            hv_Height.Dispose();
            hv_WindowHandle.Dispose();
            hv_RowRoi.Dispose();
            hv_ColumnRoi.Dispose();
            hv_PhiRoi.Dispose();
            hv_Length1Roi.Dispose();
            hv_Length2Roi.Dispose();
            hv_Length1Roi2.Dispose();
            hv_Length2Roi2.Dispose();
            hv_RowP.Dispose();
            hv_ColumnP.Dispose();
            hv_PhiP.Dispose();
            hv_Length1P.Dispose();
            hv_Length2P.Dispose();
            hv_RowD.Dispose();
            hv_ColumnD.Dispose();
            hv_PhiD.Dispose();
            hv_Length1D.Dispose();
            hv_Length2D.Dispose();
            hv_PinLenght.Dispose();
            hv_RowTransP.Dispose();
            hv_ColTransP.Dispose();
            hv_PhiTransP.Dispose();
            hv_FinishModelID.Dispose();
            hv_Index.Dispose();
            hv_RowF.Dispose();
            hv_ColumnF.Dispose();
            hv_AngleF.Dispose();
            hv_Score1.Dispose();
            hv_HomMat2DF.Dispose();
            hv_RowTrans.Dispose();
            hv_ColTrans.Dispose();
            hv_PhiTrans.Dispose();
            hv_MeasureHandle.Dispose();
            hv_RowEdgeFirstH.Dispose();
            hv_ColumnEdgeFirstH.Dispose();
            hv_AmplitudeFirstH.Dispose();
            hv_RowEdgeSecondH.Dispose();
            hv_ColumnEdgeSecondH.Dispose();
            hv_AmplitudeSecondH.Dispose();
            hv_IntraDistanceH.Dispose();
            hv_InterDistanceH.Dispose();
            hv_DistanceH.Dispose();
            hv_MeasureHandle1.Dispose();
            hv_RowEdgeFirstW.Dispose();
            hv_ColumnEdgeFirstW.Dispose();
            hv_AmplitudeFirstW.Dispose();
            hv_RowEdgeSecondW.Dispose();
            hv_ColumnEdgeSecondW.Dispose();
            hv_AmplitudeSecondW.Dispose();
            hv_IntraDistanceW.Dispose();
            hv_InterDistanceW.Dispose();
            hv_DistanceW.Dispose();
            hv_NumberOfPin.Dispose();
            hv_MeasureHandle2.Dispose();
            hv_RowEdgeFirstP.Dispose();
            hv_ColumnEdgeFirstP.Dispose();
            hv_AmplitudeFirstP.Dispose();
            hv_RowEdgeSecondP.Dispose();
            hv_ColumnEdgeSecondP.Dispose();
            hv_AmplitudeSecondP.Dispose();
            hv_IntraDistanceP.Dispose();
            hv_InterDistanceP.Dispose();
            hv_RowPin1.Dispose();
            hv_RowPin2.Dispose();
            hv_ColumnPin.Dispose();
            hv_MinDistance.Dispose();
            hv_RowMinP1.Dispose();
            hv_ColumnMinP1.Dispose();
            hv_RowMinP2.Dispose();
            hv_ColumnMinP2.Dispose();
            hv_DistanceP.Dispose();
            hv_DistancePinWidth.Dispose();
            hv_DistanceMin.Dispose();
            hv_DistanceMax.Dispose();
            hv_DifferenceOfPin.Dispose();
            #endregion
        }


        /// <summary>
        /// 设置窗口字体
        /// </summary>
        public void set_display_font(HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font,
      HTuple hv_Bold, HTuple hv_Slant)
        {
            // Local iconic variables 

            // Local control variables 

            HTuple hv_OS = new HTuple(), hv_Fonts = new HTuple();
            HTuple hv_Style = new HTuple(), hv_Exception = new HTuple();
            HTuple hv_AvailableFonts = new HTuple(), hv_Fdx = new HTuple();
            HTuple hv_Indices = new HTuple();
            HTuple hv_Font_COPY_INP_TMP = new HTuple(hv_Font);
            HTuple hv_Size_COPY_INP_TMP = new HTuple(hv_Size);

            // Initialize local and output iconic variables 
            try
            {
                hv_OS.Dispose();
                HOperatorSet.GetSystem("operating_system", out hv_OS);
                if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                    new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
                {
                    hv_Size_COPY_INP_TMP.Dispose();
                    hv_Size_COPY_INP_TMP = 16;
                }
                if ((int)(new HTuple(((hv_OS.TupleSubstr(0, 2))).TupleEqual("Win"))) != 0)
                {
                    //Restore previous behaviour
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = ((1.13677 * hv_Size_COPY_INP_TMP)).TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                else
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Size = hv_Size_COPY_INP_TMP.TupleInt()
                                ;
                            hv_Size_COPY_INP_TMP.Dispose();
                            hv_Size_COPY_INP_TMP = ExpTmpLocalVar_Size;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Courier";
                    hv_Fonts[1] = "Courier 10 Pitch";
                    hv_Fonts[2] = "Courier New";
                    hv_Fonts[3] = "CourierNew";
                    hv_Fonts[4] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Consolas";
                    hv_Fonts[1] = "Menlo";
                    hv_Fonts[2] = "Courier";
                    hv_Fonts[3] = "Courier 10 Pitch";
                    hv_Fonts[4] = "FreeMono";
                    hv_Fonts[5] = "Liberation Mono";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Luxi Sans";
                    hv_Fonts[1] = "DejaVu Sans";
                    hv_Fonts[2] = "FreeSans";
                    hv_Fonts[3] = "Arial";
                    hv_Fonts[4] = "Liberation Sans";
                }
                else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple();
                    hv_Fonts[0] = "Times New Roman";
                    hv_Fonts[1] = "Luxi Serif";
                    hv_Fonts[2] = "DejaVu Serif";
                    hv_Fonts[3] = "FreeSerif";
                    hv_Fonts[4] = "Utopia";
                    hv_Fonts[5] = "Liberation Serif";
                }
                else
                {
                    hv_Fonts.Dispose();
                    hv_Fonts = new HTuple(hv_Font_COPY_INP_TMP);
                }
                hv_Style.Dispose();
                hv_Style = "";
                if ((int)(new HTuple(hv_Bold.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Bold";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Bold.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Bold";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Slant.TupleEqual("true"))) != 0)
                {
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_Style = hv_Style + "Italic";
                            hv_Style.Dispose();
                            hv_Style = ExpTmpLocalVar_Style;
                        }
                    }
                }
                else if ((int)(new HTuple(hv_Slant.TupleNotEqual("false"))) != 0)
                {
                    hv_Exception.Dispose();
                    hv_Exception = "Wrong value of control parameter Slant";
                    throw new HalconException(hv_Exception);
                }
                if ((int)(new HTuple(hv_Style.TupleEqual(""))) != 0)
                {
                    hv_Style.Dispose();
                    hv_Style = "Normal";
                }
                hv_AvailableFonts.Dispose();
                HOperatorSet.QueryFont(hv_WindowHandle, out hv_AvailableFonts);
                hv_Font_COPY_INP_TMP.Dispose();
                hv_Font_COPY_INP_TMP = "";
                for (hv_Fdx = 0; (int)hv_Fdx <= (int)((new HTuple(hv_Fonts.TupleLength())) - 1); hv_Fdx = (int)hv_Fdx + 1)
                {
                    hv_Indices.Dispose();
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        hv_Indices = hv_AvailableFonts.TupleFind(
                            hv_Fonts.TupleSelect(hv_Fdx));
                    }
                    if ((int)(new HTuple((new HTuple(hv_Indices.TupleLength())).TupleGreater(
                        0))) != 0)
                    {
                        if ((int)(new HTuple(((hv_Indices.TupleSelect(0))).TupleGreaterEqual(0))) != 0)
                        {
                            hv_Font_COPY_INP_TMP.Dispose();
                            using (HDevDisposeHelper dh = new HDevDisposeHelper())
                            {
                                hv_Font_COPY_INP_TMP = hv_Fonts.TupleSelect(
                                    hv_Fdx);
                            }
                            break;
                        }
                    }
                }
                if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(""))) != 0)
                {
                    throw new HalconException("Wrong value of control parameter Font");
                }
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    {
                        HTuple
                          ExpTmpLocalVar_Font = (((hv_Font_COPY_INP_TMP + "-") + hv_Style) + "-") + hv_Size_COPY_INP_TMP;
                        hv_Font_COPY_INP_TMP.Dispose();
                        hv_Font_COPY_INP_TMP = ExpTmpLocalVar_Font;
                    }
                }
                HOperatorSet.SetFont(hv_WindowHandle, hv_Font_COPY_INP_TMP);

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {

                hv_Font_COPY_INP_TMP.Dispose();
                hv_Size_COPY_INP_TMP.Dispose();
                hv_OS.Dispose();
                hv_Fonts.Dispose();
                hv_Style.Dispose();
                hv_Exception.Dispose();
                hv_AvailableFonts.Dispose();
                hv_Fdx.Dispose();
                hv_Indices.Dispose();

                throw HDevExpDefaultException;
            }
        }

        /// <summary>
        /// 窗口显示信息
        /// </summary>
        public void disp_message(HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem,
      HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
        {



            // Local iconic variables 

            // Local control variables 

            HTuple hv_GenParamName = new HTuple(), hv_GenParamValue = new HTuple();
            HTuple hv_Color_COPY_INP_TMP = new HTuple(hv_Color);
            HTuple hv_Column_COPY_INP_TMP = new HTuple(hv_Column);
            HTuple hv_CoordSystem_COPY_INP_TMP = new HTuple(hv_CoordSystem);
            HTuple hv_Row_COPY_INP_TMP = new HTuple(hv_Row);

            
            //Convert the parameters for disp_text.
            if ((int)((new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
                new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(new HTuple())))) != 0)
            {

                hv_Color_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP.Dispose();
                hv_GenParamName.Dispose();
                hv_GenParamValue.Dispose();

                return;
            }
            if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Row_COPY_INP_TMP.Dispose();
                hv_Row_COPY_INP_TMP = 12;
            }
            if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
            {
                hv_Column_COPY_INP_TMP.Dispose();
                hv_Column_COPY_INP_TMP = 12;
            }
            //
            //Convert the parameter Box to generic parameters.
            hv_GenParamName.Dispose();
            hv_GenParamName = new HTuple();
            hv_GenParamValue.Dispose();
            hv_GenParamValue = new HTuple();
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(0))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleEqual("false"))) != 0)
                {
                    //Display no box
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(0))).TupleNotEqual("true"))) != 0)
                {
                    //Set a color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "box_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(0));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            if ((int)(new HTuple((new HTuple(hv_Box.TupleLength())).TupleGreater(1))) != 0)
            {
                if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleEqual("false"))) != 0)
                {
                    //Display no shadow.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                "false");
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
                else if ((int)(new HTuple(((hv_Box.TupleSelect(1))).TupleNotEqual("true"))) != 0)
                {
                    //Set a shadow color other than the default.
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamName = hv_GenParamName.TupleConcat(
                                "shadow_color");
                            hv_GenParamName.Dispose();
                            hv_GenParamName = ExpTmpLocalVar_GenParamName;
                        }
                    }
                    using (HDevDisposeHelper dh = new HDevDisposeHelper())
                    {
                        {
                            HTuple
                              ExpTmpLocalVar_GenParamValue = hv_GenParamValue.TupleConcat(
                                hv_Box.TupleSelect(1));
                            hv_GenParamValue.Dispose();
                            hv_GenParamValue = ExpTmpLocalVar_GenParamValue;
                        }
                    }
                }
            }
            //Restore default CoordSystem behavior.
            if ((int)(new HTuple(hv_CoordSystem_COPY_INP_TMP.TupleNotEqual("window"))) != 0)
            {
                hv_CoordSystem_COPY_INP_TMP.Dispose();
                hv_CoordSystem_COPY_INP_TMP = "image";
            }
            //
            if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(""))) != 0)
            {
                //disp_text does not accept an empty string for Color.
                hv_Color_COPY_INP_TMP.Dispose();
                hv_Color_COPY_INP_TMP = new HTuple();
            }
            //
            HOperatorSet.DispText(hv_WindowHandle, hv_String, hv_CoordSystem_COPY_INP_TMP,
                hv_Row_COPY_INP_TMP, hv_Column_COPY_INP_TMP, hv_Color_COPY_INP_TMP, hv_GenParamName,
                hv_GenParamValue);

            hv_Color_COPY_INP_TMP.Dispose();
            hv_Column_COPY_INP_TMP.Dispose();
            hv_CoordSystem_COPY_INP_TMP.Dispose();
            hv_Row_COPY_INP_TMP.Dispose();
            hv_GenParamName.Dispose();
            hv_GenParamValue.Dispose();

            return;
        }

        /// <summary>
        /// 生产箭头
        /// </summary>
        public void gen_arrow_contour_xld(out HObject ho_Arrow, HTuple hv_Row1, HTuple hv_Column1,
     HTuple hv_Row2, HTuple hv_Column2, HTuple hv_HeadLength, HTuple hv_HeadWidth)
        {



            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];

            // Local iconic variables 

            HObject ho_TempArrow = null;

            // Local control variables 

            HTuple hv_Length = new HTuple(), hv_ZeroLengthIndices = new HTuple();
            HTuple hv_DR = new HTuple(), hv_DC = new HTuple(), hv_HalfHeadWidth = new HTuple();
            HTuple hv_RowP1 = new HTuple(), hv_ColP1 = new HTuple();
            HTuple hv_RowP2 = new HTuple(), hv_ColP2 = new HTuple();
            HTuple hv_Index = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Arrow);
            HOperatorSet.GenEmptyObj(out ho_TempArrow);
            try
            {
                //Init
                ho_Arrow.Dispose();
                HOperatorSet.GenEmptyObj(out ho_Arrow);
                //
                //Calculate the arrow length
                hv_Length.Dispose();
                HOperatorSet.DistancePp(hv_Row1, hv_Column1, hv_Row2, hv_Column2, out hv_Length);
                //
                //Mark arrows with identical start and end point
                //(set Length to -1 to avoid division-by-zero exception)
                hv_ZeroLengthIndices.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ZeroLengthIndices = hv_Length.TupleFind(
                        0);
                }
                if ((int)(new HTuple(hv_ZeroLengthIndices.TupleNotEqual(-1))) != 0)
                {
                    if (hv_Length == null)
                        hv_Length = new HTuple();
                    hv_Length[hv_ZeroLengthIndices] = -1;
                }
                //
                //Calculate auxiliary variables.
                hv_DR.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DR = (1.0 * (hv_Row2 - hv_Row1)) / hv_Length;
                }
                hv_DC.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_DC = (1.0 * (hv_Column2 - hv_Column1)) / hv_Length;
                }
                hv_HalfHeadWidth.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_HalfHeadWidth = hv_HeadWidth / 2.0;
                }
                //
                //Calculate end points of the arrow head.
                hv_RowP1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowP1 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) + (hv_HalfHeadWidth * hv_DC);
                }
                hv_ColP1.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ColP1 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) - (hv_HalfHeadWidth * hv_DR);
                }
                hv_RowP2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_RowP2 = (hv_Row1 + ((hv_Length - hv_HeadLength) * hv_DR)) - (hv_HalfHeadWidth * hv_DC);
                }
                hv_ColP2.Dispose();
                using (HDevDisposeHelper dh = new HDevDisposeHelper())
                {
                    hv_ColP2 = (hv_Column1 + ((hv_Length - hv_HeadLength) * hv_DC)) + (hv_HalfHeadWidth * hv_DR);
                }
                //
                //Finally create output XLD contour for each input point pair
                for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_Length.TupleLength())) - 1); hv_Index = (int)hv_Index + 1)
                {
                    if ((int)(new HTuple(((hv_Length.TupleSelect(hv_Index))).TupleEqual(-1))) != 0)
                    {
                        //Create_ single points for arrows with identical start and end point
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_TempArrow.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_TempArrow, hv_Row1.TupleSelect(
                                hv_Index), hv_Column1.TupleSelect(hv_Index));
                        }
                    }
                    else
                    {
                        //Create arrow contour
                        using (HDevDisposeHelper dh = new HDevDisposeHelper())
                        {
                            ho_TempArrow.Dispose();
                            HOperatorSet.GenContourPolygonXld(out ho_TempArrow, ((((((((((hv_Row1.TupleSelect(
                                hv_Index))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                                hv_RowP1.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)))).TupleConcat(
                                hv_RowP2.TupleSelect(hv_Index)))).TupleConcat(hv_Row2.TupleSelect(hv_Index)),
                                ((((((((((hv_Column1.TupleSelect(hv_Index))).TupleConcat(hv_Column2.TupleSelect(
                                hv_Index)))).TupleConcat(hv_ColP1.TupleSelect(hv_Index)))).TupleConcat(
                                hv_Column2.TupleSelect(hv_Index)))).TupleConcat(hv_ColP2.TupleSelect(
                                hv_Index)))).TupleConcat(hv_Column2.TupleSelect(hv_Index)));
                        }
                    }
                    {
                        HObject ExpTmpOutVar_0;
                        HOperatorSet.ConcatObj(ho_Arrow, ho_TempArrow, out ExpTmpOutVar_0);
                        ho_Arrow.Dispose();
                        ho_Arrow = ExpTmpOutVar_0;
                    }
                }
                ho_TempArrow.Dispose();

                hv_Length.Dispose();
                hv_ZeroLengthIndices.Dispose();
                hv_DR.Dispose();
                hv_DC.Dispose();
                hv_HalfHeadWidth.Dispose();
                hv_RowP1.Dispose();
                hv_ColP1.Dispose();
                hv_RowP2.Dispose();
                hv_ColP2.Dispose();
                hv_Index.Dispose();

                return;
            }
            catch (HalconException HDevExpDefaultException)
            {
                ho_TempArrow.Dispose();

                hv_Length.Dispose();
                hv_ZeroLengthIndices.Dispose();
                hv_DR.Dispose();
                hv_DC.Dispose();
                hv_HalfHeadWidth.Dispose();
                hv_RowP1.Dispose();
                hv_ColP1.Dispose();
                hv_RowP2.Dispose();
                hv_ColP2.Dispose();
                hv_Index.Dispose();

                throw HDevExpDefaultException;
            }
        }

        /// <summary>
        /// 计算成品的像素当量
        /// </summary>
        /// <param name="ho_Image"></param>
        /// <param name="hv_RowRoi"></param>
        /// <param name="hv_ColumnRoi"></param>
        /// <param name="hv_PhiRoi"></param>
        /// <param name="hv_Length1Roi"></param>
        /// <param name="hv_Length2Roi"></param>
        /// <param name="hv_Width">图像宽度</param>
        /// <param name="hv_Height">图像高度</param>
        /// <param name="hv_MMPPV">纵向像素当量</param>
        /// <param name="hv_MMPPH">横向像素当量</param>
        public void calculate_pixel_equivalent(HObject ho_Image, HTuple hv_RowRoi, HTuple hv_ColumnRoi,
    HTuple hv_PhiRoi, HTuple hv_Length1Roi, HTuple hv_Length2Roi, HTuple hv_Width,
    HTuple hv_Height, out HTuple hv_MMPPV, out HTuple hv_MMPPH)
        {
            // Local iconic variables 

            // Local control variables 
            HTuple hv_MeasureHandleW = new HTuple(), hv_RowEdgeFirst1 = new HTuple();
            HTuple hv_ColumnEdgeFirst1 = new HTuple(), hv_AmplitudeFirst1 = new HTuple();
            HTuple hv_RowEdgeSecond1 = new HTuple(), hv_ColumnEdgeSecond1 = new HTuple();
            HTuple hv_AmplitudeSecond1 = new HTuple(), hv_IntraDistance1 = new HTuple();
            HTuple hv_InterDistance1 = new HTuple(), hv_W = new HTuple();
            HTuple hv_MeasureHandleH = new HTuple(), hv_RowEdgeFirst = new HTuple();
            HTuple hv_ColumnEdgeFirst = new HTuple(), hv_AmplitudeFirst = new HTuple();
            HTuple hv_RowEdgeSecond = new HTuple(), hv_ColumnEdgeSecond = new HTuple();
            HTuple hv_AmplitudeSecond = new HTuple(), hv_IntraDistance = new HTuple();
            HTuple hv_InterDistance = new HTuple(), hv_H = new HTuple();
            // Initialize local and output iconic variables 
            hv_MMPPV = new HTuple();
            hv_MMPPH = new HTuple();
            //计算竖直方向像素当量
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_MeasureHandleW.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowRoi, hv_ColumnRoi, hv_PhiRoi - 1.57, hv_Length1Roi + 50,
                    hv_Length2Roi - 150, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandleW);
            }
            hv_RowEdgeFirst1.Dispose(); hv_ColumnEdgeFirst1.Dispose(); hv_AmplitudeFirst1.Dispose(); hv_RowEdgeSecond1.Dispose(); hv_ColumnEdgeSecond1.Dispose(); hv_AmplitudeSecond1.Dispose(); hv_IntraDistance1.Dispose(); hv_InterDistance1.Dispose();
            HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandleW, 2, 30, "all", "all", out hv_RowEdgeFirst1,
                out hv_ColumnEdgeFirst1, out hv_AmplitudeFirst1, out hv_RowEdgeSecond1, out hv_ColumnEdgeSecond1,
                out hv_AmplitudeSecond1, out hv_IntraDistance1, out hv_InterDistance1);
            hv_W.Dispose();
            hv_W = 4;
            hv_MMPPV.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_MMPPV = hv_W / hv_IntraDistance1;
            }

            //计算水平方向像素当量
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_MeasureHandleH.Dispose();
                HOperatorSet.GenMeasureRectangle2(hv_RowRoi, hv_ColumnRoi, hv_PhiRoi, hv_Length1Roi + 50,
                    hv_Length2Roi - 200, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandleH);
            }
            hv_RowEdgeFirst.Dispose(); hv_ColumnEdgeFirst.Dispose(); hv_AmplitudeFirst.Dispose(); hv_RowEdgeSecond.Dispose(); hv_ColumnEdgeSecond.Dispose(); hv_AmplitudeSecond.Dispose(); hv_IntraDistance.Dispose(); hv_InterDistance.Dispose();
            HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandleH, 2, 30, "all", "all", out hv_RowEdgeFirst,
                out hv_ColumnEdgeFirst, out hv_AmplitudeFirst, out hv_RowEdgeSecond, out hv_ColumnEdgeSecond,
                out hv_AmplitudeSecond, out hv_IntraDistance, out hv_InterDistance);
            hv_H.Dispose();
            hv_H = 5;
            hv_MMPPH.Dispose();
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_MMPPH = hv_H / hv_IntraDistance;
            }

            hv_MeasureHandleW.Dispose();
            hv_RowEdgeFirst1.Dispose();
            hv_ColumnEdgeFirst1.Dispose();
            hv_AmplitudeFirst1.Dispose();
            hv_RowEdgeSecond1.Dispose();
            hv_ColumnEdgeSecond1.Dispose();
            hv_AmplitudeSecond1.Dispose();
            hv_IntraDistance1.Dispose();
            hv_InterDistance1.Dispose();
            hv_W.Dispose();
            hv_MeasureHandleH.Dispose();
            hv_RowEdgeFirst.Dispose();
            hv_ColumnEdgeFirst.Dispose();
            hv_AmplitudeFirst.Dispose();
            hv_RowEdgeSecond.Dispose();
            hv_ColumnEdgeSecond.Dispose();
            hv_AmplitudeSecond.Dispose();
            hv_IntraDistance.Dispose();
            hv_InterDistance.Dispose();
            hv_H.Dispose();

            return;
        }

        /// <summary>
        /// 创建成品定位模板
        /// </summary>
        /// <param name="ho_Image"></param>
        /// <param name="hv_WindowHandler"></param>
        /// <param name="hv_path"></param>
        public void gen_finish_shape_model(HObject ho_Image, HTuple hv_WindowHandler, HTuple hv_path)
        {
            // Local iconic variables 

            HObject ho_RectangleRoi, ho_RectangleRoi2;
            HObject ho_RegionDifference, ho_ImageReduced, ho_ModelContours;
            HObject ho_ContoursAffineTrans;

            // Local control variables 

            HTuple hv_RowRoi = new HTuple(), hv_ColumnRoi = new HTuple();
            HTuple hv_PhiRoi = new HTuple(), hv_Length1Roi = new HTuple();
            HTuple hv_Length2Roi = new HTuple(), hv_FinishModelID = new HTuple();
            HTuple hv_RowFind = new HTuple(), hv_ColumnFind = new HTuple();
            HTuple hv_AngleFind = new HTuple(), hv_Score = new HTuple();
            HTuple hv_HomMat2D = new HTuple();
            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_RectangleRoi);
            HOperatorSet.GenEmptyObj(out ho_RectangleRoi2);
            HOperatorSet.GenEmptyObj(out ho_RegionDifference);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_ModelContours);
            HOperatorSet.GenEmptyObj(out ho_ContoursAffineTrans);
            disp_message(hv_WindowHandler, "框选芯苞范围", "window", 12, 12,
                "blue", "false");
            hv_RowRoi.Dispose(); hv_ColumnRoi.Dispose(); hv_PhiRoi.Dispose(); hv_Length1Roi.Dispose(); hv_Length2Roi.Dispose();
            HOperatorSet.DrawRectangle2(hv_WindowHandler, out hv_RowRoi, out hv_ColumnRoi,
                out hv_PhiRoi, out hv_Length1Roi, out hv_Length2Roi);
            ho_RectangleRoi.Dispose();
            HOperatorSet.GenRectangle2(out ho_RectangleRoi, hv_RowRoi, hv_ColumnRoi, hv_PhiRoi,
                hv_Length1Roi, hv_Length2Roi);
            //draw_rectangle2 (WindowHandle, RowRoi2, ColumnRoi2, PhiRoi2, Length1Roi2, Length2Roi2)
            disp_message(hv_WindowHandler, "屏蔽引脚范围", "window", 12, 12,
                "blue", "false");
            hv_RowRoi.Dispose(); hv_ColumnRoi.Dispose(); hv_PhiRoi.Dispose(); hv_Length1Roi.Dispose(); hv_Length2Roi.Dispose();
            HOperatorSet.DrawRectangle2(hv_WindowHandler, out hv_RowRoi, out hv_ColumnRoi,
                out hv_PhiRoi, out hv_Length1Roi, out hv_Length2Roi);
            ho_RectangleRoi2.Dispose();
            HOperatorSet.GenRectangle2(out ho_RectangleRoi2, hv_RowRoi, hv_ColumnRoi, hv_PhiRoi,
                hv_Length1Roi, hv_Length2Roi);
            ho_RegionDifference.Dispose();
            HOperatorSet.Difference(ho_RectangleRoi, ho_RectangleRoi2, out ho_RegionDifference
                );
            ho_ImageReduced.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_RegionDifference, out ho_ImageReduced);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_FinishModelID.Dispose();
                HOperatorSet.CreateShapeModel(ho_ImageReduced, "auto", -((new HTuple(15)).TupleRad()
                    ), (new HTuple(30)).TupleRad(), (new HTuple(0.5)).TupleRad(), "auto", "use_polarity",
                    "auto", "auto", out hv_FinishModelID);
            }
            HOperatorSet.WriteShapeModel(hv_FinishModelID, hv_path);
            ho_ModelContours.Dispose();
            HOperatorSet.GetShapeModelContours(out ho_ModelContours, hv_FinishModelID, 1);
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
                hv_RowFind.Dispose(); hv_ColumnFind.Dispose(); hv_AngleFind.Dispose(); hv_Score.Dispose();
                HOperatorSet.FindShapeModel(ho_ImageReduced, hv_FinishModelID, -((new HTuple(15)).TupleRad()
                    ), (new HTuple(30)).TupleRad(), 0.5, 1, 0.7, "least_squares", 0, 0.9, out hv_RowFind,
                    out hv_ColumnFind, out hv_AngleFind, out hv_Score);
            }
            hv_HomMat2D.Dispose();
            HOperatorSet.VectorAngleToRigid(0, 0, 0, hv_RowFind, hv_ColumnFind, hv_AngleFind,
                out hv_HomMat2D);
            ho_ContoursAffineTrans.Dispose();
            HOperatorSet.AffineTransContourXld(ho_ModelContours, out ho_ContoursAffineTrans,
                hv_HomMat2D);
            ho_RectangleRoi.Dispose();
            ho_RectangleRoi2.Dispose();
            ho_RegionDifference.Dispose();
            ho_ImageReduced.Dispose();
            ho_ModelContours.Dispose();
            ho_ContoursAffineTrans.Dispose();

            hv_RowRoi.Dispose();
            hv_ColumnRoi.Dispose();
            hv_PhiRoi.Dispose();
            hv_Length1Roi.Dispose();
            hv_Length2Roi.Dispose();
            hv_FinishModelID.Dispose();
            hv_RowFind.Dispose();
            hv_ColumnFind.Dispose();
            hv_AngleFind.Dispose();
            hv_Score.Dispose();
            hv_HomMat2D.Dispose();

            return;
        }

    }

}

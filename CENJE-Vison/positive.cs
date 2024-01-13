//
// File generated by HDevelop for HALCON/.NET (C#) Version 20.11.0.0
// Non-ASCII strings in this file are encoded in local-8-bit encoding (cp936).
// 
// Please note that non-ASCII characters in string constants are exported
// as octal codes in order to guarantee that the strings are correctly
// created on all systems, independent on any compiler settings.
// 
// Source files with different encoding should not be mixed in one project.
//

using HalconDotNet;

public partial class HDevelopExport
{
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
  public HDevelopExport()
  {
    // Default settings used in HDevelop
    HOperatorSet.SetSystem("width", 512);
    HOperatorSet.SetSystem("height", 512);
    if (HalconAPI.isWindows)
      HOperatorSet.SetSystem("use_window_thread","true");
    action();
  }
#endif

#if !NO_EXPORT_MAIN
  // Main procedure 
  private void action()
  {


    // Stack for temporary objects 
    HObject[] OTemp = new HObject[20];

    // Local iconic variables 

    HObject ho_Image, ho_FlowersRoi, ho_FlowersModelContours;
    HObject ho_RegionAffineTrans=null, ho_ImageFlowers=null;
    HObject ho_Region=null, ho_RegionClosing=null, ho_RegionDifference=null;
    HObject ho_RegionOpening=null, ho_ConnectedRegions=null;
    HObject ho_SelectedRegions=null, ho_SortedRegions=null;
    HObject ho_RegionLinesH=null, ho_RegionLinesV=null, ho_Rectangle=null;
    HObject ho_ImageFlower1=null, ho_RegionFlower1=null, ho_RegionDifference1=null;
    HObject ho_SelectedRegions1=null, ho_ConnectedRegions1=null;
    HObject ho_SortedRegionsFlower=null, ho_ObjectSelected=null;
    HObject ho_ContourLine=null;

    // Local control variables 

    HTuple hv_ImageFiles = new HTuple(), hv_Width = new HTuple();
    HTuple hv_Height = new HTuple(), hv_WindowHandle = new HTuple();
    HTuple hv_FlowersRow = new HTuple(), hv_FlowersColumn = new HTuple();
    HTuple hv_FlowersPhi = new HTuple(), hv_FlowersLength1 = new HTuple();
    HTuple hv_FlowersLength2 = new HTuple(), hv_Row = new HTuple();
    HTuple hv_Column = new HTuple(), hv_mmpp = new HTuple();
    HTuple hv_areaMax = new HTuple(), hv_areaMin = new HTuple();
    HTuple hv_L2Max = new HTuple(), hv_L2Min = new HTuple();
    HTuple hv_AngelMax = new HTuple(), hv_AngelMin = new HTuple();
    HTuple hv_RowL2 = new HTuple(), hv_ColumnL2 = new HTuple();
    HTuple hv_PhiL2 = new HTuple(), hv_Length1L2 = new HTuple();
    HTuple hv_Length2L2 = new HTuple(), hv_RowLine = new HTuple();
    HTuple hv_ColumnLine = new HTuple(), hv_PhiLine = new HTuple();
    HTuple hv_Length1Line = new HTuple(), hv_Length2Line = new HTuple();
    HTuple hv_RowPin = new HTuple(), hv_ColumnPin = new HTuple();
    HTuple hv_PhiPin = new HTuple(), hv_Length1Pin = new HTuple();
    HTuple hv_Length2Pin = new HTuple(), hv_FlowersModelID = new HTuple();
    HTuple hv_RowFind = new HTuple(), hv_ColumnFind = new HTuple();
    HTuple hv_AngleFind = new HTuple(), hv_Score = new HTuple();
    HTuple hv_HomMat2D = new HTuple(), hv_Index = new HTuple();
    HTuple hv_PhiTransL = new HTuple(), hv_RowTransL = new HTuple();
    HTuple hv_ColTransL = new HTuple(), hv_PhiTransL2 = new HTuple();
    HTuple hv_RowTransL2 = new HTuple(), hv_ColTransL2 = new HTuple();
    HTuple hv_PhiTransPin = new HTuple(), hv_RowTransP = new HTuple();
    HTuple hv_ColTransP = new HTuple(), hv_Number = new HTuple();
    HTuple hv_Area = new HTuple(), hv_FCC = new HTuple(), hv_FCR = new HTuple();
    HTuple hv_FlowerArea1 = new HTuple(), hv_FlowerArea2 = new HTuple();
    HTuple hv_FlowerArea3 = new HTuple(), hv_i = new HTuple();
    HTuple hv_j = new HTuple(), hv_AreaF = new HTuple(), hv_Row1 = new HTuple();
    HTuple hv_Column1 = new HTuple(), hv_MeasureHandle = new HTuple();
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
    // Initialize local and output iconic variables 
    HOperatorSet.GenEmptyObj(out ho_Image);
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
    HOperatorSet.GenEmptyObj(out ho_ContourLine);
    //Image Acquisition 01: Code generated by Image Acquisition 01
    hv_ImageFiles.Dispose();
    HOperatorSet.ListFiles("C:/Users/WXB/Desktop/20230923", (new HTuple("files")).TupleConcat(
        "follow_links"), out hv_ImageFiles);
    hv_ImageFiles.Dispose();
    HOperatorSet.ListFiles("C:/Users/WXB/Desktop/picture/negativeNG", (new HTuple("files")).TupleConcat(
        "follow_links"), out hv_ImageFiles);
    {
    HTuple ExpTmpOutVar_0;
    HOperatorSet.TupleRegexpSelect(hv_ImageFiles, (new HTuple("\\.(tif|tiff|gif|bmp|jpg|jpeg|jp2|png|pcx|pgm|ppm|pbm|xwd|ima|hobj)$")).TupleConcat(
        "ignore_case"), out ExpTmpOutVar_0);
    hv_ImageFiles.Dispose();
    hv_ImageFiles = ExpTmpOutVar_0;
    }
    using (HDevDisposeHelper dh = new HDevDisposeHelper())
    {
    ho_Image.Dispose();
    HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(0));
    }
    hv_Width.Dispose();hv_Height.Dispose();
    HOperatorSet.GetImageSize(ho_Image, out hv_Width, out hv_Height);
    HOperatorSet.SetWindowAttr("background_color","black");
    HOperatorSet.OpenWindow(0,0,hv_Width,hv_Height,0,"visible","",out hv_WindowHandle);
    HDevWindowStack.Push(hv_WindowHandle);
    if (HDevWindowStack.IsOpen())
    {
      HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
    }
    if (HDevWindowStack.IsOpen())
    {
      HOperatorSet.SetDraw(HDevWindowStack.GetActive(), "margin");
    }
    //����roi����
    //����roi
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
    //�������ص���
    //draw_rectangle2 (WindowHandle, Row, Column, Phi, Length1, Length2)
    //gen_measure_rectangle2 (Row, Column, Phi, Length1, Length2, Width, Height, 'nearest_neighbor', MeasureHandlemmpp)
    //measure_pairs (Image, MeasureHandlemmpp, 2, 30, 'all', 'all', RowEdgeFirst, ColumnEdgeFirst, AmplitudeFirst, RowEdgeSecond, ColumnEdgeSecond, AmplitudeSecond, IntraDistance, InterDistance)
    //mm := 2.7
    //mmpp := mm/IntraDistance
    hv_mmpp.Dispose();
    hv_mmpp = 0.013;
    //���Χ
    hv_areaMax.Dispose();
    hv_areaMax = 500;
    hv_areaMin.Dispose();
    hv_areaMin = 350;
    hv_L2Max.Dispose();
    hv_L2Max = 0.55;
    hv_L2Min.Dispose();
    hv_L2Min = 0.1;
    hv_AngelMax.Dispose();
    hv_AngelMax = 95;
    hv_AngelMin.Dispose();
    hv_AngelMin = 85;
    //*************************
    //*************************
    //L2λ��
    //L2λ��ROI
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
    //�±�Ե��ROI���β���
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
    //����ֱ��ROI
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
    //������������ģ��
    //reduce_domain (Image, FlowersRoi, ImageReduced)
    //create_shape_model (ImageReduced, 'auto', -rad(15), rad(30), rad(0.5), 'auto', 'use_polarity', 'auto', 'auto', FlowersModelID)
    //write_shape_model (FlowersModelID, 'D:/CENJE-Vison/Model/FlowersModel.shm')
    hv_FlowersModelID.Dispose();
    HOperatorSet.ReadShapeModel("D:/CENJE-Vison/Model/FlowersModel.shm", out hv_FlowersModelID);
    ho_FlowersModelContours.Dispose();
    HOperatorSet.GetShapeModelContours(out ho_FlowersModelContours, hv_FlowersModelID, 
        1);

    //
    //find_shape_model (Image, FlowersModelID, -rad(15), rad(30), 0.5, 1, 0.5, 'least_squares', 0, 0.9, RowFind, ColumnFind, AngleFind, Score)
    //vector_angle_to_rigid (FlowersRow, FlowersColumn, FlowersPhi, RowFind, ColumnFind, AngleFind, HomMat2D)
    //affine_trans_region (FlowersRoi, RegionAffineTrans, HomMat2D, 'nearest_neighbor')





    for (hv_Index=0; (int)hv_Index<=(int)((new HTuple(hv_ImageFiles.TupleLength()
        ))-1); hv_Index = (int)hv_Index + 1)
    {
      if (HDevWindowStack.IsOpen())
      {
        HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
      }
      if (HDevWindowStack.IsOpen())
      {
        HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
      }
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      ho_Image.Dispose();
      HOperatorSet.ReadImage(out ho_Image, hv_ImageFiles.TupleSelect(hv_Index));
      }
      //���һ���λ��
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_RowFind.Dispose();hv_ColumnFind.Dispose();hv_AngleFind.Dispose();hv_Score.Dispose();
      HOperatorSet.FindShapeModel(ho_Image, hv_FlowersModelID, -((new HTuple(15)).TupleRad()
          ), (new HTuple(30)).TupleRad(), 0.5, 1, 0.5, "least_squares", 0, 0.9, out hv_RowFind, 
          out hv_ColumnFind, out hv_AngleFind, out hv_Score);
      }
      hv_HomMat2D.Dispose();
      HOperatorSet.VectorAngleToRigid(hv_FlowersRow, hv_FlowersColumn, hv_FlowersPhi, 
          hv_RowFind, hv_ColumnFind, hv_AngleFind, out hv_HomMat2D);
      ho_RegionAffineTrans.Dispose();
      HOperatorSet.AffineTransRegion(ho_FlowersRoi, out ho_RegionAffineTrans, hv_HomMat2D, 
          "nearest_neighbor");

      //roi����任
      hv_PhiTransL.Dispose();
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_PhiTransL = hv_PhiLine+(hv_AngleFind-hv_FlowersPhi);
      }
      hv_RowTransL.Dispose();hv_ColTransL.Dispose();
      HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowLine, hv_ColumnLine, out hv_RowTransL, 
          out hv_ColTransL);
      hv_PhiTransL2.Dispose();
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_PhiTransL2 = hv_PhiL2+(hv_AngleFind-hv_FlowersPhi);
      }
      hv_RowTransL2.Dispose();hv_ColTransL2.Dispose();
      HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowL2, hv_ColumnL2, out hv_RowTransL2, 
          out hv_ColTransL2);
      hv_PhiTransPin.Dispose();
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_PhiTransPin = hv_PhiPin+(hv_AngleFind-hv_FlowersPhi);
      }
      hv_RowTransP.Dispose();hv_ColTransP.Dispose();
      HOperatorSet.AffineTransPixel(hv_HomMat2D, hv_RowPin, hv_ColumnPin, out hv_RowTransP, 
          out hv_ColTransP);

      //������
      ho_ImageFlowers.Dispose();
      HOperatorSet.ReduceDomain(ho_Image, ho_RegionAffineTrans, out ho_ImageFlowers
          );
      ho_Region.Dispose();
      HOperatorSet.Threshold(ho_ImageFlowers, out ho_Region, 145, 255);
      ho_RegionClosing.Dispose();
      HOperatorSet.ClosingCircle(ho_Region, out ho_RegionClosing, 4.5);
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
      hv_Area.Dispose();hv_Row.Dispose();hv_Column.Dispose();
      HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area, out hv_Row, out hv_Column);
      //����ʮ�����и��
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      ho_RegionLinesH.Dispose();
      HOperatorSet.GenRegionLine(out ho_RegionLinesH, hv_Row, hv_Column-35, hv_Row, 
          hv_Column+35);
      }
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      ho_RegionLinesV.Dispose();
      HOperatorSet.GenRegionLine(out ho_RegionLinesV, hv_Row-35, hv_Column, hv_Row+35, 
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
      //����1�Ķ仨�����
      hv_FlowerArea1.Dispose();
      hv_FlowerArea1 = new HTuple();
      hv_FlowerArea1[0] = 0;
      hv_FlowerArea1[1] = 0;
      hv_FlowerArea1[2] = 0;
      hv_FlowerArea1[3] = 0;
      //����2�Ķ仨�����
      hv_FlowerArea2.Dispose();
      hv_FlowerArea2 = new HTuple();
      hv_FlowerArea2[0] = 0;
      hv_FlowerArea2[1] = 0;
      hv_FlowerArea2[2] = 0;
      hv_FlowerArea2[3] = 0;
      //����3�Ķ仨�����
      hv_FlowerArea3.Dispose();
      hv_FlowerArea3 = new HTuple();
      hv_FlowerArea3[0] = 0;
      hv_FlowerArea3[1] = 0;
      hv_FlowerArea3[2] = 0;
      hv_FlowerArea3[3] = 0;
      HTuple end_val115 = hv_Number-1;
      HTuple step_val115 = 1;
      for (hv_i=0; hv_i.Continue(end_val115, step_val115); hv_i = hv_i.TupleAdd(step_val115))
      {
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        ho_Rectangle.Dispose();
        HOperatorSet.GenRectangle2(out ho_Rectangle, hv_Row.TupleSelect(hv_i), hv_Column.TupleSelect(
            hv_i), hv_FlowersPhi, hv_FlowersLength1-15, hv_FlowersLength2/3);
        }
        ho_ImageFlower1.Dispose();
        HOperatorSet.ReduceDomain(ho_ImageFlowers, ho_Rectangle, out ho_ImageFlower1
            );
        ho_RegionFlower1.Dispose();
        HOperatorSet.Threshold(ho_ImageFlower1, out ho_RegionFlower1, 148, 255);
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
        ho_SortedRegionsFlower.Dispose();
        HOperatorSet.SortRegion(ho_ConnectedRegions1, out ho_SortedRegionsFlower, 
            "character", "true", "row");
        for (hv_j=0; (int)hv_j<=3; hv_j = (int)hv_j + 1)
        {
          switch (hv_i.I)
          {
          case 0:
            using (HDevDisposeHelper dh = new HDevDisposeHelper())
            {
            ho_ObjectSelected.Dispose();
            HOperatorSet.SelectObj(ho_SortedRegionsFlower, out ho_ObjectSelected, 
                hv_j+1);
            }
            hv_AreaF.Dispose();hv_Row1.Dispose();hv_Column1.Dispose();
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
                hv_j+1);
            }
            hv_AreaF.Dispose();hv_Row1.Dispose();hv_Column1.Dispose();
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
                hv_j+1);
            }
            hv_AreaF.Dispose();hv_Row1.Dispose();hv_Column1.Dispose();
            HOperatorSet.AreaCenter(ho_ObjectSelected, out hv_AreaF, out hv_Row1, 
                out hv_Column1);
            if (hv_FlowerArea3 == null)
              hv_FlowerArea3 = new HTuple();
            hv_FlowerArea3[hv_j] = hv_AreaF;
            break;
          }
        }
      }
      //stop ()

      //�Ҳ���Ե
      hv_MeasureHandle.Dispose();
      HOperatorSet.GenMeasureRectangle2(hv_RowTransL, hv_ColTransL, hv_PhiTransL, 
          hv_Length1Line, hv_Length2Line, hv_Width, hv_Height, "nearest_neighbor", 
          out hv_MeasureHandle);
      hv_RowEdgeLine.Dispose();hv_ColumnEdgeLine.Dispose();hv_AmplitudeLine.Dispose();hv_DistanceLine.Dispose();
      HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandle, 2, 20, "positive", "first", 
          out hv_RowEdgeLine, out hv_ColumnEdgeLine, out hv_AmplitudeLine, out hv_DistanceLine);
      if (HDevWindowStack.IsOpen())
      {
        HOperatorSet.SetColor(HDevWindowStack.GetActive(), "green");
      }
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      HOperatorSet.DispLine(hv_WindowHandle, hv_RowEdgeLine-(hv_Length1Line*(hv_PhiLine.TupleSin()
          )), hv_ColumnEdgeLine-(hv_Length1Line*(hv_PhiLine.TupleCos())), hv_RowEdgeLine+(hv_Length1Line*(hv_PhiLine.TupleSin()
          )), hv_ColumnEdgeLine+(hv_Length1Line*(hv_PhiLine.TupleCos())));
      }
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      ho_ContourLine.Dispose();
      HOperatorSet.GenContourPolygonXld(out ho_ContourLine, ((hv_RowEdgeLine-(hv_Length1Line*(hv_PhiLine.TupleSin()
          )))).TupleConcat(hv_RowEdgeLine+(hv_Length1Line*(hv_PhiLine.TupleSin()))), 
          ((hv_ColumnEdgeLine-(hv_Length1Line*(hv_PhiLine.TupleCos())))).TupleConcat(
          hv_ColumnEdgeLine+(hv_Length1Line*(hv_PhiLine.TupleCos()))));
      }
      //�����������Ե
      hv_MeasureHandleL.Dispose();
      HOperatorSet.GenMeasureRectangle2(hv_RowTransL2, hv_ColTransL2, hv_PhiTransL2, 
          hv_Length1L2, hv_Length2L2, hv_Width, hv_Height, "nearest_neighbor", out hv_MeasureHandleL);

      hv_RowEdgeFirstL.Dispose();hv_ColumnEdgeFirstL.Dispose();hv_AmplitudeFirstL.Dispose();hv_RowEdgeSecondL.Dispose();hv_ColumnEdgeSecondL.Dispose();hv_AmplitudeSecondL.Dispose();hv_IntraDistance1.Dispose();hv_InterDistance1.Dispose();
      HOperatorSet.MeasurePairs(ho_Image, hv_MeasureHandleL, 3, 60, "positive", "all", 
          out hv_RowEdgeFirstL, out hv_ColumnEdgeFirstL, out hv_AmplitudeFirstL, 
          out hv_RowEdgeSecondL, out hv_ColumnEdgeSecondL, out hv_AmplitudeSecondL, 
          out hv_IntraDistance1, out hv_InterDistance1);

      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      HOperatorSet.DispLine(hv_WindowHandle, hv_RowEdgeFirstL-(hv_Length2L2*(hv_PhiTransL2.TupleCos()
          )), hv_ColumnEdgeFirstL-(hv_Length2L2*(hv_PhiTransL2.TupleSin())), hv_RowEdgeFirstL+(hv_Length2L2*(hv_PhiTransL2.TupleCos()
          )), hv_ColumnEdgeFirstL+(hv_Length2L2*(hv_PhiTransL2.TupleSin())));
      }

      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      HOperatorSet.DispLine(hv_WindowHandle, hv_RowEdgeSecondL-(hv_Length2L2*(hv_PhiTransL2.TupleCos()
          )), hv_ColumnEdgeSecondL-(hv_Length2L2*(hv_PhiTransL2.TupleSin())), hv_RowEdgeSecondL+(hv_Length2L2*(hv_PhiTransL2.TupleCos()
          )), hv_ColumnEdgeSecondL+(hv_Length2L2*(hv_PhiTransL2.TupleSin())));
      }
      //���±�Ե
      //gen_rectangle2 (rect, RowL, ColumnL, PhiL-1.57, Length1L-90, Length2L)
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_MeasureHandleL.Dispose();
      HOperatorSet.GenMeasureRectangle2(hv_RowTransL2, hv_ColTransL2, hv_PhiTransL2-1.57, 
          hv_Length1L2-90, hv_Length2L2, hv_Width, hv_Height, "nearest_neighbor", 
          out hv_MeasureHandleL);
      }
      hv_RowEdgeD.Dispose();hv_ColumnEdgeD.Dispose();hv_AmplitudeD.Dispose();hv_DistanceD.Dispose();
      HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandleL, 2, 30, "negative", "first", 
          out hv_RowEdgeD, out hv_ColumnEdgeD, out hv_AmplitudeD, out hv_DistanceD);
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      HOperatorSet.DispLine(hv_WindowHandle, hv_RowEdgeD-(hv_Length2L2*(hv_PhiTransL2.TupleSin()
          )), hv_ColumnEdgeD-(hv_Length2L2*(hv_PhiTransL2.TupleCos())), hv_RowEdgeD+(hv_Length2L2*(hv_PhiTransL2.TupleSin()
          )), hv_ColumnEdgeD+(hv_Length2L2*(hv_PhiTransL2.TupleCos())));
      }
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_Distance.Dispose();
      HOperatorSet.DistancePl(hv_RowEdgeD, hv_ColumnEdgeD, hv_RowEdgeLine-(hv_Length1Line*(hv_PhiTransL.TupleSin()
          )), hv_ColumnEdgeLine-(hv_Length1Line*(hv_PhiTransL.TupleCos())), hv_RowEdgeLine+(hv_Length1Line*(hv_PhiTransL.TupleSin()
          )), hv_ColumnEdgeLine+(hv_Length1Line*(hv_PhiTransL.TupleCos())), out hv_Distance);
      }
      hv_mDistance.Dispose();
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      hv_mDistance = hv_Distance*hv_mmpp;
      }
      using (HDevDisposeHelper dh = new HDevDisposeHelper())
      {
      HOperatorSet.WriteString(hv_WindowHandle, "Distance: "+hv_mDistance);
      }

      //����ֱ��
      hv_MeasureHandlePin.Dispose();
      HOperatorSet.GenMeasureRectangle2(hv_RowTransP, hv_ColTransP, hv_PhiTransPin, 
          hv_Length1Pin, hv_Length2Pin, hv_Width, hv_Height, "nearest_neighbor", 
          out hv_MeasureHandlePin);
      hv_RowEdgePin.Dispose();hv_ColumnEdgePin.Dispose();hv_AmplitudePin.Dispose();hv_Distance1.Dispose();
      HOperatorSet.MeasurePos(ho_Image, hv_MeasureHandlePin, 1, 30, "positive", "first", 
          out hv_RowEdgePin, out hv_ColumnEdgePin, out hv_AmplitudePin, out hv_Distance1);
      if ((int)((new HTuple((new HTuple(hv_RowEdgePin.TupleLength())).TupleEqual(
          1))).TupleAnd(new HTuple((new HTuple(hv_ColumnEdgePin.TupleLength())).TupleEqual(
          1)))) != 0)
      {
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        HOperatorSet.DispLine(hv_WindowHandle, hv_RowEdgePin-(hv_Length2Pin*(hv_PhiPin.TupleCos()
            )), hv_ColumnEdgePin-(hv_Length2Pin*(hv_PhiPin.TupleSin())), hv_RowEdgePin+(hv_Length2Pin*(hv_PhiPin.TupleCos()
            )), hv_ColumnEdgePin+(hv_Length2Pin*(hv_PhiPin.TupleSin())));
        }
      }
      else
      {
        if (HDevWindowStack.IsOpen())
        {
          HOperatorSet.SetColor(HDevWindowStack.GetActive(), "red");
        }
        HOperatorSet.WriteString(hv_WindowHandle, "Can Not Find Pin!");
      }

      //����íƫ�Ƕ�
      if ((int)((new HTuple((new HTuple(hv_RowEdgePin.TupleLength())).TupleEqual(
          1))).TupleAnd(new HTuple((new HTuple(hv_ColumnEdgePin.TupleLength())).TupleEqual(
          1)))) != 0)
      {
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        hv_RadPin.Dispose();
        HOperatorSet.AngleLl(hv_RowEdgeD-(hv_Length2L2*(hv_PhiTransL2.TupleSin())), 
            hv_ColumnEdgeD-(hv_Length2L2*(hv_PhiTransL2.TupleCos())), hv_RowEdgeD+(hv_Length2L2*(hv_PhiTransL2.TupleSin()
            )), hv_ColumnEdgeD+(hv_Length2L2*(hv_PhiTransL2.TupleCos())), hv_RowEdgePin-(hv_Length2Pin*(hv_PhiTransPin.TupleCos()
            )), hv_ColumnEdgePin-(hv_Length2Pin*(hv_PhiTransPin.TupleSin())), hv_RowEdgePin+(hv_Length2Pin*(hv_PhiTransPin.TupleCos()
            )), hv_ColumnEdgePin+(hv_Length2Pin*(hv_PhiTransPin.TupleSin())), out hv_RadPin);
        }
        hv_AnglePin.Dispose();
        HOperatorSet.TupleDeg(hv_RadPin, out hv_AnglePin);
        using (HDevDisposeHelper dh = new HDevDisposeHelper())
        {
        HOperatorSet.WriteString(hv_WindowHandle, "Angle: "+hv_AnglePin);
        }
      }

      //�ж��Ƿ�Ϊ��Ʒ
      // stop(...); only in hdevelop
    }
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
    ho_ContourLine.Dispose();

    hv_ImageFiles.Dispose();
    hv_Width.Dispose();
    hv_Height.Dispose();
    hv_WindowHandle.Dispose();
    hv_FlowersRow.Dispose();
    hv_FlowersColumn.Dispose();
    hv_FlowersPhi.Dispose();
    hv_FlowersLength1.Dispose();
    hv_FlowersLength2.Dispose();
    hv_Row.Dispose();
    hv_Column.Dispose();
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
    hv_i.Dispose();
    hv_j.Dispose();
    hv_AreaF.Dispose();
    hv_Row1.Dispose();
    hv_Column1.Dispose();
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

  }

#endif


}
#if !(NO_EXPORT_MAIN || NO_EXPORT_APP_MAIN)
public class HDevelopExportApp
{
  static void Main(string[] args)
  {
    new HDevelopExport();
  }
}
#endif

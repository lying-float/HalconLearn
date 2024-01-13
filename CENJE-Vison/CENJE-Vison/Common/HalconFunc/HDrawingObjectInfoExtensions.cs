using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENJE_Vison.Common.HalconFunc
{
    public static class HDrawObjectInfoExtensions
    {
        public static HObject GenHobject(this HDrawingObjectInfo drawInfo)
        {
            HObject hObject = null;
            switch (drawInfo.Type)
            {
                case "rectangle2":
                    HOperatorSet.GenRectangle2(out hObject, drawInfo.HTuples[0], drawInfo.HTuples[1], drawInfo.HTuples[2], drawInfo.HTuples[3], drawInfo.HTuples[4]);
                    break;
                case "rectangle1":
                    HOperatorSet.GenRectangle1(out hObject, drawInfo.HTuples[0], drawInfo.HTuples[1], drawInfo.HTuples[2], drawInfo.HTuples[3]);
                    break;
                case "circle":
                    HOperatorSet.GenCircle(out hObject, drawInfo.HTuples[0], drawInfo.HTuples[1], drawInfo.HTuples[2]);
                    break;
            }
            return hObject;
        }


        public static HTuple[] GetHTuples(this HDrawingObject drawingObject)
        {
            HTuple[] hTuples = null;
            string type = drawingObject.GetDrawingObjectParams("type");
            switch (type)
            {
                case "rectangle1":
                    hTuples = new HTuple[4];
                    hTuples[0] = drawingObject.GetDrawingObjectParams("row1");
                    hTuples[1] = drawingObject.GetDrawingObjectParams("column1");
                    hTuples[2] = drawingObject.GetDrawingObjectParams("row2");
                    hTuples[3] = drawingObject.GetDrawingObjectParams("column2");
                    break;
                case "rectangle2":
                    hTuples = new HTuple[4];
                    hTuples[0] = drawingObject.GetDrawingObjectParams("row");
                    hTuples[1] = drawingObject.GetDrawingObjectParams("column");
                    hTuples[1] = drawingObject.GetDrawingObjectParams("phi");
                    hTuples[2] = drawingObject.GetDrawingObjectParams("length1");
                    hTuples[3] = drawingObject.GetDrawingObjectParams("length2");
                    break;
                case "circle":
                    hTuples = new HTuple[4];
                    hTuples[0] = drawingObject.GetDrawingObjectParams("row");
                    hTuples[1] = drawingObject.GetDrawingObjectParams("column");
                    hTuples[2] = drawingObject.GetDrawingObjectParams("radius");
                    break;

            }
            return hTuples;
        }

    }
}

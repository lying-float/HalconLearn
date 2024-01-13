using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace CENJE_Vison.Common.HalconFunc
{
    public class HDrawingObjectInfo
    {
        public HDrawingObject HDrawingObject { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Color { get; set; }

        /// <summary>
        /// 结构数组，存储HDrawingObject的信息
        /// 例如HDrawingObject为矩形，则存储row1,column1,row2,column2
        /// </summary>
        public HTuple[] HTuples { get; set; }
    }
}

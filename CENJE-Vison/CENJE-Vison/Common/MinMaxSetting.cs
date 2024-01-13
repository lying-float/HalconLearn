using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENJE_Vison.Common
{
    /// <summary>
    /// 后台配方上下限结构体
    /// </summary>
    public static class MinMaxSetting
    {
        public struct PositiveTolerance
        {
            public double FlowerCountP;
            public double AreaMaxP;
            public double AreaMinP;
            public double FoilCrackMaxP;
            public double FoilCrackMinP;
            public double AngleMaxP;
            public double AngleMinP;
            public double L2MaxP;
            public double L2MinP;
            public double RivetOffsetMaxP;
            public double RivetOffsetMinP;

        }

        public struct NegativeTolerance
        {
            public double FlowerCountN;
            public double AreaMaxN;
            public double AreaMinN;
            public double FoilCrackMaxN;
            public double FoilCrackMinN;
            public double AngleMaxN;
            public double AngleMinN;
            public double L2MaxN;
            public double L2MinN;
            public double RivetOffsetMaxN;
            public double RivetOffsetMinN;
            public int ColorN;
        }

        public struct FinishTolerance
        {
            public double HeightMax;
            public double HeightMin;
            public double ODMax;
            public double ODMin;
            public double PinPitchMax;
            public double PinPitchMin;
            public double DifferenceOfHeightMax;
            public double DifferenceOfHeightMin;
            public double CPlineUpMax;
            public double CPlineUpMin;
            public double CPlineBottomMax;
            public double CPlineBottomMin;
        }
    }
}

﻿using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENJE_Vison.Common.HalconFunc
{
    public static class HTuplesExtension
    {
        public static HObject GenRectangle2(this HTuple[] hTuples)
        {
            if (hTuples[0].D != 0 && hTuples[1].D != 0 && hTuples[2].D != 0 && hTuples[3].D != 0 && hTuples[4].D != 0)
            {
                HObject drawObj;
                HOperatorSet.GenRectangle2(out drawObj, hTuples[0], hTuples[1], hTuples[2], hTuples[3], hTuples[4]);
                return drawObj;
            }
            return null;
        }

        public static HObject GenRectangle1(this HTuple[] hTuples)
        {
            if (hTuples[0].D != 0 && hTuples[1].D != 0 && hTuples[2].D != 0 && hTuples[3].D != 0)
            {
                HObject drawObj;
                HOperatorSet.GenRectangle1(out drawObj, hTuples[0], hTuples[1], hTuples[2], hTuples[3]);
                return drawObj;
            }
            return null;
        }

        public static HObject GenEllipse(this HTuple[] hTuples)
        {
            if (hTuples[0].D != 0 && hTuples[1].D != 0 && hTuples[2].D != 0 && hTuples[3].D != 0 && hTuples[4].D != 0)
            {
                HObject drawObj;
                HOperatorSet.GenEllipse(out drawObj, hTuples[0], hTuples[1], hTuples[2], hTuples[3], hTuples[4]);
                return drawObj;
            }
            return null;
        }

        public static HObject GenCircle(this HTuple[] hTuples)
        {
            if (hTuples[0].D != 0 && hTuples[1].D != 0 && hTuples[2].D != 0)
            {
                HObject drawObj;
                HOperatorSet.GenCircle(out drawObj, hTuples[0], hTuples[1], hTuples[2]);
                return drawObj;
            }
            return null;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using HalconDotNet;
using MvCamCtrl.NET;

namespace CENJE_Vison.Common
{
    public class HikVisionSDK
    {
        private MyCamera myCamera = new MyCamera();//相机对象
        //MyCamera.MV_CC_DEVICE_INFO_LIST m_stDeviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();//相机列表
        private MyCamera.MV_CC_DEVICE_INFO_LIST deviceList;//设备列表
        private MyCamera.MV_CC_DEVICE_INFO deviceInfo;//设备对象
        private string seriesStr;//接收相机序列号
        private MyCamera.MVCC_INTVALUE stParam;//用于接收特定的参数
        //为读取、保存图像创建的数组
        UInt32 m_nBufSizeForDriver = 4096 * 3000;
        byte[] m_pBufForDriver = new byte[4096 * 3000];
        UInt32 m_nBufSizeForSaveImage = 4096 * 3000 * 3 + 3000;
        byte[] m_pBufForSaveImage = new byte[4096 * 3000 * 3 + 3000];
        //
        //private bool m_bGrabbing = false;//是否录像
        Thread m_hReceiveThread = null;
        MyCamera.MV_FRAME_OUT_INFO_EX m_stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();

        // ch:用于从驱动获取图像的缓存 | en:Buffer for getting image from driver
        //UInt32 m_nBufSizeForDriver = 0;
        IntPtr m_BufForDriver = IntPtr.Zero;
        private static Object BufForDriverLock = new Object();

        public HikVisionSDK()
        {
            deviceList = new MyCamera.MV_CC_DEVICE_INFO_LIST();
        }

        /// <summary>
        /// 创建相机连接函数
        /// </summary>
        /// <param name="CameraList"></param>
        /// <returns></returns>
        public int ConnectCamera(string id)//连接相机，返回-1为失败，0为成功
        {
            this.seriesStr = id;
            string m_SerialNumber = "";//接收设备返回的序列号
            int temp;//接收命令执行结果
            myCamera = new MyCamera();
            try
            {
                temp = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceList);//更新设备列表
                if (temp != 0)
                {
                    //设备更新成功接收命令的返回值为0，返回值不为0则为异常
                    return -1;
                }

                //强制相机IP
                for (int i = 0; i < deviceList.nDeviceNum; i++)
                {

                    /*******该部分用于获取相机名称、序列号等，从而对指定的相机进行IP更改******/

                    deviceInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));//获取设备信息
                    IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(deviceInfo.SpecialInfo.stGigEInfo, 0);
                    MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));

                    /*****************************************************************/

                    m_SerialNumber = gigeInfo.chUserDefinedName;
                    if (deviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        //判断是否为网口相机
                        if (seriesStr.Equals(m_SerialNumber))
                        {
                            //如果相机用户名正确则修改IP
                            temp = myCamera.MV_CC_CreateDevice_NET(ref deviceInfo);//更改IP前需要创建设备对象
                            ChangeIP();
                        }
                    }
                }
                //更改IP后需要重新刷新设备列表，否则打开相机时会报错
                temp = MyCamera.MV_CC_EnumDevices_NET(MyCamera.MV_GIGE_DEVICE | MyCamera.MV_USB_DEVICE, ref deviceList);//更新设备列表
                for (int i = 0; i < deviceList.nDeviceNum; i++)
                {
                    deviceInfo = (MyCamera.MV_CC_DEVICE_INFO)Marshal.PtrToStructure(deviceList.pDeviceInfo[i], typeof(MyCamera.MV_CC_DEVICE_INFO));//获取设备
                    if (deviceInfo.nTLayerType == MyCamera.MV_GIGE_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(deviceInfo.SpecialInfo.stGigEInfo, 0);
                        MyCamera.MV_GIGE_DEVICE_INFO gigeInfo = (MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                        //m_SerialNumber = gigeInfo.chSerialNumber;//获取序列号
                        m_SerialNumber = gigeInfo.chUserDefinedName;//获取用户名
                    }
                    else if (deviceInfo.nTLayerType == MyCamera.MV_USB_DEVICE)
                    {
                        IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(deviceInfo.SpecialInfo.stUsb3VInfo, 0);
                        MyCamera.MV_USB3_DEVICE_INFO usbInfo = (MyCamera.MV_USB3_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_USB3_DEVICE_INFO));
                        m_SerialNumber = usbInfo.chUserDefinedName;
                    }
                    if (seriesStr.Equals(m_SerialNumber))
                    {
                        temp = myCamera.MV_CC_CreateDevice_NET(ref deviceInfo);
                        if (MyCamera.MV_OK != temp)
                        {
                            //创建相机失败
                            return -1;
                        }
                        temp = myCamera.MV_CC_OpenDevice_NET();//
                        if (MyCamera.MV_OK != temp)
                        {
                            //打开相机失败
                            return -1;
                        }
                        return 0;
                    }
                    continue;
                }
            }
            catch
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 关闭相机
        /// </summary>
        public int CloseCamera()//关闭相机，返回0为成功，-1为失败
        {
            int temp = StopCamera();//停止相机采集
            if (MyCamera.MV_OK != temp)
                return -1;
            temp = myCamera.MV_CC_CloseDevice_NET();
            if (MyCamera.MV_OK != temp)
                return -1;
            temp = myCamera.MV_CC_DestroyDevice_NET();
            if (MyCamera.MV_OK != temp)
                return -1;
            return 0;
        }
        /// <summary>
        /// 开始采集
        /// </summary>
        /// <returns></returns>
        public int StartCamera()//相机开始采集，返回0为成功，-1为失败
        {
            int temp = myCamera.MV_CC_StartGrabbing_NET();
            if (MyCamera.MV_OK != temp)
            {
                return -1;
            }
            return 0;
        }
        /// <summary>
        /// 停止采集
        /// </summary>
        /// <returns></returns>
        public int StopCamera()//停止相机采集，返回0为成功，-1为失败
        {
            int temp = myCamera.MV_CC_StopGrabbing_NET();
            if (MyCamera.MV_OK != temp)
                return -1;
            return 0;
        }

        /// <summary>
        /// 成功返回0失败返回-1
        /// 调用函数时可以传入需要改变的目标IP，如过没有传入则将相机IP设置为其所连接的网卡地址+1或-1
        /// </summary>
        /// <param name="IP"></param>
        /// <returns></returns>
        public int ChangeIP(string IP="")
        {
            try
            {
                IntPtr buffer = Marshal.UnsafeAddrOfPinnedArrayElement(deviceInfo.SpecialInfo.stGigEInfo, 0);
                MyCamera.MV_GIGE_DEVICE_INFO gigeInfo=(MyCamera.MV_GIGE_DEVICE_INFO)Marshal.PtrToStructure(buffer, typeof(MyCamera.MV_GIGE_DEVICE_INFO));
                IPAddress cameraIPAddress;
                string tempStr = "";
                if(IP.Trim().Equals("")||!(IPAddress.TryParse(IP,out cameraIPAddress)))
                {
                    //当前网卡的IP地址
                    UInt32 nNetIp1 = (gigeInfo.nNetExport & 0xFF000000) >> 24;
                    UInt32 nNetIp2 = (gigeInfo.nNetExport & 0x00FF0000) >> 16;
                    UInt32 nNetIp3 = (gigeInfo.nNetExport & 0x0000FF00) >> 8;
                    UInt32 nNetIp4 = (gigeInfo.nNetExport & 0x000000FF);
                    //根据网卡IP设定相机IP，如果网卡ip第四位小于252，则相机ip第四位+1，否则相机IP第四位-1
                    UInt32 cameraIp1 = nNetIp1;
                    UInt32 cameraIp2 = nNetIp2;
                    UInt32 cameraIp3 = nNetIp3;
                    UInt32 cameraIp4 = nNetIp4;
                    if (nNetIp4 < 252)
                    {
                        cameraIp4++;
                    }
                    else
                    {
                        cameraIp4--;
                    }
                    tempStr = cameraIp1 + "." + cameraIp2 + "." + cameraIp3 + "." + cameraIp4;
                }
                else
                {
                    tempStr = IP;
                }
                IPAddress.TryParse(tempStr, out cameraIPAddress);
                long cameraIP = IPAddress.NetworkToHostOrder(cameraIPAddress.Address);
                //设置相机掩码
                uint maskIp1 = (gigeInfo.nCurrentSubNetMask & 0xFF000000) >> 24;
                uint maskIp2 = (gigeInfo.nCurrentSubNetMask & 0x00FF0000) >> 16;
                uint maskIp3 = (gigeInfo.nCurrentSubNetMask & 0x0000FF00) >> 8;
                uint maskIp4 = (gigeInfo.nCurrentSubNetMask & 0x000000FF);
                IPAddress subMaskAddress;
                tempStr = maskIp1 + "." + maskIp2 + "." + maskIp3 + "." + maskIp4;
                IPAddress.TryParse(tempStr, out subMaskAddress);
                long maskIP = IPAddress.NetworkToHostOrder(subMaskAddress.Address);
                //设置网关
                uint gateIp1 = (gigeInfo.nDefultGateWay & 0xFF000000) >> 24;
                uint gateIp2 = (gigeInfo.nDefultGateWay & 0x00FF0000) >> 16;
                uint gateIp3 = (gigeInfo.nDefultGateWay & 0x0000FF00) >> 8;
                uint gateIp4 = (gigeInfo.nDefultGateWay & 0x000000FF);
                IPAddress gateAddress;
                tempStr = gateIp1 + "." + gateIp2 + "." + gateIp3 + "." + gateIp4;
                IPAddress.TryParse(tempStr, out gateAddress);
                long gateIP = IPAddress.NetworkToHostOrder(gateAddress.Address);

                int temp = myCamera.MV_GIGE_ForceIpEx_NET((UInt32)(cameraIP >> 32), (UInt32)(maskIP >> 32), (UInt32)(gateIP >> 32));//执行更改相机IP的命令
                if (temp == 0)
                    //强制IP成功
                    return 0;
                //强制IP失败
                return -1;
            }
            catch 
            {

                return -1;
            }
        }
        /// <summary>
        /// 创建发送软触发函数
        /// </summary>
        /// <returns></returns>
        public int SoftTrigger()
        {
            int temp = myCamera.MV_CC_SetCommandValue_NET("TriggerSoftware");
            if (MyCamera.MV_OK != temp)
            {
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// 读取成功返回Himage图像，失败返回NULL
        /// </summary>
        /// <returns></returns>
        public HImage ReadImage()
        {
            UInt32 nPayloadSize = 0;
            int temp = myCamera.MV_CC_GetIntValue_NET("PayloadSize", ref stParam);
            if (MyCamera.MV_OK!=temp)
            {
                return null;
            }

            nPayloadSize = stParam.nCurValue;
            if (nPayloadSize > m_nBufSizeForDriver)
            {
                m_nBufSizeForDriver = nPayloadSize;
                m_pBufForDriver = new byte[m_nBufSizeForDriver];
                m_nBufSizeForSaveImage = m_nBufSizeForDriver * 3 + 2048;
                m_pBufForSaveImage = new byte[m_nBufSizeForSaveImage];
            }
            IntPtr pData = Marshal.UnsafeAddrOfPinnedArrayElement(m_pBufForDriver, 0);//获取地址
            MyCamera.MV_FRAME_OUT_INFO_EX stFrameInfo = new MyCamera.MV_FRAME_OUT_INFO_EX();
            temp = myCamera.MV_CC_GetOneFrameTimeout_NET(pData, m_nBufSizeForSaveImage, ref stFrameInfo, 1000); //获取一帧图像，超时时间设置为1000
            if (MyCamera.MV_OK != temp)
            {
                return null;
            }
            HImage image = new HImage();
            if (IsMonoData(stFrameInfo.enPixelType))
            {
                //如果是黑白图像，利用Halcon图像库中的GenImage1算子来构建图像
                image.GenImage1("byte", (int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, pData);
            }
            else
            {
                if (stFrameInfo.enPixelType == MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed)
                {
                    //如果彩色图像是RGB8格式，则可以直接利用GenImageInterleaved算子来构建图像
                    image.GenImageInterleaved(pData, "rgb", (int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, 0, "byte", (int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, 0, 0, -1, 0);
                }
                else
                {
                    //如果彩色图像不是RGB8格式，则需要将图像格式转换为RGB8。
                    IntPtr pBufForSaveImage = IntPtr.Zero;
                    if(pBufForSaveImage == IntPtr.Zero)
                    { 
                        pBufForSaveImage = Marshal.AllocHGlobal((int)(stFrameInfo.nWidth * stFrameInfo.nHeight * 3 + 2048));
                    }
                    MyCamera.MV_PIXEL_CONVERT_PARAM stConverPixelParam = new MyCamera.MV_PIXEL_CONVERT_PARAM();
                    stConverPixelParam.nWidth = stFrameInfo.nWidth;
                    stConverPixelParam.nHeight = stFrameInfo.nHeight;
                    stConverPixelParam.pSrcData = pData;
                    stConverPixelParam.nSrcDataLen = stFrameInfo.nFrameLen;
                    stConverPixelParam.enSrcPixelType = stFrameInfo.enPixelType;
                    stConverPixelParam.enDstPixelType = MyCamera.MvGvspPixelType.PixelType_Gvsp_RGB8_Packed;//在此处选择需要转换的目标类型
                    stConverPixelParam.pDstBuffer = pBufForSaveImage;
                    stConverPixelParam.nDstBufferSize = (uint)(stFrameInfo.nWidth * stFrameInfo.nHeight * 3 + 2048);
                    myCamera.MV_CC_ConvertPixelType_NET(ref stConverPixelParam);
                    image.GenImageInterleaved(pBufForSaveImage, "rgb", (int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, 0, "byte", (int)stFrameInfo.nWidth, (int)stFrameInfo.nHeight, 0, 0, -1, 0);
                    //释放指针
                    Marshal.FreeHGlobal(pBufForSaveImage);
                }
            }
            return image;
        }

        #region 其他功能
        //1.设置Int型参数
        public int SetWidth(uint width)//设置图像宽度，成功返回0失败返回-1
        {
            int temp = myCamera.MV_CC_SetIntValue_NET("Width", width);
            if (MyCamera.MV_OK != temp)
                return 0;
            return -1;
        }
        //2.设置枚举型参数
        public int SetTriggerMode(uint TriggerMode)//设置触发事件，成功返回0失败返回-1
        {
            //1:On 触发模式
            //0:Off 非触发模式
            int temp = myCamera.MV_CC_SetEnumValue_NET("TriggerMode", TriggerMode);
            if (MyCamera.MV_OK != temp)
                return 0;
            return -1;
        }
        //3.设置浮点型型参数
        public int SetExposureTime(uint ExposureTime)//设置曝光时间（us），成功返回0失败返回-1
        {
            int temp = myCamera.MV_CC_SetFloatValue_NET("ExposureTime", ExposureTime);
            if (MyCamera.MV_OK != temp)
                return 0;
            return -1;
        }
        //4.判断是否为黑白图像
        private Boolean IsMonoData(MyCamera.MvGvspPixelType enGvspPixelType)
        {
            switch (enGvspPixelType)
            {
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono8:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono10_Packed:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12:
                case MyCamera.MvGvspPixelType.PixelType_Gvsp_Mono12_Packed:
                    return true;
                default:
                    return false;
            }
        }
        //5.设置心跳时间，成功返回0失败返回-1
        public int SetHeartBeatTime(uint heartBeatTime)
        {
            //心跳时间最小为500
            uint tempTime = heartBeatTime > 500 ? heartBeatTime : 500;
            int temp = myCamera.MV_CC_SetIntValue_NET("GevHeartbeatTimeout", tempTime);
            if (MyCamera.MV_OK != temp)
                return 0;
            return -1;
        }

        #endregion
    }
}

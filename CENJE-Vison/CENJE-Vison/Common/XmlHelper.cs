using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CENJE_Vison.Common
{
    public class XmlHelper
    {
        private string _xmlName;
        public XmlDocument XmlDoc;

        public string XmlName
        {
            get { return _xmlName; }
            set { _xmlName = value; }
        }

        public XmlHelper(string sFileName)
        {
            XmlName = sFileName;
            XmlDoc = new XmlDocument();
            OpenXml();
        }

        //~XmlHelper()
        //{
        //    SaveXml();
        //}

        public bool OpenXml()
        {
            try
            {
                XmlDoc.Load(XmlName);
                return (true);
            }

            catch (System.IO.FileNotFoundException)
            {
                CreatXml(XmlName);
                XmlDoc.Load(XmlName);
                return false;
            }
        }


        public  void SaveXml()
        {
            XmlDoc.Save(XmlName);
        }



        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="xmlname"></param>
        public void CreatXml(string xmlname)
        {
            string ConfigPath = $"{Environment.CurrentDirectory}\\Config";
            if (!Directory.Exists(ConfigPath))
            {
                Directory.CreateDirectory(ConfigPath);
            }

            XmlTextWriter writer = new XmlTextWriter(xmlname, System.Text.Encoding.UTF8);

            //使用缩进便于阅读
            writer.Formatting = Formatting.Indented;

            //XML声明
            writer.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
            //writer.WriteStartDocument();

            //书写根元素
            writer.WriteStartElement("System_Root");

            // 关闭元素
            writer.Close();
            
        }

        /// <summary>
        /// 读取节点内容
        /// </summary>
        public string ReadXml( string nodeName)
        {
            XmlNode x_Node = XmlDoc.SelectSingleNode("System_Root").SelectSingleNode(nodeName);
            return x_Node == null ? "" : x_Node.InnerText;
        }

        /// <summary>
        /// 插入新节点
        /// </summary>
        public void Insert(string insertName,string insertValue)
        {
            XmlNode x_Root = XmlDoc.SelectSingleNode("System_Root");//查找<System_Root>
            XmlNode x_Node = x_Root.SelectSingleNode(insertName);
            if (x_Node != null)
            {
                x_Node.InnerText = insertValue;
            }
            else
            {
                XmlElement x_Element = XmlDoc.CreateElement(insertName);
                x_Element.InnerText = insertValue;
                x_Root.AppendChild(x_Element);
            }
            SaveXml();
        }

        //修改节点
        public void Modify(string modifyName,string modifyValue)
        {
            XmlNode x_Root = XmlDoc.SelectSingleNode("System_Root");
            XmlNode x_Node = x_Root.SelectSingleNode(modifyName);
            if (x_Node != null)
            {
                x_Node.InnerText = modifyValue;
            }
            else
            {
                XmlElement x_Elem = XmlDoc.CreateElement(modifyName);
                x_Elem.InnerText = modifyValue;
                x_Root.AppendChild(x_Elem);
            }

            SaveXml();
        }
    }
}

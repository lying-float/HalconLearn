using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CENJE_Vison.Common
{
    public class CsvFile
    {
        #region 写CSV文件
        //字段数组转换为CSV记录行
        private static string FieldsToLine(IEnumerable<string> fields)
        {
            if (fields == null) return string.Empty;
            fields = fields.Select(field =>
            {
                if (field == null) field = string.Empty;

                field = string.Format("\"{0}\"", field.Replace("\"", "\"\""));
                return field;
            });
            string line=string.Format("{0}{1}",string.Join(",", fields),Environment.NewLine);
            return line;
        }

        //默认的字段转换方法
        private static IEnumerable<string>GetObjFields<T>(T obj,bool isTitle) where T : class
        {
            IEnumerable<string> fields;
            if(isTitle)
            {
                fields = obj.GetType().GetProperties().Select(pro => pro.Name);
            }
            else
            {
                fields=obj.GetType().GetProperties().Select(pro=>pro.GetValue(obj)?.ToString());
            }
            return fields;
        }

        /// <summary>
        /// 写CSV文件，默认第一行为标题
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">数据列表</param>
        /// <param name="path">文件路径</param>
        /// <param name="append">追加记录</param>
        /// <param name="defaultEncoding"></param>
        /// <param name="func">字段转换方法</param>
        public static void Write<T>(List<T> list, string path, bool append = true, Encoding defaultEncoding = null,
            Func<T, bool, IEnumerable<string>> func = null) where T : class
        {
            if(list == null||list.Count==0) return;
            if(defaultEncoding== null)
            {
                defaultEncoding = Encoding.UTF8;
            }
            if(func != null)
            {
                func = GetObjFields;
            }
            if(!File.Exists(path)||!append)
            {
                var fields = func(list[0], true);
                string line=FieldsToLine(fields);
                File.WriteAllText(path, line);
            }
            using(StreamWriter sw = new StreamWriter(path,true,defaultEncoding)) 
            {
                list.ForEach(obj =>
                {
                    var fields = func(obj, true);
                    string line = FieldsToLine(fields);
                    sw.WriteLine(line);
                });
            }
        }


        #endregion

        #region 读CSV文件

        #endregion
    }
}

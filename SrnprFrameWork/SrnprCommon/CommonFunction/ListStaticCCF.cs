using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;

namespace SrnprCommon.CommonFunction
{

    /// <summary>
    /// Author:Liudpc
    /// Create Date: 2010-10-8 20:25:08
    /// Description: 序列相关
    /// </summary>
    public class ListStaticCCF
    {


        /// <summary>
        /// 
        /// Author:Liudpc
        /// Create Date: 2010-10-8 20:24:54
        /// Description: 序列转化为DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entitys"></param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> entitys)
        {

            DataTable dt = new DataTable();

            //检查实体集合不能为空
            if (entitys == null || entitys.Count < 1)
            {

            }
            else
            {

                //取出第一个实体的所有Propertie
                Type entityType = entitys[0].GetType();
                PropertyInfo[] entityProperties = entityType.GetProperties();

                //生成DataTable的structure
                //生产代码中，应将生成的DataTable结构Cache起来，此处略

                for (int i = 0; i < entityProperties.Length; i++)
                {
                    //dt.Columns.Add(entityProperties[i].Name, entityProperties[i].PropertyType);
                    dt.Columns.Add(entityProperties[i].Name);
                }

                //将所有entity添加到DataTable中
                foreach (object entity in entitys)
                {
                    //检查所有的的实体都为同一类型
                    if (entity.GetType() == entityType)
                    {

                        object[] entityValues = new object[entityProperties.Length];
                        for (int i = 0; i < entityProperties.Length; i++)
                        {
                            entityValues[i] = entityProperties[i].GetValue(entity, null);

                        }
                        dt.Rows.Add(entityValues);
                    }
                }
            }
            return dt;
        }





    }
}

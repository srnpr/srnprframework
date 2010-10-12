using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;

namespace SrnprSite.Asmx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    
    public class WebWidgetHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string s = context.Request["json"];
            SrnprWeb.WebEntity.WidgetRequestWWE req = SrnprWeb.WebProcess.WidgetProcessWWP.DeserializeRequest(s);

            string sRes = SrnprWeb.WebProcess.WidgetProcessWWP.Response(req, DicProcess(req));

            context.Response.Write(sRes);
        }




        /// <summary>
        /// 
        /// Description: 返回附加的数据
        /// Author:Liudpc
        /// Create Date: 2010-8-27 10:38:44
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> DicProcess(SrnprWeb.WebEntity.WidgetRequestWWE req)
        {

            Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE> dic = new Dictionary<int, SrnprWeb.WebEntity.WidgetProcessWWE>();



            for (int i = 0, j = req.RQ.Count; i < j; i++)
            {

                switch (req.RQ[i].WidgetType)
                {

                    case "LS":

                        DataTable dtls = GetLSTableById(req.RQ[i].Id);

                        if (dtls != null)
                        {
                            dic[i] = new SrnprWeb.WebEntity.WidgetProcessWWE();
                            dic[i].DataInfo = dtls;
                        }

                        break;
                    case "GS":

                        DataTable dtgs = GetGSTableById(req.RQ[i].Id);

                        if (dtgs != null&&dtgs.Columns.Count>0)
                        {
                            dic[i] = new SrnprWeb.WebEntity.WidgetProcessWWE();
                            dic[i].DataInfo = dtgs;
                            dic[i].DataFlag = true;
                        }

                        break;
                }



            }
            return dic;
        }




        public DataTable GetLSTableById(string sId)
        {

            DataTable dt = new DataTable();

            switch (sId)
            {
                case "abc":


                    List<SrnprWeb.WebEntity.ItemKvdWWE> kvd = new List<SrnprWeb.WebEntity.ItemKvdWWE>();
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk2" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk3" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk4" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk5" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk6" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk7" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk8" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk9" });
                    dt = SrnprCommon.CommonFunction.ListStaticCCF.ListToDataTable<SrnprWeb.WebEntity.ItemKvdWWE>(kvd);
                    break;

                case "t3":


                    List<SrnprWeb.WebEntity.ItemKvdWWE> kvd2 = new List<SrnprWeb.WebEntity.ItemKvdWWE>();
                    kvd2.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk27" });
                    kvd2.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk28" });
                    kvd2.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk29" });
                    kvd2.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk30" });
                    kvd2.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk31" });
                    kvd2.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk32" });
                    dt = SrnprCommon.CommonFunction.ListStaticCCF.ListToDataTable<SrnprWeb.WebEntity.ItemKvdWWE>(kvd2);
                    break;
                case "":

                    break;
            }

            return dt;

        }








        /// <summary>
        /// 
        /// Description: GridShow加载数据
        /// Author:Liudpc
        /// Create Date: 2010-8-30 14:01:58
        /// </summary>
        /// <param name="sId"></param>
        /// <returns></returns>
        public DataTable GetGSTableById(string sId)
        {

            DataTable dt = new DataTable();

            switch (sId)
            {
                case "test.gs.listkvd":

                    dt.TableName = "test.gs.listkvd";

                    List<SrnprWeb.WebEntity.ItemKvdWWE> kvd = new List<SrnprWeb.WebEntity.ItemKvdWWE>();
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk1" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk2" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk3" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk4" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk5" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk6" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk7" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk8" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk9" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk10" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk11" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk12" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk13" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk14" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk15" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk16" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk17" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk18" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk19" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk20" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk21" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk22" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk23" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk24" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk25" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk26" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk27" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk28" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk29" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk30" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk31" });
                    kvd.Add(new SrnprWeb.WebEntity.ItemKvdWWE() { V = "vvv1", D = "ddd1", K = "kkk32" });

                   

                    dt = SrnprCommon.CommonFunction.ListStaticCCF.ListToDataTable<SrnprWeb.WebEntity.ItemKvdWWE>(kvd);


                    break;
                case "":

                    break;
            }

            return dt;

        }










        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        

    }
}

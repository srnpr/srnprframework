using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SrnprCommon.ReplaceFile;
using SrnprCommon;
using SrnprCommon.CommonConfig;
using SrnprCommon.CommonFunction;
using SrnprCommon.EnumCommon;

namespace SendEmail
{
    public class SendEmail
    {



          private static ReplaceXmlCRF replace;

          public SendEmail()
        {
            if (replace == null)
            {
                replace = new ReplaceXmlCRF();
            }
        }





        public List<DoSendEmailEntityCRF> GetSendList(string sXmlId, string sParmsContent)
        {
            List<DoSendEmailEntityCRF> doSend = new List<DoSendEmailEntityCRF>();

           

            ReplaceFileEntityCRF replaceEntity = new ReplaceFileEntityCRF();




            replaceEntity.TempleteXml = replace.GetTempleteXml(ReplaceFileConfigCCC.Config.XmlFileDirectory + sXmlId + ".xml");
            replaceEntity.ReplaceParms = sParmsContent;
            replaceEntity.ReplaceFileId = sXmlId;
            replaceEntity.DataServer = ReplaceFileConfigCCC.Config.DataServerList.SingleOrDefault(t => t.Id == replaceEntity.TempleteXml.Code.Config.DataServerId);



            DataReplaceEntityCRF dataReplace = replace.GetDataReplace(replaceEntity);




            if (replaceEntity.TempleteXml.Design.ItemRule.Count > 0)
            {
                foreach (ItemRuleEntityAtCRF itemRule in replaceEntity.TempleteXml.Design.ItemRule)
                {
                    if (itemRule.RuleType == ItemRuleType.RuleExpression)
                    {
                        ItemRuleExpressionEntityCRF ruleExpress = itemRule as ItemRuleExpressionEntityCRF;
                        string sResult =EvalFunctionCCF.Eval(replace.ReplaceParmsByDict(ruleExpress.Expression, dataReplace.MainParms)).ToLower();
                        if (sResult == "true")
                        {
                            doSend.Add(new DoSendEmailEntityCRF() { TempleteId = ruleExpress.TempleteGuid, ToEmail = replace.ReplaceParmsByDict(ruleExpress.ExpressionParm, dataReplace.MainParms) });


                        }
                    }
                }
            }




            if (doSend.Count > 0)
            {



                foreach (DoSendEmailEntityCRF send in doSend)
                {

                    ItemTempleteEmailInfoEntityCRF emailEntity = replaceEntity.TempleteXml.Design.ItemTemplete.SingleOrDefault(t => t.Guid == send.TempleteId) as ItemTempleteEmailInfoEntityCRF;
                    send.Title = replace.ReplaceParmsByDict(emailEntity.Title, dataReplace.MainParms);
                    send.Content = replace.ReplaceParmsByDict(emailEntity.Content, dataReplace.MainParms);
                    send.EmailServer = ReplaceFileConfigCCC.Config.EmailServerList.SingleOrDefault(t => t.Id == replaceEntity.TempleteXml.Code.Config.EmailServerId);

                }
            }

            return doSend;

        }




        public ResultSendEmail Send(string sXmlId,string sParmsContent)
        {



            ResultSendEmail returnResult = new ResultSendEmail();


            List<DoSendEmailEntityCRF> doSend = GetSendList(sXmlId, sParmsContent);

            if (doSend.Count > 0)
            {
                foreach (DoSendEmailEntityCRF send in doSend)
                {
                    SendEmailCCF.Send(send);
                }
            }

            int iSuccessFulCount = doSend.Count(t => t.SendSuccessFlag);
            if (iSuccessFulCount == 0)
            {
                returnResult.ReplaceResultFlag = false;
                returnResult.SendState = 3;
            }
            else
            {
                returnResult.ReplaceResultFlag = true;
                if (iSuccessFulCount == doSend.Count)
                {
                    returnResult.SendState = 1;
                }
                else
                {
                    returnResult.SendState = 2;
                }
            }



            return returnResult;


        }




        /// <summary>
        /// 
        /// Description: 重新检测所有文件并生成列表文件
        /// Author:Liudpc
        /// Create Date: 2010-6-12 11:08:06
        /// </summary>
        public void RecheckAllEmailFile()
        {
            replace.RecheckXmlFromDirectory(ReplaceFileConfigCCC.Config.XmlFileDirectory, ReplaceFileConfigCCC.Config.ListFilePath);
        }


        /// <summary>
        /// 
        /// Description: 得到所有文件的列表
        /// Author:Liudpc
        /// Create Date: 2010-6-11 16:28:27
        /// </summary>
        /// <returns></returns>
        public List<ReplaceFileListEntityCRF> GetListFileInfoByFilePath()
        {
            return replace.GetListFileInfoByFilePath(ReplaceFileConfigCCC.Config.ListFilePath);

        }



        public string[] RegexSqlStringParm(string sContent)
        {
            return replace.RegexSqlStringParm(sContent);
        }



        private bool UpdateXml(EmailDesignItem edi)
        {

            TempleteDesignEntityCRF design = GetTempleteDesign(edi.XmlId);

            ItemRuleExpressionEntityCRF ire = design.ItemRule.SingleOrDefault(t => t.TempleteGuid == edi.TempleteGuid) as ItemRuleExpressionEntityCRF;









            return true;
        }




        public TempleteDesignEntityCRF GetTempleteDesign(string sXmlId)
        {
            return replace.GetTempleteDesign(ReplaceFileConfigCCC.Config.XmlFileDirectory + sXmlId + ReplaceFileConfigCCC.Config.DesignFileApp);

        }




        public EmailDesignInfo GetDesign(string sXmlId)
        {

            EmailDesignInfo returnResult = new EmailDesignInfo();


            TempleteXmlEntityCRF txe = replace.GetTempleteXml(GetListFileInfoByFilePath().SingleOrDefault(t => t.Id == sXmlId).FilePath);


            returnResult.Title = txe.Code.Config.Title;



            string sMainParm = ReplaceFileConfigCCC.Config.MainParmReplace;

            StringBuilder sb = new StringBuilder();
            foreach (ItemMainSqlEntityCRF mainSql in txe.Code.MainSql)
            {
                foreach (string s in replace.RegexSqlStringParm(mainSql.SqlString))
                {
                    sb.Append("<li>{$" + s+ "}</li>");
                }
            }

            returnResult.Parms = sb.ToString().Trim();



            returnResult.ListItem = new List<EmailDesignItem>();
            foreach (ItemRuleExpressionEntityCRF ire in txe.Design.ItemRule)
            {

                ItemTempleteEmailInfoEntityCRF ite = txe.Design.ItemTemplete.SingleOrDefault(t => t.Guid == ire.TempleteGuid) as ItemTempleteEmailInfoEntityCRF;
               
                if (ite != null)
                {
                    EmailDesignItem edi = new EmailDesignItem();
                    
                    edi.TempleteGuid = ire.TempleteGuid;
                    edi.ToEmail = ire.ExpressionParm;
                    edi.Title = ite.Title;
                    edi.Content = ite.Content;
                    edi.RuleExpress = ire.Expression;

                    returnResult.ListItem.Add(edi);

                }


               

                
            }











            return returnResult;

        }

        
    }
}

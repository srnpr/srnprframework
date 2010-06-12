using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class SendEmailCRF
    {

        private static ReplaceXmlCRF replace;

        public  SendEmailCRF()
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




            replaceEntity.TempleteXml = replace.GetTempleteXml(CommonConfig.ReplaceFileConfigCCC.Config.XmlFileDirectory + sXmlId + ".xml");
            replaceEntity.ReplaceParms = sParmsContent;
            replaceEntity.ReplaceFileId = sXmlId;
            replaceEntity.DataServer = CommonConfig.ReplaceFileConfigCCC.Config.DataServerList.SingleOrDefault(t => t.Id == replaceEntity.TempleteXml.Code.Config.DataServerId);



            DataReplaceEntityCRF dataReplace = replace.GetDataReplace(replaceEntity);




            if (replaceEntity.TempleteXml.Design.ItemRule.Count > 0)
            {
                foreach (ItemRuleEntityAtCRF itemRule in replaceEntity.TempleteXml.Design.ItemRule)
                {
                    if (itemRule.RuleType == EnumCommon.ItemRuleType.RuleExpression)
                    {
                        ItemRuleExpressionEntityCRF ruleExpress = itemRule as ItemRuleExpressionEntityCRF;
                        string sResult = CommonFunction.EvalFunctionCCF.Eval(replace.ReplaceParmsByDict(ruleExpress.Expression, dataReplace.MainParms)).ToLower();
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
                    send.EmailServer = CommonConfig.ReplaceFileConfigCCC.Config.EmailServerList.SingleOrDefault(t => t.Id == replaceEntity.TempleteXml.Code.Config.EmailServerId);

                }
            }

            return doSend;

        }




        public ResultSendEmailEntityCRF SendEmail(string sXmlId,string sParmsContent)
        {



            ResultSendEmailEntityCRF returnResult = new ResultSendEmailEntityCRF();


            List<DoSendEmailEntityCRF> doSend = GetSendList(sXmlId, sParmsContent);

            if (doSend.Count > 0)
            {
                foreach (DoSendEmailEntityCRF send in doSend)
                {
                    CommonFunction.SendEmailCCF.Send(send);
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




        public void RecheckAllEmailFile()
        {
            replace.RecheckXmlFromDirectory(CommonConfig.ReplaceFileConfigCCC.Config.XmlFileDirectory, CommonConfig.ReplaceFileConfigCCC.Config.ListFilePath);
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
            return replace.GetListFileInfoByFilePath(CommonConfig.ReplaceFileConfigCCC.Config.ListFilePath);

        }



        public string[] RegexSqlStringParm(string sContent)
        {
            return replace.RegexSqlStringParm(sContent);
        }





        





    }
}

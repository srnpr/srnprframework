using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class SendEmailCRF
    {


        public ResultSendEmailEntityCRF SendEmail(string sXmlId,string sParmsContent)
        {



            ResultSendEmailEntityCRF returnResult = new ResultSendEmailEntityCRF();


            List<DoSendEmailEntityCRF> doSend = new List<DoSendEmailEntityCRF>();





            ReplaceXmlCRF replace = new ReplaceXmlCRF();


            ReplaceFileEntityCRF replaceEntity = new ReplaceFileEntityCRF();




            replaceEntity.TempleteXml = replace.GetTempleteXml(CommonConfig.ReplaceFileConfigCCC.Config().XmlFileDirectory + sXmlId + ".xml");
            replaceEntity.ReplaceParms = sParmsContent;
            replaceEntity.ReplaceFileId = sXmlId;
            replaceEntity.DataServer = CommonConfig.ReplaceFileConfigCCC.Config().DataServerList.SingleOrDefault(t => t.Id == replaceEntity.TempleteXml.Code.Config.DataServerId);

            

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





            foreach (DoSendEmailEntityCRF send in doSend)
            {

                 ItemTempleteEmailInfoEntityCRF emailEntity= replaceEntity.TempleteXml.Design.ItemTemplete.SingleOrDefault(t => t.Guid == send.TempleteId) as ItemTempleteEmailInfoEntityCRF;



                 send.Title = replace.ReplaceParmsByDict(emailEntity.Title, dataReplace.MainParms);


                 send.Content = replace.ReplaceParmsByDict(emailEntity.Content, dataReplace.MainParms);






            }



            return returnResult;


        }

        


    }
}

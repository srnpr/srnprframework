﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class SendEmailCRF
    {


        public ResultSendEmailEntityCRF SendEmail(string sXmlId,string sParmsContent)
        {

            ResultReplaceEntityCRF returnResult = new ResultReplaceEntityCRF();

            ReplaceXmlCRF replace = new ReplaceXmlCRF();


            ReplaceFileEntityCRF replaceEntity = new ReplaceFileEntityCRF();


            







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



                        }


                    }
                }


            }






            return returnResult;


        }

        


    }
}

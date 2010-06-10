using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class ItemRuleExpressionEntityCRF:ItemRuleEntityCRF
    {


        public string TempleteGuid { get; set; }



        public string Expression { get; set; }

        public string ExpressionParm { get; set; }


        public override SrnprCommon.EnumCommon.ItemRuleType RuleType
        {
            get { return SrnprCommon.EnumCommon.ItemRuleType.RuleExpression; }
        }
    }
}

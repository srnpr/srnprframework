using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public class ResultSendEmailDesignEntityCRF
    {

        public string Title { get; set; }



        public string Parms { get; set; }


        public Dictionary<ItemRuleExpressionEntityCRF, ItemTempleteEmailInfoEntityCRF> DicRuleTemplete { get; set; }

    }
}

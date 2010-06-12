using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprCommon.ReplaceFile
{
    public abstract class ItemRuleEntityAtCRF
    {

        public abstract EnumCommon.ItemRuleType RuleType { get; }


        public abstract string TempleteGuid { get; set; }

    }
}

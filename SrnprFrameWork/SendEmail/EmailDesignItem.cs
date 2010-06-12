using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendEmail
{
    public class EmailDesignItem
    {

        public string Title { get; set; }


        public string Content { get; set; }

        public string TempleteGuid { get; set; }

        public string ToEmail { get; set; }

        public string RuleExpress { get; set; }
    }
}

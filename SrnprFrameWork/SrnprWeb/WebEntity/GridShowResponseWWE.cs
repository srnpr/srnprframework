using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SrnprWeb.WebEntity
{
    public class GridShowResponseWWE
    {

        public GridShowRequestWWE Request { get; set; }



        public string HtmlString { get; set; }

        public List<List<string>> DataItem { get; set; }

    }
}

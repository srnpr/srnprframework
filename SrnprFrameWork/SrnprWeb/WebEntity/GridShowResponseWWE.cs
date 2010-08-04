using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace SrnprWeb.WebEntity
{
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    public class GridShowResponseWWE
    {
        [DataMember(Order = 0)]
        public GridShowRequestWWE Request { get; set; }


        [DataMember(Order = 1)]
        public string HtmlString { get; set; }

        public List<List<string>> DataItem { get; set; }

    }
}

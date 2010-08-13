using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Services;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace SrnprWeb.WebEntity
{
    [DataContract(Namespace = "http://srnprframework/srnprweb")]
    public class ItemKvdWWE
    {
        [DataMember(Order = 0)]

        /// <summary>
        /// Description: Key
        /// Author:Liudpc
        /// Create Date: 2010-8-12 18:29:49
        /// </summary>
        public string K { get; set; }


        [DataMember(Order = 1)]
        /// <summary>
        /// Description: 值
        /// Author:Liudpc
        /// Create Date: 2010-8-12 18:29:41
        /// </summary>
        public string V { get; set; }

        [DataMember(Order = 2)]
        /// <summary>
        /// Description: 描述
        /// Author:Liudpc
        /// Create Date: 2010-8-12 18:29:32
        /// </summary>
        public string D { get; set; }


    }
}

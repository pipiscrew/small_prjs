using posokanei.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posokanei.Entities
{
    public class Product : ModelBase
    {
        public Int64 id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string when2check { get; set; }
        public string dateupdated { get; set; }
    }
}

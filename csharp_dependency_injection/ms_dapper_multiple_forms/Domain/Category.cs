using System;
using App.Helpers;

namespace Domain
{
    public class Category : ModelBase
    {
        public Int64 id { get; set; }
        public string title { get; set; }
    }
}

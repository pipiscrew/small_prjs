using System;
using App.Helpers;

namespace Domain
{
    public class Product : ModelBase
    {
        public Int64 id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string when2check { get; set; }
        public string dateupdated { get; set; }
        public string smarketab { get; set; }
        public string smarketsklav { get; set; }
        public string smarketbazaar { get; set; }
        public string smarketmymarket { get; set; }
        public string comment { get; set; }
        public string homepage { get; set; }
        public string nutritiontable { get; set; }
        public Int64? category_id { get; set; }
        public string ingredients { get; set; }
    }
}

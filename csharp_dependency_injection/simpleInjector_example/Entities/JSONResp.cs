using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace posokanei.Entities
{
    // https://json2csharp.com/
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class PriceStats
    {
        public double min_price { get; set; }
        public double max_price { get; set; }
        public double avg_price { get; set; }
        public int retailer_count { get; set; }
        public double min_unit_price { get; set; }
        public object last_computed { get; set; }
    }

    public class RetailerPrice
    {
        public string retailer { get; set; }
        public string retailer_display_name { get; set; }
        public string retailer_name { get; set; }
        public double price { get; set; }
        public double price_normalized { get; set; }
        public bool is_discount { get; set; }
        public object discount_percentage { get; set; }
        public DateTime last_updated { get; set; }
        public string country { get; set; }
    }

    public class Root
    {
        public string id { get; set; }
        public string name { get; set; }
        public string brand { get; set; }
        public List<object> images { get; set; }
        public string category { get; set; }
        public List<string> category_ids { get; set; }
        public string subcategory { get; set; }
        public string description { get; set; }
        public string image_url { get; set; }
        public bool has_image { get; set; }
        public DateTime updated_at { get; set; }
        public string image_version { get; set; }
        public string unit { get; set; }
        public double unit_quantity { get; set; }
        public bool private_label { get; set; }
        public PriceStats price_stats { get; set; }
        public List<string> retailers { get; set; }
        public List<RetailerPrice> retailer_prices { get; set; }
        public List<string> available_countries { get; set; }
        public bool is_international { get; set; }
        public object history { get; set; }
    }


}

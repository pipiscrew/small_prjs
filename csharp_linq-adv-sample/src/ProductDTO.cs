using System.Runtime.Serialization;

namespace linq_adv_sample
{
    [DataContract]
    class ProductDTO
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int? Category_ID { get; set; }
        [DataMember]
        public int SubCategory_ID { get; set; }
        [DataMember]
        public int AccessoryID { get; set; }
        [DataMember]
        public string Accessory { get; set; }
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public decimal Percentage { get; set; }
        [DataMember]
        public short ProfilOption { get; set; }
        [DataMember]
        public string Profil { get; set; }
        [DataMember]
        public short Ordinal { get; set; }
        [DataMember]
        public string ProductName { get; set; }

        public ProductDTO(string ProductName, string Accessory, string Code, short ProfilOption, decimal Percentage)
        {
            this.ProductName = ProductName;
            this.Accessory = Accessory;
            this.Code = Code;
            this.ProfilOption = ProfilOption;
            this.Percentage = Percentage;
        }
    }
}

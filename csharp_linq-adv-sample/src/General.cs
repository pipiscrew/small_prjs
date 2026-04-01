using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace linq_adv_sample
{
    class General
    {
        public static string appPath;
        public static List<ProductDTO> db;
        //private static List<ProductDTO> GetInvestmentResult()
        //{

        //}


        public static List<ProductDTO> ReadDB()
        {
            if (File.Exists(appPath))
            {
                var serializer = new DataContractJsonSerializer(typeof(List<ProductDTO>));
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(File.ReadAllText(appPath, Encoding.UTF8))))
                {
                    return (List<ProductDTO>)serializer.ReadObject(ms);
                }
            }
            else
                throw new Exception("dbase not found");
        }
    }
    
}

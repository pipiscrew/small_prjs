using System; 
using System.Collections.Generic; 
using north.BusinessObject; 
 
namespace north.BusinessObject 
{ 
     /// <summary>
     /// This class inherits from the Generic List of Products. 
     /// It's used so that you can iterate through the list or collection of the 
     /// Products class.  You don't need to add any code to this helper class. 
     /// </summary>
     public class ProductsCollection : List<Products> 
     { 
         // constructor 
         public ProductsCollection() 
         { 
         } 
     } 
} 

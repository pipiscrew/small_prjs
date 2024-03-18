using System; 
using System.Collections.Generic; 
using north.BusinessObject; 
 
namespace north.BusinessObject 
{ 
     /// <summary>
     /// This class inherits from the Generic List of OrderDetails. 
     /// It's used so that you can iterate through the list or collection of the 
     /// OrderDetails class.  You don't need to add any code to this helper class. 
     /// </summary>
     public class OrderDetailsCollection : List<OrderDetails> 
     { 
         // constructor 
         public OrderDetailsCollection() 
         { 
         } 
     } 
} 

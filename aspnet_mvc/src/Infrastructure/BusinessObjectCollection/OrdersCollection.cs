using System; 
using System.Collections.Generic; 
using north.BusinessObject; 
 
namespace north.BusinessObject 
{ 
     /// <summary>
     /// This class inherits from the Generic List of Orders. 
     /// It's used so that you can iterate through the list or collection of the 
     /// Orders class.  You don't need to add any code to this helper class. 
     /// </summary>
     public class OrdersCollection : List<Orders> 
     { 
         // constructor 
         public OrdersCollection() 
         { 
         } 
     } 
} 

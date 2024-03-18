using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for OrderDetailsViewModel.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetailsViewModel class 
     /// </summary>
     public class OrderDetailsViewModelBase
     { 
         public north.Models.OrderDetailsModel OrderDetailsModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
         public OrdersCollection OrdersDropDownListData { get; set; }
         public ProductsCollection ProductsDropDownListData { get; set; }
     } 
} 

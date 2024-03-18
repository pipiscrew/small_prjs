using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for OrdersViewModel.  Do not make changes to this class,
     /// instead, put additional code in the OrdersViewModel class 
     /// </summary>
     public class OrdersViewModelBase
     { 
         public north.Models.OrdersModel OrdersModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
         public CustomersCollection CustomersDropDownListData { get; set; }
         public EmployeesCollection EmployeesDropDownListData { get; set; }
         public ShippersCollection ShippersDropDownListData { get; set; }
     } 
} 

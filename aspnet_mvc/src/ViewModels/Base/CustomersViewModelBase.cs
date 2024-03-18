using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for CustomersViewModel.  Do not make changes to this class,
     /// instead, put additional code in the CustomersViewModel class 
     /// </summary>
     public class CustomersViewModelBase
     { 
         public north.Models.CustomersModel CustomersModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
     } 
} 

using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for SuppliersViewModel.  Do not make changes to this class,
     /// instead, put additional code in the SuppliersViewModel class 
     /// </summary>
     public class SuppliersViewModelBase
     { 
         public north.Models.SuppliersModel SuppliersModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
     } 
} 

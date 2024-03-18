using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for ProductsViewModel.  Do not make changes to this class,
     /// instead, put additional code in the ProductsViewModel class 
     /// </summary>
     public class ProductsViewModelBase
     { 
         public north.Models.ProductsModel ProductsModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
         public SuppliersCollection SuppliersDropDownListData { get; set; }
         public CategoriesCollection CategoriesDropDownListData { get; set; }
     } 
} 

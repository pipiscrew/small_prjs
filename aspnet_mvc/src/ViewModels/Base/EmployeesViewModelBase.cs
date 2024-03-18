using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for EmployeesViewModel.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesViewModel class 
     /// </summary>
     public class EmployeesViewModelBase
     { 
         public north.Models.EmployeesModel EmployeesModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
         public EmployeesCollection EmployeesDropDownListData { get; set; }
     } 
} 

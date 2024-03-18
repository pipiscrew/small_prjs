using north.Models;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for EmployeesForeachViewModel.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesForeachViewModel class
     /// </summary>
     public class EmployeesForeachViewModelBase
     { 
         public EmployeesCollection EmployeesData { get; set; }
         public string[,] EmployeesFieldNames { get; set; }
         public string FieldToSort { get; set; }
         public string FieldToSortWithOrder { get; set; }
         public string FieldSortOrder { get; set; }
         public int StartPage { get; set; }
         public int EndPage { get; set; }
         public int CurrentPage { get; set; }
         public int NumberOfPagesToShow { get; set; }
         public int TotalPages { get; set; }
     } 
} 

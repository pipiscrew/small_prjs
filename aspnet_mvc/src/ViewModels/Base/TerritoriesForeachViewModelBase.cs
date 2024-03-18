using north.Models;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for TerritoriesForeachViewModel.  Do not make changes to this class,
     /// instead, put additional code in the TerritoriesForeachViewModel class
     /// </summary>
     public class TerritoriesForeachViewModelBase
     { 
         public TerritoriesCollection TerritoriesData { get; set; }
         public string[,] TerritoriesFieldNames { get; set; }
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

using System;
using north.Models;
using north.Domain;
using north.BusinessObject;

namespace north.ViewModels.Base
{ 
     /// <summary>
     /// Base class for TerritoriesViewModel.  Do not make changes to this class,
     /// instead, put additional code in the TerritoriesViewModel class 
     /// </summary>
     public class TerritoriesViewModelBase
     { 
         public north.Models.TerritoriesModel TerritoriesModel { get; set; }
         public CrudOperation Operation { get; set; }
         public string ViewControllerName { get; set; }
         public string ViewActionName { get; set; }
         public string ViewReturnUrl { get; set; }
         public RegionCollection RegionDropDownListData { get; set; }
     } 
} 

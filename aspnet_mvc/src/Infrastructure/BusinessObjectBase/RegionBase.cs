using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Region.  Do not make changes to this class,
     /// instead, put additional code in the Region class
     /// </summary>
     public class RegionBase : north.Models.RegionModel
     {
         /// <summary>
         /// Gets or sets the related Territories(s) by RegionID
         /// </summary>
         [ScriptIgnore]
         public Lazy<TerritoriesCollection> TerritoriesCollection
         {
             get
             {
                 int value;
                 bool hasValue = Int32.TryParse(RegionID.ToString(), out value);

                 if (hasValue)
                     return new Lazy<TerritoriesCollection>(() => north.BusinessObject.Territories.SelectTerritoriesCollectionByRegionID(value));
                 else
                     return null;
             }
             set { }
         }


         /// <summary>
         /// Constructor
         /// </summary>
         public RegionBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Region SelectByPrimaryKey(int regionID)
         {
             return RegionDataLayer.SelectByPrimaryKey(regionID);
         }

         /// <summary>
         /// Gets the total number of records in the Region table
         /// </summary>
         public static int GetRecordCount()
         {
             return RegionDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Region table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? regionID, string regionDescription)
         {
             return RegionDataLayer.GetRecordCountDynamicWhere(regionID, regionDescription);
         }

         /// <summary>
         /// Selects records as a collection (List) of Region sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static RegionCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return RegionDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Region sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static RegionCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return RegionDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Region sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static RegionCollection SelectSkipAndTakeDynamicWhere(int? regionID, string regionDescription, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(regionID, regionDescription);
             sortByExpression = GetSortExpression(sortByExpression);
             return RegionDataLayer.SelectSkipAndTakeDynamicWhere(regionID, regionDescription, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Region sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static RegionCollection SelectSkipAndTakeDynamicWhere(int? regionID, string regionDescription, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return RegionDataLayer.SelectSkipAndTakeDynamicWhere(regionID, regionDescription, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Region
         /// </summary>
         public static RegionCollection SelectAll()
         {
             return RegionDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Region sorted by the sort expression
         /// </summary>
         public static RegionCollection SelectAll(string sortExpression)
         {
             RegionCollection objRegionCol = RegionDataLayer.SelectAll();
             return SortByExpression(objRegionCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Region.
         /// </summary>
         public static RegionCollection SelectAllDynamicWhere(int? regionID, string regionDescription)
         {
             return RegionDataLayer.SelectAllDynamicWhere(regionID, regionDescription);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Region sorted by the sort expression.
         /// </summary>
         public static RegionCollection SelectAllDynamicWhere(int? regionID, string regionDescription, string sortExpression)
         {
             RegionCollection objRegionCol = RegionDataLayer.SelectAllDynamicWhere(regionID, regionDescription);
             return SortByExpression(objRegionCol, sortExpression);
         }

         /// <summary>
         /// Selects RegionID and RegionDescription columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static RegionCollection SelectRegionDropDownListData()
         {
             return RegionDataLayer.SelectRegionDropDownListData();
         }

         /// <summary>
         /// Sorts the RegionCollection by sort expression
         /// </summary>
         public static RegionCollection SortByExpression(RegionCollection objRegionCol, string sortExpression)
         {
             bool isSortDescending = sortExpression.ToLower().Contains(" desc");

             if (isSortDescending)
             {
                 sortExpression = sortExpression.Replace(" DESC", "");
                 sortExpression = sortExpression.Replace(" desc", "");
             }
             else
             {
                 sortExpression = sortExpression.Replace(" ASC", "");
                 sortExpression = sortExpression.Replace(" asc", "");
             }

             switch (sortExpression)
             {
                 case "RegionID":
                     objRegionCol.Sort(north.BusinessObject.Region.ByRegionID);
                     break;
                 case "RegionDescription":
                     objRegionCol.Sort(north.BusinessObject.Region.ByRegionDescription);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objRegionCol.Reverse();

             return objRegionCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public int Insert()
         {
             Region objRegion = (Region)this;
             return RegionDataLayer.Insert(objRegion);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Region objRegion = (Region)this;
             RegionDataLayer.Update(objRegion);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int regionID)
         {
             RegionDataLayer.Delete(regionID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "RegionID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares RegionID used for sorting
         /// </summary>
         public static Comparison<Region> ByRegionID = delegate(Region x, Region y)
         {
             return x.RegionID.CompareTo(y.RegionID);
         };

         /// <summary>
         /// Compares RegionDescription used for sorting
         /// </summary>
         public static Comparison<Region> ByRegionDescription = delegate(Region x, Region y)
         {
             string value1 = x.RegionDescription ?? String.Empty;
             string value2 = y.RegionDescription ?? String.Empty;
             return value1.CompareTo(value2);
         };

     }
}

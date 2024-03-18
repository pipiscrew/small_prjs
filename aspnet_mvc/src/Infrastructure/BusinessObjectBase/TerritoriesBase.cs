using System;
using System.Data;
using north.DataLayer;
using north.BusinessObject;
using north.Models;
using System.Web.Script.Serialization;

namespace north.BusinessObject.Base
{
     /// <summary>
     /// Base class for Territories.  Do not make changes to this class,
     /// instead, put additional code in the Territories class
     /// </summary>
     public class TerritoriesBase : north.Models.TerritoriesModel
     {
         /// <summary>
         /// Gets or sets the Related Region.  Related to column RegionID
         /// </summary>
         [ScriptIgnore]
         public Lazy<Region> Region1
         {
             get
             {
                 return new Lazy<Region>(() => BusinessObject.Region.SelectByPrimaryKey(RegionID));
             }
             set{ }
         } 


         /// <summary>
         /// Constructor
         /// </summary>
         public TerritoriesBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Territories SelectByPrimaryKey(string territoryID)
         {
             return TerritoriesDataLayer.SelectByPrimaryKey(territoryID);
         }

         /// <summary>
         /// Gets the total number of records in the Territories table
         /// </summary>
         public static int GetRecordCount()
         {
             return TerritoriesDataLayer.GetRecordCount();
         }

         /// <summary>
         /// Gets the total number of records in the Territories table by RegionID
         /// </summary>
         public static int GetRecordCountByRegionID(int regionID)
         {
             return TerritoriesDataLayer.GetRecordCountByRegionID(regionID);
         }

         /// <summary>
         /// Gets the total number of records in the Territories table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(string territoryID, string territoryDescription, int? regionID)
         {
             return TerritoriesDataLayer.GetRecordCountDynamicWhere(territoryID, territoryDescription, regionID);
         }

         /// <summary>
         /// Selects records as a collection (List) of Territories sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTake(int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCount();
             sortByExpression = GetSortExpression(sortByExpression);

             return TerritoriesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Territories sorted by the sortByExpression starting from the startRowIndex.
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTake(int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return TerritoriesDataLayer.SelectSkipAndTake(sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by RegionID as a collection (List) of Territories sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTakeByRegionID(int rows, int startRowIndex, out int totalRowCount, string sortByExpression, int regionID)
         {
             totalRowCount = TerritoriesDataLayer.GetRecordCountByRegionID(regionID);
             sortByExpression = GetSortExpression(sortByExpression);
             return TerritoriesDataLayer.SelectSkipAndTakeByRegionID(sortByExpression, startRowIndex, rows, regionID);
         }

         /// <summary>
         /// Selects records by RegionID as a collection (List) of Territories sorted by the sortByExpression starting from the startRowIndex
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTakeByRegionID(int rows, int startRowIndex, string sortByExpression, int regionID)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return TerritoriesDataLayer.SelectSkipAndTakeByRegionID(sortByExpression, startRowIndex, rows, regionID);
         }

         /// <summary>
         /// Selects records as a collection (List) of Territories sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTakeDynamicWhere(string territoryID, string territoryDescription, int? regionID, int rows, int startRowIndex, out int totalRowCount, string sortByExpression)
         {
             totalRowCount = GetRecordCountDynamicWhere(territoryID, territoryDescription, regionID);
             sortByExpression = GetSortExpression(sortByExpression);
             return TerritoriesDataLayer.SelectSkipAndTakeDynamicWhere(territoryID, territoryDescription, regionID, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records as a collection (List) of Territories sorted by the sortByExpression starting from the startRowIndex, based on the search parameters
         /// </summary>
         public static TerritoriesCollection SelectSkipAndTakeDynamicWhere(string territoryID, string territoryDescription, int? regionID, int rows, int startRowIndex, string sortByExpression)
         {
             sortByExpression = GetSortExpression(sortByExpression);
             return TerritoriesDataLayer.SelectSkipAndTakeDynamicWhere(territoryID, territoryDescription, regionID, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects all records as a collection (List) of Territories
         /// </summary>
         public static TerritoriesCollection SelectAll()
         {
             return TerritoriesDataLayer.SelectAll();
         }

         /// <summary>
         /// Selects all records as a collection (List) of Territories sorted by the sort expression
         /// </summary>
         public static TerritoriesCollection SelectAll(string sortExpression)
         {
             TerritoriesCollection objTerritoriesCol = TerritoriesDataLayer.SelectAll();
             return SortByExpression(objTerritoriesCol, sortExpression);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Territories.
         /// </summary>
         public static TerritoriesCollection SelectAllDynamicWhere(string territoryID, string territoryDescription, int? regionID)
         {
             return TerritoriesDataLayer.SelectAllDynamicWhere(territoryID, territoryDescription, regionID);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Territories sorted by the sort expression.
         /// </summary>
         public static TerritoriesCollection SelectAllDynamicWhere(string territoryID, string territoryDescription, int? regionID, string sortExpression)
         {
             TerritoriesCollection objTerritoriesCol = TerritoriesDataLayer.SelectAllDynamicWhere(territoryID, territoryDescription, regionID);
             return SortByExpression(objTerritoriesCol, sortExpression);
         }

         /// <summary>
         /// Selects all Territories by Region, related to column RegionID
         /// </summary>
         public static TerritoriesCollection SelectTerritoriesCollectionByRegionID(int regionID)
         {
             return TerritoriesDataLayer.SelectTerritoriesCollectionByRegionID(regionID);
         }

         /// <summary>
         /// Selects all Territories by Region, related to column RegionID, sorted by the sort expression
         /// </summary>
         public static TerritoriesCollection SelectTerritoriesCollectionByRegionID(int regionID, string sortExpression)
         {
             TerritoriesCollection objTerritoriesCol = TerritoriesDataLayer.SelectTerritoriesCollectionByRegionID(regionID);
             return SortByExpression(objTerritoriesCol, sortExpression);
         }

         /// <summary>
         /// Selects TerritoryID and TerritoryDescription columns for use with a DropDownList web control, ComboBox, CheckedBoxList, ListView, ListBox, etc
         /// </summary>
         public static TerritoriesCollection SelectTerritoriesDropDownListData()
         {
             return TerritoriesDataLayer.SelectTerritoriesDropDownListData();
         }

         /// <summary>
         /// Sorts the TerritoriesCollection by sort expression
         /// </summary>
         public static TerritoriesCollection SortByExpression(TerritoriesCollection objTerritoriesCol, string sortExpression)
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
                 case "TerritoryID":
                     objTerritoriesCol.Sort(north.BusinessObject.Territories.ByTerritoryID);
                     break;
                 case "TerritoryDescription":
                     objTerritoriesCol.Sort(north.BusinessObject.Territories.ByTerritoryDescription);
                     break;
                 case "RegionID":
                     objTerritoriesCol.Sort(north.BusinessObject.Territories.ByRegionID);
                     break;
                 default:
                     break;
             }

             if (isSortDescending)
                 objTerritoriesCol.Reverse();

             return objTerritoriesCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public string Insert()
         {
             Territories objTerritories = (Territories)this;
             return TerritoriesDataLayer.Insert(objTerritories);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public void Update()
         {
             Territories objTerritories = (Territories)this;
             TerritoriesDataLayer.Update(objTerritories);
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(string territoryID)
         {
             TerritoriesDataLayer.Delete(territoryID);
         }

         private static string GetSortExpression(string sortByExpression)
         {
             if (String.IsNullOrEmpty(sortByExpression) || sortByExpression == " asc")
                 sortByExpression = "TerritoryID";
             else if (sortByExpression.Contains(" asc"))
                 sortByExpression = sortByExpression.Replace(" asc", "");

             return sortByExpression;
         }

         /// <summary>
         /// Compares TerritoryID used for sorting
         /// </summary>
         public static Comparison<Territories> ByTerritoryID = delegate(Territories x, Territories y)
         {
             string value1 = x.TerritoryID ?? String.Empty;
             string value2 = y.TerritoryID ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares TerritoryDescription used for sorting
         /// </summary>
         public static Comparison<Territories> ByTerritoryDescription = delegate(Territories x, Territories y)
         {
             string value1 = x.TerritoryDescription ?? String.Empty;
             string value2 = y.TerritoryDescription ?? String.Empty;
             return value1.CompareTo(value2);
         };

         /// <summary>
         /// Compares RegionID used for sorting
         /// </summary>
         public static Comparison<Territories> ByRegionID = delegate(Territories x, Territories y)
         {
             return x.RegionID.CompareTo(y.RegionID);
         };

     }
}

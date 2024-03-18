using System;
using north.BusinessObject;
using System.Web.UI.WebControls;
// using System.Windows.Forms;    // Note: remove comment when using with windows forms

// for use with web api
using System.Net.Http;
using System.Configuration;
using north.Models;
using Newtonsoft.Json;
using System.Text;

/// <summary>
/// These are data-centric code examples for the Territories table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class TerritoriesExample
{
    private TerritoriesExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //TerritoriesCollection objTerritoriesCol = Territories.SelectAll();

        // ** code using the web api instead of the middle tier **
        TerritoriesCollection objTerritoriesCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/TerritoriesApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objTerritoriesCol = JsonConvert.DeserializeObject<TerritoriesCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objTerritoriesCol.Sort(Territories.ByTerritoryDescription);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objTerritoriesCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objTerritoriesCol;
        grid.DataBind();

        // Example 4:  loop through all the Territories(s)
        foreach (Territories objTerritories in objTerritoriesCol)
        {
            string territoryID = objTerritories.TerritoryID;
            string territoryDescription = objTerritories.TerritoryDescription;
            int regionID = objTerritories.RegionID;

            // optionally get the Region related to RegionID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Region objRegionRelatedToRegionID;

            if (objTerritories.Region.IsValueCreated)
                objRegionRelatedToRegionID = objTerritories.Region.Value;
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        string territoryIDSample = "01581";

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //Territories objTerritories = Territories.SelectByPrimaryKey(territoryIDSample);

        // ** code using the web api instead of the middle tier **
        Territories objTerritories = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/TerritoriesApi/SelectByPrimaryKey/" + territoryIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objTerritories = JsonConvert.DeserializeObject<Territories>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objTerritories != null)
        {
            // if record is found, a record is returned
            string territoryID = objTerritories.TerritoryID;
            string territoryDescription = objTerritories.TerritoryDescription;
            int regionID = objTerritories.RegionID;

            // optionally get the Region related to RegionID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Region objRegionRelatedToRegionID;

            if (objTerritories.Region.IsValueCreated)
                objRegionRelatedToRegionID = objTerritories.Region.Value;
        }
    }

    /// <summary>
    /// Shows how to Select all records by Region, related to column RegionID
    /// </summary>
    private void SelectTerritoriesCollectionByRegionID()
    {
        int regionIDSample = 1;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //TerritoriesCollection objTerritoriesCol = Territories.SelectTerritoriesCollectionByRegionID(regionIDSample);

        // ** code using the web api instead of the middle tier **
        TerritoriesCollection objTerritoriesCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/TerritoriesApi/SelectTerritoriesCollectionByRegionID/" + regionIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objTerritoriesCol = JsonConvert.DeserializeObject<TerritoriesCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objTerritoriesCol.Sort(Territories.ByTerritoryDescription);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objTerritoriesCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objTerritoriesCol;
        grid.DataBind();

        // Example 4:  loop through all the Territories(s)
        foreach (Territories objTerritories in objTerritoriesCol)
        {
            string territoryID = objTerritories.TerritoryID;
            string territoryDescription = objTerritories.TerritoryDescription;
            int regionID = objTerritories.RegionID;

            // optionally get the Region related to RegionID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Region objRegionRelatedToRegionID;

            if (objTerritories.Region.IsValueCreated)
                objRegionRelatedToRegionID = objTerritories.Region.Value;
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the TerritoryID and TerritoryDescription columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectTerritoriesDropDownListData() 
    { 
        TerritoriesCollection objTerritoriesCol = Territories.SelectTerritoriesDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "TerritoryID"; 
        ddl1.DataTextField = "TerritoryDescription"; 
        ddl1.DataSource = objTerritoriesCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Territories objTerritories in objTerritoriesCol) 
        { 
            ddl2.Items.Add(new ListItem(objTerritories.TerritoryDescription, objTerritories.TerritoryID)); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Territories objTerritories in objTerritoriesCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objTerritories.TerritoryDescription, objTerritories.TerritoryID)); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new Territories
        Territories objTerritories = new Territories();

        // assign values you want inserted
        objTerritories.TerritoryID = "01581";
        objTerritories.TerritoryDescription = "Westboro";
        objTerritories.RegionID = 1;

        // finally, insert a new record
        // the insert method returns the newly created primary key
        string newlyCreatedPrimaryKey = objTerritories.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objTerritories);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/TerritoriesApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
        //
        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to Update an existing record by Primary Key
    /// </summary>
    private void Update()
    {
        // first instantiate a new Territories
        Territories objTerritories = new Territories();

        // assign the existing primary key(s)
        // of the record you want updated
        objTerritories.TerritoryID = "01581";

        // assign values you want updated
        objTerritories.TerritoryDescription = "Westboro";
        objTerritories.RegionID = 1;

        // finally, update an existing record
        objTerritories.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objTerritories);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/TerritoriesApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
        //
        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to Delete an existing record by Primary Key
    /// </summary>
    private void Delete()
    {
        // delete a record by primary key
        Territories.Delete("01581");

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/TerritoriesApi/Delete/" + "01581").Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the Territories table
        int totalRecordCount = Territories.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/TerritoriesApi/GetRecordCount/").Result;
        //
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //    }
        //    else
        //    {
        //        var responseBody = response.Content.ReadAsStringAsync().Result;
        //        totalRecordCount = JsonConvert.DeserializeObject<int>(responseBody);
        //    }
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records by RegionID
    /// </summary>
    private void GetRecordCountByRegionID()
    {
        // get the total number of records in the Territories table by RegionID
        // 1 here is just a sample RegionID change the value as you see fit
        int totalRecordCount = Territories.GetRecordCountByRegionID(1);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/TerritoriesApi/GetRecordCountBy/" + "1").Result;
        //
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //    }
        //    else
        //    {
        //        var responseBody = response.Content.ReadAsStringAsync().Result;
        //        totalRecordCount = JsonConvert.DeserializeObject<int>(responseBody);
        //    }
        //}
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index.  The total number of records are also retrieved when using the SelectSkipAndTake() method.
    /// For example, if ther are 200 records (totalRecordCount), take only 10 records (numberOfRecordsToRetrieve), starting from the first index (startRetrievalFromRecordIndex = 0)
    /// The example below uses some variables, here are their definitions:
    /// totalRecordCount - total number of records if you were to retrieve everything
    /// startRetrievalFromRecordIndex - the index to start taking records from. Zero (0) E.g. If you want to skip the first 20 records, then assign 19 here.
    /// numberOfRecordsToRetrieve - take n records starting from the startRetrievalFromRecordIndex
    /// sortBy - to sort in Ascending order by Field Name, just assign just the Field Name, do not pass 'asc'
    /// sortBy - to sort in Descending order by Field Name, use the Field Name, a space and the word 'desc'
    /// </summary>
    private void SelectSkipAndTake()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "TerritoryID";
        //string sortBy = "TerritoryID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        TerritoriesCollection objTerritoriesCol2 = Territories.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objTerritoriesCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Territories(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index by the related Field Name.  The total number of records are also retrieved when using the SelectSkipAndTake() method.
    /// For example, if ther are 200 records (totalRecordCount), take only 10 records (numberOfRecordsToRetrieve), starting from the first index (startRetrievalFromRecordIndex = 0)
    /// The example below uses some variables, here are their definitions:
    /// totalRecordCount - total number of records if you were to retrieve everything
    /// startRetrievalFromRecordIndex - the index to start taking records from. Zero (0) E.g. If you want to skip the first 20 records, then assign 19 here.
    /// numberOfRecordsToRetrieve - take n records starting from the startRetrievalFromRecordIndex
    /// sortBy - to sort in Ascending order by Field Name, just assign just the Field Name, do not pass 'asc'
    /// sortBy - to sort in Descending order by Field Name, use the Field Name, a space and the word 'desc'
    /// </summary>
    private void SelectSkipAndTakeByRegionID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "TerritoryID";
        //string sortBy = "TerritoryID desc";

        // 1. select a specific number of sorted records with a RegionID = 1
        // starting from the index you specify with totalRecordCount
        TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTakeByRegionID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 1);

        // to use objTerritoriesCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Territories(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "TerritoryID";
        //string sortBy = "TerritoryID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        string territoryID = null;
        string territoryDescription = null;
        int? regionID = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        TerritoriesCollection objTerritoriesCol = Territories.SelectSkipAndTakeDynamicWhere(territoryID, territoryDescription, regionID, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objTerritoriesCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Territories(s).  The example above will only loop for 10 items.
    }
}

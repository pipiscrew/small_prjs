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
/// These are data-centric code examples for the CustomerDemographics table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class CustomerDemographicsExample
{
    private CustomerDemographicsExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectAll();

        // ** code using the web api instead of the middle tier **
        CustomerDemographicsCollection objCustomerDemographicsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/CustomerDemographicsApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objCustomerDemographicsCol = JsonConvert.DeserializeObject<CustomerDemographicsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objCustomerDemographicsCol.Sort(CustomerDemographics.ByCustomerDesc);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objCustomerDemographicsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objCustomerDemographicsCol;
        grid.DataBind();

        // Example 4:  loop through all the CustomerDemographics(s)
        foreach (CustomerDemographics objCustomerDemographics in objCustomerDemographicsCol)
        {
            string customerTypeID = objCustomerDemographics.CustomerTypeID;
            string customerDesc = objCustomerDemographics.CustomerDesc;
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        string customerTypeIDSample = "abc";

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //CustomerDemographics objCustomerDemographics = CustomerDemographics.SelectByPrimaryKey(customerTypeIDSample);

        // ** code using the web api instead of the middle tier **
        CustomerDemographics objCustomerDemographics = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/CustomerDemographicsApi/SelectByPrimaryKey/" + customerTypeIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objCustomerDemographics = JsonConvert.DeserializeObject<CustomerDemographics>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objCustomerDemographics != null)
        {
            // if record is found, a record is returned
            string customerTypeID = objCustomerDemographics.CustomerTypeID;
            string customerDesc = objCustomerDemographics.CustomerDesc;
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the CustomerTypeID and CustomerDesc columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectCustomerDemographicsDropDownListData() 
    { 
        CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectCustomerDemographicsDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "CustomerTypeID"; 
        ddl1.DataTextField = "CustomerDesc"; 
        ddl1.DataSource = objCustomerDemographicsCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (CustomerDemographics objCustomerDemographics in objCustomerDemographicsCol) 
        { 
            ddl2.Items.Add(new ListItem(objCustomerDemographics.CustomerDesc, objCustomerDemographics.CustomerTypeID)); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (CustomerDemographics objCustomerDemographics in objCustomerDemographicsCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objCustomerDemographics.CustomerDesc, objCustomerDemographics.CustomerTypeID)); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new CustomerDemographics
        CustomerDemographics objCustomerDemographics = new CustomerDemographics();

        // assign values you want inserted
        objCustomerDemographics.CustomerTypeID = "abc";
        objCustomerDemographics.CustomerDesc = "abc";

        // finally, insert a new record
        // the insert method returns the newly created primary key
        string newlyCreatedPrimaryKey = objCustomerDemographics.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objCustomerDemographics);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/CustomerDemographicsApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new CustomerDemographics
        CustomerDemographics objCustomerDemographics = new CustomerDemographics();

        // assign the existing primary key(s)
        // of the record you want updated
        objCustomerDemographics.CustomerTypeID = "abc";

        // assign values you want updated
        objCustomerDemographics.CustomerDesc = "abc";

        // finally, update an existing record
        objCustomerDemographics.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objCustomerDemographics);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/CustomerDemographicsApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        CustomerDemographics.Delete("abc");

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/CustomerDemographicsApi/Delete/" + "abc").Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the CustomerDemographics table
        int totalRecordCount = CustomerDemographics.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/CustomerDemographicsApi/GetRecordCount/").Result;
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
        string sortBy = "CustomerTypeID";
        //string sortBy = "CustomerTypeID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        CustomerDemographicsCollection objCustomerDemographicsCol2 = CustomerDemographics.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objCustomerDemographicsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the CustomerDemographics(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "CustomerTypeID";
        //string sortBy = "CustomerTypeID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        string customerTypeID = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        CustomerDemographicsCollection objCustomerDemographicsCol = CustomerDemographics.SelectSkipAndTakeDynamicWhere(customerTypeID, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objCustomerDemographicsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the CustomerDemographics(s).  The example above will only loop for 10 items.
    }
}

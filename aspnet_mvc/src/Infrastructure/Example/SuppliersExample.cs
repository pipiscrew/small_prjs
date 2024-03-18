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
/// These are data-centric code examples for the Suppliers table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class SuppliersExample
{
    private SuppliersExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //SuppliersCollection objSuppliersCol = Suppliers.SelectAll();

        // ** code using the web api instead of the middle tier **
        SuppliersCollection objSuppliersCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/SuppliersApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objSuppliersCol = JsonConvert.DeserializeObject<SuppliersCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objSuppliersCol.Sort(Suppliers.ByCompanyName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objSuppliersCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objSuppliersCol;
        grid.DataBind();

        // Example 4:  loop through all the Suppliers(s)
        foreach (Suppliers objSuppliers in objSuppliersCol)
        {
            int supplierID = objSuppliers.SupplierID;
            string companyName = objSuppliers.CompanyName;
            string contactName = objSuppliers.ContactName;
            string contactTitle = objSuppliers.ContactTitle;
            string address = objSuppliers.Address;
            string city = objSuppliers.City;
            string region1 = objSuppliers.Region1;
            string postalCode = objSuppliers.PostalCode;
            string country = objSuppliers.Country;
            string phone = objSuppliers.Phone;
            string fax = objSuppliers.Fax;
            string homePage = objSuppliers.HomePage;
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        int supplierIDSample = 1;

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //Suppliers objSuppliers = Suppliers.SelectByPrimaryKey(supplierIDSample);

        // ** code using the web api instead of the middle tier **
        Suppliers objSuppliers = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/SuppliersApi/SelectByPrimaryKey/" + supplierIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objSuppliers = JsonConvert.DeserializeObject<Suppliers>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objSuppliers != null)
        {
            // if record is found, a record is returned
            int supplierID = objSuppliers.SupplierID;
            string companyName = objSuppliers.CompanyName;
            string contactName = objSuppliers.ContactName;
            string contactTitle = objSuppliers.ContactTitle;
            string address = objSuppliers.Address;
            string city = objSuppliers.City;
            string region1 = objSuppliers.Region1;
            string postalCode = objSuppliers.PostalCode;
            string country = objSuppliers.Country;
            string phone = objSuppliers.Phone;
            string fax = objSuppliers.Fax;
            string homePage = objSuppliers.HomePage;
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the SupplierID and CompanyName columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectSuppliersDropDownListData() 
    { 
        SuppliersCollection objSuppliersCol = Suppliers.SelectSuppliersDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "SupplierID"; 
        ddl1.DataTextField = "CompanyName"; 
        ddl1.DataSource = objSuppliersCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Suppliers objSuppliers in objSuppliersCol) 
        { 
            ddl2.Items.Add(new ListItem(objSuppliers.CompanyName, objSuppliers.SupplierID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Suppliers objSuppliers in objSuppliersCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objSuppliers.CompanyName, objSuppliers.SupplierID.ToString())); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new Suppliers
        Suppliers objSuppliers = new Suppliers();

        // assign values you want inserted
        objSuppliers.CompanyName = "Exotic Liquids";
        objSuppliers.ContactName = "Charlotte Cooper";
        objSuppliers.ContactTitle = "Purchasing Manager";
        objSuppliers.Address = "49 Gilbert St.";
        objSuppliers.City = "London";
        objSuppliers.Region1 = "abc";
        objSuppliers.PostalCode = "EC1 4SD";
        objSuppliers.Country = "UK";
        objSuppliers.Phone = "(171) 555-2222";
        objSuppliers.Fax = "abc";
        objSuppliers.HomePage = "abc";

        // finally, insert a new record
        // the insert method returns the newly created primary key
        int newlyCreatedPrimaryKey = objSuppliers.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objSuppliers);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/SuppliersApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new Suppliers
        Suppliers objSuppliers = new Suppliers();

        // assign the existing primary key(s)
        // of the record you want updated
        objSuppliers.SupplierID = 1;

        // assign values you want updated
        objSuppliers.CompanyName = "Exotic Liquids";
        objSuppliers.ContactName = "Charlotte Cooper";
        objSuppliers.ContactTitle = "Purchasing Manager";
        objSuppliers.Address = "49 Gilbert St.";
        objSuppliers.City = "London";
        objSuppliers.Region1 = "abc";
        objSuppliers.PostalCode = "EC1 4SD";
        objSuppliers.Country = "UK";
        objSuppliers.Phone = "(171) 555-2222";
        objSuppliers.Fax = "abc";
        objSuppliers.HomePage = "abc";

        // finally, update an existing record
        objSuppliers.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objSuppliers);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/SuppliersApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        Suppliers.Delete(30);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/SuppliersApi/Delete/" + 30).Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the Suppliers table
        int totalRecordCount = Suppliers.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/SuppliersApi/GetRecordCount/").Result;
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
        string sortBy = "SupplierID";
        //string sortBy = "SupplierID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        SuppliersCollection objSuppliersCol = Suppliers.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        SuppliersCollection objSuppliersCol2 = Suppliers.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objSuppliersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Suppliers(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "SupplierID";
        //string sortBy = "SupplierID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        int? supplierID = null;
        string companyName = null;
        string contactName = null;
        string contactTitle = null;
        string address = null;
        string city = null;
        string region1 = null;
        string postalCode = null;
        string country = null;
        string phone = null;
        string fax = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        SuppliersCollection objSuppliersCol = Suppliers.SelectSkipAndTakeDynamicWhere(supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objSuppliersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Suppliers(s).  The example above will only loop for 10 items.
    }
}

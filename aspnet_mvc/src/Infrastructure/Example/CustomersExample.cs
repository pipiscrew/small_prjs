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
/// These are data-centric code examples for the Customers table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class CustomersExample
{
    private CustomersExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //CustomersCollection objCustomersCol = Customers.SelectAll();

        // ** code using the web api instead of the middle tier **
        CustomersCollection objCustomersCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/CustomersApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objCustomersCol = JsonConvert.DeserializeObject<CustomersCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objCustomersCol.Sort(Customers.ByCompanyName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objCustomersCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objCustomersCol;
        grid.DataBind();

        // Example 4:  loop through all the Customers(s)
        foreach (Customers objCustomers in objCustomersCol)
        {
            string customerID = objCustomers.CustomerID;
            string companyName = objCustomers.CompanyName;
            string contactName = objCustomers.ContactName;
            string contactTitle = objCustomers.ContactTitle;
            string address = objCustomers.Address;
            string city = objCustomers.City;
            string region1 = objCustomers.Region1;
            string postalCode = objCustomers.PostalCode;
            string country = objCustomers.Country;
            string phone = objCustomers.Phone;
            string fax = objCustomers.Fax;
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        string customerIDSample = "ALFKI";

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //Customers objCustomers = Customers.SelectByPrimaryKey(customerIDSample);

        // ** code using the web api instead of the middle tier **
        Customers objCustomers = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/CustomersApi/SelectByPrimaryKey/" + customerIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objCustomers = JsonConvert.DeserializeObject<Customers>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objCustomers != null)
        {
            // if record is found, a record is returned
            string customerID = objCustomers.CustomerID;
            string companyName = objCustomers.CompanyName;
            string contactName = objCustomers.ContactName;
            string contactTitle = objCustomers.ContactTitle;
            string address = objCustomers.Address;
            string city = objCustomers.City;
            string region1 = objCustomers.Region1;
            string postalCode = objCustomers.PostalCode;
            string country = objCustomers.Country;
            string phone = objCustomers.Phone;
            string fax = objCustomers.Fax;
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the CustomerID and CompanyName columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectCustomersDropDownListData() 
    { 
        CustomersCollection objCustomersCol = Customers.SelectCustomersDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "CustomerID"; 
        ddl1.DataTextField = "CompanyName"; 
        ddl1.DataSource = objCustomersCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Customers objCustomers in objCustomersCol) 
        { 
            ddl2.Items.Add(new ListItem(objCustomers.CompanyName, objCustomers.CustomerID)); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Customers objCustomers in objCustomersCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objCustomers.CompanyName, objCustomers.CustomerID)); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new Customers
        Customers objCustomers = new Customers();

        // assign values you want inserted
        objCustomers.CustomerID = "ALFKI";
        objCustomers.CompanyName = "Alfreds Futterkiste";
        objCustomers.ContactName = "Maria Anders";
        objCustomers.ContactTitle = "Sales Representative";
        objCustomers.Address = "Obere Str. 57";
        objCustomers.City = "Berlin";
        objCustomers.Region1 = "abc";
        objCustomers.PostalCode = "12209";
        objCustomers.Country = "Germany";
        objCustomers.Phone = "030-0074321";
        objCustomers.Fax = "030-0076545";

        // finally, insert a new record
        // the insert method returns the newly created primary key
        string newlyCreatedPrimaryKey = objCustomers.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objCustomers);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/CustomersApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new Customers
        Customers objCustomers = new Customers();

        // assign the existing primary key(s)
        // of the record you want updated
        objCustomers.CustomerID = "ALFKI";

        // assign values you want updated
        objCustomers.CompanyName = "Alfreds Futterkiste";
        objCustomers.ContactName = "Maria Anders";
        objCustomers.ContactTitle = "Sales Representative";
        objCustomers.Address = "Obere Str. 57";
        objCustomers.City = "Berlin";
        objCustomers.Region1 = "abc";
        objCustomers.PostalCode = "12209";
        objCustomers.Country = "Germany";
        objCustomers.Phone = "030-0074321";
        objCustomers.Fax = "030-0076545";

        // finally, update an existing record
        objCustomers.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objCustomers);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/CustomersApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        Customers.Delete("ALFKI");

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/CustomersApi/Delete/" + "ALFKI").Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the Customers table
        int totalRecordCount = Customers.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/CustomersApi/GetRecordCount/").Result;
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
        string sortBy = "CustomerID";
        //string sortBy = "CustomerID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        CustomersCollection objCustomersCol = Customers.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        CustomersCollection objCustomersCol2 = Customers.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objCustomersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Customers(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "CustomerID";
        //string sortBy = "CustomerID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        string customerID = null;
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
        CustomersCollection objCustomersCol = Customers.SelectSkipAndTakeDynamicWhere(customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objCustomersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Customers(s).  The example above will only loop for 10 items.
    }
}

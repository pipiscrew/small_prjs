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
/// These are data-centric code examples for the Employees table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class EmployeesExample
{
    private EmployeesExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //EmployeesCollection objEmployeesCol = Employees.SelectAll();

        // ** code using the web api instead of the middle tier **
        EmployeesCollection objEmployeesCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objEmployeesCol = JsonConvert.DeserializeObject<EmployeesCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objEmployeesCol.Sort(Employees.ByLastName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objEmployeesCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objEmployeesCol;
        grid.DataBind();

        // Example 4:  loop through all the Employees(s)
        foreach (Employees objEmployees in objEmployeesCol)
        {
            int employeeID = objEmployees.EmployeeID;
            string lastName = objEmployees.LastName;
            string firstName = objEmployees.FirstName;
            string title = objEmployees.Title;
            string titleOfCourtesy = objEmployees.TitleOfCourtesy;
            DateTime? birthDate = objEmployees.BirthDate;
            DateTime? hireDate = objEmployees.HireDate;
            string address = objEmployees.Address;
            string city = objEmployees.City;
            string region1 = objEmployees.Region1;
            string postalCode = objEmployees.PostalCode;
            string country = objEmployees.Country;
            string homePhone = objEmployees.HomePhone;
            string extension = objEmployees.Extension;
            string notes = objEmployees.Notes;
            int? reportsTo = objEmployees.ReportsTo;
            string photoPath = objEmployees.PhotoPath;

            // optionally get the Employees related to ReportsTo.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objEmployees.ReportsTo != null)
            {
                Employees objEmployeesRelatedToReportsTo;

                if (objEmployees.EmployeesReportsTo.IsValueCreated)
                    objEmployeesRelatedToReportsTo = objEmployees.EmployeesReportsTo.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        int employeeIDSample = 1;

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //Employees objEmployees = Employees.SelectByPrimaryKey(employeeIDSample);

        // ** code using the web api instead of the middle tier **
        Employees objEmployees = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectByPrimaryKey/" + employeeIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objEmployees = JsonConvert.DeserializeObject<Employees>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objEmployees != null)
        {
            // if record is found, a record is returned
            int employeeID = objEmployees.EmployeeID;
            string lastName = objEmployees.LastName;
            string firstName = objEmployees.FirstName;
            string title = objEmployees.Title;
            string titleOfCourtesy = objEmployees.TitleOfCourtesy;
            DateTime? birthDate = objEmployees.BirthDate;
            DateTime? hireDate = objEmployees.HireDate;
            string address = objEmployees.Address;
            string city = objEmployees.City;
            string region1 = objEmployees.Region1;
            string postalCode = objEmployees.PostalCode;
            string country = objEmployees.Country;
            string homePhone = objEmployees.HomePhone;
            string extension = objEmployees.Extension;
            string notes = objEmployees.Notes;
            int? reportsTo = objEmployees.ReportsTo;
            string photoPath = objEmployees.PhotoPath;

            // optionally get the Employees related to ReportsTo.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objEmployees.ReportsTo != null)
            {
                Employees objEmployeesRelatedToReportsTo;

                if (objEmployees.EmployeesReportsTo.IsValueCreated)
                    objEmployeesRelatedToReportsTo = objEmployees.EmployeesReportsTo.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select all records by Employees, related to column ReportsTo
    /// </summary>
    private void SelectEmployeesCollectionByReportsTo()
    {
        int reportsToSample = 2;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //EmployeesCollection objEmployeesCol = Employees.SelectEmployeesCollectionByReportsTo(reportsToSample);

        // ** code using the web api instead of the middle tier **
        EmployeesCollection objEmployeesCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectEmployeesCollectionByReportsTo/" + reportsToSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objEmployeesCol = JsonConvert.DeserializeObject<EmployeesCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objEmployeesCol.Sort(Employees.ByLastName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objEmployeesCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objEmployeesCol;
        grid.DataBind();

        // Example 4:  loop through all the Employees(s)
        foreach (Employees objEmployees in objEmployeesCol)
        {
            int employeeID = objEmployees.EmployeeID;
            string lastName = objEmployees.LastName;
            string firstName = objEmployees.FirstName;
            string title = objEmployees.Title;
            string titleOfCourtesy = objEmployees.TitleOfCourtesy;
            DateTime? birthDate = objEmployees.BirthDate;
            DateTime? hireDate = objEmployees.HireDate;
            string address = objEmployees.Address;
            string city = objEmployees.City;
            string region1 = objEmployees.Region1;
            string postalCode = objEmployees.PostalCode;
            string country = objEmployees.Country;
            string homePhone = objEmployees.HomePhone;
            string extension = objEmployees.Extension;
            string notes = objEmployees.Notes;
            int? reportsTo = objEmployees.ReportsTo;
            string photoPath = objEmployees.PhotoPath;

            // optionally get the Employees related to ReportsTo.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objEmployees.ReportsTo != null)
            {
                Employees objEmployeesRelatedToReportsTo;

                if (objEmployees.EmployeesReportsTo.IsValueCreated)
                    objEmployeesRelatedToReportsTo = objEmployees.EmployeesReportsTo.Value;
            }
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the EmployeeID and LastName columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectEmployeesDropDownListData() 
    { 
        EmployeesCollection objEmployeesCol = Employees.SelectEmployeesDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "EmployeeID"; 
        ddl1.DataTextField = "LastName"; 
        ddl1.DataSource = objEmployeesCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Employees objEmployees in objEmployeesCol) 
        { 
            ddl2.Items.Add(new ListItem(objEmployees.LastName, objEmployees.EmployeeID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Employees objEmployees in objEmployeesCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objEmployees.LastName, objEmployees.EmployeeID.ToString())); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new Employees
        Employees objEmployees = new Employees();

        // assign values you want inserted
        objEmployees.LastName = "Davolio";
        objEmployees.FirstName = "Nancy";
        objEmployees.Title = "Sales Representative";
        objEmployees.TitleOfCourtesy = "Ms.";
        objEmployees.BirthDate = DateTime.Now;
        objEmployees.HireDate = DateTime.Now;
        objEmployees.Address = "507 - 20th Ave. E. Apt. 2A";
        objEmployees.City = "Seattle";
        objEmployees.Region1 = "WA";
        objEmployees.PostalCode = "98122";
        objEmployees.Country = "USA";
        objEmployees.HomePhone = "(206) 555-9857";
        objEmployees.Extension = "5467";
        objEmployees.Notes = "Education includes a BA in psychology from Colorado State University in 1970.  She also completed \"The Art of the Cold Call.\"  Nancy is a member of Toastmasters International.";
        objEmployees.ReportsTo = 2;
        objEmployees.PhotoPath = "http://accweb/emmployees/davolio.bmp";

        // finally, insert a new record
        // the insert method returns the newly created primary key
        int newlyCreatedPrimaryKey = objEmployees.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objEmployees);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/EmployeesApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new Employees
        Employees objEmployees = new Employees();

        // assign the existing primary key(s)
        // of the record you want updated
        objEmployees.EmployeeID = 1;

        // assign values you want updated
        objEmployees.LastName = "Davolio";
        objEmployees.FirstName = "Nancy";
        objEmployees.Title = "Sales Representative";
        objEmployees.TitleOfCourtesy = "Ms.";
        objEmployees.BirthDate = DateTime.Now;
        objEmployees.HireDate = DateTime.Now;
        objEmployees.Address = "507 - 20th Ave. E. Apt. 2A";
        objEmployees.City = "Seattle";
        objEmployees.Region1 = "WA";
        objEmployees.PostalCode = "98122";
        objEmployees.Country = "USA";
        objEmployees.HomePhone = "(206) 555-9857";
        objEmployees.Extension = "5467";
        objEmployees.Notes = "Education includes a BA in psychology from Colorado State University in 1970.  She also completed \"The Art of the Cold Call.\"  Nancy is a member of Toastmasters International.";
        objEmployees.ReportsTo = 2;
        objEmployees.PhotoPath = "http://accweb/emmployees/davolio.bmp";

        // finally, update an existing record
        objEmployees.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objEmployees);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/EmployeesApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        Employees.Delete(10);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/EmployeesApi/Delete/" + 10).Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the Employees table
        int totalRecordCount = Employees.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/EmployeesApi/GetRecordCount/").Result;
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
    /// Shows how to get the total number of records by ReportsTo
    /// </summary>
    private void GetRecordCountByReportsTo()
    {
        // get the total number of records in the Employees table by ReportsTo
        // 2 here is just a sample ReportsTo change the value as you see fit
        int totalRecordCount = Employees.GetRecordCountByReportsTo(2);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/EmployeesApi/GetRecordCountBy/" + "2").Result;
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
        string sortBy = "EmployeeID";
        //string sortBy = "EmployeeID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        EmployeesCollection objEmployeesCol2 = Employees.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objEmployeesCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Employees(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByReportsTo()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "EmployeeID";
        //string sortBy = "EmployeeID desc";

        // 1. select a specific number of sorted records with a ReportsTo = 2
        // starting from the index you specify with totalRecordCount
        EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTakeByReportsTo(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 2);

        // to use objEmployeesCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Employees(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "EmployeeID";
        //string sortBy = "EmployeeID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        int? employeeID = null;
        string lastName = null;
        string firstName = null;
        string title = null;
        string titleOfCourtesy = null;
        DateTime? birthDate = null;
        DateTime? hireDate = null;
        string address = null;
        string city = null;
        string region1 = null;
        string postalCode = null;
        string country = null;
        string homePhone = null;
        string extension = null;
        int? reportsTo = null;
        string photoPath = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        EmployeesCollection objEmployeesCol = Employees.SelectSkipAndTakeDynamicWhere(employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objEmployeesCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Employees(s).  The example above will only loop for 10 items.
    }
}

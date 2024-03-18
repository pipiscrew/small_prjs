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
/// These are data-centric code examples for the Orders table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class OrdersExample
{
    private OrdersExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrdersCollection objOrdersCol = Orders.SelectAll();

        // ** code using the web api instead of the middle tier **
        OrdersCollection objOrdersCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrdersCol = JsonConvert.DeserializeObject<OrdersCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrdersCol.Sort(Orders.ByCustomerID);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrdersCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrdersCol;
        grid.DataBind();

        // Example 4:  loop through all the Orders(s)
        foreach (Orders objOrders in objOrdersCol)
        {
            int orderID = objOrders.OrderID;
            string customerID = objOrders.CustomerID;
            int? employeeID = objOrders.EmployeeID;
            DateTime? orderDate = objOrders.OrderDate;
            DateTime? requiredDate = objOrders.RequiredDate;
            DateTime? shippedDate = objOrders.ShippedDate;
            int? shipVia = objOrders.ShipVia;
            decimal? freight = objOrders.Freight;
            string shipName = objOrders.ShipName;
            string shipAddress = objOrders.ShipAddress;
            string shipCity = objOrders.ShipCity;
            string shipRegion = objOrders.ShipRegion;
            string shipPostalCode = objOrders.ShipPostalCode;
            string shipCountry = objOrders.ShipCountry;

            // optionally get the Customers related to CustomerID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (!String.IsNullOrEmpty(objOrders.CustomerID))
            {
                Customers objCustomersRelatedToCustomerID;

                if (objOrders.Customers.IsValueCreated)
                    objCustomersRelatedToCustomerID = objOrders.Customers.Value;
            }

            // optionally get the Employees related to EmployeeID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.EmployeeID != null)
            {
                Employees objEmployeesRelatedToEmployeeID;

                if (objOrders.Employees.IsValueCreated)
                    objEmployeesRelatedToEmployeeID = objOrders.Employees.Value;
            }

            // optionally get the Shippers related to ShipVia.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.ShipVia != null)
            {
                Shippers objShippersRelatedToShipVia;

                if (objOrders.Shippers.IsValueCreated)
                    objShippersRelatedToShipVia = objOrders.Shippers.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        int orderIDSample = 10248;

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //Orders objOrders = Orders.SelectByPrimaryKey(orderIDSample);

        // ** code using the web api instead of the middle tier **
        Orders objOrders = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectByPrimaryKey/" + orderIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrders = JsonConvert.DeserializeObject<Orders>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objOrders != null)
        {
            // if record is found, a record is returned
            int orderID = objOrders.OrderID;
            string customerID = objOrders.CustomerID;
            int? employeeID = objOrders.EmployeeID;
            DateTime? orderDate = objOrders.OrderDate;
            DateTime? requiredDate = objOrders.RequiredDate;
            DateTime? shippedDate = objOrders.ShippedDate;
            int? shipVia = objOrders.ShipVia;
            decimal? freight = objOrders.Freight;
            string shipName = objOrders.ShipName;
            string shipAddress = objOrders.ShipAddress;
            string shipCity = objOrders.ShipCity;
            string shipRegion = objOrders.ShipRegion;
            string shipPostalCode = objOrders.ShipPostalCode;
            string shipCountry = objOrders.ShipCountry;

            // optionally get the Customers related to CustomerID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (!String.IsNullOrEmpty(objOrders.CustomerID))
            {
                Customers objCustomersRelatedToCustomerID;

                if (objOrders.Customers.IsValueCreated)
                    objCustomersRelatedToCustomerID = objOrders.Customers.Value;
            }

            // optionally get the Employees related to EmployeeID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.EmployeeID != null)
            {
                Employees objEmployeesRelatedToEmployeeID;

                if (objOrders.Employees.IsValueCreated)
                    objEmployeesRelatedToEmployeeID = objOrders.Employees.Value;
            }

            // optionally get the Shippers related to ShipVia.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.ShipVia != null)
            {
                Shippers objShippersRelatedToShipVia;

                if (objOrders.Shippers.IsValueCreated)
                    objShippersRelatedToShipVia = objOrders.Shippers.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select all records by Customers, related to column CustomerID
    /// </summary>
    private void SelectOrdersCollectionByCustomerID()
    {
        string customerIDSample = "VINET";

        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrdersCollection objOrdersCol = Orders.SelectOrdersCollectionByCustomerID(customerIDSample);

        // ** code using the web api instead of the middle tier **
        OrdersCollection objOrdersCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectOrdersCollectionByCustomerID/" + customerIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrdersCol = JsonConvert.DeserializeObject<OrdersCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrdersCol.Sort(Orders.ByCustomerID);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrdersCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrdersCol;
        grid.DataBind();

        // Example 4:  loop through all the Orders(s)
        foreach (Orders objOrders in objOrdersCol)
        {
            int orderID = objOrders.OrderID;
            string customerID = objOrders.CustomerID;
            int? employeeID = objOrders.EmployeeID;
            DateTime? orderDate = objOrders.OrderDate;
            DateTime? requiredDate = objOrders.RequiredDate;
            DateTime? shippedDate = objOrders.ShippedDate;
            int? shipVia = objOrders.ShipVia;
            decimal? freight = objOrders.Freight;
            string shipName = objOrders.ShipName;
            string shipAddress = objOrders.ShipAddress;
            string shipCity = objOrders.ShipCity;
            string shipRegion = objOrders.ShipRegion;
            string shipPostalCode = objOrders.ShipPostalCode;
            string shipCountry = objOrders.ShipCountry;

            // optionally get the Customers related to CustomerID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (!String.IsNullOrEmpty(objOrders.CustomerID))
            {
                Customers objCustomersRelatedToCustomerID;

                if (objOrders.Customers.IsValueCreated)
                    objCustomersRelatedToCustomerID = objOrders.Customers.Value;
            }

            // optionally get the Employees related to EmployeeID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.EmployeeID != null)
            {
                Employees objEmployeesRelatedToEmployeeID;

                if (objOrders.Employees.IsValueCreated)
                    objEmployeesRelatedToEmployeeID = objOrders.Employees.Value;
            }

            // optionally get the Shippers related to ShipVia.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.ShipVia != null)
            {
                Shippers objShippersRelatedToShipVia;

                if (objOrders.Shippers.IsValueCreated)
                    objShippersRelatedToShipVia = objOrders.Shippers.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select all records by Employees, related to column EmployeeID
    /// </summary>
    private void SelectOrdersCollectionByEmployeeID()
    {
        int employeeIDSample = 5;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrdersCollection objOrdersCol = Orders.SelectOrdersCollectionByEmployeeID(employeeIDSample);

        // ** code using the web api instead of the middle tier **
        OrdersCollection objOrdersCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectOrdersCollectionByEmployeeID/" + employeeIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrdersCol = JsonConvert.DeserializeObject<OrdersCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrdersCol.Sort(Orders.ByCustomerID);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrdersCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrdersCol;
        grid.DataBind();

        // Example 4:  loop through all the Orders(s)
        foreach (Orders objOrders in objOrdersCol)
        {
            int orderID = objOrders.OrderID;
            string customerID = objOrders.CustomerID;
            int? employeeID = objOrders.EmployeeID;
            DateTime? orderDate = objOrders.OrderDate;
            DateTime? requiredDate = objOrders.RequiredDate;
            DateTime? shippedDate = objOrders.ShippedDate;
            int? shipVia = objOrders.ShipVia;
            decimal? freight = objOrders.Freight;
            string shipName = objOrders.ShipName;
            string shipAddress = objOrders.ShipAddress;
            string shipCity = objOrders.ShipCity;
            string shipRegion = objOrders.ShipRegion;
            string shipPostalCode = objOrders.ShipPostalCode;
            string shipCountry = objOrders.ShipCountry;

            // optionally get the Customers related to CustomerID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (!String.IsNullOrEmpty(objOrders.CustomerID))
            {
                Customers objCustomersRelatedToCustomerID;

                if (objOrders.Customers.IsValueCreated)
                    objCustomersRelatedToCustomerID = objOrders.Customers.Value;
            }

            // optionally get the Employees related to EmployeeID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.EmployeeID != null)
            {
                Employees objEmployeesRelatedToEmployeeID;

                if (objOrders.Employees.IsValueCreated)
                    objEmployeesRelatedToEmployeeID = objOrders.Employees.Value;
            }

            // optionally get the Shippers related to ShipVia.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.ShipVia != null)
            {
                Shippers objShippersRelatedToShipVia;

                if (objOrders.Shippers.IsValueCreated)
                    objShippersRelatedToShipVia = objOrders.Shippers.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select all records by Shippers, related to column ShipVia
    /// </summary>
    private void SelectOrdersCollectionByShipVia()
    {
        int shipViaSample = 3;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrdersCollection objOrdersCol = Orders.SelectOrdersCollectionByShipVia(shipViaSample);

        // ** code using the web api instead of the middle tier **
        OrdersCollection objOrdersCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectOrdersCollectionByShipVia/" + shipViaSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrdersCol = JsonConvert.DeserializeObject<OrdersCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrdersCol.Sort(Orders.ByCustomerID);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrdersCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrdersCol;
        grid.DataBind();

        // Example 4:  loop through all the Orders(s)
        foreach (Orders objOrders in objOrdersCol)
        {
            int orderID = objOrders.OrderID;
            string customerID = objOrders.CustomerID;
            int? employeeID = objOrders.EmployeeID;
            DateTime? orderDate = objOrders.OrderDate;
            DateTime? requiredDate = objOrders.RequiredDate;
            DateTime? shippedDate = objOrders.ShippedDate;
            int? shipVia = objOrders.ShipVia;
            decimal? freight = objOrders.Freight;
            string shipName = objOrders.ShipName;
            string shipAddress = objOrders.ShipAddress;
            string shipCity = objOrders.ShipCity;
            string shipRegion = objOrders.ShipRegion;
            string shipPostalCode = objOrders.ShipPostalCode;
            string shipCountry = objOrders.ShipCountry;

            // optionally get the Customers related to CustomerID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (!String.IsNullOrEmpty(objOrders.CustomerID))
            {
                Customers objCustomersRelatedToCustomerID;

                if (objOrders.Customers.IsValueCreated)
                    objCustomersRelatedToCustomerID = objOrders.Customers.Value;
            }

            // optionally get the Employees related to EmployeeID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.EmployeeID != null)
            {
                Employees objEmployeesRelatedToEmployeeID;

                if (objOrders.Employees.IsValueCreated)
                    objEmployeesRelatedToEmployeeID = objOrders.Employees.Value;
            }

            // optionally get the Shippers related to ShipVia.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objOrders.ShipVia != null)
            {
                Shippers objShippersRelatedToShipVia;

                if (objOrders.Shippers.IsValueCreated)
                    objShippersRelatedToShipVia = objOrders.Shippers.Value;
            }
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the OrderID and ShipName columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectOrdersDropDownListData() 
    { 
        OrdersCollection objOrdersCol = Orders.SelectOrdersDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "OrderID"; 
        ddl1.DataTextField = "ShipName"; 
        ddl1.DataSource = objOrdersCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Orders objOrders in objOrdersCol) 
        { 
            ddl2.Items.Add(new ListItem(objOrders.ShipName, objOrders.OrderID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Orders objOrders in objOrdersCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objOrders.ShipName, objOrders.OrderID.ToString())); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new Orders
        Orders objOrders = new Orders();

        // assign values you want inserted
        objOrders.CustomerID = "VINET";
        objOrders.EmployeeID = 5;
        objOrders.OrderDate = DateTime.Now;
        objOrders.RequiredDate = DateTime.Now;
        objOrders.ShippedDate = DateTime.Now;
        objOrders.ShipVia = 3;
        objOrders.Freight = Convert.ToDecimal(32.3800);
        objOrders.ShipName = "Vins et alcools Chevalier";
        objOrders.ShipAddress = "59 rue de l'Abbaye";
        objOrders.ShipCity = "Reims";
        objOrders.ShipRegion = "abc";
        objOrders.ShipPostalCode = "51100";
        objOrders.ShipCountry = "France";

        // finally, insert a new record
        // the insert method returns the newly created primary key
        int newlyCreatedPrimaryKey = objOrders.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objOrders);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/OrdersApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new Orders
        Orders objOrders = new Orders();

        // assign the existing primary key(s)
        // of the record you want updated
        objOrders.OrderID = 10248;

        // assign values you want updated
        objOrders.CustomerID = "VINET";
        objOrders.EmployeeID = 5;
        objOrders.OrderDate = DateTime.Now;
        objOrders.RequiredDate = DateTime.Now;
        objOrders.ShippedDate = DateTime.Now;
        objOrders.ShipVia = 3;
        objOrders.Freight = Convert.ToDecimal(32.3800);
        objOrders.ShipName = "Vins et alcools Chevalier";
        objOrders.ShipAddress = "59 rue de l'Abbaye";
        objOrders.ShipCity = "Reims";
        objOrders.ShipRegion = "abc";
        objOrders.ShipPostalCode = "51100";
        objOrders.ShipCountry = "France";

        // finally, update an existing record
        objOrders.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objOrders);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/OrdersApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        Orders.Delete(11078);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/OrdersApi/Delete/" + 11078).Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the Orders table
        int totalRecordCount = Orders.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrdersApi/GetRecordCount/").Result;
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
    /// Shows how to get the total number of records by CustomerID
    /// </summary>
    private void GetRecordCountByCustomerID()
    {
        // get the total number of records in the Orders table by CustomerID
        // "VINET" here is just a sample CustomerID change the value as you see fit
        int totalRecordCount = Orders.GetRecordCountByCustomerID("VINET");

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrdersApi/GetRecordCountBy/" + "VINET").Result;
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
    /// Shows how to get the total number of records by EmployeeID
    /// </summary>
    private void GetRecordCountByEmployeeID()
    {
        // get the total number of records in the Orders table by EmployeeID
        // 5 here is just a sample EmployeeID change the value as you see fit
        int totalRecordCount = Orders.GetRecordCountByEmployeeID(5);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrdersApi/GetRecordCountBy/" + "5").Result;
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
    /// Shows how to get the total number of records by ShipVia
    /// </summary>
    private void GetRecordCountByShipVia()
    {
        // get the total number of records in the Orders table by ShipVia
        // 3 here is just a sample ShipVia change the value as you see fit
        int totalRecordCount = Orders.GetRecordCountByShipVia(3);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrdersApi/GetRecordCountBy/" + "3").Result;
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
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        OrdersCollection objOrdersCol = Orders.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        OrdersCollection objOrdersCol2 = Orders.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objOrdersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Orders(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByCustomerID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // 1. select a specific number of sorted records with a CustomerID = "VINET"
        // starting from the index you specify with totalRecordCount
        OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeByCustomerID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, "VINET");

        // to use objOrdersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Orders(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByEmployeeID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // 1. select a specific number of sorted records with a EmployeeID = 5
        // starting from the index you specify with totalRecordCount
        OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeByEmployeeID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 5);

        // to use objOrdersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Orders(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByShipVia()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // 1. select a specific number of sorted records with a ShipVia = 3
        // starting from the index you specify with totalRecordCount
        OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeByShipVia(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 3);

        // to use objOrdersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Orders(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get fields with Totals
    /// </summary>
    private void SelectTotals()
    {
        // get all fields with totals for the Orders table
        Orders objOrders = Orders.SelectTotals();

        // assign each field with a total
        decimal freightTotal = objOrders.FreightTotal;
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        int? orderID = null;
        string customerID = null;
        int? employeeID = null;
        DateTime? orderDate = null;
        DateTime? requiredDate = null;
        DateTime? shippedDate = null;
        int? shipVia = null;
        decimal? freight = null;
        string shipName = null;
        string shipAddress = null;
        string shipCity = null;
        string shipRegion = null;
        string shipPostalCode = null;
        string shipCountry = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        OrdersCollection objOrdersCol = Orders.SelectSkipAndTakeDynamicWhere(orderID, customerID, employeeID, orderDate, requiredDate, shippedDate, shipVia, freight, shipName, shipAddress, shipCity, shipRegion, shipPostalCode, shipCountry, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objOrdersCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Orders(s).  The example above will only loop for 10 items.
    }
}

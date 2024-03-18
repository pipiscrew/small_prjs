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
/// These are data-centric code examples for the OrderDetails table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class OrderDetailsExample
{
    private OrderDetailsExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectAll();

        // ** code using the web api instead of the middle tier **
        OrderDetailsCollection objOrderDetailsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrderDetailsCol = JsonConvert.DeserializeObject<OrderDetailsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrderDetailsCol.Sort(OrderDetails.ByUnitPrice);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrderDetailsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrderDetailsCol;
        grid.DataBind();

        // Example 4:  loop through all the OrderDetails(s)
        foreach (OrderDetails objOrderDetails in objOrderDetailsCol)
        {
            int orderID = objOrderDetails.OrderID;
            int productID = objOrderDetails.ProductID;
            decimal unitPrice = objOrderDetails.UnitPrice;
            Int16 quantity = objOrderDetails.Quantity;
            Single discount = objOrderDetails.Discount;

            // optionally get the Orders related to OrderID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Orders objOrdersRelatedToOrderID;

            if (objOrderDetails.Orders.IsValueCreated)
                objOrdersRelatedToOrderID = objOrderDetails.Orders.Value;

            // optionally get the Products related to ProductID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Products objProductsRelatedToProductID;

            if (objOrderDetails.Products.IsValueCreated)
                objProductsRelatedToProductID = objOrderDetails.Products.Value;
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        int orderIDSample = 10248;
        int productIDSample = 11;

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrderDetails objOrderDetails = OrderDetails.SelectByPrimaryKey(orderIDSample, productIDSample);

        // ** code using the web api instead of the middle tier **
        OrderDetails objOrderDetails = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/SelectByPrimaryKey/" + orderIDSample + "/" + productIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrderDetails = JsonConvert.DeserializeObject<OrderDetails>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objOrderDetails != null)
        {
            // if record is found, a record is returned
            int orderID = objOrderDetails.OrderID;
            int productID = objOrderDetails.ProductID;
            decimal unitPrice = objOrderDetails.UnitPrice;
            Int16 quantity = objOrderDetails.Quantity;
            Single discount = objOrderDetails.Discount;

            // optionally get the Orders related to OrderID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Orders objOrdersRelatedToOrderID;

            if (objOrderDetails.Orders.IsValueCreated)
                objOrdersRelatedToOrderID = objOrderDetails.Orders.Value;

            // optionally get the Products related to ProductID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Products objProductsRelatedToProductID;

            if (objOrderDetails.Products.IsValueCreated)
                objProductsRelatedToProductID = objOrderDetails.Products.Value;
        }
    }

    /// <summary>
    /// Shows how to Select all records by Orders, related to column OrderID
    /// </summary>
    private void SelectOrderDetailsCollectionByOrderID()
    {
        int orderIDSample = 10248;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectOrderDetailsCollectionByOrderID(orderIDSample);

        // ** code using the web api instead of the middle tier **
        OrderDetailsCollection objOrderDetailsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/SelectOrderDetailsCollectionByOrderID/" + orderIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrderDetailsCol = JsonConvert.DeserializeObject<OrderDetailsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrderDetailsCol.Sort(OrderDetails.ByUnitPrice);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrderDetailsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrderDetailsCol;
        grid.DataBind();

        // Example 4:  loop through all the OrderDetails(s)
        foreach (OrderDetails objOrderDetails in objOrderDetailsCol)
        {
            int orderID = objOrderDetails.OrderID;
            int productID = objOrderDetails.ProductID;
            decimal unitPrice = objOrderDetails.UnitPrice;
            Int16 quantity = objOrderDetails.Quantity;
            Single discount = objOrderDetails.Discount;

            // optionally get the Orders related to OrderID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Orders objOrdersRelatedToOrderID;

            if (objOrderDetails.Orders.IsValueCreated)
                objOrdersRelatedToOrderID = objOrderDetails.Orders.Value;

            // optionally get the Products related to ProductID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Products objProductsRelatedToProductID;

            if (objOrderDetails.Products.IsValueCreated)
                objProductsRelatedToProductID = objOrderDetails.Products.Value;
        }
    }

    /// <summary>
    /// Shows how to Select all records by Products, related to column ProductID
    /// </summary>
    private void SelectOrderDetailsCollectionByProductID()
    {
        int productIDSample = 11;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectOrderDetailsCollectionByProductID(productIDSample);

        // ** code using the web api instead of the middle tier **
        OrderDetailsCollection objOrderDetailsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/SelectOrderDetailsCollectionByProductID/" + productIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objOrderDetailsCol = JsonConvert.DeserializeObject<OrderDetailsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objOrderDetailsCol.Sort(OrderDetails.ByUnitPrice);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objOrderDetailsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objOrderDetailsCol;
        grid.DataBind();

        // Example 4:  loop through all the OrderDetails(s)
        foreach (OrderDetails objOrderDetails in objOrderDetailsCol)
        {
            int orderID = objOrderDetails.OrderID;
            int productID = objOrderDetails.ProductID;
            decimal unitPrice = objOrderDetails.UnitPrice;
            Int16 quantity = objOrderDetails.Quantity;
            Single discount = objOrderDetails.Discount;

            // optionally get the Orders related to OrderID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Orders objOrdersRelatedToOrderID;

            if (objOrderDetails.Orders.IsValueCreated)
                objOrdersRelatedToOrderID = objOrderDetails.Orders.Value;

            // optionally get the Products related to ProductID.
            // Note this is lazily loaded which means there's no value until you ask for it
            Products objProductsRelatedToProductID;

            if (objOrderDetails.Products.IsValueCreated)
                objProductsRelatedToProductID = objOrderDetails.Products.Value;
        }
    }

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new OrderDetails
        OrderDetails objOrderDetails = new OrderDetails();

        // assign values you want inserted
        objOrderDetails.OrderID = 11078;
        objOrderDetails.ProductID = 78;
        objOrderDetails.UnitPrice = Convert.ToDecimal(14.0000);
        objOrderDetails.Quantity = 12;
        objOrderDetails.Discount = 0f;

        // finally, insert a new record
        objOrderDetails.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objOrderDetails);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/OrderDetailsApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new OrderDetails
        OrderDetails objOrderDetails = new OrderDetails();

        // assign the existing primary key(s)
        // of the record you want updated
        objOrderDetails.OrderID = 10248;
        objOrderDetails.ProductID = 11;

        // assign values you want updated
        objOrderDetails.UnitPrice = Convert.ToDecimal(14.0000);
        objOrderDetails.Quantity = 12;
        objOrderDetails.Discount = 0f;

        // finally, update an existing record
        objOrderDetails.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objOrderDetails);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/OrderDetailsApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        OrderDetails.Delete(11078, 78);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/OrderDetailsApi/Delete/?" + "orderID=" + "11078" + "&productID=" + "78").Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the OrderDetails table
        int totalRecordCount = OrderDetails.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/GetRecordCount/").Result;
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
    /// Shows how to get the total number of records by OrderID
    /// </summary>
    private void GetRecordCountByOrderID()
    {
        // get the total number of records in the OrderDetails table by OrderID
        // 10248 here is just a sample OrderID change the value as you see fit
        int totalRecordCount = OrderDetails.GetRecordCountByOrderID(10248);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/GetRecordCountBy/" + "10248").Result;
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
    /// Shows how to get the total number of records by ProductID
    /// </summary>
    private void GetRecordCountByProductID()
    {
        // get the total number of records in the OrderDetails table by ProductID
        // 11 here is just a sample ProductID change the value as you see fit
        int totalRecordCount = OrderDetails.GetRecordCountByProductID(11);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/GetRecordCountBy/" + "11").Result;
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
        OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        OrderDetailsCollection objOrderDetailsCol2 = OrderDetails.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objOrderDetailsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the OrderDetails(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByOrderID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // 1. select a specific number of sorted records with a OrderID = 10248
        // starting from the index you specify with totalRecordCount
        OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTakeByOrderID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 10248);

        // to use objOrderDetailsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the OrderDetails(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByProductID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "OrderID";
        //string sortBy = "OrderID desc";

        // 1. select a specific number of sorted records with a ProductID = 11
        // starting from the index you specify with totalRecordCount
        OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTakeByProductID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 11);

        // to use objOrderDetailsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the OrderDetails(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get fields with Totals
    /// </summary>
    private void SelectTotals()
    {
        // get all fields with totals for the OrderDetails table
        OrderDetails objOrderDetails = OrderDetails.SelectTotals();

        // assign each field with a total
        decimal unitPriceTotal = objOrderDetails.UnitPriceTotal;
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
        int? productID = null;
        decimal? unitPrice = null;
        Int16? quantity = null;
        Single? discount = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        OrderDetailsCollection objOrderDetailsCol = OrderDetails.SelectSkipAndTakeDynamicWhere(orderID, productID, unitPrice, quantity, discount, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objOrderDetailsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the OrderDetails(s).  The example above will only loop for 10 items.
    }
}

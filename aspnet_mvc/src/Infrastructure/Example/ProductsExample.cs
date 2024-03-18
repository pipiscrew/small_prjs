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
/// These are data-centric code examples for the Products table.
/// You can cut and paste the respective codes into your application
/// by changing the sample values assigned from these examples.
/// NOTE: This class contains private methods because they're
/// not meant to be called by an outside client.  Each method contains
/// code for the respective example being shown
/// </summary>
public sealed class ProductsExample
{
    private ProductsExample()
    {
    }

    /// <summary>
    /// Shows how to Select all records.  It also shows how to sort, bind, and loop through records.
    /// </summary>
    private void SelectAll()
    {
        // select all records
        // uncomment this code if you would rather access the middle tier instead of the web api
        //ProductsCollection objProductsCol = Products.SelectAll();

        // ** code using the web api instead of the middle tier **
        ProductsCollection objProductsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectAll").Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objProductsCol = JsonConvert.DeserializeObject<ProductsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objProductsCol.Sort(Products.ByProductName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objProductsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objProductsCol;
        grid.DataBind();

        // Example 4:  loop through all the Products(s)
        foreach (Products objProducts in objProductsCol)
        {
            int productID = objProducts.ProductID;
            string productName = objProducts.ProductName;
            int? supplierID = objProducts.SupplierID;
            int? categoryID = objProducts.CategoryID;
            string quantityPerUnit = objProducts.QuantityPerUnit;
            decimal? unitPrice = objProducts.UnitPrice;
            Int16? unitsInStock = objProducts.UnitsInStock;
            Int16? unitsOnOrder = objProducts.UnitsOnOrder;
            Int16? reorderLevel = objProducts.ReorderLevel;
            bool discontinued = objProducts.Discontinued;

            // optionally get the Suppliers related to SupplierID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.SupplierID != null)
            {
                Suppliers objSuppliersRelatedToSupplierID;

                if (objProducts.Suppliers.IsValueCreated)
                    objSuppliersRelatedToSupplierID = objProducts.Suppliers.Value;
            }

            // optionally get the Categories related to CategoryID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.CategoryID != null)
            {
                Categories objCategoriesRelatedToCategoryID;

                if (objProducts.Categories.IsValueCreated)
                    objCategoriesRelatedToCategoryID = objProducts.Categories.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select a record by Primary Key.  It also shows how to retrieve Lazily-loaded related Objects.  Related Objects are assigned for each Foreign Key.
    /// </summary>
    private void SelectByPrimaryKey()
    {
        int productIDSample = 1;

        // select a record by primary key(s)

        // uncomment this code if you would rather access the middle tier instead of the web api
        //Products objProducts = Products.SelectByPrimaryKey(productIDSample);

        // ** code using the web api instead of the middle tier **
        Products objProducts = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectByPrimaryKey/" + productIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objProducts = JsonConvert.DeserializeObject<Products>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        if (objProducts != null)
        {
            // if record is found, a record is returned
            int productID = objProducts.ProductID;
            string productName = objProducts.ProductName;
            int? supplierID = objProducts.SupplierID;
            int? categoryID = objProducts.CategoryID;
            string quantityPerUnit = objProducts.QuantityPerUnit;
            decimal? unitPrice = objProducts.UnitPrice;
            Int16? unitsInStock = objProducts.UnitsInStock;
            Int16? unitsOnOrder = objProducts.UnitsOnOrder;
            Int16? reorderLevel = objProducts.ReorderLevel;
            bool discontinued = objProducts.Discontinued;

            // optionally get the Suppliers related to SupplierID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.SupplierID != null)
            {
                Suppliers objSuppliersRelatedToSupplierID;

                if (objProducts.Suppliers.IsValueCreated)
                    objSuppliersRelatedToSupplierID = objProducts.Suppliers.Value;
            }

            // optionally get the Categories related to CategoryID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.CategoryID != null)
            {
                Categories objCategoriesRelatedToCategoryID;

                if (objProducts.Categories.IsValueCreated)
                    objCategoriesRelatedToCategoryID = objProducts.Categories.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select all records by Suppliers, related to column SupplierID
    /// </summary>
    private void SelectProductsCollectionBySupplierID()
    {
        int supplierIDSample = 1;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //ProductsCollection objProductsCol = Products.SelectProductsCollectionBySupplierID(supplierIDSample);

        // ** code using the web api instead of the middle tier **
        ProductsCollection objProductsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectProductsCollectionBySupplierID/" + supplierIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objProductsCol = JsonConvert.DeserializeObject<ProductsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objProductsCol.Sort(Products.ByProductName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objProductsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objProductsCol;
        grid.DataBind();

        // Example 4:  loop through all the Products(s)
        foreach (Products objProducts in objProductsCol)
        {
            int productID = objProducts.ProductID;
            string productName = objProducts.ProductName;
            int? supplierID = objProducts.SupplierID;
            int? categoryID = objProducts.CategoryID;
            string quantityPerUnit = objProducts.QuantityPerUnit;
            decimal? unitPrice = objProducts.UnitPrice;
            Int16? unitsInStock = objProducts.UnitsInStock;
            Int16? unitsOnOrder = objProducts.UnitsOnOrder;
            Int16? reorderLevel = objProducts.ReorderLevel;
            bool discontinued = objProducts.Discontinued;

            // optionally get the Suppliers related to SupplierID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.SupplierID != null)
            {
                Suppliers objSuppliersRelatedToSupplierID;

                if (objProducts.Suppliers.IsValueCreated)
                    objSuppliersRelatedToSupplierID = objProducts.Suppliers.Value;
            }

            // optionally get the Categories related to CategoryID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.CategoryID != null)
            {
                Categories objCategoriesRelatedToCategoryID;

                if (objProducts.Categories.IsValueCreated)
                    objCategoriesRelatedToCategoryID = objProducts.Categories.Value;
            }
        }
    }

    /// <summary>
    /// Shows how to Select all records by Categories, related to column CategoryID
    /// </summary>
    private void SelectProductsCollectionByCategoryID()
    {
        int categoryIDSample = 1;

        // uncomment this code if you would rather access the middle tier instead of the web api
        //ProductsCollection objProductsCol = Products.SelectProductsCollectionByCategoryID(categoryIDSample);

        // ** code using the web api instead of the middle tier **
        ProductsCollection objProductsCol = null;

        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
            HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectProductsCollectionByCategoryID/" + categoryIDSample).Result;

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                objProductsCol = JsonConvert.DeserializeObject<ProductsCollection>(responseBody);
            }
        }
        // ** code using the web api instead of the middle tier **

        // Example 1:  you can optionally sort the collection in ascending order by your chosen field 
        objProductsCol.Sort(Products.ByProductName);

        // Example 2:  to sort in descending order, add this line to the Sort code in Example 1 
        objProductsCol.Reverse();

        // Example 3:  directly bind to a GridView
        GridView grid = new GridView();
        grid.DataSource = objProductsCol;
        grid.DataBind();

        // Example 4:  loop through all the Products(s)
        foreach (Products objProducts in objProductsCol)
        {
            int productID = objProducts.ProductID;
            string productName = objProducts.ProductName;
            int? supplierID = objProducts.SupplierID;
            int? categoryID = objProducts.CategoryID;
            string quantityPerUnit = objProducts.QuantityPerUnit;
            decimal? unitPrice = objProducts.UnitPrice;
            Int16? unitsInStock = objProducts.UnitsInStock;
            Int16? unitsOnOrder = objProducts.UnitsOnOrder;
            Int16? reorderLevel = objProducts.ReorderLevel;
            bool discontinued = objProducts.Discontinued;

            // optionally get the Suppliers related to SupplierID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.SupplierID != null)
            {
                Suppliers objSuppliersRelatedToSupplierID;

                if (objProducts.Suppliers.IsValueCreated)
                    objSuppliersRelatedToSupplierID = objProducts.Suppliers.Value;
            }

            // optionally get the Categories related to CategoryID.
            // Note this is lazily loaded which means there's no value until you ask for it
            if (objProducts.CategoryID != null)
            {
                Categories objCategoriesRelatedToCategoryID;

                if (objProducts.Categories.IsValueCreated)
                    objCategoriesRelatedToCategoryID = objProducts.Categories.Value;
            }
        }
    }
 
    /// <summary> 
    /// The example below shows how to Select the ProductID and ProductName columns for use with a with a Drop Down List, Combo Box, Checked Box List, List View, List Box, etc 
    /// </summary> 
    private void SelectProductsDropDownListData() 
    { 
        ProductsCollection objProductsCol = Products.SelectProductsDropDownListData(); 
 
        // Example 1:  directly bind to a drop down list 
        DropDownList ddl1 = new DropDownList(); 
        ddl1.DataValueField = "ProductID"; 
        ddl1.DataTextField = "ProductName"; 
        ddl1.DataSource = objProductsCol; 
        ddl1.DataBind(); 
 
        // Example 2:  add each item through a loop 
        DropDownList ddl2 = new DropDownList(); 
 
        foreach (Products objProducts in objProductsCol) 
        { 
            ddl2.Items.Add(new ListItem(objProducts.ProductName, objProducts.ProductID.ToString())); 
        } 
 
        // Example 3:  bind to a combo box.  Note: remove comment when using with windows forms 
        // ComboBox cbx1 = new ComboBox(); 
 
        // foreach (Products objProducts in objProductsCol) 
        // { 
        //     cbx1.Items.Add(new ListItem(objProducts.ProductName, objProducts.ProductID.ToString())); 
        // } 
    } 

    /// <summary>
    /// Shows how to Insert or Create a New Record
    /// </summary>
    private void Insert()
    {
        // first instantiate a new Products
        Products objProducts = new Products();

        // assign values you want inserted
        objProducts.ProductName = "Chai";
        objProducts.SupplierID = 1;
        objProducts.CategoryID = 1;
        objProducts.QuantityPerUnit = "10 boxes x 20 bags";
        objProducts.UnitPrice = Convert.ToDecimal(18.0000);
        objProducts.UnitsInStock = 39;
        objProducts.UnitsOnOrder = 0;
        objProducts.ReorderLevel = 10;
        objProducts.Discontinued = false;

        // finally, insert a new record
        // the insert method returns the newly created primary key
        int newlyCreatedPrimaryKey = objProducts.Insert();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objProducts);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/ProductsApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        // first instantiate a new Products
        Products objProducts = new Products();

        // assign the existing primary key(s)
        // of the record you want updated
        objProducts.ProductID = 1;

        // assign values you want updated
        objProducts.ProductName = "Chai";
        objProducts.SupplierID = 1;
        objProducts.CategoryID = 1;
        objProducts.QuantityPerUnit = "10 boxes x 20 bags";
        objProducts.UnitPrice = Convert.ToDecimal(18.0000);
        objProducts.UnitsInStock = 39;
        objProducts.UnitsOnOrder = 0;
        objProducts.ReorderLevel = 10;
        objProducts.Discontinued = false;

        // finally, update an existing record
        objProducts.Update();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //string serializedModel = JsonConvert.SerializeObject(objProducts);
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.PostAsync("api/ProductsApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
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
        Products.Delete(78);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.DeleteAsync("api/ProductsApi/Delete/" + 78).Result;

        //    if (!response.IsSuccessStatusCode)
        //        throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
        //}
    }

    /// <summary>
    /// Shows how to get the total number of records
    /// </summary>
    private void GetRecordCount()
    {
        // get the total number of records in the Products table
        int totalRecordCount = Products.GetRecordCount();

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/ProductsApi/GetRecordCount/").Result;
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
    /// Shows how to get the total number of records by SupplierID
    /// </summary>
    private void GetRecordCountBySupplierID()
    {
        // get the total number of records in the Products table by SupplierID
        // 1 here is just a sample SupplierID change the value as you see fit
        int totalRecordCount = Products.GetRecordCountBySupplierID(1);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/ProductsApi/GetRecordCountBy/" + "1").Result;
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
    /// Shows how to get the total number of records by CategoryID
    /// </summary>
    private void GetRecordCountByCategoryID()
    {
        // get the total number of records in the Products table by CategoryID
        // 1 here is just a sample CategoryID change the value as you see fit
        int totalRecordCount = Products.GetRecordCountByCategoryID(1);

        //// if you want to access the middle tier though the web api do this instead, make sure to comment code above
        //int totalRecordCount = 0;
        //
        //using (var client = new HttpClient())
        //{
        //    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
        //    HttpResponseMessage response = client.GetAsync("api/ProductsApi/GetRecordCountBy/" + "1").Result;
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
        string sortBy = "ProductID";
        //string sortBy = "ProductID desc";

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount
        ProductsCollection objProductsCol = Products.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // 2. or you can also select a specific number of sorted records starting from the index you specify Without the totalRecordCount
        ProductsCollection objProductsCol2 = Products.SelectSkipAndTake(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, sortBy);

        // to use objProductsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Products(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeBySupplierID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "ProductID";
        //string sortBy = "ProductID desc";

        // 1. select a specific number of sorted records with a SupplierID = 1
        // starting from the index you specify with totalRecordCount
        ProductsCollection objProductsCol = Products.SelectSkipAndTakeBySupplierID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 1);

        // to use objProductsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Products(s).  The example above will only loop for 10 items.
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
    private void SelectSkipAndTakeByCategoryID()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "ProductID";
        //string sortBy = "ProductID desc";

        // 1. select a specific number of sorted records with a CategoryID = 1
        // starting from the index you specify with totalRecordCount
        ProductsCollection objProductsCol = Products.SelectSkipAndTakeByCategoryID(numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy, 1);

        // to use objProductsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Products(s).  The example above will only loop for 10 items.
    }

    /// <summary>
    /// Shows how to get fields with Totals
    /// </summary>
    private void SelectTotals()
    {
        // get all fields with totals for the Products table
        Products objProducts = Products.SelectTotals();

        // assign each field with a total
        decimal unitPriceTotal = objProducts.UnitPriceTotal;
    }

    /// <summary>
    /// Shows how to get a specific number of sorted records, starting from an index, based on Search Parameters.  The number of records are also retrieved.
    /// </summary>
    private void SelectSkipAndTakeDynamicWhere()
    {
        int totalRecordCount;
        int startRetrievalFromRecordIndex = 0;
        int numberOfRecordsToRetrieve = 10;
        string sortBy = "ProductID";
        //string sortBy = "ProductID desc";

        // search parameters, everything is nullable, only items being searched for should be filled
        // note: fields with String type uses a LIKE search, everything else uses an exact match
        // also, every field you're searching for uses the AND operator
        // e.g. int? productID = 1; string productName = "ch";
        // will translate to: SELECT....WHERE productID = 1 AND productName LIKE '%ch%'
        int? productID = null;
        string productName = null;
        int? supplierID = null;
        int? categoryID = null;
        string quantityPerUnit = null;
        decimal? unitPrice = null;
        Int16? unitsInStock = null;
        Int16? unitsOnOrder = null;
        Int16? reorderLevel = null;
        bool? discontinued = null;

        // 1. select a specific number of sorted records starting from the index you specify with totalRecordCount based on Search Parameters
        ProductsCollection objProductsCol = Products.SelectSkipAndTakeDynamicWhere(productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued, numberOfRecordsToRetrieve, startRetrievalFromRecordIndex, out totalRecordCount, sortBy);

        // to use objProductsCol please see the SelectAll() method examples
        // No need for Examples 1 and 2 because the Collection here is already sorted
        // Example 3:  directly bind to a GridView
        // Example 4:  loop through all the Products(s).  The example above will only loop for 10 items.
    }
}

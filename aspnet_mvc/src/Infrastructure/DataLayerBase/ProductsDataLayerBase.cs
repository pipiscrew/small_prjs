using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for ProductsDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the ProductsDataLayer class
     /// </summary>
     public class ProductsDataLayerBase
     {
         // constructor
         public ProductsDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Products SelectByPrimaryKey(int productID)
         {
              Products objProducts = null;
              string storedProcName = "[dbo].[Products_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@productID", productID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objProducts = CreateProductsFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objProducts;
         }

         /// <summary>
         /// Gets the total number of records in the Products table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Products_GetRecordCount]", null, null, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Products table by SupplierID
         /// </summary>
         public static int GetRecordCountBySupplierID(int supplierID)
         {
             return GetRecordCountShared("[dbo].[Products_GetRecordCountBySupplierID]", "supplierID", supplierID, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Products table by CategoryID
         /// </summary>
         public static int GetRecordCountByCategoryID(int categoryID)
         {
             return GetRecordCountShared("[dbo].[Products_GetRecordCountByCategoryID]", "categoryID", categoryID, true, null);
         }

         public static int GetRecordCountShared(string storedProcName = null, string param = null, object paramValue = null, bool isUseStoredProc = true, string dynamicSqlScript = null)
         {
              int recordCount = 0;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      switch (param)
                      {
                          case "supplierID":
                              command.Parameters.AddWithValue("@supplierID", paramValue);
                              break;
                          case "categoryID":
                              command.Parameters.AddWithValue("@categoryID", paramValue);
                              break;
                          default:
                              break;
                      }

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  recordCount = (int)dt.Rows[0]["RecordCount"];
                              }
                          }
                      }
                  }
              }

              return recordCount;
         }

         /// <summary>
         /// Gets the total number of records in the Products table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Products_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  recordCount = (int)dt.Rows[0]["RecordCount"];
                              }
                          }
                      }
                  }
              }

              return recordCount;
         }

         /// <summary>
         /// Selects Products records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static ProductsCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Products_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by SupplierID as a collection (List) of Products sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeBySupplierID(string sortByExpression, int startRowIndex, int rows, int supplierID)
         {
             return SelectShared("[dbo].[Products_SelectSkipAndTakeBySupplierID]", "supplierID", supplierID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by CategoryID as a collection (List) of Products sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeByCategoryID(string sortByExpression, int startRowIndex, int rows, int categoryID)
         {
             return SelectShared("[dbo].[Products_SelectSkipAndTakeByCategoryID]", "categoryID", categoryID, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Products records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static ProductsCollection SelectSkipAndTakeDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued, string sortByExpression, int startRowIndex, int rows)
         {
              ProductsCollection objProductsCol = null;
              string storedProcName = "[dbo].[Products_SelectSkipAndTakeWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // select, skip, take, sort parameters
                      command.Parameters.AddWithValue("@start", startRowIndex);
                      command.Parameters.AddWithValue("@numberOfRows", rows);
                      command.Parameters.AddWithValue("@sortByExpression", sortByExpression);

                      // search parameters
                      AddSearchCommandParamsShared(command, productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objProductsCol = new ProductsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Products objProducts = CreateProductsFromDataRowShared(dr);
                                      objProductsCol.Add(objProducts);
                                  }
                              }
                          }
                      }
                  }
              }

              return objProductsCol;
         }

         /// <summary>
         /// Gets the grand total or sum of fields with a money of decimal data type
         /// </summary>
         public static Products SelectTotals()
         {
              Products objProducts = null;
              string storedProcName = "[dbo].[Products_SelectTotals]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  if(dt.Rows[0]["UnitPriceTotal"] != DBNull.Value)
                                       objProducts.UnitPriceTotal = (decimal)dt.Rows[0]["UnitPriceTotal"];
                              }
                          }
                      }
                  }
              }

              return objProducts;
         }

         /// <summary>
         /// Selects all Products
         /// </summary>
         public static ProductsCollection SelectAll()
         {
             return SelectShared("[dbo].[Products_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Products.
         /// </summary>
         public static ProductsCollection SelectAllDynamicWhere(int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
              ProductsCollection objProductsCol = null;
              string storedProcName = "[dbo].[Products_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, productID, productName, supplierID, categoryID, quantityPerUnit, unitPrice, unitsInStock, unitsOnOrder, reorderLevel, discontinued);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objProductsCol = new ProductsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Products objProducts = CreateProductsFromDataRowShared(dr);
                                      objProductsCol.Add(objProducts);
                                  }
                              }
                          }
                      }
                  }
              }

              return objProductsCol;
         }

         /// <summary>
         /// Selects all Products by Suppliers, related to column SupplierID
         /// </summary>
         public static ProductsCollection SelectProductsCollectionBySupplierID(int supplierID)
         {
             return SelectShared("[dbo].[Products_SelectAllBySupplierID]", "supplierID", supplierID);
         }

         /// <summary>
         /// Selects all Products by Categories, related to column CategoryID
         /// </summary>
         public static ProductsCollection SelectProductsCollectionByCategoryID(int categoryID)
         {
             return SelectShared("[dbo].[Products_SelectAllByCategoryID]", "categoryID", categoryID);
         }

         /// <summary>
         /// Selects ProductID and ProductName columns for use with a DropDownList web control
         /// </summary>
         public static ProductsCollection SelectProductsDropDownListData()
         {
              ProductsCollection objProductsCol = null;
              string storedProcName = "[dbo].[Products_SelectDropDownListData]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objProductsCol = new ProductsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Products objProducts = new Products();
                                      objProducts.ProductID = (int)dr["ProductID"];
                                      objProducts.ProductName = (string)(dr["ProductName"]);

                                      objProductsCol.Add(objProducts);
                                  }
                              }
                          }
                      }
                  }
              }

              return objProductsCol;
         }

         public static ProductsCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              ProductsCollection objProductsCol = null;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // select, skip, take, sort parameters
                      if (!String.IsNullOrEmpty(sortByExpression) && startRowIndex != null && rows != null)
                      {
                          command.Parameters.AddWithValue("@start", startRowIndex.Value);
                          command.Parameters.AddWithValue("@numberOfRows", rows.Value);
                          command.Parameters.AddWithValue("@sortByExpression", sortByExpression);
                      }

                      // parameters
                      switch (param)
                      {
                          case "supplierID":
                              command.Parameters.AddWithValue("@supplierID", paramValue);
                              break;
                          case "categoryID":
                              command.Parameters.AddWithValue("@categoryID", paramValue);
                              break;
                          default:
                              break;
                      }

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objProductsCol = new ProductsCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Products objProducts = CreateProductsFromDataRowShared(dr);
                                      objProductsCol.Add(objProducts);
                                  }
                              }
                          }
                      }
                  }
              }

              return objProductsCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Products objProducts)
         {
             string storedProcName = "[dbo].[Products_Insert]";
             return InsertUpdate(objProducts, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Products objProducts)
         {
             string storedProcName = "[dbo].[Products_Update]";
             InsertUpdate(objProducts, true, storedProcName);
         }

         private static int InsertUpdate(Products objProducts, bool isUpdate, string storedProcName)
         {
              int newlyCreatedProductID = objProducts.ProductID;

              object supplierID = objProducts.SupplierID;
              object categoryID = objProducts.CategoryID;
              object quantityPerUnit = objProducts.QuantityPerUnit;
              object unitPrice = objProducts.UnitPrice;
              object unitsInStock = objProducts.UnitsInStock;
              object unitsOnOrder = objProducts.UnitsOnOrder;
              object reorderLevel = objProducts.ReorderLevel;

              if (objProducts.SupplierID == null)
                  supplierID = System.DBNull.Value;

              if (objProducts.CategoryID == null)
                  categoryID = System.DBNull.Value;

              if (String.IsNullOrEmpty(objProducts.QuantityPerUnit))
                  quantityPerUnit = System.DBNull.Value;

              if (objProducts.UnitPrice == null)
                  unitPrice = System.DBNull.Value;

              if (objProducts.UnitsInStock == null)
                  unitsInStock = System.DBNull.Value;

              if (objProducts.UnitsOnOrder == null)
                  unitsOnOrder = System.DBNull.Value;

              if (objProducts.ReorderLevel == null)
                  reorderLevel = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      if (isUpdate)
                      {
                          // for update only
                          command.Parameters.AddWithValue("@productID", objProducts.ProductID);
                      }

                      command.Parameters.AddWithValue("@productName", objProducts.ProductName);
                      command.Parameters.AddWithValue("@supplierID", supplierID);
                      command.Parameters.AddWithValue("@categoryID", categoryID);
                      command.Parameters.AddWithValue("@quantityPerUnit", quantityPerUnit);
                      command.Parameters.AddWithValue("@unitPrice", unitPrice);
                      command.Parameters.AddWithValue("@unitsInStock", unitsInStock);
                      command.Parameters.AddWithValue("@unitsOnOrder", unitsOnOrder);
                      command.Parameters.AddWithValue("@reorderLevel", reorderLevel);
                      command.Parameters.AddWithValue("@discontinued", objProducts.Discontinued);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedProductID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedProductID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int productID)
         {
              string storedProcName = "[dbo].[Products_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@productID", productID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? productID, string productName, int? supplierID, int? categoryID, string quantityPerUnit, decimal? unitPrice, Int16? unitsInStock, Int16? unitsOnOrder, Int16? reorderLevel, bool? discontinued)
         {
              if(productID != null)
                  command.Parameters.AddWithValue("@productID", productID);
              else
                  command.Parameters.AddWithValue("@productID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(productName))
                  command.Parameters.AddWithValue("@productName", productName);
              else
                  command.Parameters.AddWithValue("@productName", System.DBNull.Value);

              if(supplierID != null)
                  command.Parameters.AddWithValue("@supplierID", supplierID);
              else
                  command.Parameters.AddWithValue("@supplierID", System.DBNull.Value);

              if(categoryID != null)
                  command.Parameters.AddWithValue("@categoryID", categoryID);
              else
                  command.Parameters.AddWithValue("@categoryID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(quantityPerUnit))
                  command.Parameters.AddWithValue("@quantityPerUnit", quantityPerUnit);
              else
                  command.Parameters.AddWithValue("@quantityPerUnit", System.DBNull.Value);

              if(unitPrice != null)
                  command.Parameters.AddWithValue("@unitPrice", unitPrice);
              else
                  command.Parameters.AddWithValue("@unitPrice", System.DBNull.Value);

              if(unitsInStock != null)
                  command.Parameters.AddWithValue("@unitsInStock", unitsInStock);
              else
                  command.Parameters.AddWithValue("@unitsInStock", System.DBNull.Value);

              if(unitsOnOrder != null)
                  command.Parameters.AddWithValue("@unitsOnOrder", unitsOnOrder);
              else
                  command.Parameters.AddWithValue("@unitsOnOrder", System.DBNull.Value);

              if(reorderLevel != null)
                  command.Parameters.AddWithValue("@reorderLevel", reorderLevel);
              else
                  command.Parameters.AddWithValue("@reorderLevel", System.DBNull.Value);

              if(discontinued != null)
                  command.Parameters.AddWithValue("@discontinued", discontinued);
              else
                  command.Parameters.AddWithValue("@discontinued", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Products object from the passed data row
         /// </summary>
         private static Products CreateProductsFromDataRowShared(DataRow dr)
         {
             Products objProducts = new Products();

             objProducts.ProductID = (int)dr["ProductID"];
             objProducts.ProductName = dr["ProductName"].ToString();

             if (dr["SupplierID"] != System.DBNull.Value)
                 objProducts.SupplierID = (int)dr["SupplierID"];
             else
                 objProducts.SupplierID = null;


             if (dr["CategoryID"] != System.DBNull.Value)
                 objProducts.CategoryID = (int)dr["CategoryID"];
             else
                 objProducts.CategoryID = null;


             if (dr["QuantityPerUnit"] != System.DBNull.Value)
                 objProducts.QuantityPerUnit = dr["QuantityPerUnit"].ToString();
             else
                 objProducts.QuantityPerUnit = null;

             if (dr["UnitPrice"] != System.DBNull.Value)
                 objProducts.UnitPrice = (decimal)dr["UnitPrice"];
             else
                 objProducts.UnitPrice = null;

             if (dr["UnitsInStock"] != System.DBNull.Value)
                 objProducts.UnitsInStock = (Int16)dr["UnitsInStock"];
             else
                 objProducts.UnitsInStock = null;

             if (dr["UnitsOnOrder"] != System.DBNull.Value)
                 objProducts.UnitsOnOrder = (Int16)dr["UnitsOnOrder"];
             else
                 objProducts.UnitsOnOrder = null;

             if (dr["ReorderLevel"] != System.DBNull.Value)
                 objProducts.ReorderLevel = (Int16)dr["ReorderLevel"];
             else
                 objProducts.ReorderLevel = null;
             objProducts.Discontinued = (bool)dr["Discontinued"];

             return objProducts;
         }
     }
}

using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for SuppliersDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the SuppliersDataLayer class
     /// </summary>
     public class SuppliersDataLayerBase
     {
         // constructor
         public SuppliersDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Suppliers SelectByPrimaryKey(int supplierID)
         {
              Suppliers objSuppliers = null;
              string storedProcName = "[dbo].[Suppliers_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@supplierID", supplierID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objSuppliers = CreateSuppliersFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objSuppliers;
         }

         /// <summary>
         /// Gets the total number of records in the Suppliers table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Suppliers_GetRecordCount]", null, null, true, null);
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
         /// Gets the total number of records in the Suppliers table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Suppliers_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);

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
         /// Selects Suppliers records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static SuppliersCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Suppliers_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Suppliers records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static SuppliersCollection SelectSkipAndTakeDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, string sortByExpression, int startRowIndex, int rows)
         {
              SuppliersCollection objSuppliersCol = null;
              string storedProcName = "[dbo].[Suppliers_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objSuppliersCol = new SuppliersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Suppliers objSuppliers = CreateSuppliersFromDataRowShared(dr);
                                      objSuppliersCol.Add(objSuppliers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objSuppliersCol;
         }

         /// <summary>
         /// Selects all Suppliers
         /// </summary>
         public static SuppliersCollection SelectAll()
         {
             return SelectShared("[dbo].[Suppliers_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Suppliers.
         /// </summary>
         public static SuppliersCollection SelectAllDynamicWhere(int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
              SuppliersCollection objSuppliersCol = null;
              string storedProcName = "[dbo].[Suppliers_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, supplierID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objSuppliersCol = new SuppliersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Suppliers objSuppliers = CreateSuppliersFromDataRowShared(dr);
                                      objSuppliersCol.Add(objSuppliers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objSuppliersCol;
         }

         /// <summary>
         /// Selects SupplierID and CompanyName columns for use with a DropDownList web control
         /// </summary>
         public static SuppliersCollection SelectSuppliersDropDownListData()
         {
              SuppliersCollection objSuppliersCol = null;
              string storedProcName = "[dbo].[Suppliers_SelectDropDownListData]";

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
                                  objSuppliersCol = new SuppliersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Suppliers objSuppliers = new Suppliers();
                                      objSuppliers.SupplierID = (int)dr["SupplierID"];
                                      objSuppliers.CompanyName = (string)(dr["CompanyName"]);

                                      objSuppliersCol.Add(objSuppliers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objSuppliersCol;
         }

         public static SuppliersCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              SuppliersCollection objSuppliersCol = null;

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

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objSuppliersCol = new SuppliersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Suppliers objSuppliers = CreateSuppliersFromDataRowShared(dr);
                                      objSuppliersCol.Add(objSuppliers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objSuppliersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Suppliers objSuppliers)
         {
             string storedProcName = "[dbo].[Suppliers_Insert]";
             return InsertUpdate(objSuppliers, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Suppliers objSuppliers)
         {
             string storedProcName = "[dbo].[Suppliers_Update]";
             InsertUpdate(objSuppliers, true, storedProcName);
         }

         private static int InsertUpdate(Suppliers objSuppliers, bool isUpdate, string storedProcName)
         {
              int newlyCreatedSupplierID = objSuppliers.SupplierID;

              object contactName = objSuppliers.ContactName;
              object contactTitle = objSuppliers.ContactTitle;
              object address = objSuppliers.Address;
              object city = objSuppliers.City;
              object region1 = objSuppliers.Region1;
              object postalCode = objSuppliers.PostalCode;
              object country = objSuppliers.Country;
              object phone = objSuppliers.Phone;
              object fax = objSuppliers.Fax;
              object homePage = objSuppliers.HomePage;

              if (String.IsNullOrEmpty(objSuppliers.ContactName))
                  contactName = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.ContactTitle))
                  contactTitle = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.Address))
                  address = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.City))
                  city = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.Region1))
                  region1 = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.PostalCode))
                  postalCode = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.Country))
                  country = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.Phone))
                  phone = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.Fax))
                  fax = System.DBNull.Value;

              if (String.IsNullOrEmpty(objSuppliers.HomePage))
                  homePage = System.DBNull.Value;

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
                          command.Parameters.AddWithValue("@supplierID", objSuppliers.SupplierID);
                      }

                      command.Parameters.AddWithValue("@companyName", objSuppliers.CompanyName);
                      command.Parameters.AddWithValue("@contactName", contactName);
                      command.Parameters.AddWithValue("@contactTitle", contactTitle);
                      command.Parameters.AddWithValue("@address", address);
                      command.Parameters.AddWithValue("@city", city);
                      command.Parameters.AddWithValue("@region1", region1);
                      command.Parameters.AddWithValue("@postalCode", postalCode);
                      command.Parameters.AddWithValue("@country", country);
                      command.Parameters.AddWithValue("@phone", phone);
                      command.Parameters.AddWithValue("@fax", fax);
                      command.Parameters.AddWithValue("@homePage", homePage);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedSupplierID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedSupplierID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int supplierID)
         {
              string storedProcName = "[dbo].[Suppliers_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@supplierID", supplierID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? supplierID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
              if(supplierID != null)
                  command.Parameters.AddWithValue("@supplierID", supplierID);
              else
                  command.Parameters.AddWithValue("@supplierID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(companyName))
                  command.Parameters.AddWithValue("@companyName", companyName);
              else
                  command.Parameters.AddWithValue("@companyName", System.DBNull.Value);

              if(!String.IsNullOrEmpty(contactName))
                  command.Parameters.AddWithValue("@contactName", contactName);
              else
                  command.Parameters.AddWithValue("@contactName", System.DBNull.Value);

              if(!String.IsNullOrEmpty(contactTitle))
                  command.Parameters.AddWithValue("@contactTitle", contactTitle);
              else
                  command.Parameters.AddWithValue("@contactTitle", System.DBNull.Value);

              if(!String.IsNullOrEmpty(address))
                  command.Parameters.AddWithValue("@address", address);
              else
                  command.Parameters.AddWithValue("@address", System.DBNull.Value);

              if(!String.IsNullOrEmpty(city))
                  command.Parameters.AddWithValue("@city", city);
              else
                  command.Parameters.AddWithValue("@city", System.DBNull.Value);

              if(!String.IsNullOrEmpty(region1))
                  command.Parameters.AddWithValue("@region1", region1);
              else
                  command.Parameters.AddWithValue("@region1", System.DBNull.Value);

              if(!String.IsNullOrEmpty(postalCode))
                  command.Parameters.AddWithValue("@postalCode", postalCode);
              else
                  command.Parameters.AddWithValue("@postalCode", System.DBNull.Value);

              if(!String.IsNullOrEmpty(country))
                  command.Parameters.AddWithValue("@country", country);
              else
                  command.Parameters.AddWithValue("@country", System.DBNull.Value);

              if(!String.IsNullOrEmpty(phone))
                  command.Parameters.AddWithValue("@phone", phone);
              else
                  command.Parameters.AddWithValue("@phone", System.DBNull.Value);

              if(!String.IsNullOrEmpty(fax))
                  command.Parameters.AddWithValue("@fax", fax);
              else
                  command.Parameters.AddWithValue("@fax", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Suppliers object from the passed data row
         /// </summary>
         private static Suppliers CreateSuppliersFromDataRowShared(DataRow dr)
         {
             Suppliers objSuppliers = new Suppliers();

             objSuppliers.SupplierID = (int)dr["SupplierID"];
             objSuppliers.CompanyName = dr["CompanyName"].ToString();

             if (dr["ContactName"] != System.DBNull.Value)
                 objSuppliers.ContactName = dr["ContactName"].ToString();
             else
                 objSuppliers.ContactName = null;

             if (dr["ContactTitle"] != System.DBNull.Value)
                 objSuppliers.ContactTitle = dr["ContactTitle"].ToString();
             else
                 objSuppliers.ContactTitle = null;

             if (dr["Address"] != System.DBNull.Value)
                 objSuppliers.Address = dr["Address"].ToString();
             else
                 objSuppliers.Address = null;

             if (dr["City"] != System.DBNull.Value)
                 objSuppliers.City = dr["City"].ToString();
             else
                 objSuppliers.City = null;

             if (dr["Region1"] != System.DBNull.Value)
                 objSuppliers.Region1 = dr["Region1"].ToString();
             else
                 objSuppliers.Region1 = null;

             if (dr["PostalCode"] != System.DBNull.Value)
                 objSuppliers.PostalCode = dr["PostalCode"].ToString();
             else
                 objSuppliers.PostalCode = null;

             if (dr["Country"] != System.DBNull.Value)
                 objSuppliers.Country = dr["Country"].ToString();
             else
                 objSuppliers.Country = null;

             if (dr["Phone"] != System.DBNull.Value)
                 objSuppliers.Phone = dr["Phone"].ToString();
             else
                 objSuppliers.Phone = null;

             if (dr["Fax"] != System.DBNull.Value)
                 objSuppliers.Fax = dr["Fax"].ToString();
             else
                 objSuppliers.Fax = null;

             if (dr["HomePage"] != System.DBNull.Value)
                 objSuppliers.HomePage = dr["HomePage"].ToString();
             else
                 objSuppliers.HomePage = null;

             return objSuppliers;
         }
     }
}

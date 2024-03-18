using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for CustomersDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the CustomersDataLayer class
     /// </summary>
     public class CustomersDataLayerBase
     {
         // constructor
         public CustomersDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Customers SelectByPrimaryKey(string customerID)
         {
              Customers objCustomers = null;
              string storedProcName = "[dbo].[Customers_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@customerID", customerID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCustomers = CreateCustomersFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objCustomers;
         }

         /// <summary>
         /// Gets the total number of records in the Customers table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Customers_GetRecordCount]", null, null, true, null);
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
         /// Gets the total number of records in the Customers table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Customers_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);

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
         /// Selects Customers records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static CustomersCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Customers_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Customers records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static CustomersCollection SelectSkipAndTakeDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax, string sortByExpression, int startRowIndex, int rows)
         {
              CustomersCollection objCustomersCol = null;
              string storedProcName = "[dbo].[Customers_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCustomersCol = new CustomersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Customers objCustomers = CreateCustomersFromDataRowShared(dr);
                                      objCustomersCol.Add(objCustomers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomersCol;
         }

         /// <summary>
         /// Selects all Customers
         /// </summary>
         public static CustomersCollection SelectAll()
         {
             return SelectShared("[dbo].[Customers_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Customers.
         /// </summary>
         public static CustomersCollection SelectAllDynamicWhere(string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
              CustomersCollection objCustomersCol = null;
              string storedProcName = "[dbo].[Customers_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, customerID, companyName, contactName, contactTitle, address, city, region1, postalCode, country, phone, fax);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objCustomersCol = new CustomersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Customers objCustomers = CreateCustomersFromDataRowShared(dr);
                                      objCustomersCol.Add(objCustomers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomersCol;
         }

         /// <summary>
         /// Selects CustomerID and CompanyName columns for use with a DropDownList web control
         /// </summary>
         public static CustomersCollection SelectCustomersDropDownListData()
         {
              CustomersCollection objCustomersCol = null;
              string storedProcName = "[dbo].[Customers_SelectDropDownListData]";

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
                                  objCustomersCol = new CustomersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Customers objCustomers = new Customers();
                                      objCustomers.CustomerID = (string)dr["CustomerID"];
                                      objCustomers.CompanyName = (string)(dr["CompanyName"]);

                                      objCustomersCol.Add(objCustomers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomersCol;
         }

         public static CustomersCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              CustomersCollection objCustomersCol = null;

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
                                  objCustomersCol = new CustomersCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Customers objCustomers = CreateCustomersFromDataRowShared(dr);
                                      objCustomersCol.Add(objCustomers);
                                  }
                              }
                          }
                      }
                  }
              }

              return objCustomersCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static string Insert(Customers objCustomers)
         {
             string storedProcName = "[dbo].[Customers_Insert]";
             return InsertUpdate(objCustomers, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Customers objCustomers)
         {
             string storedProcName = "[dbo].[Customers_Update]";
             InsertUpdate(objCustomers, true, storedProcName);
         }

         private static string InsertUpdate(Customers objCustomers, bool isUpdate, string storedProcName)
         {
              string newlyCreatedCustomerID = objCustomers.CustomerID;

              object contactName = objCustomers.ContactName;
              object contactTitle = objCustomers.ContactTitle;
              object address = objCustomers.Address;
              object city = objCustomers.City;
              object region1 = objCustomers.Region1;
              object postalCode = objCustomers.PostalCode;
              object country = objCustomers.Country;
              object phone = objCustomers.Phone;
              object fax = objCustomers.Fax;

              if (String.IsNullOrEmpty(objCustomers.ContactName))
                  contactName = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.ContactTitle))
                  contactTitle = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.Address))
                  address = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.City))
                  city = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.Region1))
                  region1 = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.PostalCode))
                  postalCode = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.Country))
                  country = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.Phone))
                  phone = System.DBNull.Value;

              if (String.IsNullOrEmpty(objCustomers.Fax))
                  fax = System.DBNull.Value;

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@customerID", objCustomers.CustomerID);
                      command.Parameters.AddWithValue("@companyName", objCustomers.CompanyName);
                      command.Parameters.AddWithValue("@contactName", contactName);
                      command.Parameters.AddWithValue("@contactTitle", contactTitle);
                      command.Parameters.AddWithValue("@address", address);
                      command.Parameters.AddWithValue("@city", city);
                      command.Parameters.AddWithValue("@region1", region1);
                      command.Parameters.AddWithValue("@postalCode", postalCode);
                      command.Parameters.AddWithValue("@country", country);
                      command.Parameters.AddWithValue("@phone", phone);
                      command.Parameters.AddWithValue("@fax", fax);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedCustomerID = (string)command.ExecuteScalar();
                  }
              }

              return newlyCreatedCustomerID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(string customerID)
         {
              string storedProcName = "[dbo].[Customers_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@customerID", customerID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, string customerID, string companyName, string contactName, string contactTitle, string address, string city, string region1, string postalCode, string country, string phone, string fax)
         {
              if(!String.IsNullOrEmpty(customerID))
                  command.Parameters.AddWithValue("@customerID", customerID);
              else
                  command.Parameters.AddWithValue("@customerID", System.DBNull.Value);

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
         /// Creates a Customers object from the passed data row
         /// </summary>
         private static Customers CreateCustomersFromDataRowShared(DataRow dr)
         {
             Customers objCustomers = new Customers();

             objCustomers.CustomerID = dr["CustomerID"].ToString();
             objCustomers.CompanyName = dr["CompanyName"].ToString();

             if (dr["ContactName"] != System.DBNull.Value)
                 objCustomers.ContactName = dr["ContactName"].ToString();
             else
                 objCustomers.ContactName = null;

             if (dr["ContactTitle"] != System.DBNull.Value)
                 objCustomers.ContactTitle = dr["ContactTitle"].ToString();
             else
                 objCustomers.ContactTitle = null;

             if (dr["Address"] != System.DBNull.Value)
                 objCustomers.Address = dr["Address"].ToString();
             else
                 objCustomers.Address = null;

             if (dr["City"] != System.DBNull.Value)
                 objCustomers.City = dr["City"].ToString();
             else
                 objCustomers.City = null;

             if (dr["Region1"] != System.DBNull.Value)
                 objCustomers.Region1 = dr["Region1"].ToString();
             else
                 objCustomers.Region1 = null;

             if (dr["PostalCode"] != System.DBNull.Value)
                 objCustomers.PostalCode = dr["PostalCode"].ToString();
             else
                 objCustomers.PostalCode = null;

             if (dr["Country"] != System.DBNull.Value)
                 objCustomers.Country = dr["Country"].ToString();
             else
                 objCustomers.Country = null;

             if (dr["Phone"] != System.DBNull.Value)
                 objCustomers.Phone = dr["Phone"].ToString();
             else
                 objCustomers.Phone = null;

             if (dr["Fax"] != System.DBNull.Value)
                 objCustomers.Fax = dr["Fax"].ToString();
             else
                 objCustomers.Fax = null;

             return objCustomers;
         }
     }
}

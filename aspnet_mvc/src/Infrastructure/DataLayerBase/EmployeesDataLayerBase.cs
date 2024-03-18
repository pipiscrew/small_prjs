using System;
using System.Data;
using System.Data.SqlClient;
using north.BusinessObject;
using System.Configuration;

namespace north.DataLayer.Base
{
     /// <summary>
     /// Base class for EmployeesDataLayer.  Do not make changes to this class,
     /// instead, put additional code in the EmployeesDataLayer class
     /// </summary>
     public class EmployeesDataLayerBase
     {
         // constructor
         public EmployeesDataLayerBase()
         {
         }

         /// <summary>
         /// Selects a record by primary key(s)
         /// </summary>
         public static Employees SelectByPrimaryKey(int employeeID)
         {
              Employees objEmployees = null;
              string storedProcName = "[dbo].[Employees_SelectByPrimaryKey]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@employeeID", employeeID);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objEmployees = CreateEmployeesFromDataRowShared(dt.Rows[0]);
                              }
                          }
                      }
                  }
              }

              return objEmployees;
         }

         /// <summary>
         /// Gets the total number of records in the Employees table
         /// </summary>
         public static int GetRecordCount()
         {
             return GetRecordCountShared("[dbo].[Employees_GetRecordCount]", null, null, true, null);
         }

         /// <summary>
         /// Gets the total number of records in the Employees table by ReportsTo
         /// </summary>
         public static int GetRecordCountByReportsTo(int reportsTo)
         {
             return GetRecordCountShared("[dbo].[Employees_GetRecordCountByReportsTo]", "reportsTo", reportsTo, true, null);
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
                          case "reportsTo":
                              command.Parameters.AddWithValue("@reportsTo", paramValue);
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
         /// Gets the total number of records in the Employees table based on search parameters
         /// </summary>
         public static int GetRecordCountDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
              int recordCount = 0;
              string storedProcName = "[dbo].[Employees_GetRecordCountWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);

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
         /// Selects Employees records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of rows)
         /// </summary>
         public static EmployeesCollection SelectSkipAndTake(string sortByExpression, int startRowIndex, int rows)
         {
             return SelectShared("[dbo].[Employees_SelectSkipAndTake]", null, null, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects records by ReportsTo as a collection (List) of Employees sorted by the sortByExpression and returns the rows (# of records) starting from the startRowIndex
         /// </summary>
         public static EmployeesCollection SelectSkipAndTakeByReportsTo(string sortByExpression, int startRowIndex, int rows, int reportsTo)
         {
             return SelectShared("[dbo].[Employees_SelectSkipAndTakeByReportsTo]", "reportsTo", reportsTo, true, null, sortByExpression, startRowIndex, rows);
         }

         /// <summary>
         /// Selects Employees records sorted by the sortByExpression and returns records from the startRowIndex with rows (# of records) based on search parameters
         /// </summary>
         public static EmployeesCollection SelectSkipAndTakeDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath, string sortByExpression, int startRowIndex, int rows)
         {
              EmployeesCollection objEmployeesCol = null;
              string storedProcName = "[dbo].[Employees_SelectSkipAndTakeWhereDynamic]";

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
                      AddSearchCommandParamsShared(command, employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objEmployeesCol = new EmployeesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Employees objEmployees = CreateEmployeesFromDataRowShared(dr);
                                      objEmployeesCol.Add(objEmployees);
                                  }
                              }
                          }
                      }
                  }
              }

              return objEmployeesCol;
         }

         /// <summary>
         /// Selects all Employees
         /// </summary>
         public static EmployeesCollection SelectAll()
         {
             return SelectShared("[dbo].[Employees_SelectAll]", String.Empty, null);
         }

         /// <summary>
         /// Selects records based on the passed filters as a collection (List) of Employees.
         /// </summary>
         public static EmployeesCollection SelectAllDynamicWhere(int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
              EmployeesCollection objEmployeesCol = null;
              string storedProcName = "[dbo].[Employees_SelectAllWhereDynamic]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // search parameters
                      AddSearchCommandParamsShared(command, employeeID, lastName, firstName, title, titleOfCourtesy, birthDate, hireDate, address, city, region1, postalCode, country, homePhone, extension, reportsTo, photoPath);

                      using (SqlDataAdapter da = new SqlDataAdapter(command))
                      {
                          DataTable dt = new DataTable();
                          da.Fill(dt);

                          if (dt != null)
                          {
                              if (dt.Rows.Count > 0)
                              {
                                  objEmployeesCol = new EmployeesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Employees objEmployees = CreateEmployeesFromDataRowShared(dr);
                                      objEmployeesCol.Add(objEmployees);
                                  }
                              }
                          }
                      }
                  }
              }

              return objEmployeesCol;
         }

         /// <summary>
         /// Selects all Employees by Employees, related to column ReportsTo
         /// </summary>
         public static EmployeesCollection SelectEmployeesCollectionByReportsTo(int employeeID)
         {
             return SelectShared("[dbo].[Employees_SelectAllByReportsTo]", "reportsTo", employeeID);
         }

         /// <summary>
         /// Selects EmployeeID and LastName columns for use with a DropDownList web control
         /// </summary>
         public static EmployeesCollection SelectEmployeesDropDownListData()
         {
              EmployeesCollection objEmployeesCol = null;
              string storedProcName = "[dbo].[Employees_SelectDropDownListData]";

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
                                  objEmployeesCol = new EmployeesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Employees objEmployees = new Employees();
                                      objEmployees.EmployeeID = (int)dr["EmployeeID"];
                                      objEmployees.LastName = (string)(dr["LastName"]);

                                      objEmployeesCol.Add(objEmployees);
                                  }
                              }
                          }
                      }
                  }
              }

              return objEmployeesCol;
         }

         public static EmployeesCollection SelectShared(string storedProcName, string param, object paramValue, bool isUseStoredProc = true, string dynamicSqlScript = null, string sortByExpression = null, int? startRowIndex = null, int? rows = null)
         {
              EmployeesCollection objEmployeesCol = null;

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
                          case "reportsTo":
                              command.Parameters.AddWithValue("@reportsTo", paramValue);
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
                                  objEmployeesCol = new EmployeesCollection();

                                  foreach (DataRow dr in dt.Rows)
                                  {
                                      Employees objEmployees = CreateEmployeesFromDataRowShared(dr);
                                      objEmployeesCol.Add(objEmployees);
                                  }
                              }
                          }
                      }
                  }
              }

              return objEmployeesCol;
         }

         /// <summary>
         /// Inserts a record
         /// </summary>
         public static int Insert(Employees objEmployees)
         {
             string storedProcName = "[dbo].[Employees_Insert]";
             return InsertUpdate(objEmployees, false, storedProcName);
         }

         /// <summary>
         /// Updates a record
         /// </summary>
         public static void Update(Employees objEmployees)
         {
             string storedProcName = "[dbo].[Employees_Update]";
             InsertUpdate(objEmployees, true, storedProcName);
         }

         private static int InsertUpdate(Employees objEmployees, bool isUpdate, string storedProcName)
         {
              int newlyCreatedEmployeeID = objEmployees.EmployeeID;

              object title = objEmployees.Title;
              object titleOfCourtesy = objEmployees.TitleOfCourtesy;
              object birthDate = objEmployees.BirthDate;
              object hireDate = objEmployees.HireDate;
              object address = objEmployees.Address;
              object city = objEmployees.City;
              object region1 = objEmployees.Region1;
              object postalCode = objEmployees.PostalCode;
              object country = objEmployees.Country;
              object homePhone = objEmployees.HomePhone;
              object extension = objEmployees.Extension;
              object notes = objEmployees.Notes;
              object reportsTo = objEmployees.ReportsTo;
              object photoPath = objEmployees.PhotoPath;

              if (String.IsNullOrEmpty(objEmployees.Title))
                  title = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.TitleOfCourtesy))
                  titleOfCourtesy = System.DBNull.Value;

              if (objEmployees.BirthDate == null)
                  birthDate = System.DBNull.Value;

              if (objEmployees.HireDate == null)
                  hireDate = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.Address))
                  address = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.City))
                  city = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.Region1))
                  region1 = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.PostalCode))
                  postalCode = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.Country))
                  country = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.HomePhone))
                  homePhone = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.Extension))
                  extension = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.Notes))
                  notes = System.DBNull.Value;

              if (objEmployees.ReportsTo == null)
                  reportsTo = System.DBNull.Value;

              if (String.IsNullOrEmpty(objEmployees.PhotoPath))
                  photoPath = System.DBNull.Value;

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
                          command.Parameters.AddWithValue("@employeeID", objEmployees.EmployeeID);
                      }

                      command.Parameters.AddWithValue("@lastName", objEmployees.LastName);
                      command.Parameters.AddWithValue("@firstName", objEmployees.FirstName);
                      command.Parameters.AddWithValue("@title", title);
                      command.Parameters.AddWithValue("@titleOfCourtesy", titleOfCourtesy);
                      command.Parameters.AddWithValue("@birthDate", birthDate);
                      command.Parameters.AddWithValue("@hireDate", hireDate);
                      command.Parameters.AddWithValue("@address", address);
                      command.Parameters.AddWithValue("@city", city);
                      command.Parameters.AddWithValue("@region1", region1);
                      command.Parameters.AddWithValue("@postalCode", postalCode);
                      command.Parameters.AddWithValue("@country", country);
                      command.Parameters.AddWithValue("@homePhone", homePhone);
                      command.Parameters.AddWithValue("@extension", extension);
                      command.Parameters.AddWithValue("@notes", notes);
                      command.Parameters.AddWithValue("@reportsTo", reportsTo);
                      command.Parameters.AddWithValue("@photoPath", photoPath);

                      if (isUpdate)
                          command.ExecuteNonQuery();
                      else
                          newlyCreatedEmployeeID = (int)command.ExecuteScalar();
                  }
              }

              return newlyCreatedEmployeeID;
         }

         /// <summary>
         /// Deletes a record based on primary key(s)
         /// </summary>
         public static void Delete(int employeeID)
         {
              string storedProcName = "[dbo].[Employees_Delete]";

              using (SqlConnection connection = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]))
              {
                  connection.Open();

                  using (SqlCommand command = new SqlCommand(storedProcName, connection))
                  {
                      command.CommandType = CommandType.StoredProcedure;

                      // parameters
                      command.Parameters.AddWithValue("@employeeID", employeeID);

                      // execute
                      command.ExecuteNonQuery();
                  }
              }
         }

         /// <summary>
         /// Adds search parameters to the Command object
         /// </summary>
         private static void AddSearchCommandParamsShared(SqlCommand command, int? employeeID, string lastName, string firstName, string title, string titleOfCourtesy, DateTime? birthDate, DateTime? hireDate, string address, string city, string region1, string postalCode, string country, string homePhone, string extension, int? reportsTo, string photoPath)
         {
              if(employeeID != null)
                  command.Parameters.AddWithValue("@employeeID", employeeID);
              else
                  command.Parameters.AddWithValue("@employeeID", System.DBNull.Value);

              if(!String.IsNullOrEmpty(lastName))
                  command.Parameters.AddWithValue("@lastName", lastName);
              else
                  command.Parameters.AddWithValue("@lastName", System.DBNull.Value);

              if(!String.IsNullOrEmpty(firstName))
                  command.Parameters.AddWithValue("@firstName", firstName);
              else
                  command.Parameters.AddWithValue("@firstName", System.DBNull.Value);

              if(!String.IsNullOrEmpty(title))
                  command.Parameters.AddWithValue("@title", title);
              else
                  command.Parameters.AddWithValue("@title", System.DBNull.Value);

              if(!String.IsNullOrEmpty(titleOfCourtesy))
                  command.Parameters.AddWithValue("@titleOfCourtesy", titleOfCourtesy);
              else
                  command.Parameters.AddWithValue("@titleOfCourtesy", System.DBNull.Value);

              if(birthDate != null)
                  command.Parameters.AddWithValue("@birthDate", birthDate);
              else
                  command.Parameters.AddWithValue("@birthDate", System.DBNull.Value);

              if(hireDate != null)
                  command.Parameters.AddWithValue("@hireDate", hireDate);
              else
                  command.Parameters.AddWithValue("@hireDate", System.DBNull.Value);

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

              if(!String.IsNullOrEmpty(homePhone))
                  command.Parameters.AddWithValue("@homePhone", homePhone);
              else
                  command.Parameters.AddWithValue("@homePhone", System.DBNull.Value);

              if(!String.IsNullOrEmpty(extension))
                  command.Parameters.AddWithValue("@extension", extension);
              else
                  command.Parameters.AddWithValue("@extension", System.DBNull.Value);

              if(reportsTo != null)
                  command.Parameters.AddWithValue("@reportsTo", reportsTo);
              else
                  command.Parameters.AddWithValue("@reportsTo", System.DBNull.Value);

              if(!String.IsNullOrEmpty(photoPath))
                  command.Parameters.AddWithValue("@photoPath", photoPath);
              else
                  command.Parameters.AddWithValue("@photoPath", System.DBNull.Value);

         }

         /// <summary>
         /// Creates a Employees object from the passed data row
         /// </summary>
         private static Employees CreateEmployeesFromDataRowShared(DataRow dr)
         {
             Employees objEmployees = new Employees();

             objEmployees.EmployeeID = (int)dr["EmployeeID"];
             objEmployees.LastName = dr["LastName"].ToString();
             objEmployees.FirstName = dr["FirstName"].ToString();

             if (dr["Title"] != System.DBNull.Value)
                 objEmployees.Title = dr["Title"].ToString();
             else
                 objEmployees.Title = null;

             if (dr["TitleOfCourtesy"] != System.DBNull.Value)
                 objEmployees.TitleOfCourtesy = dr["TitleOfCourtesy"].ToString();
             else
                 objEmployees.TitleOfCourtesy = null;

             if (dr["BirthDate"] != System.DBNull.Value)
                 objEmployees.BirthDate = (DateTime)dr["BirthDate"];
             else
                 objEmployees.BirthDate = null;

             if (dr["HireDate"] != System.DBNull.Value)
                 objEmployees.HireDate = (DateTime)dr["HireDate"];
             else
                 objEmployees.HireDate = null;

             if (dr["Address"] != System.DBNull.Value)
                 objEmployees.Address = dr["Address"].ToString();
             else
                 objEmployees.Address = null;

             if (dr["City"] != System.DBNull.Value)
                 objEmployees.City = dr["City"].ToString();
             else
                 objEmployees.City = null;

             if (dr["Region1"] != System.DBNull.Value)
                 objEmployees.Region1 = dr["Region1"].ToString();
             else
                 objEmployees.Region1 = null;

             if (dr["PostalCode"] != System.DBNull.Value)
                 objEmployees.PostalCode = dr["PostalCode"].ToString();
             else
                 objEmployees.PostalCode = null;

             if (dr["Country"] != System.DBNull.Value)
                 objEmployees.Country = dr["Country"].ToString();
             else
                 objEmployees.Country = null;

             if (dr["HomePhone"] != System.DBNull.Value)
                 objEmployees.HomePhone = dr["HomePhone"].ToString();
             else
                 objEmployees.HomePhone = null;

             if (dr["Extension"] != System.DBNull.Value)
                 objEmployees.Extension = dr["Extension"].ToString();
             else
                 objEmployees.Extension = null;

             if (dr["Notes"] != System.DBNull.Value)
                 objEmployees.Notes = dr["Notes"].ToString();
             else
                 objEmployees.Notes = null;

             if (dr["ReportsTo"] != System.DBNull.Value)
                 objEmployees.ReportsTo = (int)dr["ReportsTo"];
             else
                 objEmployees.ReportsTo = null;


             if (dr["PhotoPath"] != System.DBNull.Value)
                 objEmployees.PhotoPath = dr["PhotoPath"].ToString();
             else
                 objEmployees.PhotoPath = null;

             return objEmployees;
         }
     }
}

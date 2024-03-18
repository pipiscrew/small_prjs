using System;
using System.Text;
using System.Linq;
using System.Web.Mvc;
using north;
using north.BusinessObject;
using north.Models;
using north.ViewModels;
using north.Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;

namespace north.Controllers.Base
{ 
     /// <summary>
     /// Base class for OrdersController.  Do not make changes to this class,
     /// instead, put additional code in the OrdersController class 
     /// </summary>
     public class OrdersControllerBase : Controller
     { 

         /// <summary>
         /// GET: /Orders/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /Orders/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(OrdersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditOrders(viewModel, CrudOperation.Add);

                     if (Url.IsLocalUrl(returnUrl))
                         return Redirect(returnUrl);
                     else
                         return RedirectToAction("Index");
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             // if we got this far, something failed, redisplay form
             return GetAddViewModel();
         }

         private ActionResult GetAddViewModel()
         {
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "Orders";
             viewModel.ViewActionName = "Add";
             viewModel.CustomersDropDownListData = GetCustomersDropDownListData();
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();
             viewModel.ShippersDropDownListData = GetShippersDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Orders/Update/5
         /// </summary>
         public ActionResult Update(int id)
         {
             return GetUpdateViewModel(id);
         }

         /// <summary>
         /// POST: /Orders/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int id, OrdersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditOrders(viewModel, CrudOperation.Update);

                     if (Url.IsLocalUrl(returnUrl))
                         return Redirect(returnUrl);
                     else
                         return RedirectToAction("Index");
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             // if we got this far, something failed, redisplay form
             return GetUpdateViewModel(id);
         }

         public ActionResult GetUpdateViewModel(int id)
         {
             Models.OrdersModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "Orders";
             viewModel.ViewActionName = "Update";
             viewModel.CustomersDropDownListData = GetCustomersDropDownListData();
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();
             viewModel.ShippersDropDownListData = GetShippersDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /Orders/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int id, OrdersViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/OrdersApi/Delete/" + id).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /Orders/Details/5
         /// </summary>
         public ActionResult Details(int id)
         {
             Models.OrdersModel model = GetModelByPrimaryKey(id);

             // assign values to the view model
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = model;
             viewModel.CustomersDropDownListData = GetCustomersDropDownListData();
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();
             viewModel.ShippersDropDownListData = GetShippersDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Orders/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Orders/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, OrdersViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditOrders(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Orders/ListTotals
         /// </summary>
         public ActionResult ListTotals()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /Orders/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /Orders/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.OrdersModel model)
         {
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = model;

             AddEditOrders(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /Orders/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.OrdersModel model)
         {
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = model;

             AddEditOrders(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /Orders/ListForeach
         /// </summary>
         public ActionResult ListForeach(string sidx, string sord, int? page)
         {
             int rows = Convert.ToInt32(ConfigurationManager.AppSettings["GridNumberOfRows"]);
             int numberOfPagesToShow = Convert.ToInt32(ConfigurationManager.AppSettings["GridNumberOfPagesToShow"]);
             int currentPage = page == null ? 1 : Convert.ToInt32(page);
             int startRowIndex = ((currentPage * rows) - rows) + 1;
             int totalRecords = 0;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/OrdersApi/GetRecordCount/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     totalRecords = JsonConvert.DeserializeObject<int>(responseBody);
                 }
             }

             int totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
             OrdersCollection objOrdersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objOrdersCol = JsonConvert.DeserializeObject<OrdersCollection>(responseBody);
                 }
             }

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"OrderID", "Order ID"},
                 {"CustomerID", "Customer ID"},
                 {"EmployeeID", "Employee ID"},
                 {"OrderDate", "Order Date"},
                 {"RequiredDate", "Required Date"},
                 {"ShippedDate", "Shipped Date"},
                 {"ShipVia", "Ship Via"},
                 {"Freight", "Freight"},
                 {"ShipName", "Ship Name"},
                 {"ShipAddress", "Ship Address"},
                 {"ShipCity", "Ship City"},
                 {"ShipRegion", "Ship Region"},
                 {"ShipPostalCode", "Ship Postal Code"},
                 {"ShipCountry", "Ship Country"}
             };

             // view model
             OrdersForeachViewModel viewModel = new OrdersForeachViewModel();
             viewModel.OrdersData = objOrdersCol;
             viewModel.OrdersFieldNames = fieldNames;
             viewModel.TotalPages = totalPages;
             viewModel.CurrentPage = currentPage;
             viewModel.FieldToSort = String.IsNullOrEmpty(sidx) ? "OrderID" : sidx;
             viewModel.FieldSortOrder = String.IsNullOrEmpty(sord) ? "asc" : sord;
             viewModel.FieldToSortWithOrder = String.IsNullOrEmpty(sidx) ? "OrderID" : (sidx + " " + sord).Trim();
             viewModel.NumberOfPagesToShow = numberOfPagesToShow;
             viewModel.StartPage = Functions.GetPagerStartPage(currentPage, numberOfPagesToShow, totalPages);
             viewModel.EndPage = Functions.GetPagerEndPage(viewModel.StartPage, currentPage, numberOfPagesToShow, totalPages);

             return View(viewModel);
         }

         /// <summary>
         /// GET: /Orders/ListMasterDetailGridByCustomerID
         /// </summary>
         public ActionResult ListMasterDetailGridByCustomerID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListMasterDetailGridByEmployeeID
         /// </summary>
         public ActionResult ListMasterDetailGridByEmployeeID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListMasterDetailGridByShipVia
         /// </summary>
         public ActionResult ListMasterDetailGridByShipVia()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListMasterDetailSubGridByCustomerID
         /// </summary>
         public ActionResult ListMasterDetailSubGridByCustomerID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListMasterDetailSubGridByEmployeeID
         /// </summary>
         public ActionResult ListMasterDetailSubGridByEmployeeID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListMasterDetailSubGridByShipVia
         /// </summary>
         public ActionResult ListMasterDetailSubGridByShipVia()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /Orders/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(OrdersViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 // do something here before redirecting

                 if (Url.IsLocalUrl(returnUrl))
                     return Redirect(returnUrl);
                 else
                     return RedirectToAction("/");
             }

             // if we got this far, something failed, redisplay form
             return View(GetUnboundViewModel());
         }

         private OrdersViewModel GetUnboundViewModel()
         {
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = null;
             viewModel.ViewControllerName = "Orders";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /Orders/AddEditOrders
         /// </summary>
         private void AddEditOrders(OrdersViewModel viewModel, CrudOperation operation)
         {
             Models.OrdersModel model = viewModel.OrdersModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/OrdersApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/OrdersApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private OrdersViewModel GetViewModel()
         {
             OrdersViewModel viewModel = new OrdersViewModel();
             viewModel.OrdersModel = null;
             viewModel.CustomersDropDownListData = GetCustomersDropDownListData();
             viewModel.EmployeesDropDownListData = GetEmployeesDropDownListData();
             viewModel.ShippersDropDownListData = GetShippersDropDownListData();

             return viewModel;
         }

         /// <summary>
         /// GET: /Orders/ListGroupedByCustomerID
         /// </summary>
         public ActionResult ListGroupedByCustomerID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListGroupedByEmployeeID
         /// </summary>
         public ActionResult ListGroupedByEmployeeID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListGroupedByShipVia
         /// </summary>
         public ActionResult ListGroupedByShipVia()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListTotalsGroupedByCustomerID
         /// </summary>
         public ActionResult ListTotalsGroupedByCustomerID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListTotalsGroupedByEmployeeID
         /// </summary>
         public ActionResult ListTotalsGroupedByEmployeeID()
         {
             return View();
         }

         /// <summary>
         /// GET: /Orders/ListTotalsGroupedByShipVia
         /// </summary>
         public ActionResult ListTotalsGroupedByShipVia()
         {
             return View();
         }

         private Models.OrdersModel GetModelByPrimaryKey(int orderID)
         {
             Models.OrdersModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectByPrimaryKey/" + orderID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     Orders objOrders = JsonConvert.DeserializeObject<Orders>(responseBody);

                     // assign values to the model
                     model = new Models.OrdersModel();
                     model.OrderID = objOrders.OrderID;
                     model.CustomerID = objOrders.CustomerID;
                     model.EmployeeID = objOrders.EmployeeID;
                     model.OrderDate = objOrders.OrderDate;
                     model.RequiredDate = objOrders.RequiredDate;
                     model.ShippedDate = objOrders.ShippedDate;
                     model.ShipVia = objOrders.ShipVia;
                     model.Freight = objOrders.Freight;
                     model.ShipName = objOrders.ShipName;
                     model.ShipAddress = objOrders.ShipAddress;
                     model.ShipCity = objOrders.ShipCity;
                     model.ShipRegion = objOrders.ShipRegion;
                     model.ShipPostalCode = objOrders.ShipPostalCode;
                     model.ShipCountry = objOrders.ShipCountry;
                 }
             }

             return model;
         }

         private CustomersCollection GetCustomersDropDownListData()
         {
             CustomersCollection objCustomersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/CustomersApi/SelectCustomersDropDownListData/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objCustomersCol = JsonConvert.DeserializeObject<CustomersCollection>(responseBody);
                 }
             }

             return objCustomersCol;
         }

         private EmployeesCollection GetEmployeesDropDownListData()
         {
             EmployeesCollection objEmployeesCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/EmployeesApi/SelectEmployeesDropDownListData/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objEmployeesCol = JsonConvert.DeserializeObject<EmployeesCollection>(responseBody);
                 }
             }

             return objEmployeesCol;
         }

         private ShippersCollection GetShippersDropDownListData()
         {
             ShippersCollection objShippersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/ShippersApi/SelectShippersDropDownListData/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objShippersCol = JsonConvert.DeserializeObject<ShippersCollection>(responseBody);
                 }
             }

             return objShippersCol;
         }
     } 
} 

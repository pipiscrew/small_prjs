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
     /// Base class for OrderDetailsController.  Do not make changes to this class,
     /// instead, put additional code in the OrderDetailsController class 
     /// </summary>
     public class OrderDetailsControllerBase : Controller
     { 

         /// <summary>
         /// GET: /OrderDetails/
         /// </summary>
         public ActionResult Index()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/Add
         /// </summary>
         public ActionResult Add()
         {
             return GetAddViewModel();
         }

         /// <summary>
         /// POST: /OrderDetails/Add
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Add(OrderDetailsViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // add new record
                     AddEditOrderDetails(viewModel, CrudOperation.Add);

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
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = null;
             viewModel.Operation = CrudOperation.Add;
             viewModel.ViewControllerName = "OrderDetails";
             viewModel.ViewActionName = "Add";
             viewModel.OrdersDropDownListData = GetOrdersDropDownListData();
             viewModel.ProductsDropDownListData = GetProductsDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /OrderDetails/Update/5
         /// </summary>
         public ActionResult Update(int orderID, int productID)
         {
             return GetUpdateViewModel(orderID, productID);
         }

         /// <summary>
         /// POST: /OrderDetails/Update/5
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Update(int orderID, int productID, OrderDetailsViewModel viewModel, string returnUrl)
         {
             if (ModelState.IsValid)
             {
                 try
                 {
                     // update record
                     AddEditOrderDetails(viewModel, CrudOperation.Update);

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
             return GetUpdateViewModel(orderID, productID);
         }

         public ActionResult GetUpdateViewModel(int orderID, int productID)
         {
             Models.OrderDetailsModel model = GetModelByPrimaryKey(orderID, productID);

             // assign values to the view model
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = model;
             viewModel.Operation = CrudOperation.Update;
             viewModel.ViewControllerName = "OrderDetails";
             viewModel.ViewActionName = "Update";
             viewModel.OrdersDropDownListData = GetOrdersDropDownListData();
             viewModel.ProductsDropDownListData = GetProductsDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// POST: /OrderDetails/Delete/5
         /// </summary>
         [HttpPost]
         public ActionResult Delete(int orderID, int productID, OrderDetailsViewModel viewModel, string returnUrl)
         {
             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.DeleteAsync("api/OrderDetailsApi/Delete/?" + "orderID=" + orderID + "&productID=" + productID).Result;

                 if (!response.IsSuccessStatusCode)
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());

                 return Json(true);
             }
         }

         /// <summary>
         /// GET: /OrderDetails/Details/5
         /// </summary>
         public ActionResult Details(int orderID, int productID)
         {
             Models.OrderDetailsModel model = GetModelByPrimaryKey(orderID, productID);

             // assign values to the view model
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = model;
             viewModel.OrdersDropDownListData = GetOrdersDropDownListData();
             viewModel.ProductsDropDownListData = GetProductsDropDownListData();

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "Index";

             return View(viewModel);
         }

         /// <summary>
         /// GET: /OrderDetails/ListCrudRedirect
         /// </summary>
         public ActionResult ListCrudRedirect()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListReadOnly
         /// </summary>
         public ActionResult ListReadOnly()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListCrud
         /// </summary>
         public ActionResult ListCrud()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /OrderDetails/ListCrud
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult ListCrud(string inputSubmit, OrderDetailsViewModel viewModel)
         {
             if (ModelState.IsValid)
             {
                 CrudOperation operation = CrudOperation.Add;

                 if (inputSubmit == "Update")
                     operation = CrudOperation.Update;

                 try
                 {
                     AddEditOrderDetails(viewModel, operation);
                 }
                 catch(Exception ex)
                 {
                     ModelState.AddModelError("", ex.Message);
                 }
             }

             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /OrderDetails/ListTotals
         /// </summary>
         public ActionResult ListTotals()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListSearch
         /// </summary>
         public ActionResult ListSearch()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// GET: /OrderDetails/ListScrollLoad
         /// </summary>
         public ActionResult ListScrollLoad()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListInline
         /// </summary>
         public ActionResult ListInline()
         {
             return View(GetViewModel());
         }

         /// <summary>
         /// POST: /OrderDetails/ListInlineAdd
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineAdd(Models.OrderDetailsModel model)
         {
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = model;

             AddEditOrderDetails(viewModel, CrudOperation.Add);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// POST: /OrderDetails/ListInlineUpdate
         /// </summary>
         [HttpPost]
         public ActionResult ListInlineUpdate(Models.OrderDetailsModel model)
         {
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = model;

             AddEditOrderDetails(viewModel, CrudOperation.Update);
             return Json("", JsonRequestBehavior.AllowGet);
         }

         /// <summary>
         /// GET: /OrderDetails/ListForeach
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
                 HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/GetRecordCount/").Result;

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
             OrderDetailsCollection objOrderDetailsCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/SelectSkipAndTake/?rows=" + rows + "&startRowIndex=" + startRowIndex + "&sortByExpression=" + sidx + " " + sord).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objOrderDetailsCol = JsonConvert.DeserializeObject<OrderDetailsCollection>(responseBody);
                 }
             }

             // fields and titles
             string[,] fieldNames = new string[,] {
                 {"OrderID", "Order ID"},
                 {"ProductID", "Product ID"},
                 {"UnitPrice", "Unit Price"},
                 {"Quantity", "Quantity"},
                 {"Discount", "Discount"}
             };

             // view model
             OrderDetailsForeachViewModel viewModel = new OrderDetailsForeachViewModel();
             viewModel.OrderDetailsData = objOrderDetailsCol;
             viewModel.OrderDetailsFieldNames = fieldNames;
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
         /// GET: /OrderDetails/ListMasterDetailGridByOrderID
         /// </summary>
         public ActionResult ListMasterDetailGridByOrderID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListMasterDetailGridByProductID
         /// </summary>
         public ActionResult ListMasterDetailGridByProductID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListMasterDetailSubGridByOrderID
         /// </summary>
         public ActionResult ListMasterDetailSubGridByOrderID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListMasterDetailSubGridByProductID
         /// </summary>
         public ActionResult ListMasterDetailSubGridByProductID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/Unbound
         /// </summary>
         public ActionResult Unbound()
         {
             return View(GetUnboundViewModel());
         }

         /// <summary>
         /// POST: /OrderDetails/Unbound
         /// </summary>
         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult Unbound(OrderDetailsViewModel viewModel, string returnUrl)
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

         private OrderDetailsViewModel GetUnboundViewModel()
         {
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = null;
             viewModel.ViewControllerName = "OrderDetails";

             if (Request.UrlReferrer != null)
                 viewModel.ViewReturnUrl = Request.UrlReferrer.PathAndQuery;
             else
                 viewModel.ViewReturnUrl = "/";

             return viewModel;
         }

         /// <summary>
         /// GET: /OrderDetails/AddEditOrderDetails
         /// </summary>
         private void AddEditOrderDetails(OrderDetailsViewModel viewModel, CrudOperation operation)
         {
             Models.OrderDetailsModel model = viewModel.OrderDetailsModel;
             string serializedModel = JsonConvert.SerializeObject(model);

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response;

                 if (operation == CrudOperation.Add)
                     response = client.PostAsync("api/OrderDetailsApi/Insert", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;
                 else
                     response = client.PostAsync("api/OrderDetailsApi/Update", new StringContent(serializedModel, Encoding.UTF8, "application/json")).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
             }
         }

         private OrderDetailsViewModel GetViewModel()
         {
             OrderDetailsViewModel viewModel = new OrderDetailsViewModel();
             viewModel.OrderDetailsModel = null;
             viewModel.OrdersDropDownListData = GetOrdersDropDownListData();
             viewModel.ProductsDropDownListData = GetProductsDropDownListData();

             return viewModel;
         }

         /// <summary>
         /// GET: /OrderDetails/ListGroupedByOrderID
         /// </summary>
         public ActionResult ListGroupedByOrderID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListGroupedByProductID
         /// </summary>
         public ActionResult ListGroupedByProductID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListTotalsGroupedByOrderID
         /// </summary>
         public ActionResult ListTotalsGroupedByOrderID()
         {
             return View();
         }

         /// <summary>
         /// GET: /OrderDetails/ListTotalsGroupedByProductID
         /// </summary>
         public ActionResult ListTotalsGroupedByProductID()
         {
             return View();
         }

         private Models.OrderDetailsModel GetModelByPrimaryKey(int orderID, int productID)
         {
             Models.OrderDetailsModel model = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/OrderDetailsApi/SelectByPrimaryKey/?" + "orderID=" + orderID + "&productID=" + productID).Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     OrderDetails objOrderDetails = JsonConvert.DeserializeObject<OrderDetails>(responseBody);

                     // assign values to the model
                     model = new Models.OrderDetailsModel();
                     model.OrderID = objOrderDetails.OrderID;
                     model.ProductID = objOrderDetails.ProductID;
                     model.UnitPrice = objOrderDetails.UnitPrice;
                     model.Quantity = objOrderDetails.Quantity;
                     model.Discount = objOrderDetails.Discount;
                 }
             }

             return model;
         }

         private OrdersCollection GetOrdersDropDownListData()
         {
             OrdersCollection objOrdersCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/OrdersApi/SelectOrdersDropDownListData/").Result;

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

             return objOrdersCol;
         }

         private ProductsCollection GetProductsDropDownListData()
         {
             ProductsCollection objProductsCol = null;

             using (var client = new HttpClient())
             {
                 client.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiBaseAddress"]);
                 HttpResponseMessage response = client.GetAsync("api/ProductsApi/SelectProductsDropDownListData/").Result;

                 if (!response.IsSuccessStatusCode)
                 {
                     throw new Exception("Error Status Code: " + response.StatusCode.ToString() + " Error Reason: " + response.ReasonPhrase + " Error Message: " + response.RequestMessage.ToString());
                 }
                 else
                 {
                     var responseBody = response.Content.ReadAsStringAsync().Result;
                     objProductsCol = JsonConvert.DeserializeObject<ProductsCollection>(responseBody);
                 }
             }

             return objProductsCol;
         }
     } 
} 

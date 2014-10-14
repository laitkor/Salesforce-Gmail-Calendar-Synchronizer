using SalesforceEventSync.WebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using System.Threading;
using System.IO;
using Google.Apis.Util.Store;


namespace SalesforceEventSync.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            //var calendarList = 
                getGoogleEventList();

            //SforceService objSalesforceService = SFAuthenticate();

            //foreach (var CalEvent in calendarList)
            //{
            //    WebReference.Event objEvent = new WebReference.Event();

            //    objEvent.Subject = "Test Calendar Event New";
            //    objEvent.Description = "Test Calendar Event New";
            //    objEvent.Location = "India";
            //    objEvent.StartDateTimeSpecified = true;
            //    objEvent.StartDateTime = new DateTime(2014, 10, 15, 18, 0, 0);
            //    objEvent.EndDateTimeSpecified = true;
            //    objEvent.EndDateTime = new DateTime(2014, 10, 15, 18, 0, 0).AddHours(2);

            //    SaveResult[] objSaveResult = objSalesforceService.create(new WebReference.Event[] { objEvent });
            //}
            //objSalesforceService.logout();
            
            return View();
        }


        private SforceService SFAuthenticate()
        {
            SforceService objSalesService = new SforceService();
            objSalesService.Timeout = 60000;

            // Initialize salesforce service

            string userName = "raina.dilawari@laitkor.com";
            string password = "raina@laitkor09UaLZ3mVKN8gYjFi4kYt1DFpxI";

            LoginResult objLoginResult = objSalesService.login(userName, password);
            objSalesService.Url = objLoginResult.serverUrl;
            objSalesService.SessionHeaderValue = new SessionHeader();
            objSalesService.SessionHeaderValue.sessionId = objLoginResult.sessionId;
            GetUserInfoResult objUserInfo = objLoginResult.userInfo;

            return objSalesService;
        }

        private EventsResource getGoogleEventList()
        {
            UserCredential credential;
            //using (var fs = new FileStream(Server.MapPath("~/Content/client_secret_48938331338-v8lgvsugfbpjujbonvgt7kddp4e20ce0.apps.googleusercontent.com.json"), FileMode.Open, FileAccess.Read))
            //{
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        //GoogleClientSecrets.Load(fs).Secrets,
                        new ClientSecrets
                        {
                            ClientId = "48938331338-v8lgvsugfbpjujbonvgt7kddp4e20ce0.apps.googleusercontent.com",
                            ClientSecret = "jczdSaVUrQsGAY4I10JaO55w"
                        },
                        new[] { CalendarService.Scope.Calendar },
                        "user",
                        CancellationToken.None
                        //new FileDataStore("Calendar.Auth.store")
                        ).Result;
            //}
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Calendar API sample"
            });

            //Events objEvent = new Events();
            
            

            return service.Events;
        }

        public ActionResult EventConfirm()
        {
            return View();
        }
    }
}

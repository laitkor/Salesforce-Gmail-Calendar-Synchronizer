using SalesforceEventSync.WebReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesforceEventSync.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()
        {
            SforceService objSalesforceService = SFAuthenticate();

            Event objEvent = new Event();

            objEvent.Subject = "Test Calendar Event New";
            objEvent.Description = "Test Calendar Event New";
            objEvent.Location = "India";
            objEvent.StartDateTimeSpecified = true;
            objEvent.StartDateTime = new DateTime(2014, 10, 15, 18, 0, 0);
            objEvent.EndDateTimeSpecified = true;
            objEvent.EndDateTime = new DateTime(2014, 10, 15, 18, 0, 0).AddHours(2);

            SaveResult[] objSaveResult = objSalesforceService.create(new Event[] { objEvent });

            objSalesforceService.logout();
            
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

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SalesforceEventSync.Controllers;
using System.Threading;
using SalesforceEventSync.Models;

namespace GoogleSalesforceEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public async Task<ActionResult> CalendarAsync(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppAuthFlowMetadata()).AuthorizeAsync(cancellationToken);

            if (result.Credential == null)
                return new RedirectResult(result.RedirectUri);

            var calendarService = new CalendarService(new BaseClientService.Initializer
            {
                HttpClientInitializer = result.Credential,
                ApplicationName = "Calendar API sample"
            });

            var listRes = calendarService.CalendarList.List();

            var lst = await listRes.ExecuteAsync();

            var calItems = (from cal in lst.Items
                            select new CalendarModel
                            {
                                Id = cal.Id,
                                Description = cal.Description
                            });

           
            List<EventModel> evtLst = new List<EventModel>();
            foreach (var cal in calItems)
            {                
                var listEvent = await calendarService.Events.List(cal.Id).ExecuteAsync();

                var eventItems = (from evnt in listEvent.Items
                                  select new EventModel
                                  {
                                      Id = evnt.Id,
                                      Summary = evnt.Summary,
                                      Description = evnt.Description,
                                      StartDate = evnt.Start.Date,
                                      StartTime = evnt.Start.DateTime,
                                      IsEndTime = !evnt.EndTimeUnspecified,
                                      EndDate = evnt.End.Date,
                                      EndTime = evnt.End.DateTime,
                                      Location = evnt.Location,
                                      IsAllDay = (evnt.Start.Date != null && evnt.Start.DateTime == null) ? true : false
                                  });

                evtLst = evtLst.Union(eventItems).ToList();
            }

            bool IsAdded = new Salesforce().AddEvent(evtLst);

            if (IsAdded)
                TempData["Msg"] = "Events Added Succesfully";
            else
                TempData["Msg"] = "Events cannot be added";
            return View();
        }
    }
}

using GoogleSalesforceEvents.WebReference;
using SalesforceEventSync.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoogleSalesforceEvents.Controllers
{
    public class Salesforce
    {
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

        public bool AddEvent(List<EventModel> lstEvent)
        {
            SforceService objSalesforceService = SFAuthenticate();

            foreach (var CalEvent in lstEvent)
            {
                WebReference.Event objEvent = new WebReference.Event();

                objEvent.Subject = CalEvent.Summary;
                objEvent.Description = CalEvent.Description;
                objEvent.Location = CalEvent.Location;
                objEvent.StartDateTimeSpecified = true;
                objEvent.StartDateTime = CalEvent.IsAllDay ? Convert.ToDateTime(CalEvent.StartDate) : CalEvent.StartTime;
                objEvent.EndDateTimeSpecified = !CalEvent.IsAllDay;
                objEvent.EndDateTime = CalEvent.IsAllDay ? null : CalEvent.EndTime;
                objEvent.IsAllDayEventSpecified = CalEvent.IsAllDay;
                objEvent.IsAllDayEvent = CalEvent.IsAllDay;

                SaveResult[] objSaveResult = objSalesforceService.create(new WebReference.Event[] { objEvent });
            }
            objSalesforceService.logout();

            return true;
        }
    }
}
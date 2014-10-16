using Google.Apis.Calendar.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesforceEventSync.Models
{
    public class CalendarModel
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class EventModel
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public DateTime? StartTime { get; set; }
        public bool? IsEndTime { get; set; }
        public string EndDate { get; set; }
        public DateTime? EndTime { get; set; }
        public string Location { get; set; }
        public bool IsAllDay { get; set; }
    }
}
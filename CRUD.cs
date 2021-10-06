using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ConsoleGoogleCalendar;

namespace ConsoleGoogleCalendar
{
    class CRUD
    {
        //Create
        public static bool CreateEvent(CalendarService service, DateTime startTime, string[] details, DateTime endTime)
        {
            bool success = false;
            var ev = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = startTime;

            EventDateTime end = new EventDateTime();
            end.DateTime = endTime;

            ev.Start = start;
            ev.End = end;
            ev.Summary = details[0];
            ev.Description = details[1];
            ev.ColorId = UserInput.ReturnColorID(details[2]).ToString();
            ev.Location = details[3];

            try
            {

            Event testEvent = service.Events.Insert(ev, "primary").Execute();
                success = true;
            Console.WriteLine("Created");
            }
            catch(Exception error)
            {
                Console.WriteLine(error);
                success = false;
            }
            return success;
        }

        //Read
        public static void ReadEvents(CalendarService service)
        {
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Events events = request.Execute();
            Console.WriteLine("Upcoming events:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }
        }
        //Update
        //Delete
    }
}

using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Globalization;

namespace JikApp.Data.Calendar
{
    public class CalendarService
    {
        static PersianCalendar pc = new PersianCalendar();

        public List<Event> GetDayEvents(DateTime _reqDate)
        {
            var db = EF_Model.dbContext;
            return db.Events.Where(d => d.DateTime.Date == _reqDate.Date).ToList();
        }

        // TODO: Get persian month events
        public List<Event> GetMonthEvents(DateTime _reqDate)
        {
            int _reqPer = pc.GetMonth(_reqDate);
            var db = EF_Model.dbContext;
            return db.Events.AsEnumerable().Where(d => pc.GetMonth(d.DateTime) == _reqPer).OrderBy(d => d.DateTime).ToList();
        }

        // TODO: Get persian year events
        public List<Event> GetYearEvents(DateTime _reqDate)
        {
            int _reqPer = pc.GetYear(_reqDate);
            var db = EF_Model.dbContext;
            return db.Events.Where(d => pc.GetYear(d.DateTime) == _reqPer).ToList();
        }

        public async Task<Task> AddDayEventsAsync(DateTime _reqDate, string _title, TYPE eventType)
        {
            var db = EF_Model.dbContext;
            db.Events.Add(new Event()
            {
                Title = _title,
                DateTime = _reqDate.Date,
                EventType = eventType
            });
            await db.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}

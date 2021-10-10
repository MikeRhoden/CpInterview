using CpInterview.Models;
using System.Collections.Generic;

namespace CpInterview.Manager
{
  public interface ICalendarManager
  {
    IList<CalendarEvent> RetrieveEvents();
    void AddEvent(CalendarEvent calendarEventEntity);
  }
}
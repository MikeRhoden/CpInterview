using CpInterview.DataEntities;
using System.Collections.Generic;

namespace CpInterview.Manager
{
  public interface IApiAccessor
  {
    IList<CalendarEventReadEntity> RetrieveCalendarEvents();
    void AddEvent(CalendarEventWriteEntity calendarEventToWrite);
  }
}
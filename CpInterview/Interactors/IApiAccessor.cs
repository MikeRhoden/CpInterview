using CpInterview.DataEntities;
using System.Collections.Generic;

namespace CpInterview.Interactors
{
  public interface IApiAccessor
  {
    IList<CalendarEventEntity> RetrieveCalendarEvents();
  }
}
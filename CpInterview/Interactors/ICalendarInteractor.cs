using System.Collections.Generic;

namespace CpInterview.Interactors
{
  public interface ICalendarInteractor
  {
    IList<CalendarEvent> RetrieveEvents();
  }
}
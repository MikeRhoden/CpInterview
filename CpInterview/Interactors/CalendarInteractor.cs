using CpInterview.DataEntities;
using System.Collections.Generic;

namespace CpInterview.Interactors
{
  public class CalendarInteractor : ICalendarInteractor
  {
    private IApiAccessor apiAccessor;

    public CalendarInteractor(IApiAccessor apiAccessor)
    {
      this.apiAccessor = apiAccessor;
    }

    public IList<CalendarEvent> RetrieveEvents()
    {
      IList<CalendarEventEntity> calendarEventEntities = apiAccessor.RetrieveCalendarEvents();
      return MapToCalendarEvents(calendarEventEntities);
    }

    private IList<CalendarEvent> MapToCalendarEvents(IList<CalendarEventEntity> calendarEventEntities)
    {
      var calendarEvents = new List<CalendarEvent>();
      foreach (var calendarEventEntity in calendarEventEntities)
      {
        calendarEvents.Add(
          new CalendarEvent()
          {
            Id = calendarEventEntity.Id,
            Title = calendarEventEntity.Title,
            Description = calendarEventEntity.Description,
            StartDate = calendarEventEntity.StartDate.ToLocalTime(),
            EndDate = calendarEventEntity.EndDate.ToLocalTime()
          }
        );
      }
      return calendarEvents;
    }
  }
}
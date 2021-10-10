using CpInterview.DataEntities;
using CpInterview.Models;
using System.Collections.Generic;

namespace CpInterview.Manager
{
  public class CalendarManager : ICalendarManager
  {
    private IApiAccessor apiAccessor;

    public CalendarManager(IApiAccessor apiAccessor)
    {
      this.apiAccessor = apiAccessor;
    }

    public void AddEvent(CalendarEvent calendarEvent)
    {
      //TODO check for calendar conflict
      //TODO make sure end time is after start time
      CalendarEventWriteEntity calendarEventToWrite = MapCalendarEventForWriting(calendarEvent);
      apiAccessor.AddEvent(calendarEventToWrite);
    }

    private CalendarEventWriteEntity MapCalendarEventForWriting(CalendarEvent calendarEvent)
    {
      return new CalendarEventWriteEntity()
      {
        Title = calendarEvent.Title,
        Description = calendarEvent.Description,
        StartDate = calendarEvent.StartDate.ToUniversalTime(),
        EndDate = calendarEvent.EndDate.ToUniversalTime()
      };
    }

    public IList<CalendarEvent> RetrieveEvents()
    {
      IList<CalendarEventReadEntity> calendarEventEntities = apiAccessor.RetrieveCalendarEvents();
      return MapToCalendarEvents(calendarEventEntities);
    }

    private IList<CalendarEvent> MapToCalendarEvents(IList<CalendarEventReadEntity> calendarEventEntities)
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
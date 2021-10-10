using CpInterview.DataEntities;
using CpInterview.Manager;
using CpInterview.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CpInterview.Tests
{
  [TestClass]
  public class CalendarManagerTests
  {
    [TestMethod]
    public void CanRetrieveEvents()
    {
      IApiAccessor apiAccessor = new MockApiAccessor();
      ICalendarManager calendarManager = new CalendarManager(apiAccessor);

      IList<CalendarEvent> calendarEvents = calendarManager.RetrieveEvents();

      var firstEvent = calendarEvents.FirstOrDefault();
      AssertFirstEventIsValid(firstEvent);
      Assert.AreEqual(4, calendarEvents.Count);
      Assert.AreEqual("guid4", calendarEvents[3].Id);
    }

    private void AssertFirstEventIsValid(CalendarEvent calendarEvent)
    {
      Assert.AreEqual("guid1", calendarEvent.Id);
      Assert.AreEqual("My Interview with CivicPlus", calendarEvent.Title);
      Assert.AreEqual("A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.", calendarEvent.Description);
      Assert.AreEqual("10/13/2021 2:00:00 PM", calendarEvent.StartDate.ToString());
      Assert.AreEqual("10/13/2021 3:30:00 PM", calendarEvent.EndDate.ToString());
    }

    [TestMethod]
    public void CanAddEvent()
    {
      IApiAccessor apiAccessor = new MockApiAccessor();
      ICalendarManager calendarManager = new CalendarManager(apiAccessor);
      CalendarEvent calendarEvent = new CalendarEvent()
      {
        Title = "My Mock Name",
        Description = "My Mock Description",
        StartDate = new System.DateTime(2021, 10, 13, 16, 30, 0, System.DateTimeKind.Local),
        EndDate = new System.DateTime(2021, 10, 13, 16, 30, 0, System.DateTimeKind.Local)
      };

      calendarManager.AddEvent(calendarEvent);

      MockApiAccessor spy = apiAccessor as MockApiAccessor;
      AssertMappedEventForAccessorIsValid(calendarEvent, spy);
      Assert.AreEqual(1, spy.countCallsToAddEvent);
    }

    private void AssertMappedEventForAccessorIsValid(CalendarEvent calendarEvent, MockApiAccessor spy)
    {
      Assert.AreEqual(calendarEvent.Title, spy.spyCalendarEventWriteEntity.Title);
      Assert.AreEqual(calendarEvent.Description, spy.spyCalendarEventWriteEntity.Description);
      Assert.AreEqual(calendarEvent.StartDate.ToUniversalTime(), spy.spyCalendarEventWriteEntity.StartDate);
      Assert.AreEqual(calendarEvent.EndDate.ToUniversalTime(), spy.spyCalendarEventWriteEntity.EndDate);
    }
  }

  internal class MockApiAccessor : IApiAccessor
  {
    public CalendarEventWriteEntity spyCalendarEventWriteEntity { get; set; }
    public int countCallsToAddEvent { get; set; }
    public void AddEvent(CalendarEventWriteEntity calendarEventToWrite)
    {
      spyCalendarEventWriteEntity = calendarEventToWrite;
      countCallsToAddEvent++;
    }

    public IList<CalendarEventReadEntity> RetrieveCalendarEvents()
    {
      return new List<CalendarEventReadEntity>()
      {
        new CalendarEventReadEntity()
        {
          Id = "guid1",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },
        new CalendarEventReadEntity()
        {
          Id = "guid2",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },
        new CalendarEventReadEntity()
        {
          Id = "guid3",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },
        new CalendarEventReadEntity()
        {
          Id = "guid4",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },

      };
    }
  }
}

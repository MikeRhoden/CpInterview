using CpInterview.DataEntities;
using CpInterview.Interactors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CpInterview.Tests
{
  [TestClass]
  public class CalendarInteractorTests
  {
    [TestMethod]
    public void CanRetrieveEvents()
    {
      IApiAccessor apiAccessor = new MockApiAccessor();
      ICalendarInteractor calendarInteractor = new CalendarInteractor(apiAccessor);
      IList<CalendarEvent> calendarEvents = calendarInteractor.RetrieveEvents();
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
  }

  internal class MockApiAccessor : IApiAccessor
  {
    public IList<CalendarEventEntity> RetrieveCalendarEvents()
    {
      return new List<CalendarEventEntity>()
      {
        new CalendarEventEntity()
        {
          Id = "guid1",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },
        new CalendarEventEntity()
        {
          Id = "guid2",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },
        new CalendarEventEntity()
        {
          Id = "guid3",
          Title = "My Interview with CivicPlus",
          Description = "A panel interview with distinguished members of the CivicPlus engineering team. Fun will be had.",
          StartDate = new System.DateTime(2021, 10, 13, 19, 0, 0, System.DateTimeKind.Utc),
          EndDate = new System.DateTime(2021, 10, 13, 20, 30, 0, System.DateTimeKind.Utc),
        },
        new CalendarEventEntity()
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

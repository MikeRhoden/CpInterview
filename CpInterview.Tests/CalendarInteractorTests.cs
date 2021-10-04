using Microsoft.VisualStudio.TestTools.UnitTesting;
using CpInterview.Interactors;

namespace CpInterview.Tests
{
  [TestClass]
  public class CalendarInteractorTests
  {
    [TestMethod]
    public void CanMakeCalendarInteractor()
    {
      ICalendarInteractor calendarInteractor = new CalendarInteractor();
    }

  }
}

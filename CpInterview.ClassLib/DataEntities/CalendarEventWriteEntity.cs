using System;

namespace CpInterview.DataEntities
{
  public class CalendarEventWriteEntity
  {
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}
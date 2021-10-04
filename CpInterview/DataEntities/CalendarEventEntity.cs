using System;

namespace CpInterview.DataEntities
{
  public class CalendarEventEntity
  {
    public string Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

  }
}
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

/*
"
{
\"total\":2,
\"items\":[
  {\"id\":\"90155eb9-7827-4a13-b706-1f6c4e2376e7\",\"title\":\"2\",\"description\":\"2\",
    \"startDate\":\"2021-10-05T20:52:51Z\",
    \"endDate\":\"2021-10-05T21:52:51Z\"},
  {\"id\":\"47316453-6cea-4594-a920-9d01a406593a\",\"title\":\"1\",\"description\":\"1\",\"startDate\":\"2021-10-04T20:52:51Z\",\"endDate\":\"2021-10-04T21:52:51Z\"}]}"*/
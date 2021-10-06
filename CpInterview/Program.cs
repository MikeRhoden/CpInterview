using CpInterview.ApiAccess;
using CpInterview.Interactors;
using System;

namespace CpInterview
{
  class Program
  {
    static void Main(string[] args)
    {
      var i = new CalendarInteractor(new ApiAccessor());
      var e = i.RetrieveEvents();
      foreach (var ev in e)
        Console.WriteLine($"{ev.Title} - {ev.Description} - {ev.StartDate} - {ev.EndDate}");
    }
  }
}

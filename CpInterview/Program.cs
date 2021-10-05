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
      i.RetrieveEvents();
      Console.WriteLine($"howdy");
    }
  }
}

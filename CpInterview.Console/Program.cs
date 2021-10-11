using System;
using ConsoleTables;
using CpInterview.ApiAccess;
using CpInterview.Manager;
using CpInterview.Models;

namespace CpInterview
{
  class Program
  {
    static void Main(string[] args)
    {
      //TODO add dependency injection here for
      //IApiAccessor, ICalendarManger
      Prompt();
    }

    private static void Prompt()
    {
      var quit = false;
      while (!quit)
      {
        Intro();
        var input = System.Console.ReadLine(); ;
        if (input.ToLower() == "l")
          ListCalendarEvents();
        if (input.ToLower() == "a")
          AddEvent();
        if (input.ToLower() == "q")
          quit = true;
      }
      Console.WriteLine("");
      Console.WriteLine("Goodbye. Thanks for visiting!.");
    }

    private static void Prompt1()
    {
      var quit = false;
      while (!quit)
      {
        Intro();
        var readKey = System.Console.ReadKey();
        var k = readKey.Key;
        if (k == ConsoleKey.L)
          ListCalendarEvents();
        if (k == ConsoleKey.A)
          AddEvent();
        if (k == ConsoleKey.Q)
          quit = true;
      }
      Console.WriteLine("");
      Console.WriteLine("Goodbye. Thanks for visiting!");
    }

    private static void AddEvent()
    {
      try
      {
        Console.WriteLine("");
        Console.WriteLine("Please enter Title, Description, Start, and End for your Event ...");
        Console.Write("Title: ");
        var title = System.Console.ReadLine();
        Console.Write("Description: ");
        var description = System.Console.ReadLine();
        Console.Write("Start (mm/dd/yy hh:mm AM): ");
        System.DateTime start = System.DateTime.Parse(System.Console.ReadLine());
        Console.Write("End (mm/dd/yy hh:mm AM): ");
        System.DateTime end = System.DateTime.Parse(System.Console.ReadLine());

        if (end <= start)
          throw new Exception("The end time of your event must be after it starts.");

        var calendarEvent = new CalendarEvent()
        {
          Title = title,
          Description = description,
          StartDate = start,
          EndDate = end
        };

        var calendarManager = new CalendarManager(new ApiAccessor());
        calendarManager.AddEvent(calendarEvent);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Event Added!");
        Console.ResetColor();
      }
      catch (Exception e)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Failed to add event. {e.Message}");
        Console.ResetColor();
      }
    }

    private static void Intro()
    {
      System.Console.WriteLine("");

      System.Console.WriteLine("Welcome to your calendar events!");
      System.Console.WriteLine("Q - Quit.");
      System.Console.WriteLine("L - List all events.");
      System.Console.WriteLine("A - Add an event.");
    }

    private static void ListCalendarEvents()
    {
      System.Console.WriteLine("");

      var calendarManager = new CalendarManager(new ApiAccessor());
      var events = calendarManager.RetrieveEvents();

      if (events.Count == 0)
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("There are no events.");
        Console.ResetColor();
      }
      else
      {
        var table = new ConsoleTable("Title", "Description", "Start Time", "End Time");
        foreach (var ev in events)
          table.AddRow(ev.Title, ev.Description, ev.StartDate, ev.EndDate);
        table.Write();
      }
    }
  }
}

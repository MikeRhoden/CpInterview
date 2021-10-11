using System;
using CpInterview.ApiAccess;
using CpInterview.Manager;
using CpInterview.Models;

namespace CpInterview.Console
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
      System.Console.WriteLine("");
      System.Console.WriteLine("Goodbye. Thanks for visiting!.");
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
      System.Console.WriteLine("");
      System.Console.WriteLine("Goodbye. Thanks for visiting!.");
    }

    private static void AddEvent()
    {
      try
      {
        System.Console.WriteLine("");
        System.Console.WriteLine("Please enter Title, Description, Start, and End for your Event ...");
        System.Console.Write("Title: ");
        var title = System.Console.ReadLine();
        System.Console.Write("Description: ");
        var description = System.Console.ReadLine();
        System.Console.Write("Start (mm/dd/yy hh:mm AM): ");
        System.DateTime start = System.DateTime.Parse(System.Console.ReadLine());
        System.Console.Write("End (mm/dd/yy hh:mm AM): ");
        System.DateTime end = System.DateTime.Parse(System.Console.ReadLine());

        //TODO Check for valid input on dates and length of title and description
        var calendarEvent = new CalendarEvent()
        {
          Title = title,
          Description = description,
          StartDate = start,
          EndDate = end
        };

        var calendarManager = new CalendarManager(new ApiAccessor());
        calendarManager.AddEvent(calendarEvent);
      }
      catch (Exception e)
      {
        System.Console.ForegroundColor = ConsoleColor.Red;
        System.Console.WriteLine($"Failed to add event. {e.Message}");
        System.Console.ResetColor();
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
      //TODO Format output in a console table
      System.Console.WriteLine("");

      var calendarManager = new CalendarManager(new ApiAccessor());
      var events = calendarManager.RetrieveEvents();
      foreach (var ev in events)
        System.Console.WriteLine($"{ev.Title} - {ev.Description} - {ev.StartDate} - {ev.EndDate}");
    }
  }
}

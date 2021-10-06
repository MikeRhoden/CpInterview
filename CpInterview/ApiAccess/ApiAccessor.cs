using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CpInterview.DataEntities;
using CpInterview.Interactors;
using Newtonsoft.Json;

namespace CpInterview.ApiAccess
{
  public class ApiAccessor : IApiAccessor
  {
    private string token;

    public ApiAccessor()
    {
      CpToken cpToken = CpToken.Instance;
      this.token = CpToken.value;
    }
    public IList<CalendarEventEntity> RetrieveCalendarEvents()
    {
      var s = RetrieveEvents().Result;
      var d = JsonConvert.DeserializeObject<CalendarEventEntityJson>(s);
      return d.items;
    }

    private async Task<string> RetrieveEvents()
    {
      using (var httpClient = new HttpClient())
      {
        var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

        //make sure we are passing right media type
        if (defaultRequestHeaders.Accept == null
          || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
        {
          httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //add token to request headers
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

        HttpResponseMessage response = await httpClient.GetAsync("https://interview.cpdv.ninja/5122899e-8cd5-4b63-bd10-2ac6b7d08f93/api/Events");
        return await response.Content.ReadAsStringAsync();
      }
    }

    private class CalendarEventEntityJson
    {
      public int total { get; set; }
      public IList<CalendarEventEntity> items { get; set; }
    }
  }




}

//"{\"access_token\":\"eyJhbGciOiJSUzI1NiIsImtpZCI6IlBFdU9rQjh2ek9tZnNXbENEYnlFVnciLCJ0eXAiOiJhdCtqd3QifQ.eyJuYmYiOjE2MzMzOTA5ODksImV4cCI6MTYzNTk4Mjk4OSwiaXNzIjoiaHR0cHM6Ly9jb250ZW50LXJldmlldy5jaXZpY3BsdXMuY29tL2lkZW50aXR5LXNlcnZlciIsImNsaWVudF9pZCI6InNvZnR3YXJlLWVuZ2luZWVyLWludGVydmlld3M6NTEyMjg5OWUtOGNkNS00YjYzLWJkMTAtMmFjNmI3ZDA4ZjkzIiwianRpIjoiM0UxRThCMzlGRTg5QjEwMzgxMDk1MEJCRjJEMDU2NDAiLCJpYXQiOjE2MzMzOTA5ODksInNjb3BlIjpbImhjbXMtYXBpIl19.B4fhIxs9t79PkUHni1UOfi20UgcXrdJ1LWHJXHB4aP9lJjsWops4mOTHdU7tR7eETUm-BlRJvc4pa8Wobub2jVK7T8-Khom5CZvP6bdr-EALDcJCMjh6kGrETiO08UYrxMS-Am__dtmhUuHR4ZxgwDTyDvBecXo9S7L92IzOIafDU611DFZF4NEywVxu5dWkfb-VnJFJTMwc6oND1AYPs69t6rdrbYtdSlZKS-rnDv6f5HZowoonR02uH597SdU6aVMeIwEU6m4zs1p8l6w6adQfigDHWVqjsw4KxNTYhh4eoAYB_fug_Buu1YwzudV5Gb3ll0rfZTzOw7y6BQv3GA\",\"expires_in\":2592000}"
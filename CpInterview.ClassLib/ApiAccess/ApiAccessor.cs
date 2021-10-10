using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CpInterview.DataEntities;
using CpInterview.Manager;
using Newtonsoft.Json;

namespace CpInterview.ApiAccess
{

  public class ApiAccessor : IApiAccessor
  {
    private string token;
    //TODO put this in app config
    private const string EVENT_ENDPOINT = "https://interview.cpdv.ninja/5122899e-8cd5-4b63-bd10-2ac6b7d08f93/api/Events";


    public ApiAccessor()
    {
      //TODO if token is expired create a new instance of the singleton CpToken
      CpToken cpToken = CpToken.Instance;
      this.token = CpToken.value;
    }

    public void AddEvent(CalendarEventWriteEntity calendarEventToWrite)
    {
      using (var httpClient = GetHttpClient())
      {
        StringContent postContent = new StringContent(JsonConvert.SerializeObject(calendarEventToWrite), Encoding.UTF8, "application/json-patch+json");
        var response = httpClient.PostAsync(EVENT_ENDPOINT, postContent).Result;
        if (response.IsSuccessStatusCode)
        {
          Console.WriteLine("Success");
        }
        else
        {
          var content = response.Content.ReadAsStringAsync().Result;
          Console.WriteLine(content);
          //TODO return error to client
        }
      }
    }

    public IList<CalendarEventReadEntity> RetrieveCalendarEvents()
    {
      var eventsJsonString = RetrieveEvents().Result;
      var calendarEventEntityJson = JsonConvert.DeserializeObject<CalendarEventEntityJson>(eventsJsonString);
      return calendarEventEntityJson.items;
    }

    private async Task<string> RetrieveEvents()
    {
      using (var httpClient = GetHttpClient())
      {
        HttpResponseMessage response = await httpClient.GetAsync(EVENT_ENDPOINT);
        return await response.Content.ReadAsStringAsync();
      }
    }

    private HttpClient GetHttpClient()
    {
      var httpClient = new HttpClient();
      var defaultRequestHeaders = httpClient.DefaultRequestHeaders;

      if (defaultRequestHeaders.Accept == null
        || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
      {
        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      }

      httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.token);

      return httpClient;
    }

    private class CalendarEventEntityJson
    {
      public int total { get; set; }
      public IList<CalendarEventReadEntity> items { get; set; }
    }
  }




}
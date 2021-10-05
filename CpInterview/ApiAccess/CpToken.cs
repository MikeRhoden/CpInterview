using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CpInterview.ApiAccess
{
  public sealed class CpToken
  {
    private static readonly Lazy<CpToken> token = new Lazy<CpToken>(() => new CpToken());
    public static string value;

    public static CpToken Instance { get { return token.Value; } }

    private CpToken()
    {
      var s = FetchToken().Result;
      var rawToken = JsonConvert.DeserializeObject<RawToken>(s);
      value = rawToken.access_token;
    }

    private static async Task<string> FetchToken()
    {
      using (var httpClient = new HttpClient())
      {
        {
          var defaultRequestHeaders = httpClient.DefaultRequestHeaders;
          if (defaultRequestHeaders.Accept == null || !defaultRequestHeaders.Accept.Any(m => m.MediaType == "application/json"))
          {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
          }

          var clientSecret = new ClientSecrets();
          var clientSecretContent = JsonConvert.SerializeObject(clientSecret);
          var response = await httpClient.PostAsync("https://interview.cpdv.ninja/5122899e-8cd5-4b63-bd10-2ac6b7d08f93/api/Auth",
            new StringContent(clientSecretContent, Encoding.UTF8, "application/json-patch+json"));
          return await response.Content.ReadAsStringAsync();
        }
      }
    }

    internal class ClientSecrets
    {
      public string ClientId { get; }
      public string ClientSecret { get; }
      public ClientSecrets()
      {
        this.ClientId = "5122899e-8cd5-4b63-bd10-2ac6b7d08f93";
        this.ClientSecret = "rc5tmfpfhbmii5eiuwcpsuy5shoerlzsuwmyrbgbyjux";
      }
    }

    internal class RawToken
    {
      public string access_token { set; get; }
      public string expires_in { set; get; }
    }
  }
}
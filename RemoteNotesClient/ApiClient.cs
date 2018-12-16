using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Linq;

namespace RemoteNotes
{
  public class ApiClient
  {
    private readonly string _baseApiUrl;
    private Dictionary<string, string> _authHeaders = new Dictionary<string, string>();

    public ApiClient(string baseApiUrl)
    {
      _baseApiUrl = baseApiUrl;
    }

    public async Task<User> SignIn(string email, string password)
    {
      var response = await _baseApiUrl
    .AppendPathSegment("auth/sign_in")
    .PostJsonAsync(new
    {
      email,
      password
    });

      SaveAuthHeaders(response.Headers);
      var result = await response.ReadFromJsonAsync();
      return result.User;
    }

    public async Task SignOut()
    {
      await _baseApiUrl
    .AppendPathSegment("auth/sign_out")
    .WithHeaders(_authHeaders)
    .DeleteAsync();
    }

    public async Task<User> UpdateProfile(string name, string base64Image)
    {
      var response = await _baseApiUrl
    .AppendPathSegment("auth")
    .WithHeaders(_authHeaders)
    .PutJsonAsync(new
    {
      name,
      image = base64Image
    });

      SaveAuthHeaders(response.Headers);
      var result = await response.ReadFromJsonAsync();
      return result.User;
    }

    public async Task<Note> CreateNote(string body)
    {
      var response = await _baseApiUrl
    .AppendPathSegment("notes")
    .WithHeaders(_authHeaders)
    .PostJsonAsync(new
    {
      body
    });

      SaveAuthHeaders(response.Headers);
      var result = await response.ReadFromJsonAsync();
      return result.Note;
    }

    public async Task<Note> UpdateNote(string id, string body)
    {
      var response = await _baseApiUrl
    .AppendPathSegment("notes")
    .AppendPathSegment(id)
    .WithHeaders(_authHeaders)
    .PutJsonAsync(new
    {
      body
    });

      SaveAuthHeaders(response.Headers);
      var result = await response.ReadFromJsonAsync();
      return result.Note;
    }

    public async Task DeleteNote(string id)
    {
      var response = await _baseApiUrl
    .AppendPathSegment("notes")
    .AppendPathSegment(id)
    .WithHeaders(_authHeaders)
    .DeleteAsync();

      SaveAuthHeaders(response.Headers);
    }

    public async Task<List<Note>> GetNotes()
    {
      var response = await _baseApiUrl
    .AppendPathSegment("notes")
    .WithHeaders(_authHeaders)
    .GetAsync();

      SaveAuthHeaders(response.Headers);
      var result = await response.ReadFromJsonAsync();
      return result.Notes;
    }

    private void SaveAuthHeaders(HttpResponseHeaders headers)
    {
      foreach (var key in new[] { "Uid", "Client", "Access-Token", "Expiry" })
      {
        if (headers.TryGetValues(key, out IEnumerable<string> values))
        {
          _authHeaders[key] = values.First();
        }
      }
    }
  }

  public static class ApiHelpers
  {

    private static DefaultContractResolver contractResolver = new DefaultContractResolver
    {
      NamingStrategy = new SnakeCaseNamingStrategy()
    };

    private static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
    {
      ContractResolver = contractResolver,
      Formatting = Formatting.Indented
    };

    public static async Task<ResponseWrapper> ReadFromJsonAsync(this HttpResponseMessage response)
    {
      if (response.Content == null) return default(ResponseWrapper);
      string content = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<ResponseWrapper>(content, serializerSettings);
    }

    public class ResponseWrapper
    {
      public User User { get; set; }
      public Note Note { get; set; }
      public List<Note> Notes { get; set; }
    }
  }
}

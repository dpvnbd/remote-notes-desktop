using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Flurl.Http.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FluentAssertions;

namespace RemoteNotes.Tests
{
  [TestClass]
  public class ApiClientTest
  {
    private ApiClient _apiClient;
    private readonly string _baseUrl = "http://localhost:3000";
    private static HttpTest _httpTest;
     
    [ClassInitialize]
    public static void CreateHttpTest(TestContext ctx)
    {
      _httpTest = new HttpTest();
    }

    [ClassCleanup]
    public static void DisposeHttpTest()
    {
      _httpTest.Dispose();
    }

    [TestInitialize]
    public void TestInitialize()
    {
      _apiClient = new ApiClient(_baseUrl);
   
    }


    [TestMethod]
    public async Task SignInPostsToAuthSignInRoute()
    {
      var email = "aa@aa.aa";
      var password = "aa123456";
     await _apiClient.SignIn(email, password);
      _httpTest.ShouldHaveCalled($"{_baseUrl}/auth/sign_in")
    .WithVerb(HttpMethod.Post)
    .WithContentType("application/json")
    .WithRequestBody($"{{\"email\":\"{email}\",\"password\":\"{password}\"}}")
    .Times(1);
    }

    [TestMethod]
    public async Task SignInReturnsUserObject()
    {
      var email = "aa@aa.aa";

      var response = new { email, name = "name", image_base64 = "image" };
      var headers = new { client = "clientId" };
      _httpTest.RespondWith(JsonConvert.SerializeObject(response), headers: headers);

      var expectedUser = new User
      {
        Name = "name",
        Email = email,
        Image = "image"
      };

      var responseUser = await _apiClient.SignIn(email, "password");

      responseUser.Should().BeEquivalentTo(expectedUser);
    }

    [TestMethod]
    public async Task NotesIndexRequestWithSignInResponseAuthHeaders()
    {
      var headers = new { client = "clientId" };
      _httpTest.RespondWith(headers: headers);
      await _apiClient.SignIn("email", "password");
    }
  }
}

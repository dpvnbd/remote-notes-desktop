using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RemoteNotes.Specs
{
  [Binding]
  public class SharedSteps
  {
    private ApiContext _apiContext;
    public SharedSteps(ApiContext apiContext)
    {
      _apiContext = apiContext;
    }

    [Given(@"I have connected to API by url (.*)")]
    public void GivenIHaveConnectedToAPIByUrl(string apiUrl)
    {
      _apiContext.ApiClient = new ApiClient(apiUrl);
    }

    [Given(@"I am signed in with email (.*) and password (.*)")]
    public async Task GivenIAmSignedInWithEmailAndPassword(string email, string password)
    {
      try
      {
        _apiContext.SignedInUser = await _apiContext.ApiClient.SignIn(email, password);
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [Then(@"exception is thrown")]
    public void ThenExceptionIsThrown()
    {
      Assert.IsNotNull(_apiContext.Exception);
    }

    [Then(@"exception is not thrown")]
    public void ThenExceptionIsNotThrown()
    {
      Assert.IsNull(_apiContext.Exception);
    }
  }
}

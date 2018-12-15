using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RemoteNotes.Specs
{
  [Binding]
  public class SignInSteps
  {
    private ApiClient _apiClient;
    private User _user;
    private string _email;
    private Exception _exception;
    private string _updatedData;

    [Given(@"I have connected to API by url (.*)")]
    public void GivenIHaveConnectedToAPIByUrl(string apiUrl)
    {
      _apiClient = new ApiClient(apiUrl);
    }

    [When(@"I sign in with email (.*) and password (.*)")]
    [Given(@"I am signed in with email (.*) and password (.*)")]
    public async Task WhenISignInWithEmailAndPassword(string email, string password)
    {
      _email = email;
      try
      {
        _user = await _apiClient.SignIn(email, password);
      }
      catch (Exception e)
      {
        _exception = e;
      }
    }

    [When(@"I sign out")]
    public async Task WhenISignOut()
    {
      try
      {
        await _apiClient.SignOut();
      }
      catch (Exception e)
      {
        _exception = e;
      }
    }

    [When(@"I update my account with random name and base64 image")]
    public async Task WhenIUpdateNameImage()
    {
      _updatedData = DateTime.Now.ToLongTimeString();
      try
      {
        _user = await _apiClient.UpdateProfile(_updatedData, _updatedData);
      }
      catch (Exception ex)
      {
        _exception = ex;
      }
    }

    [Then(@"returned user has same name and image")]
    public void ThenReturnedUserHasSameNameAndImage()
    {
      Assert.AreEqual(_user.Name, _updatedData);
      Assert.AreEqual(_user.Image, _updatedData);
    }

    [Then(@"the result should be a User object with same email")]
    public void ThenTheResultShouldBeAUserObjectWithSameEmail()
    {
      Assert.AreEqual(_email, _user.Email);
    }

    [Then(@"exception is thrown")]
    public void ThenExceptionIsThrown()
    {
      Assert.IsNotNull(_exception);
    }

    [Then(@"exception is not thrown")]
    public void ThenExceptionIsNotThrown()
    {
      Assert.IsNull(_exception);
    }
  }
}

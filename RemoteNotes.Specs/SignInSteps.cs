using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace RemoteNotes.Specs
{
  [Binding]
  public class SignInSteps
  {
    private ApiContext _apiContext;
    private string _email;
    private string _updatedData;

    public SignInSteps(ApiContext apiContext)
    {
      _apiContext = apiContext;
    }

    [When(@"I sign in with email (.*) and password (.*)")]
    public async Task WhenISignInWithEmailAndPassword(string email, string password)
    {
      _email = email;
      try
      {
        _apiContext.SignedInUser = await _apiContext.ApiClient.SignIn(email, password);
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [When(@"I sign out")]
    public async Task WhenISignOut()
    {
      try
      {
        await _apiContext.ApiClient.SignOut();
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [When(@"I update my account with random name and base64 image")]
    public async Task WhenIUpdateNameImage()
    {
      _updatedData = DateTime.Now.ToLongTimeString();
      try
      {
        _apiContext.SignedInUser = await _apiContext.ApiClient.UpdateProfile(_updatedData, _updatedData);
      }
      catch (Exception e)
      {
        _apiContext.Exception = e;
      }
    }

    [Then(@"returned user has same name and image")]
    public void ThenReturnedUserHasSameNameAndImage()
    {
      Assert.AreEqual(_apiContext.SignedInUser.Name, _updatedData);
      Assert.AreEqual(_apiContext.SignedInUser.Image, _updatedData);
    }

    [Then(@"the result should be a User object with same email")]
    public void ThenTheResultShouldBeAUserObjectWithSameEmail()
    {
      Assert.AreEqual(_email, _apiContext.SignedInUser.Email);
    }
  }
}

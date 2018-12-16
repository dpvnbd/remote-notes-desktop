using RemoteNotes;
using RemoteNotesApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteNotesApp
{
  public class AppApiClient
  {
    public ApiClient ApiClient { get; }


    public AppApiClient()
    {
      ApiClient = new ApiClient("https://remote-notes.herokuapp.com/");
      Mediator.Register("SignIn", SignIn);
      Mediator.Register("SignOut", SignOut);
      Mediator.Register("UpdateProfile", UpdateProfile);
    }

    protected async void SignIn(dynamic data)
    {
      string email = (string)data.Email;
      string password = (string)data.Password;
      try
      {
        var user = await ApiClient.SignIn(email, password);
        Mediator.NotifyColleagues("User", user);
        Mediator.NotifyColleagues("ChangeView", "UpdateProfile");
      }
      catch (Exception ex)
      {
        Mediator.NotifyColleagues("Exception", ex);
      }
    }

    protected async void SignOut(object data)
    {
      try
      {
        await ApiClient.SignOut();
        Mediator.NotifyColleagues("ChangeView", "Login");
      }
      catch (Exception ex)
      {
        Mediator.NotifyColleagues("Exception", ex);
      }
    }

    protected async void UpdateProfile(dynamic data)
    {
      try
      {
        var user = await ApiClient.UpdateProfile(data.Name, data.Image);
        Mediator.NotifyColleagues("User", user);
        Mediator.NotifyColleagues("Message", "Profile has been updated");        
      }
      catch (Exception ex)
      {
        Mediator.NotifyColleagues("Exception", ex);
      }
    }
  }
}

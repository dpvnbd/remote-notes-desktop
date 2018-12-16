using RemoteNotes;
using RemoteNotesApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RemoteNotesApp
{
  public class MainWindowViewModel : ViewModelBase
  {
    private AppApiClient _appApiClient;

    private object _loginView = new LoginView();
    private object _updateProfileView = new UpdateProfileView();

    private object _currentView;
    private string _message;
    private BitmapImage _userImage;

    public BitmapImage UserImage
    {
      get => _userImage;
      set
      {
        _userImage = value;
        OnPropertyChanged("UserImage");
      }
    }

    public string Message
    {
      get => _message;
      set
      {
        _message = value;
        OnPropertyChanged("Message");
      }
    }

    public object CurrentView
    {
      get => _currentView;
      set
      {
        _currentView = value;
        OnPropertyChanged("CurrentView");
      }
    }

    public MainWindowViewModel()
    {
      CurrentView = _loginView;
      Mediator.Register("ChangeView", OnChangeView);
      Mediator.Register("Exception", OnException);
      Mediator.Register("Message", OnMessage);
      Mediator.Register("User", OnUser);

      _appApiClient = new AppApiClient();
    }

    public void OnChangeView(dynamic data)
    {
      var viewName = (string)data;
      switch (viewName)
      {
        case "Login":
          {
            CurrentView = _loginView;
            break;
          }
        case "UpdateProfile":
          {
            CurrentView = _updateProfileView;
            break;
          }
      }
    }

    public void OnException(dynamic data)
    {
      Message = (string)data.Message;
    }

    private void OnMessage(object obj)
    {
      Message = (string)obj;
    }

    private void OnUser(object data)
    {
      var user = (User)data;
      Message = $"Signed in as {user.Name} {user.Email}";
      UserImage = ImageBase64Converter.ConvertFromBase64ToImage(user.Image);
    }
  }
}

using Microsoft.Win32;
using RemoteNotes;
using RemoteNotesApp.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace RemoteNotesApp
{
  public class UpdateProfileViewModel : ViewModelBase
  {
    public UpdateProfileViewModel()
    {
      Mediator.Register("User", OnUser);
    }

    private void OnUser(object obj)
    {
      var user = (User)obj;
      Name = user.Name;
      _imageBase64 = user.Image;
      UserImage = ImageBase64Converter.ConvertFromBase64ToImage(_imageBase64);
    }

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

    private string _imageBase64;

    private string _name;

    public string Name
    {
      get => _name;
      set
      {
        _name = value;
        OnPropertyChanged("Name");
      }
    }

    private ICommand _updateProfileCommand;

    public ICommand UpdateProfileCommand
    {
      get
      {
        return _updateProfileCommand ?? (_updateProfileCommand = new RelayCommand(
           x =>
           {
             Mediator.NotifyColleagues("UpdateProfile", new { Name, Image = _imageBase64 });
           }));
      }
    }

    private ICommand _openImageCommand;

    public ICommand OpenImageCommand
    {
      get
      {
        return _openImageCommand ?? (_openImageCommand = new RelayCommand(x => ExecuteOpenImageDialog()));
      }
    }

    private void ExecuteOpenImageDialog()
    {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.png) | *.jpg; *.jpeg; *.jpe; *.png";
      if (openFileDialog.ShowDialog() == true)
      {
        var bytes = File.ReadAllBytes(openFileDialog.FileName);
        _imageBase64 = Convert.ToBase64String(bytes);

        try
        {
          UserImage = ImageBase64Converter.ConvertFromBase64ToImage(_imageBase64);
        }
        catch (Exception ex)
        {
          _imageBase64 = null;
          UserImage = null;
        }
      }
    }
  }
}

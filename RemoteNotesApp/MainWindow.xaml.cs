using Microsoft.Win32;
using RemoteNotes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RemoteNotesApp
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private ApiClient apiClient;
    private string _userImageBase64;
    public MainWindow()
    {
      InitializeComponent();
      apiClient = new ApiClient("https://remote-notes.herokuapp.com/");
    }

    private async void signInButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        var user = await apiClient.SignIn(emailTextBox.Text, passwordPasswordBox.Password);
        UpdateUserInfo(user);
      }
      catch (Exception ex)
      {
        statusTextBlock.Text = ex.Message;
      }
    }

    private async void signOutButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        await apiClient.SignOut();
        UpdateUserInfo(null);
      }
      catch (Exception ex)
      {
        statusTextBlock.Text = ex.Message;
      }
    }

    private void UpdateUserInfo(User user)
    {
      if (user == null)
      {
        statusTextBlock.Text = "Signed out";
        userEmailTextBlock.Text = "email";
        userNameTextBox.Text = "name";
        userImage.Source = null;
      }
      else
      {
        statusTextBlock.Text = "Signed in as " + user.Email;
        userEmailTextBlock.Text = user.Email;
        userNameTextBox.Text = user.Name;
        userImage.Source = ConvertFromBase64(user.Image);
      }
    }

    private BitmapImage ConvertFromBase64(string base64Image)
    {
      if (base64Image == null || base64Image.Length == 0)
      {
        return null;
      }

      // var base64Image = "iVBORw0KGgoAAAANSUhEUgAAAGQAAABkCAYAAABw4pVUAAAAnElEQVR42u3RAQ0AAAgDoJvc6FrDOahATdLhjBIiBCFCECIEIUIQIkSIEIQIQYgQhAhBiBCEIEQIQoQgRAhChCAEIUIQIgQhQhAiBCEIEYIQIQgRghAhCEGIEIQIQYgQhAhBCEKEIEQIQoQgRAhCECIEIUIQIgQhQhCCECEIEYIQIQgRghCECEGIEIQIQYgQhAgRIgQhQhAiBCHfLcjClZ2EzWBMAAAAAElFTkSuQmCC";
      byte[] binaryData = Convert.FromBase64String(base64Image);

      BitmapImage bi = new BitmapImage();
      bi.BeginInit();
      bi.StreamSource = new MemoryStream(binaryData);
      bi.EndInit();

      return bi;
    }

    private async void updateUserButton_Click(object sender, RoutedEventArgs e)
    {
      try
      {
        var user = await apiClient.UpdateProfile(userNameTextBox.Text, _userImageBase64);
        UpdateUserInfo(user);
      }
      catch (Exception ex)
      {
        statusTextBlock.Text = ex.Message;
      }      
    }

    private void openImageButton_Click(object sender, RoutedEventArgs e)
    {
      var openFileDialog = new OpenFileDialog();
      openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.png) | *.jpg; *.jpeg; *.jpe; *.png";
      if (openFileDialog.ShowDialog() == true)
      {
        var bytes = File.ReadAllBytes(openFileDialog.FileName);
        _userImageBase64 = Convert.ToBase64String(bytes);

        try
        {
          userImage.Source = ConvertFromBase64(_userImageBase64);
        }
        catch (Exception ex)
        {
          _userImageBase64 = null;
          userImage.Source = null;
        }
      }
    }
  }
}

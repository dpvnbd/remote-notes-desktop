using RemoteNotesApp.Util;
using System;
using System.Collections.Generic;
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
  public partial class LoginView : UserControl
  {
    public string CurrentView { get; set; }

    public LoginView()
    {
      InitializeComponent();
    }

    private void signInButton_Click(object sender, RoutedEventArgs e)
    {
      Mediator.NotifyColleagues("SignIn", new { Email = emailTextBox.Text, passwordBox.Password });
    }
  }
}

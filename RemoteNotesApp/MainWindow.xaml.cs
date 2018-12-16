using Microsoft.Win32;
using RemoteNotes;
using RemoteNotesApp.Util;
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
    public MainWindow()
    {
      InitializeComponent();
    }

    private void signOutButton_Click(object sender, RoutedEventArgs e)
    {
      Mediator.NotifyColleagues("SignOut");
    }

    private void updateProfileButton_Click(object sender, RoutedEventArgs e)
    {
      Mediator.NotifyColleagues("ChangeView", "UpdateProfile");
    }
  }
}

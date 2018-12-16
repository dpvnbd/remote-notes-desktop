using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RemoteNotesApp.Util
{
  public class ImageBase64Converter
  {
    public static BitmapImage ConvertFromBase64ToImage(string base64Image)
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

    public static string ConvertFromFileToBase64(string fileName)
    {
      var bytes = File.ReadAllBytes(fileName);
      return Convert.ToBase64String(bytes);
    }
  }
}

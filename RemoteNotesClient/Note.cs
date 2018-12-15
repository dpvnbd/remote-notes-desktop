using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteNotes
{
  public class Note
  {
    public User User { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}

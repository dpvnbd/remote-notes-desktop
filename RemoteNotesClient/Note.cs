using System;
using System.Collections.Generic;
using System.Text;

namespace RemoteNotes
{
  public class Note
  {
    public string Id { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}

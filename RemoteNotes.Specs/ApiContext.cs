using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteNotes.Specs
{
  public class ApiContext
  {
    public ApiClient ApiClient { get; set; }
    public User SignedInUser { get; set; }
    public Exception Exception { get; set; }
  }
}

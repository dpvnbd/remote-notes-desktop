using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteNotesApp.Util
{
  public class EventArgs<T> : EventArgs
  {
    public EventArgs(T value)
    {
      Value = value;
    }

    public T Value { get; private set; }
  }
}

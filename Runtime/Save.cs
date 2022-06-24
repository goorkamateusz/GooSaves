using System;
using System.Collections.Generic;

namespace Goo.Saves
{
    [Serializable]
    public class Save : Dictionary<string, SaveSerializable>
    {
    }
}

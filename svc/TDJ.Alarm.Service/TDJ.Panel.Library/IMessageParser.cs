using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJ.Panel.Library
{
    public interface IMessageParser
    {
        Message Process(string rawOutputMessage);
    }
}

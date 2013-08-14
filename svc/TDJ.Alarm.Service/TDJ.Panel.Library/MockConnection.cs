using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJ.Panel.Library
{
    public class MockConnection : IPanelConnection
    {
        public string ComPort { get; set; }

        public Message Get()
        {
            return new Message();
        }

        public void Send(string text)
        {
            
        }

        public void Close()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}

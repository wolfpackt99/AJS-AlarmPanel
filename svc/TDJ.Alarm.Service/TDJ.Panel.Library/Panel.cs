using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Newtonsoft.Json;
using Ninject;

namespace TDJ.Panel.Library
{
    [Export(typeof(IPanel))]
    public class Panel : IPanel, IDisposable
    {
        [Inject]
        public IMessageParser Parser { get; set; }

        [Inject]
        public INotifier FireClient { get; set; }

        [Inject]
        public ILog Log { get; set; }

        [Inject]
        public IPanelConnection PanelConnection { get; set; }

        public void SetStay()
        {
            Log.DebugFormat("Set Stay Start");
            PanelConnection.Send(CommandString("3"));
            Log.DebugFormat("Set Stay End");
        }

        public void SetAway()
        {
            Log.DebugFormat("Set Away Start");
            PanelConnection.Send(CommandString("2"));
            Log.DebugFormat("Set Away End");
        }

        public void Disarm()
        {
            Log.DebugFormat("Set Disarm Start");
            PanelConnection.Send(CommandString("1"));
            Log.DebugFormat("Set Disarm End");
        }

        public bool? Chime()
        {
            PanelConnection.Send(CommandString("9"));
            var msg = GetMessage();
            if (msg != null)
            {
                return msg.Chime_Mode;
            }
            return null;
        }

        public void GetPanelStatus()
        {
            var msg = GetMessage();
            Console.WriteLine(JsonConvert.SerializeObject(msg));
            FireClient.SendMessage<TDJ.Panel.Library.Message>(msg);
        }

        public void ExecuteCommand(PanelCommand command)
        {
            Log.DebugFormat("Command: {0}", command);
            switch (command)
            {
                case PanelCommand.SetAway:
                    SetAway();
                    break;
                case PanelCommand.SetStay:
                    SetStay();
                    break;
                case PanelCommand.Disarm:
                    Disarm();
                    break;
                case PanelCommand.SetChime:
                    Chime();
                    break;
                case PanelCommand.Status:
                    GetPanelStatus();
                    break;
            }
        }

        private Message GetMessage()
        {
            var msg = PanelConnection.Get();
            
            return msg;
        }
        
        private string CommandString(string command)
        {
            return Config.PinCode + command;
        }

        public void Dispose()
        {
            PanelConnection.Close();
        }
    }
}

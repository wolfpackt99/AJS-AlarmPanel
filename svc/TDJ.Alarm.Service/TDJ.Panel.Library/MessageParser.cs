using System.Text.RegularExpressions;
using Common.Logging;
using Ninject;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJ.Panel.Library
{
    [Export(typeof(IMessageParser))]
    public class MessageParser : IMessageParser
    {
        [Inject]
        public ILog Logger { get; set; }

        private readonly string _regexPattern = "(\"(?:[^\"]|\"\")*\"|[^,]*),(\"(?:[^\"]|\"\")*\"|[^,]*),(\"(?:[^\"]|\"\")*\"|[^,]*),(\"(?:[^\"]|\"\")*\"|[^,]*)";

        public Message Process(string rawOutputMessage)
        {
            var m = new List<Message>();
            var strArrays = Regex.Split(rawOutputMessage, this._regexPattern);

            if (strArrays.Length != 6)
            {
                Logger.DebugFormat("1. Bad packet or parse error: '{0}'", rawOutputMessage);
            }
            else
            {
                UInt32 iMask = 0;
                try
                {
                    string mask = strArrays[3].Substring(3, 8);
                    iMask = Convert.ToUInt32(mask, 16);
                    Logger.DebugFormat("iMask: {0}", iMask);
                }
                catch
                {
                    Logger.DebugFormat(string.Format("2. Bad packet or parse error: '{0}'", rawOutputMessage));
                }
                
                if ((0xFF80 & iMask) > 0)
                {
                    Logger.DebugFormat("iMask: {0}, 0XFF80 & iMask {1} >0 = {2}", iMask, (0xFF80 & iMask), (0xFF80 & iMask)>0);
                    Message msg = new Message();
                    msg.Text = string.Empty;
                    try
                    {
                        msg.Ready = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(1, 1)));
                        msg.Armed_Away = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(2, 1)));
                        msg.Armed_Home = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(3, 1)));
                        msg.Back_Light = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(4, 1)));
                        msg.Programming_Mode = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(5, 1)));
                        msg.Beeps = Convert.ToInt16(strArrays[1].Substring(6, 1));
                        msg.Bypass = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(7, 1)));
                        msg.AC = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(8, 1)));
                        msg.Chime_Mode = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(9, 1)));
                        msg.AlarmEventOccured = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(10, 1)));
                        msg.AlarmBell = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(11, 1)));
                        msg.LowBattery = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(12, 1)));
                        msg.EntryDelayOff = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(13, 1)));
                        msg.FireAlarm = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(14, 1)));
                        msg.CheckZone = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(15, 1)));
                        msg.PerimeterOnly = Convert.ToBoolean(Convert.ToInt16(strArrays[1].Substring(16, 1)));
                        msg.Numeric = strArrays[2];
                        msg.Text = strArrays[4].Substring(1, 32);
                        int num2 = Convert.ToInt16(strArrays[3].Substring(19, 2), 16);
                        if ((num2 & 1) <= 0)
                        {
                            msg.Cursor = -1;
                        }
                        else
                        {
                            msg.Cursor = Convert.ToInt16(strArrays[3].Substring(21, 2), 16);
                        }

                        Logger.DebugFormat("Packet OK: '{0}'", rawOutputMessage);
                        Logger.DebugFormat(" -Ready: '{0}'", msg.Ready);
                        Logger.DebugFormat(" -Numeric: '{0}'", msg.Numeric);
                        Logger.DebugFormat(" -num2: '{0}'", num2);
                        Logger.DebugFormat(" -Cursor: '{0}'", msg.Cursor);
                        msg.BadPacket = false;
                        if (msg.Numeric == "0fc" && iMask == 128)
                        {
                            return msg;
                        }
                    }
                    catch
                    {
                        msg.BadPacket = true;
                        Logger.DebugFormat("3. Bad packet or parse error: '{0}'", rawOutputMessage);
                    }
                }
            }
            return null;
        }
    }
}

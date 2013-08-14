using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJ.Panel.Library
{
    public class Message
    {
        public bool Ready { get; set; }

        public bool Armed_Away { get; set; }

        public bool Armed_Home { get; set; }

        public bool Back_Light { get; set; }

        public bool Programming_Mode { get; set; }

        public int Beeps { get; set; }

        public bool Bypass { get; set; }

        public bool AC { get; set; }

        public bool Chime_Mode { get; set; }

        public bool AlarmEventOccured { get; set; }

        public bool AlarmBell { get; set; }

        public bool LowBattery { get; set; }

        public bool EntryDelayOff { get; set; }

        public bool FireAlarm { get; set; }

        public bool CheckZone { get; set; }

        public bool PerimeterOnly { get; set; }

        public string Numeric { get; set; }

        public string Text { get; set; }

        public int Cursor { get; set; }

        public bool BadPacket { get; set; }
    }
}

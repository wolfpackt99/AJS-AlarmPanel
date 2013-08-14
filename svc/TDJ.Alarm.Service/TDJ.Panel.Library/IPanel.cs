using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDJ.Panel.Library
{
    public interface IPanel
    {
        void SetStay();
        void SetAway();
        void Disarm();
        bool? Chime();
        void ExecuteCommand(PanelCommand command);
        void GetPanelStatus();
    }
}

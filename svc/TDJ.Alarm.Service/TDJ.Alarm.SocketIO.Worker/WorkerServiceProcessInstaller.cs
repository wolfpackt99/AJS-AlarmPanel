using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TDJ.Alarm.SocketIO.Worker
{
    [RunInstaller(true)]
    public class WorkerServiceProcessInstaller : ServiceProcessInstaller
    {
        public WorkerServiceProcessInstaller()
        {
            this.Account = ServiceAccount.NetworkService;
        }
    }
}

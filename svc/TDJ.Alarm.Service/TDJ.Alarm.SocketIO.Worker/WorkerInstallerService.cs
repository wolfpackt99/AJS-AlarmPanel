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
    public class WorkerServiceInstaller : ServiceInstaller
    {
        private Lazy<string> serviceName = new Lazy<string>(() => Program.ServiceName);
        private Lazy<string> displayName = new Lazy<string>(() => Program.DisplayName);

        public WorkerServiceInstaller()
        {
            this.Description = "Provides a service to " + serviceName;
            this.DisplayName = displayName.Value;
            this.ServiceName = serviceName.Value;
            this.StartType = ServiceStartMode.Automatic;
        }
    }
}

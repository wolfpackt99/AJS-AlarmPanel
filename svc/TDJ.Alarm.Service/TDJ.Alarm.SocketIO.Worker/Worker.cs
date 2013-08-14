using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ninject;
using SocketIOClient.Messages;
using System.Net;
using System.IO;
using TDJ.Panel.Library;

namespace TDJ.Alarm.SocketIO.Worker
{
    public partial class Worker : ServiceBase
    {
        private static readonly IKernel _kernel = new StandardKernel(new Bindings());
        private readonly IPanel _panel;

        public Worker()
        {
            InitializeComponent();
            //send panel a message....
            _panel = _kernel.Get<IPanel>();
        }

        internal void Start()
        {
            Console.WriteLine("intializing Rabbit Listener");
            var bus = RabbitHutch.CreateBus("host=localhost", serviceRegister => serviceRegister.Register(serviceProvider => new QueueConventions() as IConventions));
            bus.Subscribe<object>("panelStatusRequest", c => _panel.GetPanelStatus());
            bus.Subscribe<PanelCommand>("panelCommand", c => _panel.ExecuteCommand(c));
            
        }
        
        internal void Shutdown()
        {

        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            Start();
        }

        protected override void OnStop()
        {
            base.OnStop();
            Shutdown();
        }
    }
}

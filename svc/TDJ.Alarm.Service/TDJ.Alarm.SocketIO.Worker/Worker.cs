using System;
using System.ServiceProcess;
using EasyNetQ;
using Ninject;
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

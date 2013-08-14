using Common.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject.Extensions.NamedScope;

namespace TDJ.Panel.Library
{
    [Export(typeof(Bindings))]
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<ILog>().ToMethod(t => Common.Logging.LogManager.GetCurrentClassLogger());
            Bind<IMessageParser>().To(typeof (MessageParser)).InSingletonScope();
            Bind<INotifier>().To(typeof (FirebaseClient)).InCallScope();
            Bind<IPanelConnection>().To(typeof (PanelConnection)).InCallScope();
            Bind<IPanel>().To(typeof (Panel)).InCallScope();
        }
    }
}

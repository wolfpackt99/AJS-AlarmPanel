using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TDJ.Alarm.SocketIO.Worker
{
    class Program
    {
        public static string ServiceName { get; private set; }
        public static string DisplayName { get; private set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            bool install = false, uninstall = false, console = false;
            try
            {
                foreach (var arg in args)
                {
                    switch (arg)
                    {
                        case "-i":
                        case "-install":
                            install = true;
                            break;
                        case "-u":
                        case "-uninstall":
                            uninstall = true;
                            break;
                        case "-c":
                        case "-console":
                            console = true;
                            break;
                        default:
                            Console.Error.WriteLine("Argument not expected: " + arg);
                            break;
                    }
                }

                if (install)
                {
                    Install(false, args);
                }

                if (uninstall)
                {
                    Install(true, args);
                }

                if (console)
                {
                    Console.WriteLine("System running, press any key to stop");

                    var fl = new Worker();
                    fl.Start();

                    Console.ReadKey(true);

                    fl.Shutdown();
                }
                else if (!(install || uninstall))
                {
                    ServiceBase[] services = { new Worker() };
                    ServiceBase.Run(services);
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        static void Install(bool undo, string[] args)
        {
            try
            {
                if (args.Length < 2)
                {
                    Console.Error.WriteLine("Please provide 2 args: the 1st argument needs to be -i[-install], -u[-uninstall], or -c[-console], and the 2nd argument needs to be a service name");

                    return;
                }

                if (args[0] != "-i" && args[0] != "-install" && args[0] != "-u" && args[0] != "-uninstall")
                {
                    Console.Error.WriteLine("Please provide 2 args: the 1st argument needs to be -i[-install], -u[-uninstall], or -c[-console], and the 2nd argument needs to be a service name");

                    return;
                }

                ServiceName = args[1];
                DisplayName = "FM: " + ServiceName;

                Console.WriteLine(undo ? "uninstalling ..." : "installing ..." + ServiceName);
                using (var inst = new AssemblyInstaller(typeof(Program).Assembly, args))
                {
                    IDictionary state = new Hashtable();
                    inst.UseNewContext = true;

                    try
                    {
                        if (undo)
                        {
                            inst.Uninstall(state);
                        }
                        else
                        {
                            inst.Install(state);
                            inst.Commit(state);

                            var serviceController = new ServiceController(ServiceName);
                            serviceController.Start();
                        }
                    }
                    catch
                    {
                        try
                        {
                            inst.Rollback(state);
                        }
                        catch { }

                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}

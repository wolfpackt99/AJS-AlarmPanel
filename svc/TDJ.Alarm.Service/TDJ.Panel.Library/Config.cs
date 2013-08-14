using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDJ.Panel.Library
{
    public static class Config
    {
        const string PINCODE = "PinCode";

        static readonly Lazy<string> s_pinCode = new Lazy<string>(() => ConfigurationManager.AppSettings[PINCODE]);
        public static string PinCode { get { return s_pinCode.Value; } }
    }
}

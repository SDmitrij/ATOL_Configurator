using System;
using System.IO;

namespace Configurator.Base
{
    public static class Log
    {
        public static void ResultToFile(string result)
        {
            var logString = string.Format("{0} {1}\n", DateTime.Now.ToString(), result);
            var path = string.Format("{0}\\result.txt", Directory.GetCurrentDirectory());
            ToFile(path, logString);
        }

        public static void HardwareStateToFile(string result, bool json = false)
        {         
            if (!json) ToFile(string.Format("{0}\\hardware_state.txt", Directory.GetCurrentDirectory()), result);           
            ToFile(string.Format("{0}\\hardware_state.json", Directory.GetCurrentDirectory()), result);
        }

        private static void ToFile(string path, string result)
        {
            if (!File.Exists(path)) File.WriteAllText(path, result);
            File.AppendAllText(path, result);           
        }
    }
}

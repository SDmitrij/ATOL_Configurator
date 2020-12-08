using Configurator.Base.Device;
using Configurator.Base.Initialize;
using Configurator.Base.Out;
using System;
using System.IO;

namespace Configurator
{
    class Program
    {
        static void Main(string[] args)
        {
            var confPath = Path.Combine(string.Format("{0}\\conf.json", Environment.CurrentDirectory));
            try 
            {
                if (!File.Exists(confPath))
                    throw new Exception("Can't find conf.json file in app directory");
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
            
            var config = ConfigInstance.GetInstance(confPath).GetConfig();
            var log = new Log(config);
            new Interaction(config, log).Begin();          
        }
    }
}

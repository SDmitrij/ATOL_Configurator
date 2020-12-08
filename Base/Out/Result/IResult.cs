using Configurator.Base.Model;

namespace Configurator.Base.Out.Result
{
    public interface IResult
    {        
        public void ApplyConfig(Config config);
        public string GetResult();
        public string GetResultFile();
        public bool Json();
    }
}

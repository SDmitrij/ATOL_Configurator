using Atol.Drivers10.Fptr;
using Configurator.Base.Out;
using Configurator.Base.Out.Result;
using System;

namespace Configurator.Base.Initialize
{
    public class FptrInstance
    {
        #region private
        private static FptrInstance _fptrInstance;
        private readonly Fptr _fptr;

        private FptrInstance(Log log)
        {
            try
            {
                _fptr = new Fptr();
            }
            catch (Exception e)
            {
                log.Accept(new Execution(e.Message));
                log.Write();
            }
        }
        #endregion

        public static FptrInstance GetInstance(Log log)
        {
            if (_fptrInstance is null) _fptrInstance = new FptrInstance(log);         
            return _fptrInstance;
        }

        public Fptr GetFptr()
        {
            return _fptr;
        }
    }
}

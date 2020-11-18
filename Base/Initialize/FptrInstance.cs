using Atol.Drivers10.Fptr;
using System;

namespace Configurator.Base.Initialize
{
    public class FptrInstance
    {
        #region private
        private static FptrInstance _fptrInstance;
        private readonly Fptr _fptr;

        private FptrInstance()
        {
            try
            {
                _fptr = new Fptr();
            }
            catch (Exception e)
            {
                Log.ResultToFile(e.Message);
            }
        }
        #endregion

        public static FptrInstance GetInstance()
        {
            if (_fptrInstance is null) _fptrInstance = new FptrInstance();         
            return _fptrInstance;
        }

        public Fptr GetFptr()
        {
            return _fptr;
        }
    }
}

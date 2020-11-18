using Atol.Drivers10.Fptr;
using Configurator.Base.Initialize;
using Configurator.Base.Model;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace Configurator.Base.Device
{
    public class Interaction
    {
        public Interaction()
        {
            _fptr = FptrInstance.GetInstance().GetFptr();
        }

        public void Begin()
        {         
            try
            {
                OpenFptr();
                FptrLogicConn();
                CheckShiftStatus();
                CollectDeviceStatistics();
                ApplySettings();
                Finish();
            } catch(Exception e)
            {              
                Log.ResultToFile(e.Message);
            }
        }

        #region private

        private readonly Fptr _fptr;
        private string _jsonToExecute;

        private void OpenFptr()
        {
            if (_fptr.open() < 0)
                throw new Exception(string.Format("Something wrong with connection: " +
                    "{0} [{1}]", _fptr.errorCode(), _fptr.errorDescription()));
        }

        private void Finish()
        {
            _fptr.setParam(Constants.LIBFPTR_PARAM_REPORT_TYPE, Constants.LIBFPTR_RT_CLOSE_SHIFT);

            if (_fptr.report() < 0 && _fptr.checkDocumentClosed() < 0 && _fptr.close() < 0)
                throw new Exception(string.Format("Can't finish administrator's session: {0} [{1}]",
                    _fptr.errorCode(), _fptr.errorDescription()));
        }

        private void FptrLogicConn()
        {
            if (!_fptr.isOpened())
                throw new Exception(string.Format("Can't open fptr logical connection: {0} [{1}]",
                    _fptr.errorCode(), _fptr.errorDescription()));
        }

        private void CheckShiftStatus()
        { 
            if (_fptr.openShift() < 0)
                throw new Exception("The shift is already open, close shift to continue interaction.");
        }

        private void ApplySettings()
        {
            ReadJson();
            ValidateJsonToExecute();

            _fptr.setParam(Constants.LIBFPTR_PARAM_JSON_DATA, _jsonToExecute);

            var processed = _fptr.processJson();
            var result = _fptr.getParamString(Constants.LIBFPTR_PARAM_JSON_DATA);

            if (processed < 0 && result.Equals(string.Empty))
            {
                throw new Exception(string.Format("An a error occured in json execution: {0} [{1}]",
                    _fptr.errorCode(), _fptr.errorDescription()));
            }

            Log.ResultToFile(string.Format("Success: {0}", result));
        }

        private void ValidateJsonToExecute()
        {
            _fptr.setParam(Constants.LIBFPTR_PARAM_JSON_DATA, _jsonToExecute);

            if (_fptr.validateJson() < 0)
            {
                throw new Exception(string.Format("Wrong json: " +
                    "{0} [{1}]", _fptr.errorCode(), _fptr.errorDescription()));
            }
        }

        private void ReadJson()
        {
            var path = string.Format("{0}\\Json\\config.json", Directory.GetCurrentDirectory());
           
            if (!File.Exists(path)) throw new Exception("Can't find config.json file.");
            
            _jsonToExecute = File.ReadAllText(path);
        }

        private void CollectDeviceStatistics()
        {
            var res = new StringBuilder();
            res.Append(string.Format("------- Тех. состояние ККТ на {0} -------\n", DateTime.Now.ToString()));

            var state = new State(_fptr);
            var stateProperties = typeof(State).GetProperties();
           
            foreach (var prop in stateProperties)
            {
                var attr = (DescriptionAttribute)prop.GetCustomAttributes(false).FirstOrDefault();
                var val = prop.GetGetMethod().Invoke(state, null);

                res.Append(string.Format("{0}: {1},\n", attr.Description, val));          
            }

            res.Append("-----------");

            Log.HardwareStateToFile(res.ToString());
            Log.HardwareStateToFile(JsonSerializer.Serialize(state), true);
        }
        #endregion
    }
}

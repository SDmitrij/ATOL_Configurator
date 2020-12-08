using Atol.Drivers10.Fptr;
using System.ComponentModel;

namespace Configurator.Base.Model
{
    public class State
    {
        private readonly Fptr _fptr;

        public State(Fptr fptr) => _fptr = fptr;
        
        [Description("Заводской номер")]
        public string SerialNumber
        {
            get
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_SERIAL_NUMBER);
                _fptr.queryData();
                
                return _fptr.getParamString(Constants.LIBFPTR_PARAM_SERIAL_NUMBER); 
            }
        }

        [Description("Постоянный ресурс отрезчика")]
        public string PermanentSegmentResource 
        { 
            get 
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_CUTTER_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_ROLLUP);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            } 
        }

        [Description("Сбрасываемый ресурс отрезчика")]
        public string ResetableSegmentResource 
        { 
            get
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_CUTTER_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_RESETTABLE);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            }
        }

        [Description("Постоянный ресурс шагового двигателя для всех шагов")]
        public string PermanentStepMotoResForAllSteps 
        { 
            get 
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_STEP_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_ROLLUP);
                _fptr.setParam(Constants.LIBFPTR_PARAM_STEP_COUNTER_TYPE, Constants.LIBFPTR_SCT_OVERALL);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            } 
        }

        [Description("Постоянный ресурс шагового двигателя для шагов вперед")]
        public string PermanentStepMotoResForForwardSteps 
        {
            get 
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_STEP_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_ROLLUP);
                _fptr.setParam(Constants.LIBFPTR_PARAM_STEP_COUNTER_TYPE, Constants.LIBFPTR_SCT_FORWARD);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            }
        }
        
        [Description("Сбрасываемый ресурс шагового двигателя для всех шагов")]
        public string ResetableStepMotoResForAllSteps 
        { 
            get
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_STEP_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_RESETTABLE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_STEP_COUNTER_TYPE, Constants.LIBFPTR_SCT_OVERALL);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            }
        }

        [Description("Сбрасываемый ресурс шагового двигателя для шагов вперед")]
        public string ResetableStepMotoResForForwadSteps 
        { 
            get
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_STEP_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_RESETTABLE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_STEP_COUNTER_TYPE, Constants.LIBFPTR_SCT_FORWARD);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            }
        }

        [Description("Постоянный ресурс термо-печатающей головки")]
        public string PermanentThermoPrintHeadResource 
        {
            get 
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_TERMAL_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_ROLLUP);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            }
        }

        [Description("Сбрасываемый ресурс термо-печатающей головки")]
        public string ResetableThermoPrintHeadResource 
        { 
            get 
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_TERMAL_RESOURCE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_COUNTER_TYPE, Constants.LIBFPTR_CT_RESETTABLE);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_COUNT).ToString();
            } 
        }

        [Description("Температура термо-печатающей головки")]
        public string TermoPrintHeadTemperature 
        { 
            get 
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_PRINTER_TEMPERATURE);
                _fptr.queryData();

                return _fptr.getParamInt(Constants.LIBFPTR_PARAM_PRINTER_TEMPERATURE).ToString();
            } 
        }

        [Description("Состояние источника питания")]
        public object PowerSupplyState
        {
            get
            {
                _fptr.setParam(Constants.LIBFPTR_PARAM_DATA_TYPE, Constants.LIBFPTR_DT_POWER_SOURCE_STATE);
                _fptr.setParam(Constants.LIBFPTR_PARAM_POWER_SOURCE_TYPE, Constants.LIBFPTR_PST_BATTERY);
                _fptr.queryData();

                return new
                {
                    BatteryCharge = _fptr.getParamInt(Constants.LIBFPTR_PARAM_BATTERY_CHARGE).ToString(),
                    Voltage = _fptr.getParamDouble(Constants.LIBFPTR_PARAM_VOLTAGE).ToString(),
                    UseBattery = _fptr.getParamBool(Constants.LIBFPTR_PARAM_USE_BATTERY) ? "Yes" : "No",
                    BatteryCharging = _fptr.getParamBool(Constants.LIBFPTR_PARAM_BATTERY_CHARGING) ? "Yes" : "No",
                    CanPrintUsingBattery = _fptr.getParamBool(Constants.LIBFPTR_PARAM_CAN_PRINT_WHILE_ON_BATTERY) ? "Yes" : "No"
                };
            }
        }
    }
}

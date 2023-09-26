using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
//using System.Threading.Channels;
using System.Threading.Tasks;
using TCatSysManagerLib;
using TcAutomation.Core.Contracts;
using TwinCAT.Ads;


namespace TcAutomation.Core
{
    public class AdsController : IAdsController
    {
        private AdsClient _client;
        private string _amsNetId;
        private int _portForAds;
        private string _nameOfEnableVar;

        public AdsController(string amsNetId, int portForAds, string nameOfEnableVar)
        {
            this._amsNetId = amsNetId;
            this._portForAds = portForAds;
            this._nameOfEnableVar = nameOfEnableVar;
        }

        public string CreateInstance()
        {
            try
            {
                this._client = new AdsClient();
            }
            catch (Exception e)
            {
                return $"Error while creating AdsClient instance: {e}";
            }
            return "Success";
        }

        public string ClientConnect()
        {
            try
            {
                this._client.Connect(_amsNetId, _portForAds);
            }
            catch (Exception e)
            {
                return $"Error while connecting to PLC: {e}";
            }
            return "Success";
        }

        public string AdsReadFromPlc(string nameOfIntVarToRead, string defaultNameOfIntVarToRead)
        {
            int value = 0;
            string resultString = string.Empty;

            // first try the general info
            try
            {
                DeviceInfo deviceInfo = _client.ReadDeviceInfo();
                Version version = deviceInfo.Version.ConvertToStandard();
                resultString += $"Device name: {deviceInfo.Name}\n";
                resultString += $"Device version: {version}\n";
            }
            catch (Exception e)
            {
                return $"Error while reading device info from PLC: {e}";
            }

            // now try to read a variable
            try
            {
                try
                {
                    value = (int)_client.ReadValue
                    (
                        //"MAIN.uiCounter",
                        nameOfIntVarToRead,
                        typeof(int)
                    );
                    resultString += $"Value of variable {nameOfIntVarToRead}: {value}\n";
                }
                catch (Exception e)
                {
                    value = (int)_client.ReadValue
                    (
                        defaultNameOfIntVarToRead,
                        typeof(int)
                    );
                    resultString += $"Value of variable {defaultNameOfIntVarToRead}: {value}\n";
                }
            }
            catch (Exception e)
            {
                return $"Error while reading variable from PLC: {e}";
            }

            return resultString;
        }

        public string ToggleEnable()
        {
            string resultString = string.Empty;
            bool valueToWrite = true;

            try
            {
                var boolStatus = _client.ReadValue(_nameOfEnableVar, typeof(bool));

                if (boolStatus is bool)
                {
                    bool isTrue = (bool)boolStatus;

                    if (isTrue)
                    {
                        // Write false to the PLC
                        valueToWrite = false;
                        _client.WriteValue(_nameOfEnableVar, valueToWrite);
                        resultString = $"Value written: {valueToWrite}";
                    }
                    else
                    {
                        // Write true to the PLC
                        valueToWrite = true;
                        _client.WriteValue(_nameOfEnableVar, valueToWrite);
                        resultString = $"Value written: {valueToWrite}";
                    }
                }
                else
                {
                    resultString = "Value read is not boolean!";
                }
            }
            catch (Exception e)
            {
                return $"Error while enabling / disabling: {e}";
            }
            return resultString;
        }

        public string ToggleStartStop()
        {
            string resultString = string.Empty;

            try
            {
                StateInfo stateInfo = _client.ReadState();
                AdsState state = AdsState.Invalid;
                state = stateInfo.AdsState;
                short deviceState = stateInfo.DeviceState;

                if (state == AdsState.Stop)
                {
                    _client.WriteControl(new StateInfo(AdsState.Run, 0));
                }
                else if (state == AdsState.Run)
                {
                    _client.WriteControl(new StateInfo(AdsState.Stop, 0));
                }

                stateInfo = _client.ReadState();
                state = stateInfo.AdsState;
                deviceState = stateInfo.DeviceState;

                resultString += $"DeviceState: {deviceState}\n";
                resultString += $"AdsState: {state}\n";
            }
            catch (Exception e)
            {
                return $"Error while starting / stopping: {e}";
            }
            return resultString;
        }

        public string ClientDisconnect()
        {
            try
            {
                this._client.Disconnect();
                this._client.Dispose();
            }
            catch (Exception e)
            {
                return $"Error while disconnecting from PLC: {e}";
            }
            return "Success";
        }
    }
}

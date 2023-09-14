using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TCatSysManagerLib;
using TcAutomation.Core.Contracts;
using TwinCAT.Ads;


namespace TcAutomation.Core
{
    public class AdsController : IAdsController
    {
        private AdsClient client;
        private string amsNetId;
        private int portForAds;
        private string nameOfEnableVar;

        public AdsController(string amsNetId, int portForAds, string nameOfEnableVar)
        {
            this.amsNetId = amsNetId;
            this.portForAds = portForAds;
            this.nameOfEnableVar = nameOfEnableVar;
        }

        public string CreateInstance()
        {
            try
            {
                this.client = new AdsClient();
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
                this.client.Connect(amsNetId, portForAds);
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
                DeviceInfo deviceInfo = client.ReadDeviceInfo();
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
                    value = (int)client.ReadValue
                    (
                        nameOfIntVarToRead,
                        typeof(int)
                    );
                    resultString += $"Value of variable {nameOfIntVarToRead}: {value}\n";
                }
                catch (Exception e)
                {
                    value = (int)client.ReadValue
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
                var boolStatus = client.ReadValue(nameOfEnableVar, typeof(bool));

                if (boolStatus is bool)
                {
                    bool isTrue = (bool)boolStatus;

                    if (isTrue)
                    {
                        // Write false to the PLC
                        valueToWrite = false;
                        client.WriteValue(nameOfEnableVar, valueToWrite);
                        resultString = $"Value written: {valueToWrite}";
                    }
                    else
                    {
                        // Write true to the PLC
                        valueToWrite = true;
                        client.WriteValue(nameOfEnableVar, valueToWrite);
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
                StateInfo stateInfo = client.ReadState();
                AdsState state = AdsState.Invalid;
                state = stateInfo.AdsState;
                short deviceState = stateInfo.DeviceState;

                if (state == AdsState.Stop)
                {
                    client.WriteControl(new StateInfo(AdsState.Run, 0));
                }
                else if (state == AdsState.Run)
                {
                    client.WriteControl(new StateInfo(AdsState.Stop, 0));
                }

                stateInfo = client.ReadState();
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
                this.client.Disconnect();
                this.client.Dispose();
            }
            catch (Exception e)
            {
                return $"Error while disconnecting from PLC: {e}";
            }
            return "Success";
        }
    }
}

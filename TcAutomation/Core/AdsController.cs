using System;
using TcAutomation.Core.Contracts;
using TwinCAT.Ads;
using TcAutomation.Utilities.Messages;


/// <summary>
/// Used to manipulate the ADS library for TwinCAT.
/// </summary>
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

        /// <summary>
        /// Creates an instance of the AdsClient class.
        /// </summary>
        /// <returns></returns>
        public string CreateInstance()
        {
            try
            {
                this._client = new AdsClient();
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.CreatingAdsClientError, e.Message);
            }
            return CustomReturnMessages.returnStringWhenSuccess;
        }

        /// <summary>
        /// Connects to the PLC.
        /// </summary>
        /// <returns></returns>
        public string ClientConnect()
        {
            try
            {
                this._client.Connect(_amsNetId, _portForAds);
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.ConnectingToPlcError, e.Message);
            }
            return CustomReturnMessages.returnStringWhenSuccess;
        }

        /// <summary>
        /// Reads the device info and a variable from the PLC.
        /// </summary>
        /// <param name="nameOfIntVarToRead"></param>
        /// <param name="defaultNameOfIntVarToRead"></param>
        /// <returns></returns>
        public string AdsReadFromPlc(string nameOfIntVarToRead, string defaultNameOfIntVarToRead)
        {
            int value = 0;
            string resultString = string.Empty;

            // first try to read the general info
            try
            {
                DeviceInfo deviceInfo = _client.ReadDeviceInfo();
                Version version = deviceInfo.Version.ConvertToStandard();
                resultString += $"Device name: {deviceInfo.Name}\n";
                resultString += $"Device version: {version}\n";
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.ReadingDeviceInfoError, e.Message);
            }

            // now try to read a variable
            try
            {
                try
                {
                    value = (int)_client.ReadValue
                    (
                        nameOfIntVarToRead,
                        typeof(int)
                    );
                    resultString += $"Value of variable {nameOfIntVarToRead}: \n {value}\n";
                }
                catch (Exception e)
                {
                    value = (int)_client.ReadValue
                    (
                        defaultNameOfIntVarToRead,
                        typeof(int)
                    );
                    resultString += $"Value of variable {defaultNameOfIntVarToRead}: \n {value}\n";
                }
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.ReadingVariableError, e.Message);
            }

            return resultString;
        }

        /// <summary>
        /// Toggle the enable variable in the PLC program.
        /// </summary>
        /// <returns></returns>
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
                return string.Format(CustomExceptionMessages.EnablingDisablingError, e.Message);
            }
            return resultString;
        }

        /// <summary>
        /// Toggle the start / stop state of the PLC program.
        /// </summary>
        /// <returns></returns>
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
                return string.Format(CustomExceptionMessages.StartingStoppingError, e.Message);
            }
            return resultString;
        }

        /// <summary>
        /// Disconnects from the PLC.
        /// </summary>
        /// <returns></returns>
        public string ClientDisconnect()
        {
            try
            {
                this._client.Disconnect();
                this._client.Dispose();
            }
            catch (Exception e)
            {
                return string.Format(CustomExceptionMessages.DisconnectingError, e.Message);
            }
            return CustomReturnMessages.returnStringWhenSuccess;
        }
    }
}

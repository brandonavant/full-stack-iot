// Copyright (c) Brandon Avant. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Iot.Device.Pn532;
using System.Device.I2c;
using Iot.Device.Pn532.ListPassive;
using System.Threading;
using System.Collections.Concurrent;

namespace BrandonAvant.FullStackIoT.FobScanner
{
    public class Program
    {
        /// <summary>
        /// A thread-safe key/value collection that allows multiple threads to share information.
        /// </summary>
        /// <typeparam name="string">The key.</typeparam>
        /// <typeparam name="object">The object associated with the given key.</typeparam>
        /// <returns></returns>
        private static ConcurrentDictionary<string, object> _sharedDict = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// The main thread of the application.
        /// </summary>
        /// <param name="args">Array of runtime arguments.</param>
        static void Main(string[] args)
        {
            try
            {
                var i2cDeviceAddress = 0x24;
                var device = I2cDevice.Create(new I2cConnectionSettings(1, i2cDeviceAddress));

                FirmwareVersion version;
                ScannerState initialState = new ScannerState { Status = AvailabilityStatus.Idle };

                _sharedDict.TryAdd(SharedStateKeys.SCANNER_STATE, initialState);

                using (var pn532 = new Pn532(device))
                {
                    version = pn532.FirmwareVersion;
                    if (version == null)
                    {
                        throw new Exception("Unable to determine firmware version.");
                    }

                    Console.WriteLine("Listening...");

                    while (true)
                    {
                        ScannerState currentState = GetCurrentState();

                        // Avoid scanning when we are awaiting a response from the Cloud.
                        if (currentState.Status == AvailabilityStatus.Idle)
                        {
                            ReadMiFare(pn532);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Reads the NFCID from a card presented to the PN532.
        /// </summary>
        /// <param name="pn532">The initialized Pn532 object.</param>
        static void ReadMiFare(Pn532 pn532)
        {
            byte[] retData = null;
            ScannerState currentState;
            Timer operationTimeout;
            string nfcId = string.Empty;

            while ((!Console.KeyAvailable))
            {
                retData = pn532.ListPassiveTarget(MaxTarget.One, TargetBaudRate.B106kbpsTypeA);
                if (retData != null)
                    break;

                Thread.Sleep(200);
            }

            if (retData == null)
                return;

            var decrypted = pn532.TryDecode106kbpsTypeA(retData.AsSpan().Slice(1));
            if (decrypted != null)
            {
                currentState = GetCurrentState();

                if (currentState.Status == AvailabilityStatus.Idle)
                {
                    nfcId = BitConverter.ToString(decrypted.NfcId);
                    Console.WriteLine($"Authenticating: {nfcId} {DateTime.UtcNow.ToString()}...");

                    UpdateScannerStatus(AvailabilityStatus.AwaitingAuthentication, nfcId);
                    // TODO: Send a request to IoT Hub with the NFCID

                    operationTimeout = new Timer(
                        x => ExpireAuth(),
                        null, 8000, Timeout.Infinite
                    );
                }
            }
        }

        /// <summary>
        /// Marks an existing authentication as expired, thus transitioning the state back to 'Idle'.
        /// </summary>
        private static void ExpireAuth()
        {
            // TODO: Implement logic to either turn the green light red (if the auth was successful)
            UpdateScannerStatus(AvailabilityStatus.Idle);


            // TODO: Implement log to tell the React app that the auth has expired.

            Console.WriteLine($"Auth expired {DateTime.UtcNow.ToString()}.");
        }

        /// <summary>
        /// Updates the ScannerState with a new status.
        /// </summary>
        /// <param name="newStatus">The new status.</param>
        /// <param name="nfcId">The nfcId associated with transitioning to a status of 'AwaitingAuthentication'.</param>
        /// <remarks>When the status is set to 'AwaitingAuthentication', the nfcId MUST be specified.</remarks>
        private static void UpdateScannerStatus(AvailabilityStatus newStatus, string nfcId = null)
        {
            ScannerState currentState, previousState;

            if (newStatus == AvailabilityStatus.AwaitingAuthentication && nfcId == null)
            {
                throw new Exception("When transitioning to 'AwaitingAuthentication', an nfcId must be specified.");
            }

            currentState = GetCurrentState();
            previousState = currentState;

            currentState.Status = newStatus;

            if (nfcId != null)
            {
                currentState.LastScanNfcId = nfcId;
                currentState.LastScanTimestamp = DateTime.UtcNow;
            }

            if (!_sharedDict.TryUpdate(SharedStateKeys.SCANNER_STATE, currentState, previousState))
            {
                throw new Exception("Unexpected state change.");
            }
        }

        /// <summary>
        /// Retrieves the current scanner state from the concurrent dictionary.
        /// </summary>
        /// <returns></returns>
        private static ScannerState GetCurrentState()
        {
            if (!_sharedDict.TryGetValue(SharedStateKeys.SCANNER_STATE, out object state))
            {
                throw new Exception("Failed to retrieve scanner state.");
            }

            return (ScannerState)state;
        }


        // TODO: Add IoT Hub Listener
    }
}

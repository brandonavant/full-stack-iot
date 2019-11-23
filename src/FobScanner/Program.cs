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
        
        static void Main(string[] args)
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
                    ScannerState currentState;

                    if (!_sharedDict.TryGetValue(SharedStateKeys.SCANNER_STATE, out object state))
                    {
                        throw new Exception("Failed to retrieve scanner state.");
                    }

                    currentState = (ScannerState)state;

                    // Avoid scanning when we are awaiting a response from the Cloud.
                    if (currentState.Status == AvailabilityStatus.Idle)
                    {
                        ReadMiFare(pn532);
                    }
                }
            }
        }

        /// <summary>
        /// Reads the NFCID from a card presented to the PN532.
        /// </summary>
        /// <param name="pn532">The initialized Pn532 object.</param>
        static void ReadMiFare(Pn532 pn532)
        {
            byte[] retData = null;

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
                Console.WriteLine($"Authenticating: {BitConverter.ToString(decrypted.NfcId)}...");

                
                // TODO: Send a request to IoT Hub with the NFCID and wait for a response.
            }
        }
    }
}

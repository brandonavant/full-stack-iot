// Copyright (c) Brandon Avant. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using Newtonsoft.Json;

namespace BrandonAvant.FullStackIoT.FobScanner
{
    /// <summary>
    /// Keeps track of the state of the scanner.
    /// </summary>
    public class ScannerState
    {
        /// <summary>
        /// The status of the scanner; either available for scanning or pending a reply from the Cloud.
        /// </summary>
        [JsonProperty("status", Required = Required.Always)]
        public AvailabilityStatus Status { get; set; }

        /// <summary>
        /// The UTC timestamp of the last tag scan.
        /// </summary>
        [JsonProperty("lastScanTimestamp")]
        public DateTime LastScanTimestamp { get; set; }

        /// <summary>
        /// The NfcId of the last tag scan.
        /// </summary>
        [JsonProperty("lastScanNfcId")]
        public byte[] LastScanNfcId { get; set; }
    }

}
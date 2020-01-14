// Copyright (c) Brandon Avant. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BrandonAvant.FullStackIoT.FobScanner
{
    /// <summary>
    /// Indicates the availability status of the RFID scanner.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum AvailabilityStatus
    {
        /// <summary>
        /// The scanner is idle and available to scan.
        /// </summary>
        [EnumMember(Value = "Idle")]
        Idle = 1,

        /// <summary>
        /// The scanner has sent an auth inquiry and is awaiting a response.
        /// </summary>
        [EnumMember(Value = "AwaitingAuthentication")]
        AwaitingAuthentication = 2
    }
}
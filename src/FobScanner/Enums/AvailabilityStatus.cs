// Copyright (c) Brandon Avant. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

namespace BrandonAvant.FullStackIoT.FobScanner
{
    /// <summary>
    /// Indicates the availability status of the RFID scanner.
    /// </summary>
    public enum AvailabilityStatus
    {
        /// <summary>
        /// The scanner is idle and available to scan.
        /// </summary>
        Idle = 1,

        /// <summary>
        /// The scanner has sent an auth inquiry and is awaiting a response.
        /// </summary>
        AwaitingAuthentication = 2
    }
}
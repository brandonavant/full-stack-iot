// Copyright (c) Brandon Avant. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using Newtonsoft.Json;

namespace BrandonAvant.FullStackIoT.FobScanner
{
    /// <summary>
    /// DTO with which a request for authenticating an scanned nfcId can be made.
    /// </summary>
    public class ScannerAuthRequest : D2CMessage
    {
        /// <summary>
        /// Indicates identity of this D2C message.
        /// </summary>
        [JsonProperty("messageType")]
        public override MessageType MessageType => MessageType.ScannerAuthRequest;

        /// <summary>
        /// The nfcId associated with the user who shall be authenticated.
        /// </summary>
        [JsonProperty("nfcId")]
        public string NfcId { get; private set; }

        /// <summary>
        /// Initializes a new instance of ScannerAuthRequest.
        /// </summary>
        /// <param name="nfcId">The nfcId to authenticate.</param>
        public ScannerAuthRequest(string nfcId) { NfcId = nfcId; }
    }
}

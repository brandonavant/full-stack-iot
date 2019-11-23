using System;

namespace FobScanner
{
    /// <summary>
    /// Keeps track of the state of the scanner.
    /// </summary>
    public class ScannerState
    {
        /// <summary>
        /// The status of the scanner; either available for scanning or pending a reply from the Cloud.
        /// </summary>
        public AvailabilityStatus Status { get; set; }

        /// <summary>
        /// The UTC timestamp of the last tag scan.
        /// </summary>
        public DateTime LastScanTimestamp { get; set; }

        /// <summary>
        /// The NfcId of the last tag scan.
        /// </summary>
        public byte[] LastScanNfcId { get; set; }
    }

}
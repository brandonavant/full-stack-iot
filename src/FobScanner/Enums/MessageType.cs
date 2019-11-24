using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace BrandonAvant.FullStackIoT.FobScanner
{
    /// <summary>
    /// An enumeration of the available D2C messages.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MessageType
    {
        /// <summary>
        /// A request to authenticate a scanned nfcId.ss
        /// </summary>
        [EnumMember(Value="ScannerAuthRequest")]
        ScannerAuthRequest = 1
    }
}

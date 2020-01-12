namespace BrandonAvant.FullStackIoT.FobScanner
{
    /// <summary>
    /// Base constructor for D2C messages.
    /// </summary>
    public abstract class D2CMessage
    {
        /// <summary>
        /// The type of message being constructed.
        /// </summary>
        public abstract MessageType MessageType { get; }

        /// <summary>
        /// Base constructor for D2CMessage inherited classes.
        /// </summary>
        protected D2CMessage() { }
    }
}

namespace Cryptography
{
    /// <summary>
    /// An interface for decoding encoding strings
    /// </summary>
    public interface IDecoder
    {
        /// <summary>
        /// Implement this method and add the functionality to decode an encoded string
        /// </summary>
        /// <param name="encodedText">The string to be decoded</param>
        string Decode(string encodedText);
    }
}
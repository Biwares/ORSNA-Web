namespace Orsna.Helpers
{
    public interface ISecurity
    {
        void Validate(string u, string i);
        string Base64Decode(string sData);
        string Base64Encode(string sData);
        string DecodeHex(string HexString);
    }
}

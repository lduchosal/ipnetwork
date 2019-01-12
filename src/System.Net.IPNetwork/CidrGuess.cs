namespace System.Net
{
    public interface CidrGuess
    {
        bool TryGuessCidr(string ip, out byte cidr);
    }
}

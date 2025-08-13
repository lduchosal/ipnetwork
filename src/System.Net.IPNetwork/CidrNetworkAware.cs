using System.Net.Sockets;

namespace System.Net;

/// <summary>
/// Tries to guess a network-aware CIDR prefix length from a textual IP address
/// </summary>
public sealed class CidrNetworkAware : ICidrGuess
{
    /// <summary>
    /// Tries to guess a network-aware CIDR prefix length from a textual IP address.
    /// IPv4: honors trailing 0s (network) and trailing 255s (wildcard hint) at octet boundaries.
    /// IPv6: honors trailing :0000 at hextet (16-bit) boundaries. Optional trailing :ffff wildcard heuristic is off by default.
    /// </summary>
    /// <param name="input">IP address as string (no slash). Example: "192.0.43.0" or "2001:db8::".</param>
    /// <param name="cidr">Guessed CIDR (0..32 for IPv4, 0..128 for IPv6).</param>
    /// <returns>true if parsed and guessed; false if input is not a valid IP address.</returns>
    public bool TryGuessCidr(string input, out byte cidr)
    {
        cidr = 0;
        if (string.IsNullOrWhiteSpace(input))
            return false;

        // Reject if user passed a slash - this API expects a plain address.
        // (You can relax this if you want to honor an explicitly supplied prefix.)
        if (input.Contains("/"))
            return false;

        if (!IPAddress.TryParse(input.Trim(), out var ip))
            return false;

        if (ip.AddressFamily == AddressFamily.InterNetwork)
        {
            cidr = GuessIpv4(ip);
            return true;
        }
        else if (ip.AddressFamily == AddressFamily.InterNetworkV6)
        {
            cidr = GuessIpv6(ip);
            return true;
        }

        return false;
    }

    private byte GuessIpv4(IPAddress ip)
    {
        byte[] b = ip.GetAddressBytes(); // length 4

        // /0 if all 0s or all 255s
        bool allZero = b[0] == 0 && b[1] == 0 && b[2] == 0 && b[3] == 0;
        bool allFf   = b[0] == 255 && b[1] == 255 && b[2] == 255 && b[3] == 255;
        if (allZero || allFf) return 0;

        // Network-aware boundaries via trailing zeros (network) OR trailing 255 (wildcard hint)
        bool last3Zero = b[1] == 0   && b[2] == 0   && b[3] == 0;
        bool last2Zero =              b[2] == 0     && b[3] == 0;
        bool last1Zero =                               b[3] == 0;

        bool last3Ff   = b[1] == 255 && b[2] == 255 && b[3] == 255;
        bool last2Ff   =              b[2] == 255   && b[3] == 255;
        bool last1Ff   =                               b[3] == 255;

        if (last3Zero || last3Ff) return 8;
        if (last2Zero || last2Ff) return 16;
        if (last1Zero || last1Ff) return 24;

        // Otherwise host address
        return 32;
    }

    private byte GuessIpv6(IPAddress ip)
    {
        byte[] b = ip.GetAddressBytes(); // length 16

        // Count trailing zero hextets (pairs of bytes == 0x0000)
        int trailingZeroHextets = CountTrailingHextets(b, 0x0000);
        if (trailingZeroHextets == 8) return 0; // all zero address '::'
        if (trailingZeroHextets > 0)  return (byte)(128 - 16 * trailingZeroHextets);

        // Otherwise host address
        return 128;
    }

    private int CountTrailingHextets(byte[] bytes, ushort value)
    {
        // bytes.Length must be 16 for IPv6
        int count = 0;
        for (int i = bytes.Length - 2; i >= 0; i -= 2)
        {
            ushort hextet = (ushort)((bytes[i] << 8) | bytes[i + 1]);
            if (hextet == value) count++;
            else break;
        }
        return count;
    }
}
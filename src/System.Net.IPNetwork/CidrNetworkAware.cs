using System.Net.Sockets;

namespace System.Net;

/// <summary>
/// If your CidrGuess is “network-aware” based only on what humans usually encode in the textual address, a good heuristic is:
/// 
/// IPv4 (dotted-quad)
///    • Treat trailing 0s as “network bits” and trailing 255s as a “wildcard” hint for the same boundary.
///    • Otherwise, fall back to /32 (no safe aggregation from the string alone).
///    • Special case: 0.0.0.0 (or 255.255.255.255) → /0.
///
/// Rule of thumb
///  Ends with .0 → /24
///  Ends with .0.0 or .255.255 → /16
///  Ends with .0.0.0 or .255.255.255 → /8
///  Else → /32
///
/// Matches your examples
///  Parse("192.0.43.8") → /32
///  Parse("192.0.43.0") → /24
///  Parse("192.43.0.0") → /16
///  Parse("192.0.43.255") → /24 (wildcard hint)
///  Parse("192.43.255.255") → /16 (wildcard hint)
///
/// So: in a network-aware context like this, you generally don’t emit /25, /26, etc.,
/// because there’s no reliable visual cue for those in dotted-quad—stick to /32, /24, /16, /8, /0.
///
/// IPv6 (colon-hex)
///  IPv6 is grouped by hextets (16-bit chunks), so mirror the idea at 16-bit boundaries.
///  Use trailing :0000 hextets as “network bits”.
///  Otherwise, fall back to /128.
///  Note: operationally, /64 is the standard host subnet size, but you should still infer from the string, not assumptions.
///
/// Rule of thumb
///  Ends with :0000 → /112
///  Ends with :0000:0000 → /96
///  Ends with three trailing :0000 → /80
///  …
///  Ends with four trailing :0000 → /64
///  Else → /128
///
/// Examples
///  2001:db8:1:2:3:4:5:6 → /128
///  2001:db8:1:2:3:4:5:0000 → /112
///  2001:db8:1:2:3:4:0000:0000 → /96
///  2001:db8:1:2:3:0000:0000:0000 → /80
///  2001:db8:1:2:0000:0000:0000:0000 → /64
///
/// TL;DR
///  IPv4: stick to /32, /24, /16, /8, /0 based on trailing .0/.255; otherwise /32.
///  IPv6: infer /128, /112, /96, /80, /64, … based on trailing :0000 groups; otherwise /128.
/// </summary>
public sealed class CidrNetworkAware : ICidrGuess
{
    /// <summary>
    /// Tries to guess a network-aware CIDR prefix length from a textual IP address.
    /// IPv4: honors trailing 0s (network) and trailing 255s (wildcard hint) at octet boundaries.
    /// IPv6: honors trailing :0000 at hextet (16-bit) boundaries. Optional trailing :ffff wildcard heuristic is off by default.
    /// </summary>
    /// <param name="ip">IP address as string (no slash). Example: "192.0.43.0" or "2001:db8::".</param>
    /// <param name="cidr">Guessed CIDR (0..32 for IPv4, 0..128 for IPv6).</param>
    /// <returns>true if parsed and guessed; false if input is not a valid IP address.</returns>
    public bool TryGuessCidr(string ip, out byte cidr)
    {
        cidr = 0;
        if (string.IsNullOrWhiteSpace(ip))
            return false;

        // Reject if user passed a slash - this API expects a plain address.
        // (You can relax this if you want to honor an explicitly supplied prefix.)
        if (ip.Contains("/"))
            return false;

        if (!IPAddress.TryParse(ip.Trim(), out var ipAddress))
            return false;

        if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
        {
            cidr = GuessIpv4(ipAddress);
            return true;
        }
        else if (ipAddress.AddressFamily == AddressFamily.InterNetworkV6)
        {
            cidr = GuessIpv6(ipAddress);
            return true;
        }

        return false;
    }

    private static byte GuessIpv4(IPAddress ip)
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

    private static byte GuessIpv6(IPAddress ip)
    {
        byte[] b = ip.GetAddressBytes(); // length 16

        // Count trailing zero hextets (pairs of bytes == 0x0000)
        int trailingZeroHextets = CountTrailingHextets(b, 0x0000);
        if (trailingZeroHextets == 8) return 0; // all zero address '::'
        if (trailingZeroHextets > 0)  return (byte)(128 - 16 * trailingZeroHextets);

        // Otherwise host address
        return 128;
    }

    private static int CountTrailingHextets(byte[] bytes, ushort value)
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
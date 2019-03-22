using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("System.Net.IPNetwork")]
[assembly: AssemblyDescription("IPNetwork C# library take care of complex network, ip, ipv4, ipv6, netmask, cidr, subnet, subnetting, supernet and supernetting calculation for .Net developpers. It works with IPv4 and IPv6 as well. It is written in C# for .NetStandard and coreclr and has a light and clean API and is fully unit tested.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Luc Dvchosal")]
[assembly: AssemblyProduct("System.Net.IPNetwork")]
[assembly: AssemblyCopyright("Copyright © Luc Dvchosal 2018")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("2e650d19-7041-43bd-acab-48a0b2ff2524")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("2.4.0.0")]
[assembly: AssemblyFileVersion("2.4.0.0")]
#if !TRAVISCI
[assembly: InternalsVisibleTo("System.Net.IPNetwork.TestProject.NetCore, PublicKey=00240000048000009400000006020000002400005253413100040000010001004d29ae79cfcf603de0200afc96f4d8304aa857341b78e706fedb3f0ac9c9d613443cea78a1ee687def573ad45b5cdc0abeeb1db304eec7c07331015d8aeeb3fd5e092273a2347e6cb54803a00484807c64bc3092f17619abfc5290133efad358a27747bfe71d1dc23b461d7cf91272844fc7a8390dc63b16236729dadb2c21bc")]
[assembly: InternalsVisibleTo("System.Net.IPNetwork.TestProject.NetFramework, PublicKey=00240000048000009400000006020000002400005253413100040000010001004d29ae79cfcf603de0200afc96f4d8304aa857341b78e706fedb3f0ac9c9d613443cea78a1ee687def573ad45b5cdc0abeeb1db304eec7c07331015d8aeeb3fd5e092273a2347e6cb54803a00484807c64bc3092f17619abfc5290133efad358a27747bfe71d1dc23b461d7cf91272844fc7a8390dc63b16236729dadb2c21bc")]
#endif
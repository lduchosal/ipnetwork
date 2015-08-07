using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Net.IPNetwork.ConsoleApplication {
    public enum ActionEnum {
        Usage,
        PrintNetworks,
        Subnet,
        Supernet,
        WideSupernet,
        ListIPAddress,
        ContainNetwork,
        OverlapNetwork,
        SubstractNetwork
    }
}

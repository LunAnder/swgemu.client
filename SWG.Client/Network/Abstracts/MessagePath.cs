using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SWG.Client.Network.Abstracts
{
    enum MessagePath : int
    {
        None = 0,
        DataChannelA = 1,
        RoutedReliable = 2,
        RoutedMulti = 3,
        BuildPacketStarted = 4,
        BuildPacketEnded = 5,
        Multi = 6,
        BuildPacketUnreliable = 7
    };

}

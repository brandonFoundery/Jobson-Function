using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobson_Data.Enums
{
    public enum JobStatusEnum
    {
        New = 0,
        ReadyForBid,
        WaitingForClient,
        Won,
        ClosedLost,
        ClosedNoResponse
    }
}

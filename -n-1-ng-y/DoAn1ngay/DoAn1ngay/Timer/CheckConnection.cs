using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DoAn1ngay.Timer
{
    public class CheckConnection
    {
        private int invokeCount;
        private int maxCount;

        public CheckConnection(int count)
        {
            invokeCount = 0;
            maxCount = count;
        }

        public void CheckStatus(Object stateInfo)
        {
            AutoResetEvent autoEvent = (AutoResetEvent)stateInfo;
            

            if (invokeCount == maxCount)
            {
                // Reset the counter and signal the waiting thread.
                invokeCount = 0;
                autoEvent.Set();
            }
        }




    }
}

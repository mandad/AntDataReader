using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    interface ANTDataInterpreter
    {
        void HaveMessages();
        void OnChannelClosed();
        void OnChannelOpened();
        void DisplayMessage(string message);
    }
}

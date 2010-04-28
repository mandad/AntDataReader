using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    /// <summary>
    /// The interface defining a class that can recieve callbacks from the AntCommunication and BufferedReader classes
    /// </summary>
    interface ANTDataInterpreter
    {
        void HaveMessages();
        void OnChannelClosed();
        void OnChannelOpened();
        void DisplayMessage(string message);
    }
}

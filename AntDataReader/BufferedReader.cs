using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntDataReader
{
    class BufferedReader
    {
        const int maxIndex = 10000;
        byte[] buffer;
        int readIndex;
        int writeIndex;
        int cmpWrite;
        int messageStart;
        int messageLength;
        ANTDataInterpreter parent;
        List<byte[]> messages;
        bool readingMessage;

        /// <summary>
        /// The messages currently stored, destructive on read
        /// </summary>
        public List<byte[]> Messages
        {
            get //we clear the messages on read
            {
                List<byte[]> retVal = new List<byte[]>(messages);
                messages.Clear();
                return retVal;
            }
        }

        public BufferedReader(ANTDataInterpreter parentForm)
        {
            buffer = new byte[maxIndex + 1];
            readIndex = 0;
            writeIndex = 0;
            cmpWrite = 0;
            messages = new List<byte[]>();
            readingMessage = false;
            parent = parentForm;
        }

        public void AddNewReceived(byte[] data)
        {
            if ((writeIndex + data.Length) <= maxIndex)
            {
                Buffer.BlockCopy(data, 0, buffer, writeIndex, data.Length);
                writeIndex += data.Length;
            }
            else
            {
                //need to wrap
                int atEnd = maxIndex - writeIndex + 1;
                Buffer.BlockCopy(data, 0, buffer, writeIndex, atEnd);
                Buffer.BlockCopy(data, atEnd - 1, buffer, 0, data.Length - atEnd);
                writeIndex = data.Length - atEnd; // reset index
                cmpWrite = maxIndex;
            }

            CheckForMessage();
            if (messages.Count > 0)
            {
                parent.HaveMessages();
            }
        }

        private void CheckForMessage()
        {
            if (readingMessage)
            {
                //check if got lenght yet, if we are checking again we have more data and thus the length
                if (messageLength == 0)
                {
                    if (messageStart == maxIndex)
                    {
                        messageLength = buffer[0] + 4;
                    }
                    else
                    {
                        messageLength = buffer[messageStart + 1] + 4;   //message has 4 fixed bytes on top of data
                    }
                }
                //now see if we can read the whole message
                if ((writeIndex + cmpWrite) >= (messageStart + messageLength))
                {
                    byte[] curMessage = new byte[messageLength];
                    if ((messageStart + messageLength) <= maxIndex)
                    {
                        Buffer.BlockCopy(buffer, messageStart, curMessage, 0, messageLength);
                        messageStart += messageLength;
                    }
                    else
                    {
                        int atEnd = maxIndex - messageStart + 1;
                        Buffer.BlockCopy(buffer, messageStart, curMessage, 0, atEnd);
                        Buffer.BlockCopy(buffer, 0, curMessage, atEnd - 1, messageLength - atEnd);
                        messageStart = messageLength - atEnd;
                        cmpWrite = 0;
                    }
                    messages.Add(curMessage);

                    readIndex = messageStart;
                    messageLength = 0;  //to signify that the message has been read
                    //check if another message
                    if (readIndex < (writeIndex + cmpWrite))
                    {
                        if (buffer[messageStart] != 0xA4)
                        {
                            readingMessage = false;
                        }
                        else if ((readIndex + 1) < (writeIndex + cmpWrite))
                        {
                            CheckForMessage();   //might be another, but needs to have message length at least
                        }
                    }
                    else
                    {
                        readingMessage = false;
                    }
                }
            }
            else
            {

                while ((buffer[readIndex] != 0xA4) && (readIndex < (writeIndex + cmpWrite)))
                {
                    readIndex++;
                    //wrap on checking
                    if (readIndex > maxIndex)
                    {
                        readIndex = 0;
                        cmpWrite = 0;
                    }
                }
                //see if we actually found the sync
                if ((buffer[readIndex] == 0xA4) && (readIndex != (writeIndex + cmpWrite)))
                {
                    messageStart = readIndex;
                    readingMessage = true;
                }
                //do we have a length?
                if ((messageStart + 1) < (writeIndex + cmpWrite))
                {
                    CheckForMessage();
                }
            }

        }


    }
}

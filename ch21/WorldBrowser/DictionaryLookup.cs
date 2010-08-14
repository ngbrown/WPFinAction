using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace WorldBrowser
{
    public class DictionaryLookup
    {
        private Encoding conversationEncoding = Encoding.UTF8;
        private string defaultServer = "dict.us.dict.org";
        private readonly int defaultPort = 2628;
        private string dictionaryToUse = "world02";
        private const int bufferSize = 4096;

        public FlowDocument DefineWord(string word)
        {
            string dictionary = "*";
            if ((dictionaryToUse.Length > 0) || (string.Compare(dictionaryToUse, "all", true) == 0))
                dictionary = dictionaryToUse;

            string command = "DEFINE " + dictionary + " " + "'" + word + "'";

            string result = ExecuteCommand(command);

            return TextToFlowDocument(word, result);
        }

        public FlowDocument TextToFlowDocument(string word, string result)
        {
            FlowDocument doc = new FlowDocument();

            // copied from Dictionary app
            Paragraph para1 = new Paragraph();
            para1.FontSize = 18;
            para1.Inlines.Add(new Bold(new Run(word)));
            doc.Blocks.Add(para1);
            Paragraph paraDate = new Paragraph();
            paraDate.Inlines.Add(new Run("Looked up: " + DateTime.Now.ToString("T")));
            doc.Blocks.Add(paraDate);
            Paragraph para2 = new Paragraph();
            para2.Inlines.Add(new Run(result));
            doc.Blocks.Add(para2);

            return doc;
        }

        private string ExecuteCommand(string command)
        {
            try
            {
                StringBuilder response = new StringBuilder();
                Thread.Sleep(4000);
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(defaultServer, defaultPort);
                    using (Stream clientStream = client.GetStream())
                    {
                        //response.Append(
                        GetResponse(command + "\r\n", clientStream); //);
                        response.Append(GetResponse("QUIT\r\n", clientStream));
                        //);
                    }
                }
                return response.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string GetResponse(string requestString, Stream clientStream)
        {
            byte[] request = conversationEncoding.GetBytes(requestString);
            clientStream.Write(request, 0, request.Length);
            clientStream.Flush();
            byte[] response = new byte[bufferSize];
            StringBuilder sb = new StringBuilder();
            int currentPosition = 0, bytesRead = 0;
            while ((bytesRead = clientStream.Read(response, 0, bufferSize)) > 0)
            {
                sb.Append(conversationEncoding.GetString(response, 0, bytesRead));
                currentPosition += bytesRead;
                if (bytesRead < bufferSize)
                    break;
            }

            return sb.ToString();
        }
    }
}

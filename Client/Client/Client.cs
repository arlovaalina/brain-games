using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Client
{
    public class Client
    {
        const string ADD_USER = "ADD_USER";
        const string DEL_USER = "DEL_USER";
        const string LST_USER = "LST_USER";
        const string NAME_ERR = "NAME_ERR";
        const string INV_LIST = "INV_LIST";
        const string FRM_LIST = "FRM_LIST";
        const string ACPT_INV = "ACPT_INV";
        const int COMMAND_LENGTH = 8;

        private int Port;
        private IPHostEntry ipHost;
        private IPAddress ipAddress;
        private IPEndPoint ipEndPoint;
        private Socket clientSocket;
        string receivedData;
        byte[] bytes;
        int bytesAmount;
        public string Login;
        private object RegisterForm;
        private object MainForm;

        public Client(string login, object registerForm, object mainForm)
        {
            Login = login;
            RegisterForm = registerForm;
            MainForm = mainForm;
        }

        public void ConnectToServer(string ip, int port)
        {
            Port = port;
            ipHost = Dns.GetHostEntry(ip);
            ipAddress = ipHost.AddressList[0];
            ipEndPoint = new IPEndPoint(ipAddress, port);
            clientSocket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.NoDelay = true;
            clientSocket.SendTimeout = 50;
            clientSocket.Connect(ipEndPoint);
            Thread messagesThread = new Thread(ReceiveMessagesFromServer);
            messagesThread.Start();
        }

        public void Disconnect()
        {
            clientSocket.Close();
        }

        public void ReceiveMessagesFromServer()
        {
            string receivedCommand = "";
            string receivedMessage = "";
            try
            {
                while (true)
                {
                    string receivedData = ReceiveData();
                    if (IsCommand(receivedData))
                    {
                        receivedCommand = receivedData.Substring(0, COMMAND_LENGTH);
                        receivedMessage = receivedData.Substring(COMMAND_LENGTH);
                    }

                    switch (receivedCommand)
                    {
                        case LST_USER:
                            (MainForm as MainForm).PrintUserList(receivedMessage);
                            break;
                        case INV_LIST:
                            (MainForm as MainForm).PrintInvitation(receivedMessage, "forList");
                            break;
                        case FRM_LIST:
                            (MainForm as MainForm).PrintInvitation(receivedMessage, "fromList");
                            break;
                        case ACPT_INV:
                            (MainForm as MainForm).HighlightAcceptedInvitation(receivedMessage);
                            break;
                    }
                }
            }
            catch
            {
                //(MainForm as MainForm).PrintError("We are sorry :( But server was disconnected.");
                (MainForm as MainForm).CloseForm();
            }
        }

        public string ReceiveData()
        {
            receivedData = null;
            bytes = new byte[1024];
            bytesAmount = clientSocket.Receive(bytes);
            receivedData += Encoding.UTF8.GetString(bytes, 0, bytesAmount);
            return receivedData;
        }

        public void SendMessageToServer(string message)
        {
            try
            {
                byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                int bytesSent = clientSocket.Send(messageBytes);
            }
            catch
            {
                (MainForm as MainForm).PrintError("We are sorry :( But server was disconnected.");
            }
        }

        public bool IsCommand(string data)
        {
            string command = data.Substring(0, COMMAND_LENGTH);
            if (command == LST_USER || command == NAME_ERR || command == INV_LIST || command == FRM_LIST ||
                command == ACPT_INV)
                return true;
            else
                return false;
        }
    }
}

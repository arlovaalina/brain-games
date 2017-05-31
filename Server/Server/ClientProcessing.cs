using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;

namespace Server
{
    class ClientProcessing
    {
        const string ADD_USER = "ADD_USER";
        const string CON_LOST = "CON_LOST";
        const string LST_USER = "LST_USER";
        const string NAME_ERR = "NAME_ERR";
        const string INV_USER = "INV_USER";
        const string INV_LIST = "INV_LIST";
        const string FRM_LIST = "FRM_LIST";
        const string LIST_INV = "LIST_INV";
        const string START_GM = "START_GM";
        const string ACPT_INV = "ACPT_INV";
        const int COMMAND_LENGTH = 8;

        public struct Clients
        {
            public Socket clientSocket;
            public string clientName;
        }
        static List<Clients> ClientList = new List<Clients>();
        Socket Client;
        string Login;

        public ClientProcessing(Socket client)
        {
            Client = client;
            Client.NoDelay = true;
            Thread process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        public void Process()
        {
            try
            {
                string receivedCommand = "";
                string receivedMessage = "";

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
                        case ADD_USER:
                            AddUser(receivedMessage);
                            break;
                        case INV_USER:
                            InviteUser(receivedMessage);
                            break;
                        /*case CON_LOST:
                            DeleteConnection();
                            break;*/
                        case START_GM:
                            AcceptInvitation(receivedMessage);
                            // Start game
                            break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Сервер завершил соединение с клиентом");
            }
        }

        public string ReceiveData()
        {
            string receivedData = null;
            byte[] bytes = new byte[1024];
            int bytesAmount = Client.Receive(bytes);
            receivedData += Encoding.UTF8.GetString(bytes, 0, bytesAmount);
            return receivedData;
        }

        public void SendMessageToClient(Socket client, string message)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            int bytesSent = client.Send(messageBytes);
        }

        public void SendMessageToAll(string message)
        {
            byte[] messageBytes;
            messageBytes = Encoding.UTF8.GetBytes(message);
            for (int i = 0; i < ClientList.Count; i++)
            {
                ClientList[i].clientSocket.Send(messageBytes);
            }
        }

        public void AddClientToList(Socket client, string login)
        {
            Login = login;
            Clients clientTemp = new Clients();
            clientTemp.clientSocket = client;
            clientTemp.clientName = login;
            ClientList.Add(clientTemp);
        }

        public bool IsInClientList(Socket client)
        {
            int counter = ClientList.Count;
            for (int i = 0; i < counter; i++)
            {
                if (ClientList[i].clientSocket == client)
                    return true;
            }
            return false;
        }

        public bool IsInClientList(string login)
        {
            int counter = ClientList.Count;
            for (int i = 0; i < counter; i++)
            {
                if (ClientList[i].clientName == login)
                    return true;
            }
            return false;
        }

        public Clients FindClientInList(string login)
        {
            int counter = ClientList.Count;
            for (int i = 0; i < counter; i++)
            {
                if (ClientList[i].clientName == login)
                    return ClientList[i];
            }
            return ClientList[0];
        }

        public string GetClientList()
        {
            int counter = ClientList.Count;
            string clientList = "";
            for (int i = 0; i < counter; i++)
            {
                clientList += ClientList[i].clientName + "\n";
            }
            return clientList;
        }

        public bool IsCommand(string data)
        {
            string command = data.Substring(0, COMMAND_LENGTH);
            if (command == ADD_USER || command == CON_LOST || command == INV_USER || command == LST_USER ||
                command == NAME_ERR || command == START_GM)
                return true;
            else
                return false;
        }

        public void AddUser(string login)
        {
            if (!IsInClientList(login))
            {
                AddClientToDatabase(login);
                AddClientToList(Client, login);
                string clientList = GetClientList();
                SendMessageToAll(LST_USER + clientList);
            }
            else
            {
                SendMessageToClient(Client, NAME_ERR);
            }
        }

        public void InviteUser(string receivedMessage)
        {
            Clients opponentClient = FindClientInList(receivedMessage);
            // add to invitation database new invitation
            AddInvitationToDatabase(opponentClient.clientName, Login);
            // get form database list of invitations
            string opponentInvitationList = GetInboxInvitationsFromDatabase(opponentClient.clientName);
            SendMessageToClient(opponentClient.clientSocket, INV_LIST + opponentInvitationList + "\n");
            string clientInvitationList = GetOutboxInvitationsFromDatabase(Login);
            SendMessageToClient(Client, FRM_LIST + clientInvitationList + "\n");
        }

        public void AcceptInvitation(string opponentLogin)
        {
            Clients opponentClient = FindClientInList(opponentLogin);
            SendMessageToClient(opponentClient.clientSocket, ACPT_INV + Login);
        }

        public SqlConnection GetDatabaseConnection()
        {
            string connectionStr = @"Data Source=ALINA-PC\SQLEXPRESS;Initial Catalog=master;" +
          "Integrated Security=SSPI;Pooling=False";
            SqlConnection dbConnection = new SqlConnection(connectionStr);
            return dbConnection;
        }

        public void AddClientToDatabase(string login)
        {
            SqlConnection dbConnection = GetDatabaseConnection();
            string sqlQuery = "INSERT INTO Player (Login) VALUES ('" + login +"')";
            SqlCommand command = new SqlCommand(sqlQuery, dbConnection);

            dbConnection.Open();

            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public void AddInvitationToDatabase(string playerLogin, string opponentLogin)
        {
            SqlConnection dbConnection = GetDatabaseConnection();
            dbConnection.Open();

            string sqlQuery = "INSERT INTO Invitation (Player_ID, Opponent_ID)" +
            "VALUES ((SELECT Player_ID FROM Player WHERE Login = '" + playerLogin + "')," +
	        "(SELECT Player_ID FROM Player WHERE Login = '"+ opponentLogin +"'))";
            SqlCommand command = new SqlCommand(sqlQuery, dbConnection);
            command.ExecuteNonQuery();

            dbConnection.Close();
        }

        public string GetInboxInvitationsFromDatabase(string login)
        {
            SqlConnection dbConnection = GetDatabaseConnection();
            string sqlQuery = "SELECT pl.Login FROM Player as p " +
                "JOIN Invitation as i ON i.Player_ID = p.Player_ID "+ 
                "JOIN Player as pl ON pl.Player_ID = i.Opponent_ID WHERE p.Login = '" + login +"'";
            SqlCommand command = new SqlCommand(sqlQuery, dbConnection);

            dbConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();
            string result = "";
            while (dataReader.Read())
            {
                result = result + dataReader[0] + "\n";
            }

            dbConnection.Close();
            return result;
        }

        public string GetOutboxInvitationsFromDatabase(string login)
        {
            SqlConnection dbConnection = GetDatabaseConnection();
            string sqlQuery = "SELECT pl.Login FROM Player as p " +
                "JOIN Invitation as i ON i.Opponent_ID = p.Player_ID " +
                "JOIN Player as pl ON pl.Player_ID = i.Player_ID WHERE p.Login = '" + login + "'";
            SqlCommand command = new SqlCommand(sqlQuery, dbConnection);

            dbConnection.Open();

            SqlDataReader dataReader = command.ExecuteReader();
            string result = "";
            while (dataReader.Read())
            {
                result = result + dataReader[0] + "\n";
            }

            dbConnection.Close();
            return result;
        }

        public void DeleteConnection()
        {
            Client.Close();
        }
    }
}

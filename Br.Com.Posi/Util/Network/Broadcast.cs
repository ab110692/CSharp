using Br.Com.Posi.Event;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Timers;
using Br.Com.Posi.Util.Network;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Br.Com.Posi.Util.Network
{
    public class Broadcast
    {

        public event MessageEventHandler OnPostReceive;
        public event MessageReportEventHandler OnSubmit;

        private UdpClient udpServer;
        private UdpClient udpClient;

        private IPAddress myIPAddress;

        private BackgroundWorker serverTask;
        private BackgroundWorker clientTask;

        private byte[] MessageData;

        private const int DELAY_SERVER_TIME = 7000;
        private const int DELAY_CLIENT_TIME = 7000;

        private object lockServer = new object();
        private object lockClient = new object();

        private string Message;


        public Broadcast(string messageCommunication)
        {
            this.Message = messageCommunication;

            this.myIPAddress = this.myIPAddress.GetMyIP();

            udpServer = new UdpClient(8888);
            udpClient = new UdpClient();

            MessageData = Encoding.ASCII.GetBytes(Message);

            serverTask = new BackgroundWorker();
            serverTask.WorkerSupportsCancellation = true;
            serverTask.DoWork += ServerTimeTask_DoWork;
            serverTask.RunWorkerCompleted += ServerTimeTask_RunWorkerCompleted;
            serverTask.RunWorkerAsync();

            clientTask = new BackgroundWorker();
            clientTask.WorkerSupportsCancellation = true;
            clientTask.DoWork += ClientTask_DoWork;
            clientTask.RunWorkerCompleted += ClientTask_RunWorkerCompleted;
            clientTask.RunWorkerAsync();
        }

        private async void ClientTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            await Task.Delay(DELAY_CLIENT_TIME);
            clientTask.CancelAsync();
            clientTask.RunWorkerAsync();
        }

        private void ClientTask_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (lockClient)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8888);
                udpClient.EnableBroadcast = true;
                udpClient.Send(Encoding.ASCII.GetBytes(Message), Encoding.ASCII.GetBytes(Message).Length, endPoint);
                byte[] data = udpClient.Receive(ref endPoint);
                string aux = Encoding.ASCII.GetString(data);
                if (!IPAddress.Parse(aux.Split('|')[0]).ToString().Equals(myIPAddress.ToString()) && !aux.Split('|')[1].Equals("fail"))
                {
                    OnPostReceive.Invoke(new MessageArgs() { Message = aux.Split('|')[1] });
                }
            }
        }

        private async void ServerTimeTask_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            await Task.Delay(DELAY_SERVER_TIME);
            serverTask.CancelAsync();
            serverTask.RunWorkerAsync();
        }

        private void ServerTimeTask_DoWork(object sender, DoWorkEventArgs e)
        {
            lock (lockServer)
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 8888);
                byte[] data = udpServer.Receive(ref endPoint);
                string aux = Encoding.ASCII.GetString(data);
                if (aux.Equals(Message))
                {
                    MessageArgs args = OnSubmit.Invoke();
                    if (args != null)
                    {
                        string m = $"{myIPAddress}|{args.Message}";
                        udpServer.Send(Encoding.ASCII.GetBytes(m), Encoding.ASCII.GetBytes(m).Length, endPoint);
                    }
                    else
                    {
                        string m = $"{myIPAddress}|fail";
                        udpServer.Send(Encoding.ASCII.GetBytes(m), Encoding.ASCII.GetBytes(m).Length, endPoint);
                    }
                }
            }
        }
    }
}

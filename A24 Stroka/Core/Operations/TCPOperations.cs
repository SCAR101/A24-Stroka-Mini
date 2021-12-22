using A24_Stroka.Core.Global;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Threading;

namespace A24_Stroka.Core.Operations
{
    public class TCPOperations : OnPropertyChangedClass
    {
        private readonly IAppProp _appprop;
        public TCPOperations(IAppProp appprop)
        {
            _appprop = appprop;
            this.myDelegate = new ShowMessage(this.ShowMessageMethod);
            this.threadRecieve = new Thread(new ThreadStart(this.ReceiveMessage));
            this.threadRecieve.IsBackground = true;
            this.threadRecieve.Start();
        }

        System.Net.Sockets.TcpClient A24SportTitleControllerClient = new System.Net.Sockets.TcpClient();

        private UdpClient udpRecieve = new UdpClient(32006);
        private Thread threadRecieve;

        public delegate void ShowMessage(string message);
        public ShowMessage myDelegate;

        public Dispatcher Dispatcher { get; } = System.Windows.Application.Current.Dispatcher;

        private void ReceiveMessage()
        {
            while (true)
            {
                byte[] bytes;
                do
                {
                    IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 32006);
                    bytes = this.udpRecieve.Receive(ref remoteEP);
                    remoteEP.Address.ToString();
                }
                while (bytes.Length <= 0);
                this.Dispatcher.Invoke((Delegate)this.myDelegate, (object)Encoding.UTF8.GetString(bytes));
            }
        }
        private void ShowMessageMethod(string message)
        {
            if (message.Contains("StatusPlay"))
            {
                _appprop.ChangeStatus("StatusPlay");
            }
            else if (message == "StatusStop")
            {
                _appprop.ChangeStatus("StatusStop");
            }
            else if (message == "RefreshDataGrid")
            {
                SqlOperations sqlOperation = new SqlOperations();
                var files = sqlOperation.GetTable("Stroka", "*");
                Singleton.Instance.CollectionNews.Clear();
                var sqlCollection = AdditionalOperations.ConvertDataTableNewsToObservableCollection(files);
                
                lock (Singleton.Instance.CollectionNews)
                {
                    foreach (var file in sqlCollection)
                    {
                        App.Current.Dispatcher.Invoke((Action)delegate // <--- HERE
                        {
                            Singleton.Instance.CollectionNews.Add(file);
                        });

                    }
                    //Singleton.Instance.CollectionNews = AdditionalOperations.ConvertDataTableNewsToObservableCollection(files);                  
                }
            }
            else if (message == "StatusActive")
            {
                sendMessage("StatusActive," + Singleton.Instance.IPClient, Singleton.Instance.IPServer, Singleton.Instance.PortToServer);
            }
        }

        public void sendMessage(string message, string IP, int Port)
        {
            UdpClient udpClient = new UdpClient();
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse(IP), Port);
            byte[] bytes = Encoding.UTF8.GetBytes(message);
            try
            {
                udpClient.Send(bytes, bytes.Length, endPoint);
            }
            catch
            {
                int num = (int)MessageBox.Show("Error occurs.", "Exclamation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            udpClient.Close();
        }
    }
}

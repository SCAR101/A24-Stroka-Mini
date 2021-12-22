using A24_Stroka.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A24_Stroka.Core.Global
{
    public sealed class Singleton
    {
        private static volatile Singleton instance;
        private static object syncRoot = new Object();

        private Singleton()
        {
            CollectionNews = new ObservableCollection<NewsModel>(); 
        }

        public static Singleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Singleton();
                    }
                }
                return instance;
            }
        }

        public ObservableCollection<NewsModel> CollectionNews { get; set; }
    

        public string UserName { get; set; }
        public string IPServer { get; set; }
        public string IPClient { get; set; }
        public int PortToServer { get; set; }
        public int PortToClient { get; set; }
        public string IPDB { get; set; }
        public string IPAD { get; set; }
        public string IPExch { get; set; }
        public string IPCUBE1 { get; set; }
        public string IPCUBE2 { get; set; }
        public int SelectedFileID { get; set; }
       
        // lock
        public Object LockGeneralCollection = new Object();
    }
}

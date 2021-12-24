using A24_Stroka.Core;
using A24_Stroka.Core.Global;
using A24_Stroka.Core.Operations;
using A24_Stroka.Models;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace A24_Stroka.ViewModels
{
    class MainPageViewModel : OnPropertyChangedClass, INavigatedToAware
    {

        SqlOperations sqlOperation = new SqlOperations();
        private readonly IAppProp _appprop;
        private readonly TCPOperations _tCPOperations;
        private readonly INavigationManager _navigationManager;

        public MainPageViewModel()
        {
            if (!DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                throw new Exception("Конструктор предназначен только для Времени Разработки!");
        }
        public MainPageViewModel(INavigationManager navigationManager, TCPOperations tCPOperations, IAppProp appprop)
        {
            _navigationManager = navigationManager;
            _appprop = appprop;
            _tCPOperations = tCPOperations;
            InitGrid();
            if (Singleton.Instance.CollectionNews.Count != 0)
            {
                DGSelectedIndex = 2;
            }
        }

        public void OnNavigatedTo(object arg)
        {

        }

        public ObservableCollection<NewsModel> NewsList
        {
            get
            {
                lock (Singleton.Instance.CollectionNews)
                {
                    return Singleton.Instance.CollectionNews;
                }

            }
            set
            {
                lock (Singleton.Instance.CollectionNews)
                {
                    Singleton.Instance.CollectionNews = value;
                }
                OnPropertyChanged("");
            }
        }

        private int _dGSelectedIndex;
        public int DGSelectedIndex
        {
            get { return _dGSelectedIndex; }
            set
            {
                _dGSelectedIndex = value;
                OnPropertyChanged("SelectionValue");
                
            }
        }

        public ICommand CheckUncheck
        {
            get
            {
                return new RelayCommand((obj) =>
                {
                    UncheckCheck(obj);
                });
            }
        }

        private void InitGrid()
        {


            Singleton.Instance.CollectionNews.Clear();
            
            lock (Singleton.Instance.CollectionNews)
            {
                var obj = new NewsModel()
                {
                    ID = 1,
                    In_Work = true,
                    Text = "1111111111",
                    Order_By = 1,
                    View = false
                };
                Singleton.Instance.CollectionNews.Add(obj);
                var obj1 = new NewsModel()
                {
                    ID = 1,
                    In_Work = true,
                    Text = "2222222222",
                    Order_By = 1,
                    View = false
                };
                Singleton.Instance.CollectionNews.Add(obj1);
                var obj2 = new NewsModel()
                {
                    ID = 1,
                    In_Work = true,
                    Text = "3333333333",
                    Order_By = 1,
                    View = false
                };
                Singleton.Instance.CollectionNews.Add(obj2);
                NewsList = Singleton.Instance.CollectionNews;
            }
        }

        private void UncheckCheck(object obj)
        {
            var sel = Singleton.Instance.CollectionNews.Where(i => i.ID == Convert.ToInt32(obj));
        }

    }
}

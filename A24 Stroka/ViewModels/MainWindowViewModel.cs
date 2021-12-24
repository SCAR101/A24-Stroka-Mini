using A24_Stroka.Core;
using A24_Stroka.Core.Global;
using A24_Stroka.Core.Operations;
using A24_Stroka.Models;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace A24_Stroka.ViewModels
{
    class MainWindowViewModel : OnPropertyChangedClass
    {

        private readonly TCPOperations _tCPOperations;

        public MainWindowViewModel(TCPOperations tCPOperations)
        {
            _tCPOperations = tCPOperations;
        }

        public ICommand ClickClose
        {
            get
            {
                return new RelayCommand((obj) =>
                {                 
                    System.Windows.Application.Current.Shutdown();
                });
            }
        }
    }
}

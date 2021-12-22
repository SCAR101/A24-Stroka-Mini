using A24_Stroka.ViewModels;
using A24_Stroka.Views;
using Egor92.MvvmNavigation;
using Egor92.MvvmNavigation.Abstractions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace A24_Stroka
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindowViewModel mainWindowViewModel;
        private MainPageViewModel mainPageViewModel;

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow();
            var appprop = new Core.AppProp();
            var tCPOperations = new Core.Operations.TCPOperations(appprop);

            //1. Создайте менеджер навигации
            var navigationManager = new NavigationManager(mainWindow.CurrentPage);

            mainPageViewModel = new MainPageViewModel(navigationManager, tCPOperations, appprop);
            //mainPage.DataContext = mainPageViewModel;
            Resources["mainPVM"] = mainPageViewModel;

            var mainPage = new MainPage();

            


            mainWindowViewModel = new MainWindowViewModel(tCPOperations);
            mainWindow.DataContext = mainWindowViewModel;

            //2. Определите правила навигации: зарегистрируйте ключ (строку) с соответствующими View и ViewModel для него
            navigationManager.Register<MainWindow>("MainWindowKey", () => mainWindowViewModel);
            navigationManager.Register<MainPage>("MainPageKey", () => mainPageViewModel);

            //3. Отобразите стартовый UI
            navigationManager.Navigate("MainPageKey");

            mainWindow.Show();


        }
    }
}

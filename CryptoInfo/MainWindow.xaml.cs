using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CryptoInfo.ViewModels;
using NavigationService = CryptoInfo.Services.NavigationService;

namespace CryptoInfo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isDark;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            
            _isDark = false;
            ChangeTheme("Assets/light.xaml");
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainWindowViewModel();
            NavigationService.Service = MainFrame.NavigationService;
            NavigationService.Navigate("main");
        }

        private void EventSetter_OnHandler(object sender, MouseButtonEventArgs e)
        {
            var t = DataContext as MainWindowViewModel;
            
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isDark)
            {
                ChangeTheme("Assets/light.xaml");
                _isDark = false;
            }
            else
            {
                ChangeTheme("Assets/dark.xaml");
                _isDark = true;
            }
        }

        private void ChangeTheme(string URI)
        {
            var uri = new Uri(URI, UriKind.Relative);
            var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
            if (resourceDict is null)
            {
                MessageBox.Show("Can't find theme template");
                throw new FileNotFoundException();
            }
            Application.Current.Resources.Clear();
            Application.Current.Resources.MergedDictionaries.Add(resourceDict);
        }
    }
}
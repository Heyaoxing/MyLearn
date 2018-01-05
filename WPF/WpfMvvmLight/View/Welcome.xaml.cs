using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
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
using WpfMvvmLight.Controls;
using WpfMvvmLight.ViewModel;

namespace WpfMvvmLight.View
{
    /// <summary>
    /// Welcome.xaml 的交互逻辑
    /// </summary>
    public partial class Welcome : UserControl
    {
        public Welcome()
        {
            InitializeComponent();
            this.myBar.Minimum = 0;
            this.myBar.Maximum = 100;

            Messenger.Default.Register<string>(this,"WpfMvvmLight.View.Welcome.SetMessage", SetMessage);
        }

        private void CreateUserInfo()
        {
            CreateUserInfoHelper helper = new CreateUserInfoHelper();
            helper.CreateProcess += Helper_CreateProcess;
            helper.Create();
        }

        private void Helper_CreateProcess(object sender, CreateArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.myBar.Value = e.Process;
                if (e.IsComplete)
                {
                    this.myBar.Visibility = Visibility.Hidden;
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CreateUserInfo();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Messenger.Default.Send<string>("你好哇!","WpfMvvmLight.ViewModel.GetMessage");

        }

        private void SetMessage(string content)
        {
            TextBox1.Text = content;
        }
    }
}

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfMvvmLight.Controls;
using WpfMvvmLight.Model;

namespace WpfMvvmLight.ViewModel
{
    public class WelcomeViewModel : ViewModelBase
    {
        private WelcomeModel welcome;

        public WelcomeViewModel()
        {
            welcome = new WelcomeModel() { Name = "你好嘛" };

            Messenger.Default.Register<string>(this,"WpfMvvmLight.ViewModel.GetMessage", GetMessage);
        }

        public WelcomeModel Welcome
        {
            get { return welcome; }
            set
            {
                welcome = value;
                RaisePropertyChanged();
            }
        }


        private RelayCommand<string> _submitcmd;
        public RelayCommand<string> SubmitCmd
        {
            get
            {
                return new RelayCommand<string>((p) =>
                {
                    Welcome.Name = "改变了";
                });
            }
            set
            {
                _submitcmd = value;
            }
        }


        public void GetMessage(string content)
        {
            string result = content + DateTime.Now.ToString();
            Messenger.Default.Send<string>(result,"WpfMvvmLight.View.Welcome.SetMessage");
        }

      
    }
}

using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfMvvmLight.Controls
{
    public class CreateUserInfoHelper
    {
        public event EventHandler<CreateArgs> CreateProcess;

        public void Create()
        {
            Thread t = new Thread(Start);
            t.Start();
        }

        private void Start()
        {
            try
            {
                for (int i = 0; i < 101; i++)
                {
                    CreateProcess(this, new CreateArgs()
                    {
                        IsComplete = i == 100,
                        Process = i
                    });
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                CreateProcess(this, new CreateArgs()
                {
                    IsComplete = true,
                    Process = 100
                });
            }
        }
    }
}

public class CreateArgs : ObservableObject
{
    private bool _isComplete;
    public bool IsComplete
    {
        get
        {
            return _isComplete;
        }
        set
        {
            _isComplete = value;
            RaisePropertyChanged();
        }
    }

    private int _process;
    public int Process
    {
        get { return _process; }
        set
        {
            _process = value;
            RaisePropertyChanged();
        }
    }
}

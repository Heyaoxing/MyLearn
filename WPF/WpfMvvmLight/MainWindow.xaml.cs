﻿using GalaSoft.MvvmLight;
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
using WpfMvvmLight.ViewModel;

namespace WpfMvvmLight
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Closed += MainWindow_Closed;
            DataContext = new MainViewModel();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
          //  WindowStyle = WindowStyle.None;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            ((ICleanup)DataContext).Cleanup();
        }
    }
}

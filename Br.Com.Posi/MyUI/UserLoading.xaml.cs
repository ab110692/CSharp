﻿using System;
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

namespace Br.Com.Posi.MyUI
{
    /// <summary>
    /// Interaction logic for UserLoading.xaml
    /// </summary>
    public partial class UserLoading : UserControl
    {
        public UserLoading()
        {
            InitializeComponent();
        }

        public void TrocarMensagem(string mensagem)
        {
            this.txtMensagem.Text = mensagem;
        }
        
    }
}

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
using System.Windows.Shapes;
/*
להוסיף אייקון
להוסיף קידוד לסיסמא
האם כל השגיאות בהודעה נפתחת או בהערה
*/
namespace PLForms
{
    /// <summary>
    /// Interaction logic for NewClient.xaml
    /// </summary>
    public partial class NewClient : Window
    {
        public NewClient()
        {
           BE.Client client=new BE.Client();
            BE.User user;
            InitializeComponent();
            this.DataContext = client;
        }
    }
}
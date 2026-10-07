using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls;

namespace GestionMail
{
    public partial class ChronometreView : UserControl
    {
        public ChronometreView()
        {
            InitializeComponent();
            DataContext = new ChronometreViewModel();
        }
    }
}
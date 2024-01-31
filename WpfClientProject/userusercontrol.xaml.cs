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
using WpfClientProject.ServiceReferenceBank;

namespace WpfClientProject
{
    /// <summary>
    /// Interaction logic for userusercontrol.xaml
    /// </summary>
    public partial class userusercontrol : UserControl
    {
        User user1;
        public userusercontrol(User user)
        {
            InitializeComponent();
            user1 = user;
            UserName.Text = user.FirstName + " " + user.LastName;
        }
    }
}

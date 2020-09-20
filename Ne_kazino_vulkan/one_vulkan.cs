using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ne_kazino_vulkan
{
    public partial class one_vulkan : UserControl
    {
        public one_vulkan(string name_1,int p_x,int p_y)
        {
            InitializeComponent();
            name = name_1;
            locat = new Point(p_x - 6, p_y - 18);
        }

        private void one_vulkan_Load(object sender, EventArgs e)
        {

        }
        private string name;
        public Point locat;
        private void one_vulkan_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(name);
        }
    }
}

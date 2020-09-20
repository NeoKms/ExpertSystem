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
    public partial class result_job : UserControl
    {
        public result_job(string resu)
        {
            InitializeComponent();
            this.richTextBox1.Text = resu;
        }

        private void result_job_Load(object sender, EventArgs e)
        {

        }
    }
}

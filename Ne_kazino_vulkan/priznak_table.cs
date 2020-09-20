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
    public partial class priznak_table : UserControl
    {
        public priznak_table()
        {
            InitializeComponent();
            Vulkaniums newdata = new Vulkaniums();
            string[] priz_name = newdata.priz_group_mass();
            int col_prizn = newdata.col_priznak;

            for (int i = 0; i < col_prizn; i++)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = i+1;
                dataGridView1.Rows[i].Cells[1].Value = priz_name[i];
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

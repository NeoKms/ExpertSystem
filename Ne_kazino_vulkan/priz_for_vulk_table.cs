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
    public partial class priz_for_vulk_table : UserControl
    {
        public priz_for_vulk_table(int num)
        {
            InitializeComponent();
            Vulkaniums newdata = new Vulkaniums();
            int[,] priz = newdata.vulkan_priznak();
            string[] name= newdata.priz_group_mass();
            for (int i=0; i< newdata.col_priznak;i++)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = name[i];
                dataGridView1.Rows[i].Cells[1].Value = priz[num,i];
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

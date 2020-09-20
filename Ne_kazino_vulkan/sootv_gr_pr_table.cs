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
    public partial class sootv_gr_pr_table : UserControl
    {
        public sootv_gr_pr_table()
        {
            InitializeComponent();
            Vulkaniums newdata = new Vulkaniums();
            List<List<int>> gr_pr = newdata.group_priznak_2();
            int i = 0, k = 1;
            foreach (List<int> stroka in gr_pr)
            {
                string red = "";
                foreach (int el in stroka)
                {
                    red += el + " ";
                }
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = "Группа №"+(i+1);
                dataGridView1.Rows[i].Cells[1].Value = red;
                i++;
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

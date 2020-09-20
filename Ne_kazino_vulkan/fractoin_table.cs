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
    public partial class fractoin_table : UserControl
    {
        public fractoin_table()
        {
            InitializeComponent();
            Vulkaniums newdata = new Vulkaniums();
            //////////////////a
            List<List<int>> frac = newdata.get_area("a");
            int i = 0;
            foreach (List <int> stroka in frac)
            {
                string red = "";
                foreach (int el in stroka)
                {
                    red += el + " ";
                }
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = "a";
                dataGridView1.Rows[i].Cells[1].Value = red;
                i++;
            }
            ////////////////b
            frac = newdata.get_area("b");
            foreach (List<int> stroka in frac)
            {
                string red = "";
                foreach (int el in stroka)
                {
                    red += el + " ";
                }
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = "b";
                dataGridView1.Rows[i].Cells[1].Value = red;
                i++;
            }
            ///////////////////y
            frac = newdata.get_area("y");
            foreach (List<int> stroka in frac)
            {
                string red = "";
                foreach (int el in stroka)
                {
                    red += el + " ";
                }
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = "y";
                dataGridView1.Rows[i].Cells[1].Value = red;
                i++;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}

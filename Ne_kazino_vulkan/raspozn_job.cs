using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ne_kazino_vulkan
{
    public partial class raspozn_job : Form
    {
        public raspozn_job()
        {
            InitializeComponent();
            Vulkaniums newdata = new Vulkaniums();
            string[] vulk_name = newdata.vulkans_name(1);
            comboBox1.Items.AddRange(vulk_name);
            this.comboBox1.SelectedIndex = 0;
            this.comboBox2.SelectedIndex = 0;
            this.panel1.Controls.Add(table_v);
            this.Height = 550;
        }

        private void raspozn_job_Load(object sender, EventArgs e)
        {

        }
        table_vulk_gr table_v = new table_vulk_gr();
        private void button1_Click(object sender, EventArgs e)
        {
            Vulkaniums newdata = new Vulkaniums();
            raspozn jb = new raspozn();
            int[,] pri = newdata.vulkan_priznak();
            int[] id_priz_checked = P_CHB.get_priznak();
            int[] vulk = new int[id_priz_checked.Length];
            //  string red = "";
            int ki = 0;
            for (int i = 0; i < newdata.col_priznak; i++)
            {
                for (int j = 0; j < id_priz_checked.Length; j++)
                {
                    if (i == id_priz_checked[j])
                    {
                    //    red += i + " znach=" + pri[this.comboBox1.SelectedIndex, i]+"   ";
                        vulk[ki] = pri[this.comboBox1.SelectedIndex, i];
                        ki++;
                    }
                }
            }
             MessageBox.Show(table_v.get_gr()[0] +" "+ table_v.get_gr()[1] +" "+ table_v.get_gr()[2]);
            richTextBox1.Text=jb.srart(table_v.get_matrix(id_priz_checked), table_v.get_gr(), vulk);
        }
        

        private void button2_Click(object sender, EventArgs e)
        {
            
        }
        priznak_checkbox P_CHB = new priznak_checkbox(1,false);
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            if (this.comboBox2.SelectedIndex == 0)
            {
                //Перечень vulk
                this.panel1.Controls.Add(table_v);
               // MessageBox.Show("vulk");
            }
            if (this.comboBox2.SelectedIndex == 1)
            {
                //Перечень признаков
                this.panel1.Controls.Add(P_CHB);
               // MessageBox.Show("priz");
            }
        }
    }
}

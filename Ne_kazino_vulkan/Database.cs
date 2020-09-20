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
    public partial class Database : Form
    {
        public Database()
        {
            InitializeComponent();

            this.Height = 615;

            Vulkaniums newdata = new Vulkaniums();
            string[] vulk_name = newdata.vulkans_name(1);
            comboBox1.Items.AddRange(vulk_name);
            this.comboBox1.SelectedIndex = 0;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            priznak_table T_PRIZ = new priznak_table();
            this.panel1.Controls.Add(T_PRIZ);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();

            vulkans_table T_VULK = new vulkans_table();
            this.panel1.Controls.Add(T_VULK);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                this.panel1.Controls.Clear();

            grup_priz_table T_GR = new grup_priz_table();
            this.panel1.Controls.Add(T_GR);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            sootv_gr_pr_table T_GR_PR = new sootv_gr_pr_table();
                this.panel1.Controls.Add(T_GR_PR);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            fractoin_table T_FRACTION = new fractoin_table ();
            this.panel1.Controls.Add(T_FRACTION);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
          // vulkan_priznak
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.panel1.Controls.Clear();
            priz_for_vulk_table T_FRACTION = new priz_for_vulk_table(this.comboBox1.SelectedIndex);
            this.panel1.Controls.Add(T_FRACTION);
            
        }
    }
}

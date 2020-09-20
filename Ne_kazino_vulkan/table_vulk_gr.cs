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
    public partial class table_vulk_gr : UserControl
    {
        public table_vulk_gr()
        {
            InitializeComponent();
            Vulkaniums newdata = new Vulkaniums();
            string[] vulk_name = newdata.vulkans_name(1);
            for (int i = 0; i < newdata.col_vulkans; i++)
            {
                dataGridView1.RowCount++;
                dataGridView1.Rows[i].Cells[0].Value = i + 1;
                dataGridView1.Rows[i].Cells[1].Value = vulk_name[i];
                dataGridView1.Rows[i].Cells[2].Value = 0;
                dataGridView1.Rows[i].Cells[3].Value = 0;
                dataGridView1.Rows[i].Cells[4].Value = 0;
            }
        }
        int[] matrix_sootv, gr = new int[3];
        public int[] get_sootv()
        {
            return matrix_sootv;
        }
        public int[] get_gr()
        {
            return gr;
        }
        public int[,] get_matrix(int [] priznaki)
        {
            Vulkaniums newdata = new Vulkaniums();
            int col_need = 0, gr1 = 0, gr2 = 0, gr3 = 0;

            for (int i = 0; i < newdata.col_vulkans; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "1") gr1++;
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "1") gr2++;
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "1") gr3++;
            }
            col_need = gr1 + gr2 + gr3;
            gr[0] = gr1; gr[1] = gr2; gr[2] = gr3;
            int[,] matrix = new int[col_need, priznaki.Length], data = newdata.vulkan_priznak();
            matrix_sootv = new int[col_need];
            string red = "";
            //1gr
            int K = 0, jk = 0;
            // MessageBox.Show((gr1 + gr2 + gr3) + " ll  " + gr1 + gr2 + gr3);
            for (int i = 0; i < newdata.col_vulkans; i++)
            {
                if (dataGridView1.Rows[i].Cells[2].Value.ToString() == "1")
                {
                    for (int j = 0; j < newdata.col_priznak; j++)
                    {
                        for (int jo = 0; jo < priznaki.Length; jo++)
                        {
                            if (j == priznaki[jo])
                            {
                                matrix[K, jk] = data[i, j];
                                red += data[i, j]+" ";
                                jk++;
                            }
                        }
                        
                    }
                    matrix_sootv[K] = i;
                    red += "\n";
                    K++;jk = 0;
                }
            }
            //gr 2
            
            for (int i = 0; i < newdata.col_vulkans; i++)
            {
                if (dataGridView1.Rows[i].Cells[3].Value.ToString() == "1")
                {
                    for (int j = 0; j < newdata.col_priznak; j++)
                    {
                        for (int jo = 0; jo < priznaki.Length; jo++)
                        {
                            if (j == priznaki[jo])
                            {
                                matrix[K, jk] = data[i, j];
                                red += data[i, j] + " ";
                                jk++;
                            }
                        }
                    }
                    matrix_sootv[K] = i;
                    red += "\n";
                    K++;jk = 0;
                }
            }
            //gr 3
            for (int i = 0; i < newdata.col_vulkans; i++)
            {
                if (dataGridView1.Rows[i].Cells[4].Value.ToString() == "1")
                {
                    for (int j = 0; j < newdata.col_priznak; j++)
                    {
                        for (int jo = 0; jo < priznaki.Length; jo++)
                        {
                            if (j == priznaki[jo])  
                            {
                                matrix[K, jk] = data[i, j];
                                red += data[i, j] + " ";
                                jk++;
                            }
                        }
                    }
                    matrix_sootv[K] = i;
                    red += "\n";
                    K++;jk = 0;
                }
            }
            MessageBox.Show(red);
            return matrix;
        }
        private void table_vulk_gr_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Cells[2].Value = 0;
                dataGridView1.Rows[i].Cells[3].Value = 0;
                dataGridView1.Rows[i].Cells[4].Value = 0;
            }
        }
    }
}

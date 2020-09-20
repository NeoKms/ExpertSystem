using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace Ne_kazino_vulkan
{
    public partial class Form1 : Form
    {
        private int col_vulk;
        private string[] vulkans_name;
        private double[,] vulkans_koord;
        private one_vulkan[] vulkan_markers;
        private Vulk_chackbox V_CHB;
        private priznak_checkbox P_CHB; 
        private static int[] inProg(double dl, double sh)
        {
            double m = 37.90331163437636;
            double mm = -70.69116918297814;
            double x0 = 154.236670;
            double y0 = 60.326418;
            double x = (dl - x0) * (1 + m);
            double y = (sh - y0) * (1 + mm);
            return new int[] { Convert.ToInt32(x), plusC(Convert.ToInt32(y))+ Convert.ToInt32(y) };
        }
        private static int plusC(int pix)
        {
            int c = 16;

            int rez = 0;
            if (pix < 331)
                rez = pix / c;
            else
            {
                rez = 20 - (pix - 330) / c;
            }

            return rez;
        }

        public Form1()
        {
            InitializeComponent();
            
            this.Height = 700;
            this.Width = 1106; 
            Vulkaniums Vulk_data = new Vulkaniums();
            col_vulk = Vulk_data.col_vulkans;
            Vulk_data.coord_vulkans();
            vulkan_markers = new one_vulkan[col_vulk];
            vulkans_name = Vulk_data.vulkans_name(1);
            vulkans_koord = Vulk_data.coord_vulkans();
            Vulk_data.group_priznak();
            Vulk_data.priz_group_mass(1);
            for (int i = 0; i< col_vulk; i++)
             {
               // MessageBox.Show(vulkans_koord[i, 1].ToString() + "|||" + vulkans_koord[i, 0].ToString());
                int[] location_vulk = inProg(vulkans_koord[i,1],vulkans_koord[i,0]);
                one_vulkan vul = new one_vulkan(vulkans_name[i], location_vulk[0], location_vulk[1]);
                 vulkan_markers[i] = vul;
                 this.panel1.Controls.Add(vulkan_markers[i]);
                 vul.Location = vulkan_markers[i].locat;
             }
            V_CHB = new Vulk_chackbox(vulkan_markers, vulkans_name, col_vulk);
            P_CHB = new priznak_checkbox(0,true);
            this.panel2.Controls.Add(V_CHB);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
          //  MessageBox.Show(e.Location.ToString());
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
           // MessageBox.Show(e.Location.ToString());
        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        int[] martix_sopostavimosti_vulk;
        int[] martix_sopostavimosti_prizn;
        private int[,] create_matrix_v_p()
        {
            int[] id_vulk_checked = V_CHB.get_vulk();
            int[] id_priz_checked = P_CHB.get_priznak();
            int[,] result = new int[V_CHB.col_vulk_checked, P_CHB.col_priz_checked];
            martix_sopostavimosti_vulk = new int[V_CHB.col_vulk_checked];
            martix_sopostavimosti_prizn= new int[P_CHB.col_priz_checked];
            //MessageBox.Show("col_vulk_checked " + martix_sopostavimosti_vulk.Length + " col_priz_checked " + martix_sopostavimosti_prizn.Length);
            int count_vul = 0,count_prizn=0;
            Vulkaniums Vulk_data = new Vulkaniums();
            string red = "";
            int[,] mer = Vulk_data.vulkan_priznak();
            for (int i=0;i< Vulk_data.col_vulkans; i++)
            {
                bool fl = false;
                for (int j = 0; j < V_CHB.col_vulk_checked; j++)
                {
                    if (i== id_vulk_checked[j])
                    {
                        fl = true;
                        break;
                    }
                }
                if (fl==true)
                {
                    count_prizn = 0;
                    martix_sopostavimosti_vulk[count_vul] = i;
                    for (int k = 0; k < Vulk_data.col_priznak; k++)
                    {
                        
                        bool fl_2 = false;
                        for (int j = 0; j < P_CHB.col_priz_checked; j++)
                        {
                            if (k == id_priz_checked[j])
                            {
                                fl_2 = true;
                                break;
                            }
                        }
                        if (fl_2==true)
                        {
                            martix_sopostavimosti_prizn[count_prizn] = k;
                            result[count_vul, count_prizn] = mer[i, k];
                            red += mer[i, k];
                            count_prizn++;
                        }
                    }
                    count_vul++;
                }

            }
            return result;
        }
        private bool check_m(int[,] mer,int[,] all)
        {
            string red = "", red_2 = "";
            Vulkaniums Vulk_data = new Vulkaniums();
            for (int i = 0; i < Vulk_data.col_vulkans; i++)
            {
                for (int j = 0; j < Vulk_data.col_priznak; j++)
                {
                    red += mer[i, j];
                    red_2 += all[i, j];
                   // if (mer[i,j]!=all[i,j])
                   //  return false;
                }
                red += "\n";
                red_2 += "\n";
            }
            MessageBox.Show(red+"\n\n"+red_2);
            return true;
        }
        List<string> result_jobs = new List<string>();
        private void button1_Click(object sender, EventArgs e)
        {

            // Vulkaniums Vulk_data = new Vulkaniums();
            // int[,] mer = Vulk_data.vulkan_priznak();
            int[,] mer = create_matrix_v_p();
            //  MessageBox.Show(check_m(mer, Vulk_data.vulkan_priznak()).ToString());
            this.comboBox1.Items.Add("Результат автоматической классификации");
            result_jobs.Add(sopostavit_auto(P_Maski.Get_Maski(V_CHB.col_vulk_checked, P_CHB.col_priz_checked, mer)));
            result_job newpanel = new result_job(result_jobs[this.comboBox1.Items.Count - 3]);
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(newpanel);

        }
        private string sopostavit_auto(string el)
        {
            string result = el;
            for (int i=0;i< martix_sopostavimosti_vulk.Length; i++)
            {

                result=result.Replace((" "+(i + 1) + " "), (vulkans_name[martix_sopostavimosti_vulk[i]]+" "));
               // MessageBox.Show(result);
            }

            return result;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            // Vulkaniums Vulk_data = new Vulkaniums();
            // int[,] mer = Vulk_data.vulkan_priznak();
            int[,] mer = create_matrix_v_p();
            result_jobs.Add(sopostavit_auto(P_sisp_L.Get_L(V_CHB.col_vulk_checked, P_CHB.col_priz_checked, Convert.ToInt32(this.textBox1.Text), mer)));
            this.comboBox1.Items.Add("Результат классификации с заданнм L");

            result_job newpanel = new result_job(result_jobs[this.comboBox1.Items.Count - 3]);
            this.panel2.Controls.Clear();
            this.panel2.Controls.Add(newpanel);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //this.comboBox1.Items.Add("Распознавание");
            raspozn_job rsj = new raspozn_job();
            rsj.ShowDialog();
            
           // result_jobs.Add(result);
           // result_job newpanel = new result_job(result_jobs[this.comboBox1.Items.Count - 3]);
          //  this.panel2.Controls.Clear();
          //  this.panel2.Controls.Add(newpanel);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        bool flas_but4 = true;
        private void button4_Click(object sender, EventArgs e)
        {
            if (flas_but4) { this.Width = 446; flas_but4 = false; }
            else { this.Width = 1104;flas_but4 = true; }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.panel2.Controls.Clear();
            if (this.comboBox1.SelectedIndex == 0)
            {
               //Перечень vulk
                this.panel2.Controls.Add(V_CHB);
            }
            if (this.comboBox1.SelectedIndex == 1)
            {
                //Перечень признаков
                this.panel2.Controls.Add(P_CHB);
            }

            if (this.comboBox1.Text == "Результат классификации с заданнм L")
            {

                result_job newpanel = new result_job(result_jobs[this.comboBox1.SelectedIndex-2]);
                this.panel2.Controls.Add(newpanel);
            }
            if (this.comboBox1.Text == "Результат автоматической классификации")
            {

                result_job newpanel = new result_job(result_jobs[this.comboBox1.SelectedIndex-2]);
                this.panel2.Controls.Add(newpanel);
            }
        }


        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //работа с бд
            Database newdata = new Database();
            newdata.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Добавить текст в помощь пользоветелю: \n 1.\n 2.\n 3.\n 4. ");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

    }
}

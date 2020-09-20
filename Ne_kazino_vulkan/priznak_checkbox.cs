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
    public partial class priznak_checkbox : UserControl
    {
        int[] col_priz_in_gr;
        int[] index_grupp;
        int col_grup;
        int col_prizn;
        public priznak_checkbox(int el,bool fl)
        {
            InitializeComponent();
            Vulkaniums Vulk_data = new Vulkaniums();
            int[,] grup_priznak=Vulk_data.group_priznak();
            string[] priz_name = Vulk_data.priz_group_mass();
            string[] grup_name=Vulk_data.priz_group_mass(1);
            col_priz_in_gr = Vulk_data.col_priz_in_grupp();
            int now_col = col_priz_in_gr[0];
            col_prizn = Vulk_data.col_priznak;
            col_grup = Vulk_data.col_grupp;
            index_grupp = new int[col_grup];
            int k = 0;
            bool flag_gr = true;
            for (int i = 0; i < col_prizn; i++)
              {
                if (i>=now_col && flag_gr == false)
                {
                    k++;
                    now_col += col_priz_in_gr[k];
                    flag_gr = true;
                }
                if (i< now_col && flag_gr==true)
                {
                    
                    this.checkedListBox1.Items.Add("__ГРУППА№"+(k + 1).ToString() + " " + grup_name[k],true);
                    index_grupp[k] = this.checkedListBox1.Items.Count-1;
                   // MessageBox.Show(index_grupp[k].ToString());
                    flag_gr = false;
                }

                  this.checkedListBox1.Items.Add((i + 1).ToString() + " " + priz_name[i], fl);
                  checkedListBox1.SetItemChecked(i, fl);
              }
            if (el == 1)checkedListBox1.Height = 250;
        }
        public int[] get_priznak()
        {
            int k = 0, count = 0;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                bool fl = false;
                for (int j=0; j < col_grup; j++)
                {
                    if (i == index_grupp[j])
                    {
                        k++;
                        fl = true;
                        break;
                    }
                }
                if (fl) continue;
                if (checkedListBox1.GetItemChecked(i)==true)
                {
                    count++;
                }
            }
            //MessageBox.Show(count.ToString());
            int[] result = new int[count];
            col_priz_checked = count;
            count = 0;
            k = 0;
            string red = "";
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                bool fl = false;
                for (int j = 0; j < col_grup; j++)
                {
                    if (i == index_grupp[j])
                    {
                        k++;
                        fl = true;
                      //  MessageBox.Show("if "+i+" == "+ index_grupp[j]);
                        break;
                    }
                }
                if (fl) continue;
                if (checkedListBox1.GetItemChecked(i)==true)
                {
                    result[count] = i-k;
                    count++;
                    red += (i-k).ToString() + " ";
                }
            }
           //MessageBox.Show(red+"   "+result.Length.ToString());
            return result;
        }
        public int col_priz_checked { get; set; } = 0;
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool flag = false;
            for (int i=0;i< col_grup; i++)
            {
                if (checkedListBox1.SelectedIndex == index_grupp[i]) flag = true;
            }
            if (flag)
            {
                if (checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex))
                {
                    //снять все
                    int col_ind = 0;
                    int k = 0;
                    for (int i = 0; i < col_grup; i++)
                    {
                        col_ind += 1;
                        if (checkedListBox1.SelectedIndex == index_grupp[i]) { k = i; break; }
                        col_ind += col_priz_in_gr[i];
                    }
                   // MessageBox.Show(col_ind.ToString());
                    for (int i = col_ind ; i < col_ind + col_priz_in_gr[k] ; i++)
                    {
                          checkedListBox1.SetItemChecked(i, true);
                    }
                }
                else
                {
                    //добавить все
                    //снять все
                    int col_ind = 0;
                    int k = 0;
                    for (int i = 0; i < col_grup; i++)
                    {
                        col_ind += 1;
                        if (checkedListBox1.SelectedIndex == index_grupp[i]) { k = i; break; }
                        col_ind += col_priz_in_gr[i];
                    }
                   // MessageBox.Show(col_ind.ToString());
                    for (int i = col_ind; i < col_ind + col_priz_in_gr[k]; i++)
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }
                }
            }
        }

        private void priznak_checkbox_Load(object sender, EventArgs e)
        {

        }
    }
}

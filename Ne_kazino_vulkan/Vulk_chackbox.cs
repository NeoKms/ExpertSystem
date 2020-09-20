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
    public partial class Vulk_chackbox : UserControl
    {
        private one_vulkan[] vulkan_markers;
        private int col_v;
        public Vulk_chackbox(one_vulkan[] markers, string[] vulkans_name, int col_vulk)
        {
            InitializeComponent();
            vulkan_markers = markers;
            col_v = col_vulk;
            for (int i=0; i < col_vulk; i++)
            {
                this.checkedListBox1.Items.Add((i + 1).ToString() + " " + vulkans_name[i]);
                checkedListBox1.SetItemChecked(i, true);
            }
        }
        public int col_vulk_checked { get; set; } = 0;
        public int[] get_vulk()
        {
            int count = 0;
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                {
                    count++;
                }
            }
            col_vulk_checked = count;
            //MessageBox.Show(count.ToString());
            int[] result = new int[count];
            count = 0;
            string red = "";
            for (int i = 0; i < this.checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i) == true)
                {
                    result[count] = i;
                    count++;
                    red += (i).ToString() + " ";
                }
            }
           // MessageBox.Show(red + "   " + result.Length.ToString());
            return result;
        }
        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemChecked(checkedListBox1.SelectedIndex))
            {
                //Добавить на карту
                vulkan_markers[checkedListBox1.SelectedIndex].Visible = true;
            }
            else
            {
                //Удалить с карты
                vulkan_markers[checkedListBox1.SelectedIndex].Visible = false;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows.Forms;
namespace Ne_kazino_vulkan
{
    class Vulkaniums
    {
        public string path_vulkans { get; } = @"KB\ALL_VULKANS.txt";
        public string path_priznaki { get; } = @"KB\priznaki-nazvanie.txt";
        public string path_groups { get; } = @"KB\gruppa_priznakov-nazvanie.txt";
        public string path_gr_pr { get; } = @"KB\nomer_priznaka-nomer_gruppy.txt";
        public string path_folder_vulkan { get; } = @"KB\Vulkans\";
        public string path_koord { get; } = @"KB\koordinati.txt";
        public string path_area { get; } = @"KB\area.txt";
        public int col_vulkans { get; }= new int();
        public int col_grupp{ get; } = new int();
        public int col_priznak{ get; } = new int();
        //инициализатор
        public Vulkaniums()
        {
            col_vulkans = vulkan_dlina();
            col_grupp = priz_group_dlina(1);
            col_priznak = priz_group_dlina();
        }
        // Функция кол-ва признаков/групп
        public int priz_group_dlina(int value = 0) // по умолчанию 0 - путь до признаков, 1 - путь до групп
        {
            string way;
            if (value == 0)
            {
                way = path_priznaki;
            }
            else
            {
                way = path_groups;
            }
            int check = 0;
            StreamReader sr = new StreamReader(way, System.Text.Encoding.Default);
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    check++;
                }
            }
            catch (Exception e)
            {
                check = 0;
            }
            finally
            {
                sr.Close();
            }
            return check / 2;
        }
        // Функция получения массива, столбец - кол-во признаков в группе i
        public int[] col_priz_in_grupp()
        {
            int[] result = new int[col_grupp];
            int[,] priz_gr = group_priznak();
            string red = "";
            for (int i=0; i< col_grupp; i++)
            {
                int count = 0;
                for (int j=0;j< col_priznak; j++)
                {
                    if (priz_gr[i, j] == 1) count++;
                }
                result[i] = count;
                red += count.ToString()+"  ";
            }
            //MessageBox.Show(red);
            return result;
        }
        // Функция получения массива с названиями признаков/групп
        public string[] priz_group_mass(int value = 0)  // по умолчанию 0 - путь до признаков, 1 - путь до групп
        {
            int check = 0;
            string way;
            if (value == 0)
            {
                way = path_priznaki;
            }
            else
            {
                way = path_groups;
            }
            int lenght_mass = this.priz_group_dlina(value);
            string[] result = new string[lenght_mass];
            StreamReader sr = new StreamReader(way, System.Text.Encoding.Default);
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (check % 2 == 0)
                    {
                        result[check / 2] = line;
                    }
                    check++;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            string red = "";
            for (int i = 0; i < lenght_mass; i++)
            {
                red = red + result[i] + "\n";
            }
           // MessageBox.Show(red);
            return result;
        }

        // Функция получения массива 0 и 1, где строки - группы, столбцы - признаки
        public int[,] group_priznak() // 
        {
            int m = priz_group_dlina(0);
            int n = priz_group_dlina(1);
            int[,] result = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = 0;
                }
            }
            StreamReader sr = new StreamReader(path_gr_pr, System.Text.Encoding.Default);
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] next = line.Split(new char[] { ' ' });
                    result[Convert.ToInt32(next[1]) - 1, Convert.ToInt32(next[0]) - 1] = 1;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            string red = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    red=red+result[i, j].ToString()+" ";
                }
                red = red + "\n";
            }
            //MessageBox.Show(red);
            return result;
        }

        // Функция получения кол-ва вулканов (из файла с вулканами)
        public int vulkan_dlina()
        {
            int result = 0;
            StreamReader sr = new StreamReader(path_vulkans, System.Text.Encoding.Default);
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result++;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            return result;
        }
        
        // Функция получения массива с названием файлов вулканов
        public string[] vulkans_name(int num)
        {
            string[] result = new string[col_vulkans];
            int count = 0;
            StreamReader sr = new StreamReader(path_vulkans, System.Text.Encoding.Default);
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if( num==0) result[count] = line + ".txt";
                    else result[count] = line;
                    count++;
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            return result;
        }

        // Функция получения массива 0 и 1, где строки - вулканы, столбцы - признаки
        public int[,] vulkan_priznak()
        {
            int m = priz_group_dlina();
            int n = col_vulkans;
            int[,] result = new int[n, m];
            string[] vsp = vulkans_name(0);
            for (int i = 0; i < n; i++)
            {
                string line_txt = vsp[i];
                string way = String.Concat(path_folder_vulkan, line_txt);
                StreamReader sr = new StreamReader(way, System.Text.Encoding.Default);
                try
                {
                    string line;
                    int count = 0;
                    while ((line = sr.ReadLine()) != null)
                    {
                        result[i, count] = Convert.ToInt32(line);
                        count++;
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {
                    sr.Close();
                }
            }
            return result;
        }
        // Функция получения массива 0 и 1, где строки - вулканы, столбцы - координаты
        public double[,] coord_vulkans() // 
        {
            int n = col_vulkans;
            int m = 2;
            double[,] result = new double[n, m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    result[i, j] = 0;
                }
            }
            StreamReader sr = new StreamReader(path_koord, System.Text.Encoding.Default);
            try
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] next = line.Split(new char[] { ' ' });
                    result[i, 0] = Convert.ToDouble(next[0]);
                    result[i, 1] = Convert.ToDouble(next[1]);
                    i = i + 1;
                }
                
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            return result;
        }

        // Функция сохранения для ПРИЗНАКОВ
        public bool save_priznaks(string[] new_priznaki)
        {
            bool result1 = false;
            using (StreamWriter sw = new StreamWriter(path_priznaki, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < new_priznaki.Length; i++)
                {
                    sw.WriteLine(new_priznaki[i]);
                    sw.WriteLine(Convert.ToString(i + 1));
                }
                result1 = true;
                sw.Close();
            }
            return result1;
        }

        // Функция сохранения для ГРУПП
        public bool save_groups(string[] new_groups)
        {
            bool result1 = false;
            using (StreamWriter sw = new StreamWriter(path_groups, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < new_groups.Length; i++)
                {
                    sw.WriteLine(new_groups[i]);
                    sw.WriteLine(Convert.ToString(i + 1));
                }
                result1 = true;
                sw.Close();
            }
            return result1;
        }

        // Функция сохранения для ВУЛКАНОВ (ВСЕХ)
        public bool save_vulkans(string[] new_vulkans)
        {
            bool result1 = false;
            using (StreamWriter sw = new StreamWriter(path_vulkans, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < new_vulkans.Length; i++)
                {
                    sw.WriteLine(new_vulkans[i]);
                }
                result1 = true;
                sw.Close();
            }
            return result1;
        }

        // Функция сохранения связи ГРУПП С ПРИЗНАКАМИ
        public bool save_group_to_priznak(int[,] new_gr_pr, int count_group, int count_priznak)
        {
            bool result1 = false;
            using (StreamWriter sw = new StreamWriter(path_gr_pr, false, System.Text.Encoding.Default))
            {
                for (int i = 0; i < count_group; i++)
                {
                    for (int j = 0; j < count_priznak; j++)
                    {
                        if (new_gr_pr[i, j] == 1)
                        {
                            sw.WriteLine((j + 1) + " " + (i + 1));
                        }
                    }
                }
                result1 = true;
                sw.Close();
            }
            return result1;
        }

        // Функция сохранения признаков ВУЛКАНА
        public bool save_vulkan(string filename, int[,] priznaki, int index_vulkan, int count_priznak)
        {
            bool result1 = false;
            using (StreamWriter sw = new StreamWriter(path_folder_vulkan + filename + ".txt", false, System.Text.Encoding.Default))
            {
                // Добавить запись для координат вулканчика
                for (int i = 0; i < count_priznak; i++)
                {
                    sw.WriteLine(priznaki[index_vulkan, i]);
                }
                result1 = true;
                sw.Close();
            }
            return result1;
        }

        // Функция переименования ВУЛКАНА
        public bool delete_vulkan(string filename)
        {
            bool result1 = false;
            FileInfo file = new FileInfo(path_folder_vulkan + filename + ".txt");
            /*using (StreamWriter sw = new StreamWriter(path_folder_vulkan, false, System.Text.Encoding.Default))
            {

            }*/
            if (file.Exists)
            {
                file.Delete();
                // альтернатива с помощью класса File
                // File.Delete(path);
                result1 = true;
            }
            return result1;
        }

        // Функция получения продукции для определённой фракции
        public List<List<int>> get_area(string area)
        {
            List<List<int>> result = new List<List<int>>();
            StreamReader sr = new StreamReader(path_area, System.Text.Encoding.Default);
            {
                string line;
                string produk = "";
                int i = 0, k = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if ((i % 2 == 1) && (line == area))
                    {
                        string[] produks = produk.Split(new char[] { ' ' }, StringSplitOptions.None);
                        result.Add(new List<int>());
                        for (int j = 0; j < produks.Length; j++)
                        {
                            result[k].Add(Convert.ToInt32(produks[j]));
                        }
                        k++;
                    }
                    i++;
                    produk = line;

                }
                sr.Close();
            }
            sr.Close();

            return result;
        }


        public List<List<int>> group_priznak_2()
        {
            int vsego_groups = priz_group_dlina(1); // можно и проще
            List<List<int>> result = new List<List<int>>();
            StreamReader sr = new StreamReader(path_gr_pr, System.Text.Encoding.Default);
            try
            {
                string line;
                int index = 0;
                List<int> vsp = new List<int>();
                while ((line = sr.ReadLine()) != null)
                {
                    string[] next = line.Split(new char[] { ' ' });
                    if (Convert.ToInt32(next[1]) - 1 == index)
                    {
                        vsp.Add(Convert.ToInt32(next[0]));
                    }
                    else
                    {
                        result.Add(new List<int>(vsp));
                        index++;
                        vsp.Clear();
                        vsp.Add(Convert.ToInt32(next[0]));
                    }
                    //result[Convert.ToInt32(next[1])].Add(Convert.ToInt32(next[0]));

                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            return result;
        }
        // Функция возвращает массив всех фракций/area
        public string[] get_all_area()
        {
            string red = "";
            List<string> res = new List<string>();
            StreamReader sr = new StreamReader(path_area, System.Text.Encoding.Default);
            try
            {
                string line;
                int i = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    if (i % 2 == 1)
                    {
                        bool have = false;
                        foreach (string value in res)
                        {
                            if (value == line)
                            {
                                have = true;
                                break;
                            }
                        }
                        if (!have)
                        {
                            res.Add(line);
                        }
                    }
                    i++;
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                sr.Close();
            }
            string[] result = new string[res.Count];
            int j = 0;
            foreach (string value in res)
            {
                red += value;
                result[j] = value;
                j++;
            }
            
           // MessageBox.Show(red);
            return result;
        }
    }
}

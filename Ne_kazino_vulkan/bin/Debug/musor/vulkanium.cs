using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace VULKAN
{
    class vulkanium
    {
        public string path_vulkans { get; } = @"KB\ALL_VULKANS.txt";
        public string path_priznaki { get; } = @"KB\priznaki-nazvanie.txt";
        public string path_groups { get; } = @"KB\gruppa_priznakov-nazvanie.txt";
        public string path_gr_pr { get; } = @"KB\nomer_priznaka-nomer_gruppy.txt";
        public string path_folder_vulkan { get; } = @"KB\Vulkans\";

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
        public string[] vulkans_name()
        {
            int lenght_mass = vulkan_dlina();
            string[] result = new string[lenght_mass];
            int count = 0;
            StreamReader sr = new StreamReader(path_vulkans, System.Text.Encoding.Default);
            try
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    result[count] = line;
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
            int n = vulkan_dlina();
            int[,] result = new int[n, m];
            string[] vsp = vulkans_name();
            for(int i=0; i<n;i++)
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









    }
}

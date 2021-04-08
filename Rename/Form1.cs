using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rename
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Rename();
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Исключение: {ex.Message}");
            }

        }
        
        private void Rename()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Не выбрана папка");
            }
            else
            {
                // сортируем файлы по дате создания в выбранной папке
                String[] files = Directory.GetFiles(textBox1.Text).OrderBy(d => new FileInfo(d).CreationTime).ToArray();

                //перебираем все файлы 
                for (int i = 0; i < files.Length; i++)
                {
                    //если файл существует, то переименовываем с постфиксом  "_zxcm284dh3d" 
                    if (File.Exists(GetFullPath(textBox1.Text, i, true)))
                    {
                        for (int j = 0; j < files.Length; j++)
                        {
                            File.Move(files[j], GetFullPath(textBox1.Text, j, false, "_zxcm284dh3d"));
                        }
                    }
                }

                //сортируем
                String[] filesnew = Directory.GetFiles(textBox1.Text).OrderBy(d => new FileInfo(d).CreationTime).ToArray();
                
                //переименовываем в номер по порядку
                for (int i = 0; i < files.Length; i++)
                {  
                    File.Move(filesnew[i], GetFullPath(textBox1.Text, i, true));
                }

                MessageBox.Show("Переименовано " + files.Length + " файла(ов)");
            }
        }

        static string GetFullPath(string path, int count, bool first, string newname = "q")
        {
            String[] files = Directory.GetFiles(path).OrderBy(d => new FileInfo(d).CreationTime).ToArray(); // сортируем файлы по дате создания
            int ind = files[count].Length - files[count].LastIndexOf('.'); //число символов расширения с точкой
            String ras = files[count].Substring(files[count].Length - ind); //расширение
            int name = count + 1;

            String fullpath;
            //false если необходимо добавить постфикс
            if (first == true)
            {
                fullpath = path + "\\" + name + ras;
            }
            else
            {
                fullpath = path + "\\" + count + newname + ras;
            }
            
            return fullpath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = FBD.SelectedPath;
            }
        }

    }
}

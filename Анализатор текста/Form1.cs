using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Анализатор_текста
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Dictionary<string, int> Method(string[] masSt)
        {
            textBox2.Text = string.Empty;
            Dictionary<string, int> FreqDic = new Dictionary<string, int>();
            foreach (string s in masSt)
                if (FreqDic.ContainsKey(s.ToUpper()))
                    FreqDic[s.ToUpper()]++;
                else
                    FreqDic.Add(s.ToUpper(), 1);
            foreach (var k_v in FreqDic.OrderByDescending(x => x.Value))
            {
                textBox2.Text += k_v.Key + " " + k_v.Value + Environment.NewLine;
            }
            return FreqDic;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            string[] masST = textBox1.Text.ToString().Split(new char[] { ' ', ',', '.', '!', '?', '-', ':' }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> FreqDic = Method(masST);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string inpf;
                OpenFileDialog od = new OpenFileDialog();
                od.Filter = "Текстовые файлы|*.txt";
                od.Title = "Выберите файл с текстом для анализа";
                if (od.ShowDialog() == DialogResult.OK)
                {
                    inpf = od.FileName;
                    SaveFileDialog sv = new SaveFileDialog();
                    sv.Filter = "Текстовые файлы|*.txt";
                    sv.Title = "Выберите файл для сохранения результата";
                    if (sv.ShowDialog() == DialogResult.OK)
                    {
                        od.Title = "Выберите файл программы mystem";
                        od.Filter = "Приложения|*.exe";
                        if (od.ShowDialog() == DialogResult.OK)
                        {
                            System.Diagnostics.Process command = new System.Diagnostics.Process();
                            command.StartInfo.FileName = od.FileName;
                            command.StartInfo.Arguments = inpf + " " + sv.FileName;
                            command.Start();
                            MessageBox.Show("Успешно!!");
                            textBox1.Text = File.ReadAllText(sv.FileName);
                            textBox2.Text = string.Empty;
                            string[] Mas = textBox1.Text.Split(new char[] { '}' }, StringSplitOptions.RemoveEmptyEntries);
                            string ItogS = string.Empty;
                            foreach (string s in Mas)
                            {
                                if (s.Contains('|'))
                                    textBox2.Text += s.Remove(s.IndexOf('|')) + Environment.NewLine;
                                else
                                    textBox2.Text += s + Environment.NewLine;
                                string[] st = s.Split('{');
                                if (st[1].Contains('|'))
                                    ItogS += st[1].Remove(st[1].IndexOf('|')) + " ";
                                else ItogS += st[1] + " ";
                            }
                            textBox1.Text = ItogS;
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
    


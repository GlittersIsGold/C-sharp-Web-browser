using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WebBrowserKKM
{
    public partial class Form1 : Form
    {
        String HomePage = "about:blank";
        String[] Arraybox;
        String FilePath = "D:\\WebBrowserKolmykovKM - ComboBox History.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
            Arraybox = comboBox1.Items.Cast<string>().ToArray();
            SaveFile(FilePath, Arraybox, null, true);
        }

        private void навигацияToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void домашняяСтраницавToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Пустая страница")
                webBrowser1.Navigate(new Uri(HomePage));
            else webBrowser1.GoHome();
        }

        private void назадToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void вперёдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try { webBrowser1.Navigate(new Uri(comboBox1.Text.ToString())); }
            catch (UriFormatException Error) { MessageBox.Show("Ошибка! Неверный формат URL-ссылки\n\nПожалуйста, используйте http://<Ваш адрес>\n\nСодержание ошибки: " + Error.Message); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            comboBox1.Text = webBrowser1.Url.ToString();
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Navigate(comboBox1.Text);
            }
        }
        private void Navigate(String Address)
        {
            if (String.IsNullOrEmpty(Address)) return;
            if (Address.Equals("about:blank")) return;
            if ((!Address.StartsWith("http://")) && !Address.StartsWith("htpps://")) {Address = "http://" + Address;}
            try { webBrowser1.Navigate(new Uri(Address)); SaveFile(FilePath, null, Address, false); }
            catch (UriFormatException) { return; }
        }
        
        public void SaveFile(String FilePath, String [] FileArrayBox, String FileAddress, Boolean Mode)
        { 
          if (Mode == true) File.WriteAllLines(FilePath, FileArrayBox);
          if (Mode == false) File.AppendAllText(FilePath, FileAddress + Environment.NewLine);         
        }
    }
}

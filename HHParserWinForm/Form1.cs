using HHParserWinForm.InnerProg.Sender;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using HHParserWinForm.InnerProg;

namespace HHParserWinForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        AboutBrowser aboutBrowser = new AboutBrowser();
        ActionsPressButton pressButton = new ActionsPressButton();

        private void Form1_Load(object sender, EventArgs e)
        {
            aboutBrowser.SetOptions();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            aboutBrowser.BrowserIsStarted = await pressButton.StartButton(aboutBrowser, textBox1.Text);
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() => { pressButton.LoginButton(aboutBrowser, textBox2.Text); });
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                aboutBrowser.Driver.Close();
                aboutBrowser.Driver.Dispose();
            }
            catch (Exception)
            {
                aboutBrowser.DriverService.Dispose();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string[] fields = { "EmployerName", "VacancyName" };
            InnerProg.Mongo.Replacer replacer = new InnerProg.Mongo.Replacer();
            Task.Run(() => { replacer.ReplaceVacanceAndEmployerName(fields, "&nbsp;", " "); });
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            if (aboutBrowser.BrowserIsStarted)
            {
                pressButton.SaveValuesButton(aboutBrowser, textBox3.Text, textBox4.Text, comboBox1.SelectedIndex);
                do
                {
                    var r = Task.Run(() => { pressButton.GoToPageAndDoTheEvil(aboutBrowser, aboutBrowser.NumberOfSearchingPage); });
                    await r;
                }
                while (!aboutBrowser.FinalPage);
            }
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (aboutBrowser.FinalPage)
            {
                MessageBox.Show("Программа остановлена. FinalPage = true");
            }
        }
    }
}

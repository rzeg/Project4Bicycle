using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{


    public partial class Form1 : Form
    {

        List<int> exes = new List<int> { 1, 3, 7, 9 };
        List<int> whys = new List<int> { 10, 20, 30, 40 };

        public Form1()
        {
            InitializeComponent();
            chart2.Name = "Waste";
            chart2.Series.Add("Rest");
            chart2.Series[0].Points.DataBindXY(exes, whys);
            chart2.Series[0].Name = "Containers";
            chart2.Series[1].Points.DataBindXY(exes, whys);
            chart2.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            exes.Add(13);
            whys.Add(50);
            chart2.Series[0].Points.DataBindXY(exes, whys);
            chart2.Series[1].Points.DataBindXY(exes, whys);      
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            chart2.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chart2.Hide();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://mysafeinfo.com/api/data?list=englishmonarchs&format=json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            dynamic json = JsonConvert.DeserializeObject(content);
            dynamic s = json[0];
            string result = (string)s.nm;
            chart2.Series[0].Name = result;
            chart2.Show();

            for (var i = 0; i < json.Count; i++)
            {
                dynamic item = json[i];
                Console.WriteLine("Name: {0}, Lifetime: {1}", (string)item.nm, (string)item.yrs);
            }

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chart2.Hide();
        }
    }
}

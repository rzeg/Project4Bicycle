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

        List<CityDataMonth> cityDataMonth = new List<CityDataMonth>();

        string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

        public Form1()
        {
            InitializeComponent();

            chart2.Hide();
            chart1.Hide();



            chart3.Hide();
            chart4.Hide();
            chart5.Hide();


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            dynamic question1 = GetJsonURL("http://145.24.222.220/v2/questions/q1");

            dynamic question2 = GetJsonURL("http://145.24.222.220/v2/questions/q2");

            dynamic question3 = GetJsonURL("http://145.24.222.220/v2/questions/q3");

            dynamic question4a = GetJsonURL("http://145.24.222.220/v2/questions/q4a");

            dynamic question4b = GetJsonURL("http://145.24.222.220/v2/questions/q4b");


            buildQ1(question1);
            buildQ2(question2);
            buildQ3(question3);
            buildQ4(question4a, question4b);




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
            chart1.Hide();

            chart3.Hide();
            chart2.Show();
            chart4.Hide();
            chart5.Hide();

            listBox1.Show();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Hide();
            chart2.Hide();

            chart3.Hide();

            listBox1.Hide();

            chart4.Show();
            chart5.Show();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Show();
            chart2.Hide();
            chart3.Hide();
            chart4.Hide();
            chart5.Hide();

            listBox1.Hide();


        }

        private void chart1_Click_1(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            chart1.Hide();
            chart2.Hide();
            chart3.Show();
            chart4.Hide();
            chart5.Hide();
            listBox1.Hide();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            int index = (int)listBox1.SelectedIndex;

            chart2.Series[0].Points.DataBindXY(cityDataMonth[index].Months, cityDataMonth[index].Thefts);
            chart2.Series[0].Name = "Thefts";

            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;


            chart2.Series[1].Name = "Bike Containers";
            chart2.Series[1].Points.DataBindXY(cityDataMonth[index].Months, cityDataMonth[index].Trommels);

            //chart2.Series[0].Points.DataBindXY(locations, containers);
            //chart2.Series[0].Name = "Thefts";

            //chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            //chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;


            //chart2.Series[1].Name = "Bike Containers";
            //chart2.Series[1].Points.DataBindXY(locations, stolenbikes);


        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void chart5_Click(object sender, EventArgs e)
        {

        }

        public dynamic GetJsonURL(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return JsonConvert.DeserializeObject(content);
        }

        public void buildQ1(dynamic json)
        {
            List<int> amount = new List<int>();

            List<string> locations = new List<string>();

            for (var i = 0; i < json.Count; i++)
            {
                dynamic item = json[i];
                Console.WriteLine("Name: {0}, Lifetime: {1}", (string)item.Count, (string)item.Neighborhoods);

                amount.Add((int)item.Count);
                locations.Add((string)item.Neighborhoods);


            }

            chart1.Series[0].Points.DataBindXY(locations, amount);
            chart1.Series[0].Name = "Bike Containers";


        }

        public void buildQ2(dynamic json)
        {

            List<int> dates = new List<int>();


            List<string> months = new List<string>();

            List<int> stolenbikes = new List<int>();

            int count = 0;

            for (var i = 0; i < json.Count; i++)
            {
                dynamic item = json[i];
                //Console.WriteLine("Name: {0}, Lifetime: {1}", (string)item.Month, (string)item.Neighborhoods);


                if (i > 0 && json[i - 1].Year != json[i].Year)
                {
                    count += 12;
                    int date = (int)item.Month + count;
                    dates.Add(date);
                    stolenbikes.Add((int)item.StolenBikes);
                    Console.WriteLine("Date: {0}", date.ToString());

                    int curMonth = (int)item.Month - 1;
                    months.Add(monthNames[curMonth] + " " + (string)item.Year);


                }
                else
                {
                    int date = (int)item.Month + count;

                    int curMonth = (int)item.Month - 1;
                    months.Add(monthNames[curMonth] +  " " + (string)item.Year);


                    dates.Add(date);
                    stolenbikes.Add((int)item.StolenBikes);

                    Console.WriteLine("Date: {0}", date.ToString());
                }


                chart3.Series[0].Points.DataBindXY(months, stolenbikes);
                chart3.Series[0].Name = "Bike Thefts";


            }


        }

        public void buildQ3(dynamic json)
        {

            List<int> containers = new List<int>();

            List<int> stolenbikes = new List<int>();

            List<string> monthhap = new List<string>();








            List<string> locations = new List<string>();

            for (var i = 0; i < json.Count; i++)
            {
                dynamic item = json[i];
                //Console.WriteLine("Name: {0}", (string)item.Neighbourhood);

                dynamic rows = item.Rows;

                int totalThefts = 0;
                int trommels = 0;

                stolenbikes = new List<int>();
                containers = new List<int>();
                monthhap = new List<string>();

                for (var j = 0; j < rows.Count; j++)
                {
                    dynamic data = rows[j];

                    int curMonth = data.Month - 1;

                    stolenbikes.Add((int)data.Thefts);
                    containers.Add((int)data.Trommels);
                    monthhap.Add(monthNames[curMonth] + " " + (string)data.Year);


                    //Console.WriteLine("Thefts: {0}", (string)data.Thefts);
                }

                locations.Add((string)item.Neighbourhood);

                cityDataMonth.Add(new CityDataMonth(stolenbikes, containers, monthhap));

                //containers.Add((int)item.Count);
                //locations.Add((string)item.Neighborhoods);


            }



            listBox1.DataSource = locations;

            chart2.Series[0].Points.DataBindXY(cityDataMonth[0].Months, cityDataMonth[0].Thefts);
            chart2.Series[0].Name = "Thefts";

            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;


            chart2.Series[1].Name = "Bike Containers";
            chart2.Series[1].Points.DataBindXY(cityDataMonth[0].Months, cityDataMonth[0].Trommels);

        }
        public void buildQ4(dynamic json, dynamic json2)
        {

            List<int> amountstolen = new List<int>();

            List<int> amountstolen2 = new List<int>();

            List<string> brand = new List<string>();
            List<string> color = new List<string>();


            for (var i = 0; i < json.Count; i++)
            {
                dynamic item = json[i];

                brand.Add((string)item.Brand);
                amountstolen.Add((int)item.Count);
            }

            for (var j = 0; j < json2.Count; j++)
            {
                dynamic item = json2[j];

                color.Add((string)item.Color);
                amountstolen2.Add((int)item.Count);
            }


            chart4.Series[0]["PieLabelStyle"] = "Disabled";
            chart5.Series[0]["PieLabelStyle"] = "Disabled";

            chart4.Series[0].Points.DataBindXY(brand, amountstolen);
            chart4.Series[0].Name = "Thefts";

            chart5.Series[0].Points.DataBindXY(color, amountstolen2);
            chart5.Series[0].Name = "Thefts";

            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://mysafeinfo.com/api/data?list=englishmonarchs&format=json");
            //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //string content = new StreamReader(response.GetResponseStream()).ReadToEnd();
            //dynamic json = JsonConvert.DeserializeObject(content);
            //dynamic s = json[0];
            //string result = (string)s.nm;
            //chart2.Series[0].Name = result;
            //chart2.Show();

            //for (var i = 0; i < json.Count; i++)
            //{
            //    dynamic item = json[i];
            //    Console.WriteLine("Name: {0}, Lifetime: {1}", (string)item.nm, (string)item.yrs);
            //}

        }

    }


    class CityDataMonth
    {
        public CityDataMonth(List<int> theft, List<int> trommels, List<string> months)
        {
            Thefts = new List<int>(theft);
            Trommels = new List<int>(trommels);
            Months = new List<string>(months);
        }

        public List<int> Thefts { get; set; }
        public List<int> Trommels { get; set; }
        public List<string> Months { get; set; }
    }


}

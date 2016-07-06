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
using Project4Bicycle;




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

        public async void GenerateGraph()
        {
            Project4Bicycle.BrandColorGenerator generator = new Project4Bicycle.BrandColorGenerator();

            Project4Bicycle.Models.BikeBrandsGraphModel bg = await generator.GenerateBrands();

            BrandColorGenerator generator2 = new BrandColorGenerator();

            Project4Bicycle.Models.BikeColorsGraphModel bg2 = await generator.GenerateColors();


            List<string> brand = new List<string>();

            List<int> amount = new List<int>();

            for (var i = 0; i < bg.model.Count; i++)
            {
                Brand tt = bg.model[i];
                brand.Add(tt.Name);
                amount.Add(tt.Count);
            }

            List<string> color = new List<string>();

            List<int> amount2 = new List<int>();

            for (var j = 0; j < bg2.model.Count; j++)
            {
                dynamic tt = bg2.model[j];
                color.Add(tt.Name);
                amount2.Add(tt.Count);

            }
                
            chart5.Series[0].Points.DataBindXY(brand, amount);
            chart5.Series[0].Name = "Thefts";

            chart4.Series[0].Points.DataBindXY(color, amount2);
            chart4.Series[0].Name = "Thefts";

        }

        public async void GenerateGraph2()
        {
            Project4Bicycle.Top5Generator gen = new Project4Bicycle.Top5Generator();
            Project4Bicycle.Models.BikeGraphModel bg = await gen.GenerateNeighbourhoods();

            List<string> neighbourhood = new List<string>();

            List<int> amount = new List<int>();

            for (var i = 0; i < bg.model.Count; i++)
            {
                Project4Bicycle.Models.Neighbourhood tt = bg.model[i];
                neighbourhood.Add(tt.Name);
                amount.Add(tt.BikeContainerCount);
            }

            chart1.Series[0].Points.DataBindXY(neighbourhood, amount);
            chart1.Series[0].Name = "Bike Containers";

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            GenerateGraph();
            GenerateGraph2();

            //dynamic question1 = GetJsonURL("http://145.24.222.220/v2/questions/q1");

            //-dynamic question2 = GetJsonURL("http://145.24.222.220/v2/questions/q2");

            //-dynamic question3 = GetJsonURL("http://145.24.222.220/v2/questions/q3");

            //dynamic question4a = GetJsonURL("http://145.24.222.220/v2/questions/q4a");

            //dynamic question4b = GetJsonURL("http://145.24.222.220/v2/questions/q4b");


            //buildQ1(question1);
            //buildQ2(question2);
            //buildQ3(question3);
            //buildQ4(question4a, question4b);




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

            label1.Text = "Bike thefts in Rotterdam";

            label1.Show();
            label2.Hide();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Hide();
            chart2.Hide();

            chart3.Hide();

            listBox1.Hide();

            chart4.Show();
            chart5.Show();

            label1.Text = "Amount of stolen bikes by brand in Rotterdam";
            label2.Text = "Amount of stolen bikes by color in Rotterdam";

            label1.Show();
            label2.Show();

        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            chart1.Show();
            chart2.Hide();
            chart3.Hide();
            chart4.Hide();
            chart5.Hide();

            listBox1.Hide();

            label1.Text = "Top 5 bike containers in sub-areas of Rotterdam";

            label1.Show();
            label2.Hide();

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


            label1.Text = "The bike thefts over the past years in Rotterdam";

            label1.Show();
            label2.Hide();


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
                //Console.WriteLine("Name: {0}, Lifetime: {1}", (string)item.Count, (string)item.Neighborhoods);

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
                    //Console.WriteLine("Date: {0}", date.ToString());

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

                    //Console.WriteLine("Date: {0}", date.ToString());
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


            List<BrandStolen> brandstolen = new List<BrandStolen>();
            List<ColorStolen> colorstolen = new List<ColorStolen>();




            for (var i = 0; i < json.Count; i++)
            {
                dynamic item = json[i];

                brandstolen.Add(new BrandStolen((string)item.Brand, (int)item.Count));
            }

            for (var j = 0; j < json2.Count; j++)
            {
                dynamic item = json2[j];


                colorstolen.Add(new ColorStolen((string)item.Color, (int)item.Count));

            }


            var top10BrandStolen = brandstolen.OrderByDescending(w => w.Amount).Take(9).ToArray();
            var brandstolenOther = brandstolen.OrderByDescending(w => w.Amount).Skip(9).Sum(w => w.Amount);

            brand.Add("OTHER");
            amountstolen.Add(brandstolenOther);

            for (var q = 0; q < top10BrandStolen.Count(); q++)
            {
                BrandStolen item = top10BrandStolen[q];

                brand.Add(item.Brand);
                amountstolen.Add(item.Amount);
            }

            var top10ColorStolen = colorstolen.OrderByDescending(w => w.Amount).Take(9).ToArray();
            var colorstolenOther = colorstolen.OrderByDescending(w => w.Amount).Skip(9).Sum(w => w.Amount);

            color.Add("OTHER");
            amountstolen2.Add(colorstolenOther);

            for (var q = 0; q < top10ColorStolen.Count(); q++)
            {
                ColorStolen item = top10ColorStolen[q];

                color.Add(item.Coloro);
                amountstolen2.Add(item.Amount);
            }


            //chart4.Series[0]["PieLabelStyle"] = "Disabled";
            //chart5.Series[0]["PieLabelStyle"] = "Disabled";


            label1.Text = "Amount of stolen biciclyes by brand in Rotterdam";
            label2.Text = "Amount of stolen biciclyes by color in Rotterdam";



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
    class BrandStolen
    {
        public BrandStolen(string brand, int amount)
        {
            Brand = brand;
            Amount = amount;
        }

        public string Brand { get; set; }
        public int Amount { get; set; }
    }
    class ColorStolen
    {
        public ColorStolen(string color, int amount)
        {
            Coloro = color;
            Amount = amount;
        }

        public string Coloro { get; set; }
        public int Amount { get; set; }
    }


}

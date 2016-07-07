using Newtonsoft.Json;
using Project4Bicycle;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private List<CityDataMonth> cityDataMonth = new List<CityDataMonth>();

        private List<string> neighbourhood2 = new List<string>();

        private string[] monthNames = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.MonthNames;

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

        public async void GenerateGraph3()
        {
            List<string> months = new List<string>();
            List<int> stolenBike = new List<int>();
            List<int> bikeContainers = new List<int>();

            Q3Generator generator = new Q3Generator();
            await generator.LoadData();

            foreach (string neighbourhood in generator.GetNeighbourhoodList())
                neighbourhood2.Add(neighbourhood);

            listBox1.DataSource = neighbourhood2;

            generator.SetNeighbourhood(neighbourhood2[(int)listBox1.SelectedIndex]);

            Project4Bicycle.Models.Q3Model bg = generator.GenerateGraphModel();

            for (var i = 0; i < bg.model.Count; i++)
            {
                Project4Bicycle.Models.StackedData tt = bg.model[i];
                months.Add(tt.Month);
                stolenBike.Add(tt.BikeTheftCount);
                bikeContainers.Add(tt.BikeContainerCount);
            }

            chart2.Series[0].Points.DataBindXY(months, stolenBike);
            chart2.Series[0].Name = "Thefts";

            chart2.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart2.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            chart2.Series[1].Name = "Bike Containers";
            chart2.Series[1].Points.DataBindXY(months, bikeContainers);
        }

        public async void GenerateGraph4()
        {
            List<BikeTheft> theftContainer = new List<BikeTheft>();
            List<int> theftsOverYear = new List<int>();
            List<string> theftsMonths = new List<string>();

            var assembly = typeof(BikeTheftViewModel).Assembly;
            Stream stream = assembly.GetManifestResourceStream("Project4Bicycle.Data.b59c159338.csv");
            var reader = new StreamReader(stream);

            Project4Bicycle.BikeTheftFactory factory = new Project4Bicycle.BikeTheftFactory(reader);
            BikeTheft bikeTheft;

            while (factory.HasNext())
            {
                bikeTheft = factory.GetCurrent();

                theftContainer.Add(bikeTheft);
            }

            theftContainer = theftContainer.Where(theft => theft.Object == "FIETS").Where(theft => theft.City == "ROTTERDAM").ToList();

            int max2004 = theftContainer.Where(theft => theft.Year == 2004).Count();
            int max2005 = theftContainer.Where(theft => theft.Year == 2005).Count();
            int max2006 = theftContainer.Where(theft => theft.Year == 2006).Count();
            int max2007 = theftContainer.Where(theft => theft.Year == 2007).Count();
            int max2008 = theftContainer.Where(theft => theft.Year == 2008).Count();
            int max2009 = theftContainer.Where(theft => theft.Year == 2009).Count();
            int max2010 = theftContainer.Where(theft => theft.Year == 2010).Count();
            int max2011 = theftContainer.Where(theft => theft.Year == 2011).Count();
            int max2012 = theftContainer.Where(theft => theft.Year == 2012).Count();
            int max2013 = theftContainer.Where(theft => theft.Year == 2013).Count();

            theftsOverYear.Add(max2004);
            theftsOverYear.Add(max2005);
            theftsOverYear.Add(max2006);
            theftsOverYear.Add(max2007);
            theftsOverYear.Add(max2008);
            theftsOverYear.Add(max2009);
            theftsOverYear.Add(max2010);
            theftsOverYear.Add(max2011);
            theftsOverYear.Add(max2012);
            theftsOverYear.Add(max2013);

            theftsMonths.Add("2004");
            theftsMonths.Add("2005");
            theftsMonths.Add("2006");
            theftsMonths.Add("2007");
            theftsMonths.Add("2008");
            theftsMonths.Add("2009");
            theftsMonths.Add("2010");
            theftsMonths.Add("2011");
            theftsMonths.Add("2012");
            theftsMonths.Add("2013");

            chart3.Series[0].Points.DataBindXY(theftsMonths, theftsOverYear);
            chart3.Series[0].Name = "Bike Thefts";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateGraph();
            GenerateGraph2();
            GenerateGraph3();
            GenerateGraph4();

            //dynamic question1 = GetJsonURL("http://145.24.222.220/v2/questions/q1");

            //-dynamic question2 = GetJsonURL("http://145.24.222.220/v2/questions/q2");

            //dynamic question3 = GetJsonURL("http://145.24.222.220/v2/questions/q3");

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

            GenerateGraph3();
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

    //    public void buildQ2(dynamic json)
    //    {
    //        List<int> dates = new List<int>();

    //        List<string> months = new List<string>();

    //        List<int> stolenbikes = new List<int>();

    //        int count = 0;

    //        for (var i = 0; i < json.Count; i++)
    //        {
    //            dynamic item = json[i];
    //            //Console.WriteLine("Name: {0}, Lifetime: {1}", (string)item.Month, (string)item.Neighborhoods);

    //            if (i > 0 && json[i - 1].Year != json[i].Year)
    //            {
    //                count += 12;
    //                int date = (int)item.Month + count;
    //                dates.Add(date);
    //                stolenbikes.Add((int)item.StolenBikes);
    //                //Console.WriteLine("Date: {0}", date.ToString());

    //                int curMonth = (int)item.Month - 1;
    //                months.Add(monthNames[curMonth] + " " + (string)item.Year);
    //            }
    //            else
    //            {
    //                int date = (int)item.Month + count;

    //                int curMonth = (int)item.Month - 1;
    //                months.Add(monthNames[curMonth] + " " + (string)item.Year);

    //                dates.Add(date);
    //                stolenbikes.Add((int)item.StolenBikes);

    //                //Console.WriteLine("Date: {0}", date.ToString());
    //            }

    //            chart3.Series[0].Points.DataBindXY(months, stolenbikes);
    //            chart3.Series[0].Name = "Bike Thefts";
    //        }
    //    }
    }

    internal class CityDataMonth
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

    internal class BrandStolen
    {
        public BrandStolen(string brand, int amount)
        {
            Brand = brand;
            Amount = amount;
        }

        public string Brand { get; set; }
        public int Amount { get; set; }
    }

    internal class ColorStolen
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
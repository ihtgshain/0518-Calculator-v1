using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace priCalculatorApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();
        double v1 = 0;
        double v2 = 0;
        double result = 0;
        bool isOperated = false;
        string ope = "";
        const int units= 11;

        public void btnNum(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (!isOperated)
            {
                if (list1.Count > units || (list1.Count==0 && btn.Text=="0")) return;
                v1 = 0;
                list1.Add(int.Parse(btn.Text));
                foreach (int n in list1)
                {
                    v1 = v1 * 10 + n;
                }
                mDisplay.Text = $"{v1:.###}";
            }
            else 
            {
                if (list2.Count > units || (list2.Count == 0 && btn.Text == "0" && ope!="X")) return;

                v2 = 0;
                if (btn.Text == "0")
                {
                    mDisplay.Text = $"{v2}";
                    sDisplay.Text = $"{v1}  {ope}";
                }
                else
                {
                    list2.Add(int.Parse(btn.Text));
                    foreach (int n in list2)
                    {
                        v2 = v2 * 10 + n;
                    }
                    mDisplay.Text = $"{v2:.###}";
                    sDisplay.Text = $"{v1}  {ope}";
                }
                
            }
        }
        public void btnOpe(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            isOperated = true;
            ope= btn.Text;
            sDisplay.Text = $"{v1}  {ope}";
        }
        public void btnEqu(object sender, EventArgs e)
        {
            if (!isOperated || (v2==0 && ope!="X")) return;
            result = 0;

            if (ope == "+")
            {
                if (v1 >= double.MaxValue/2 || v2 >= double.MaxValue/2) {
                    allClear();
                    mDisplay.Text = "The value is Over range";
                }
                result = v1 + v2;
            }
            else if (ope == "-")
            {
                result = v1 - v2;
            }
            else if (ope == "X")
            {
                if (v1 >= Math.Pow(double.MaxValue, 0.5) || v2 >= Math.Pow(double.MaxValue, 0.5)) {
                    allClear();
                    mDisplay.Text = "The value is Over range";
                }
                result = v1 * v2;
            }
            else 
            {
                result = Math.Round(v1 / v2,4);
            }
            allClear();
            v1 = result;
            if (v1 > Math.Pow(10, 9)) mDisplay.FontSize = 40;
            else if (v1 > Math.Pow(10, 99)) mDisplay.FontSize = 30;
            mDisplay.Text = $" {v1}";
        }

        public void btnCle(object sender, EventArgs e) => allClear();

        public void allClear()
        {
            v1 = v2 = 0;
            ope = "";
            isOperated = false;
            list1.Clear();
            list2.Clear();
            sDisplay.Text = "";
            mDisplay.Text = "0";
            mDisplay.FontSize = 50;
        }
    }
}

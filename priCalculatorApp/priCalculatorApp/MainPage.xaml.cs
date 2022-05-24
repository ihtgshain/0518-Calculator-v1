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
        string s1 ="0";
        string s2 = "";
        double v1 = 0;
        double v2 = 0;
        double result = 0;
        bool isOperated = false;
        string ope = "";
        const int digits= 11;
        bool cantBack = false;
        bool isCalOver = false;

        public void btnNum(object sender, EventArgs e)
        {
            if(isCalOver) ClearAll();
            Button btn = (Button)sender;
            if (!isOperated) 
            {
                if (s1.Length > digits || (s1== "0" && btn.Text=="0")) return;
                v1 = 0;
                s1 += btn.Text;
                v1 = double.Parse(s1);
                ShowValus("mode1");
            }
            else // has pushed +-*/
            {
                if (s2.Length > digits || (s2 == "0" && btn.Text == "0" && ope!="X")) return;

                v2 = 0;
                if (btn.Text == "0")
                {
                    ShowValus("mode2");
                }
                else
                {
                    s2 += btn.Text;
                    v2 = double.Parse(s2);
                    ShowValus("mode2");
                }
            }
        }

        public void btnOpe(object sender, EventArgs e)
        {
            if (isOperated == true) Calculate();
            Button btn = (Button)sender;
            //if (isOperated == true && ope==btn.Text) Calculate();
            isOperated = true;
            ope= btn.Text;
            sDisplay.Text = $"{v1}  {ope}";
            cantBack = true;
        }
        public void Calculate()
        {
            if (!isOperated || (v2 == 0 && ope != "X")) return;
            result = 0;

            if (ope == "+")  //+ ============================================
            {
                if (v1 >= double.MaxValue / 2 || v2 >= double.MaxValue / 2)
                {
                    ClearAll();
                    mDisplay.Text = "The value is Over range";
                }
                result = v1 + v2;
            }
            else if (ope == "-") //- ============================================
            {
                result = v1 - v2;
            }
            else if (ope == "X") //X ============================================
            {
                if (v1 >= Math.Pow(double.MaxValue, 0.5) || v2 >= Math.Pow(double.MaxValue, 0.5))
                {
                    ClearAll();
                    mDisplay.Text = "The value is Over range";
                }
                result = v1 * v2;
            }
            else // divid ============================================
            {
                result = Math.Round(v1 / v2, 4);
            }
            ClearAll();
            v1 = result;
            s1 = v1.ToString();
            if (Math.Abs(v1) > Math.Pow(10, 8)) mDisplay.FontSize = 40;
            else if (Math.Abs(v1) > Math.Pow(10, 99)) mDisplay.FontSize = 25;
            mDisplay.Text = $" {v1}";
        }

        public void btnEqu(object sender, EventArgs e)
        {
            Calculate();
            isCalOver = true;
        }

        public void btnBac(object sender, EventArgs e)
        {
            if (cantBack) return;

            double temp1 = Math.Abs(v1);
            double temp2 = Math.Abs(v2);

            if (!isOperated)
            {
                if (temp1 > Math.Pow(10, digits)) return;
                if ((temp1 > 0 && temp1 <1 ) || s1.Length == 1 || (s1.Length == 2 && s1.Contains("-")))
                {
                    s1 = "0";
                    v1 = 0;
                }
                else
                {
                    string temp = s1.Remove(s1.Length - 1);
                    s1 = temp;
                    v1 = double.Parse(s1);
                }
                ShowValus("mode1");
            }
            else
            {
                if (temp2 > Math.Pow(10, digits)) return;
                if ((temp1 > 0 && temp1 < 1) || s2.Length == 1 || (s1.Length == 2 && s1.Contains("-")))
                {
                    s2 = "0";
                    v2 = 0;
                }
                else
                {
                    string temp = s2.Remove(s2.Length - 1);
                    s2 = temp;
                    v2 = double.Parse(s2);
                }
                ShowValus("mode2");
            }
        }
        
        public void ShowValus(string mode)
        {
            cantBack = false;
            if (mode == "mode1")
            {
                mDisplay.Text = s1 = $"{v1:0.###}";
            }
            else if (mode == "mode2")
            {
                mDisplay.Text = s2 = $"{v2}";
                sDisplay.Text = $"{v1}  {ope}";
            }
        }

        public void btnCle(object sender, EventArgs e) => ClearAll();

        public void ClearAll()
        {
            v1 = v2 = 0;
            ope = "";
            isOperated = false;
            s1= "0";
            s2 = "";
            sDisplay.Text = "";
            mDisplay.Text = "0";
            mDisplay.FontSize = 50;
            isCalOver = false;
        }
    }
}

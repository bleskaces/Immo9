using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IM9
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       double[] probs, freq; int[] stat;
        int N; double a; double[,] vyborka; double chi = 9.488; double X;
        
        Random r = new Random();
       
        private void toGo_Click(object sender, EventArgs e)
        {
            double Memp = 0, M = 0, Demp = 0, D = 0;
            probs = new double[5]; stat = new int[5]; freq = new double[5];

            probs[0] = (double)prob1.Value;
            probs[1] = (double)prob2.Value;
            probs[2] = (double)prob3.Value;
            probs[3] = (double)prob4.Value;
            probs[4] = 1 - probs[0] - probs[1] - probs[2] - probs[3];
            prob5.Text = probs[4].ToString();

            N = (int)tbN.Value;

            vyborka = new double[3,N];

            for (int i = 0; i < N; i++)
            {
                a = r.NextDouble();
                double summ = 0;
                for (int k = 0; k < 7; k++)
                {
                    summ += probs[k];
                    if (a <= summ) { stat[k]++; vyborka[0, i] = a; vyborka[1, i] = probs[k]; vyborka[2, i] = k; break; }
                }
            }

            for( int i=0; i< 5;i++)
            {
                freq[i] = stat[i] / (double)N;
            }
           
            for(int i = 0; i<5; i++)
            {
                M += probs[i] * stat[i];
                Memp += freq[i] * stat[i];
                D+=probs[i] * stat[i] *stat[i];
                Demp += freq[i] * stat[i] * stat[i];
            }
            D -= M * M; Demp -= Memp * Memp;
           // for (int i = 0; i<N; i++)
           // {
           //     M += vyborka[0, i] * vyborka[1, i];
           //     Memp += vyborka[0, i] * freq[(int)vyborka[2, i]];
           //     D+= vyborka[0, i] * vyborka[0, i] * vyborka[1, i];
           //     Demp += vyborka[0, i] * vyborka[0, i] * freq[(int)vyborka[2, i]];
           // }
           // D -= M * M; Demp -= Memp * Memp;

            textBox1.Text = Memp.ToString();
            textBox2.Text = Demp.ToString();

            chart1.Series[0].Points.Clear();
            for (int i = 0; i < 5; i++) chart1.Series[0].Points.AddXY(i + 1, freq[i]);

            X = 0;
            for(int i=0; i<5; i++)
            {
                X += (stat[i] * stat[i]) / (N * probs[i]);
            }
            X -= N;
            if (X > chi) textBox3.Text = "TRUE";
            else textBox3.Text = "FALSE";
        }
    }
}
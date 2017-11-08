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


namespace VoiceRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Audio Files (.wav)|*.wav";
            ofd.ShowDialog();
            string path = ofd.FileName;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                new PlaySounds(path);
                chart1.Series.Add("wave");
                chart1.Series["wave"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
                chart1.Series["wave"].ChartArea = "ChartArea1";
                NAudio.Wave.WaveChannel32 wave = new NAudio.Wave.WaveChannel32(new NAudio.Wave.WaveFileReader(path));
                byte[] buffer = new byte[16384];
                int read = 0;
                while(wave.Position<wave.Length)
                {
                    read = wave.Read(buffer, 0, 16384);
                    for (int i = 0; i < read/4; i++)
                    {
                        chart1.Series["wave"].Points.Add(BitConverter.ToSingle(buffer, i * 4));

                    }
                }

            }

        }

    }
}

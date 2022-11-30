using System;
using System.Windows.Forms;

using Lab4.DesktopClient.Simpex;

namespace Lab4.DesktopClient
{
    public partial class Form1 : Form
    {
        private readonly SimplexSoapClient _client;

        public Form1()
        {
            InitializeComponent();

            this._client = new SimplexSoapClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var a1 = new A
            {
                S = this.A1S.Text,
                K = Convert.ToInt32(this.A1K.Text),
                F = float.Parse(this.A1F.Text),
            };

            var a2 = new A
            {
                S = this.A2S.Text,
                K = Convert.ToInt32(this.A2K.Text),
                F = float.Parse(this.A2F.Text),
            };

            A a3 = this._client.Sum(a1, a2);

            this.A3S.Text = a3.S;
            this.A3K.Text = a3.K.ToString();
            this.A3F.Text = a3.F.ToString();
        }
    }
}

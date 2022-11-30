using System;

using Lab4.WebClient.ServerClass;

namespace Lab4.WebClient
{
    public partial class WebForm : System.Web.UI.Page
    {
        private readonly SimplexService _service;

        public WebForm()
        {
            this._service = new SimplexService();
        }

        protected void Send(object sender, EventArgs e)
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

            A a3 = this._service.Sum(a1, a2);

            this.A3S.Text = a3.S;
            this.A3K.Text = a3.K.ToString();
            this.A3F.Text = a3.F.ToString();
        }
    }
}
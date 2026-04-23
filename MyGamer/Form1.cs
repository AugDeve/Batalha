namespace MyGamer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            progress.Value = 0;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progress.Value < 100)
            {
                progress.Value++;
                Percentagem.Text = progress.Value.ToString() + "%";
            }
            else
            {
                var pf = new PrincipalForm();
                pf.Show();
                timer1.Enabled = false;
                this.Visible = false;
            }
        }
    }
}

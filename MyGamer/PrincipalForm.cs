using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyGamer
{
    public partial class PrincipalForm : Form
    {
        public PrincipalForm()
        {
            InitializeComponent();
            progressVida.Value = 100;
            labVida.Text = progressVida.Value.ToString() + "%";


        }
        bool direito, squerdo, spaco;
        int resultado = 0;
        int nivel = 0;

        void Novo_Valor_Nivel_Eliminado()
        {
            nivel = 0;
            labNivel.Text = "0";

            resultado = 0;
            labEliminados.Text = "0";

        }
        void bandido_Invisivel()
        {
            foreach (Control bandi in this.Controls)
            {
                foreach (Control paine in this.Controls)
                {
                    if (paine is Panel && paine.Tag == "violeta")
                    {
                        if (bandi is PictureBox && bandi.Tag == "bandido")
                        {
                            if (bandi.Bounds.IntersectsWith(paine.Bounds))
                            {
                                bandi.Visible = false;
                            }
                            if (bandi.Top > 200)
                            {
                                bandi.Visible = true;
                            }

                        }
                    }
                }
            }

        }
        void Result_Jogo()
        {
            foreach (Control e in this.Controls)
            {
                foreach (Control g in this.Controls)
                {
                    if (e is PictureBox && e.Tag == "balas")
                    {
                        if (g is PictureBox && g.Tag == "bandido")
                        {
                            if (e.Bounds.IntersectsWith(g.Bounds))
                            {
                                g.Top = -100;
                                resultado++;

                                labEliminados.Text = resultado.ToString();
                                this.Controls.Remove(e);
                                if (resultado > 100)
                                {
                                    timerBandido.Interval = 25;
                                    nivel = 1;
                                    labNivel.Text = nivel.ToString();
                                    return;
                                }
                                if (resultado > 200)
                                {
                                    timerBandido.Interval = 20;
                                    nivel = 2;
                                    labNivel.Text = nivel.ToString();
                                    return;
                                }
                                if (resultado > 300)
                                {
                                    timerBandido.Interval = 15;
                                    nivel = 3;
                                    labNivel.Text = nivel.ToString();
                                    return;
                                }
                                if (resultado > 500)
                                {
                                    timerBandido.Interval = 10;
                                    nivel = 4;
                                    labNivel.Text = nivel.ToString();
                                    return;
                                }
                                if (resultado > 800)
                                {
                                    timerBandido.Interval = 5;
                                    nivel = 5;
                                    labNivel.Text = nivel.ToString();
                                }

                            }

                        }
                    }
                }
            }
            if (Atirador.Bounds.IntersectsWith(Bandido1.Bounds) || Atirador.Bounds.IntersectsWith(Bandido2.Bounds))
            {
                panelOver.Enabled = true;
                panelOver.Visible = true;
                desligar_timer();

            }
        }
        void ligar_timer()
        {
            timerBala.Start();
            timerAtirador.Start();
            timerBandido.Start();
            timerStrela.Start();

        }
        void desligar_timer()
        {
            timerBala.Stop();
            timerAtirador.Stop();
            timerBandido.Stop();
            timerStrela.Stop();
        }
        void Mover_Bandido()
        {

            var reptir = new Random();
            int x, y;
            if (Bandido1.Top >= 678)
            {
                valor_progressBara();
                x = reptir.Next(0, 500);
                Bandido1.Location = new Point(x, 0);
            }
            if (Bandido2.Top >= 678)
            {
                valor_progressBara();
                y = reptir.Next(0, 500);
                Bandido2.Location = new Point(y, 0);
            }
            Bandido1.Top += 10;
            Bandido2.Top += 20;
        }
        void Novo_valor_progressBara()
        {
            progressVida.Value = 100;
            labVida.Text = "100%";
        }
        void valor_progressBara()
        {

            if (progressVida.Value < 5)
            {
                desligar_timer();
                panelOver.Enabled = true;
                panelOver.Visible = true;
            }
            else
            {
                progressVida.Value -= 5;
                labVida.Text = progressVida.Value.ToString() + "%";
            }
        }
        void Mover_Bala()
        {
            foreach (Control bal in this.Controls)
            {
                if (bal is PictureBox && bal.Tag == "balas")
                {
                    bal.Top -= 10;
                    if (bal.Top < 120)
                    {
                        this.Controls.Remove(bal);
                    }
                }
            }

        }
        void Criar_Bala()
        {
            var bl = new PictureBox();
            bl.SizeMode = PictureBoxSizeMode.StretchImage;
            bl.Size = new Size(15, 15);
            bl.Image = Properties.Resources.bala;
            bl.BackColor = Color.Transparent;
            bl.Tag = "balas";
            bl.Left = Atirador.Left + 23;
            bl.Top = Atirador.Top - 20;
            this.Controls.Add(bl);
            bl.BringToFront();
        }
        void Mover_Atirador()
        {
            if (direito == true)
            {
                if (Atirador.Left < 538)
                {
                    Atirador.Left += 20;
                }
            }
            if (squerdo == true)
            {
                if (Atirador.Left > 10)
                {
                    Atirador.Left -= 20;
                }
            }
        }
        void Mover_Strela()
        {
            foreach (Control st in this.Controls)
            {

                if (st is PictureBox && st.Tag == "strela")
                {

                    st.Top += 2;
                    if (st.Top > 680)
                    {
                        st.Top = 0;
                    }
                }
            }
        }


        private void PrincipalForm_KeyDown(object sender, KeyEventArgs botao)
        {
            if (botao.KeyCode == Keys.Right)
            {
                direito = true;
            }
            if (botao.KeyCode == Keys.Left)
            {
                squerdo = true;

            }

            if (botao.KeyCode == Keys.W)
            {

                Criar_Bala();
                spaco = true;

            }
            if (botao.KeyCode == Keys.Space)
            {
                if (panelOver.Visible == true)
                {
                    timerBandido.Interval = 60;
                    Novo_Valor_Nivel_Eliminado();
                    Novo_valor_progressBara();
                    ligar_timer();
                    foreach (Control g in this.Controls)
                    {
                        if (g is PictureBox && g.Tag == "bandido")
                        {
                            g.Top = 0;
                        }

                    }


                    panelOver.Visible = false;
                    panelOver.Enabled = false;
                }
                else
                {
                    if (panelMenu.Enabled == false)
                    {
                        timerBandido.Interval = 60;
                        panelMenu.Enabled = true;
                        panelMenu.Visible = true;
                        btNewjogo.Enabled = true;
                        desligar_timer();
                        return;
                    }
                    if (panelMenu.Enabled == true)
                    {
                        timerBandido.Interval = 60;
                        panelMenu.Enabled = false;
                        panelMenu.Visible = false;
                        btNewjogo.Enabled = false;

                        ligar_timer();


                    }
                }



            }

        }

        private void PrincipalForm_KeyUp(object sender, KeyEventArgs botao)
        {
            if (botao.KeyCode == Keys.Right)
            {
                direito = false;
            }
            if (botao.KeyCode == Keys.Left)
            {
                squerdo = false;
            }

        }
        private void timerStrela_Tick(object sender, EventArgs e)
        {
            Mover_Strela();

        }
        private void timerAtirador_Tick(object sender, EventArgs e)
        {
            Mover_Atirador();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Mover_Bala();
        }

        private void timerBandido_Tick(object sender, EventArgs e)
        {
            Mover_Bandido();
            bandido_Invisivel();
            Result_Jogo();
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


       


        private void PlayIniciar_Click(object sender, EventArgs e)
        {
            timerBandido.Interval = 60;
            Novo_Valor_Nivel_Eliminado();
            Novo_valor_progressBara();
            ligar_timer();
            foreach (Control g in this.Controls)
            {
                if (g is PictureBox && g.Tag == "bandido")
                {
                    g.Top = 0;
                }

            }

            panelOver.Visible = false;
            panelOver.Enabled = false;
        }

       

       

        private void btNewjogo_Click(object sender, EventArgs e)
        {
            timerBandido.Interval = 60;
            Novo_Valor_Nivel_Eliminado();
            Novo_valor_progressBara();

            foreach (Control g in this.Controls)
            {
                if (g is PictureBox && g.Tag == "bandido")
                {
                    g.Top = 0;
                }

            }

            if (panelMenu.Enabled == true)
            {
                panelMenu.Enabled = false;
                panelMenu.Visible = false;
                btNewjogo.Enabled = false;

                ligar_timer();

            }
        }

      
    }
}

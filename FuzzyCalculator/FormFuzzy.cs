using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Fuzzy
{
    enum OPERATION { SUM = 1, SUB, MUL, DIV, INT, UNI, POW }

    delegate void Switch(PictureBox a, PictureBox b);

    public partial class FormFuzzy : Form
    {
        private bool isInFront = false;
        private readonly Point pos1, pos2,pos3,pos4;
        private readonly Size size1, size2,size3,size4;
        private double alfa;
        private double alfaN;

        private OPERATION op = 0;
        private Switch mySwitch,mySwitchN;
        private FuzzyNumber A, B, ANon, BNon;
        private Render gr, grN;

        public FormFuzzy()
        {
            InitializeComponent();
           
            mySwitch += MotionDown;
            mySwitch += MotionUp;
            mySwitchN += MotionDownNon;
            mySwitchN += MotionUpNon;

            gr = new Render(pictureBox1.Width, pictureBox1.Height, 0, 100, 1, 25);
            grN = new Render(pictureBox1.Width, pictureBox3.Height, 0, 100, 1, 25);
            pos1 = pictureBox1.Location;
            pos2 = pictureBox2.Location;
            pos3 = pictureBox3.Location;
            pos4 = pictureBox4.Location;
            size1 = pictureBox1.Size;
            size2 = pictureBox2.Size;
            size3 = pictureBox3.Size;
            size4 = pictureBox4.Size;
            try
            {
                A = new FuzzyNumber(Convert.ToInt32(textBoxCenterA.Text), Convert.ToInt32(textBoxLeftA.Text), Convert.ToInt32(textBoxRightA.Text), gr.Max);
                B = new FuzzyNumber(Convert.ToInt32(textBoxCenterB.Text), Convert.ToInt32(textBoxLeftB.Text), Convert.ToInt32(textBoxRightB.Text), gr.Max);

                ANon = new FuzzyNumber(Convert.ToInt32(textBoxCenterAN.Text), Convert.ToInt32(textBoxLeftAN.Text), Convert.ToInt32(textBoxRightAN.Text), grN.Max);
                BNon = new FuzzyNumber(Convert.ToInt32(textBoxCenterBN.Text), Convert.ToInt32(textBoxLeftBN.Text), Convert.ToInt32(textBoxRightBN.Text), grN.Max);

                alfa = Convert.ToDouble(textBoxAlfa.Text);
                alfaN= Convert.ToDouble(textBoxAlphaN.Text);
                if (alfa < 0 || alfa > 1)
                    throw new Exception();
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();
                validateButtons(true);
            }
            catch
            {
                MessageBox.Show("Incorrect Data");
                A = null;
                B = null;
                ANon = null;
                BNon = null;
            }
        }

        private void buttonCalc_Click(object sender, EventArgs e)
        {
            try
            {
                A = new FuzzyNumber(Convert.ToInt32(textBoxCenterA.Text), Convert.ToInt32(textBoxLeftA.Text), Convert.ToInt32(textBoxRightA.Text), gr.Max);
                B = new FuzzyNumber(Convert.ToInt32(textBoxCenterB.Text), Convert.ToInt32(textBoxLeftB.Text), Convert.ToInt32(textBoxRightB.Text), gr.Max);

                ANon = new FuzzyNumber(Convert.ToInt32(textBoxCenterAN.Text), Convert.ToInt32(textBoxLeftAN.Text), Convert.ToInt32(textBoxRightAN.Text), grN.Max);
                BNon = new FuzzyNumber(Convert.ToInt32(textBoxCenterBN.Text), Convert.ToInt32(textBoxLeftBN.Text), Convert.ToInt32(textBoxRightBN.Text), grN.Max);
                alfa = Convert.ToDouble(textBoxAlfa.Text);
                alfaN = Convert.ToDouble(textBoxAlphaN.Text);
                if (alfa < 0 || alfa > 1)
                    throw new Exception();
                pictureBox1.Refresh();
                pictureBox2.Refresh();
                pictureBox3.Refresh();
                pictureBox4.Refresh();
                button3.Enabled = true;
                button4.Enabled = true;
                button5.Enabled = true;
                button6.Enabled = true;
                button7.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Incorrect Data !");
                A = null;
                B = null;
                ANon = null;
                BNon = null;
            }
        }

        private void Motion()
        {
            while (pictureBox1.Location.X < pictureBox2.Location.X)
            {
                pictureBox1.Location = new System.Drawing.Point(pictureBox1.Location.X + 1, pictureBox1.Location.Y);
                pictureBox2.Location = new System.Drawing.Point(pictureBox2.Location.X - 1, pictureBox2.Location.Y);
                Refresh();
            }

            while (pictureBox3.Location.X < pictureBox4.Location.X)
            {
                pictureBox3.Location = new System.Drawing.Point(pictureBox3.Location.X + 1, pictureBox3.Location.Y);
                pictureBox4.Location = new System.Drawing.Point(pictureBox4.Location.X - 1, pictureBox4.Location.Y);
                Refresh();
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;

            if (null != A && 0 == op)
            {
                Plot.MakeSystem(g, gr);
                Function.Calc(ref A);
                Plot.MakeGraphic(g, gr, A, Pens.Blue);
                Plot.AlfaCut(g, gr, A, alfa);
            }
            if (null != A && null != B && 0 != op)
            {
                switch (op)
                {
                    case OPERATION.SUM:
                        Operations.Sum(A, B, g, gr, alfa);
                        break;
                    case OPERATION.SUB:
                        Operations.Sub(A, B, g, gr, alfa);
                        break;
                    case OPERATION.MUL:
                        Operations.Mul(A, B, g, gr, alfa);
                        break;
                    case OPERATION.DIV:
                        Operations.Div(A, B, g, gr, alfa);
                        break;
                    case OPERATION.UNI:
                        Operations.Reu(A, B, g, gr);
                        break;
                    case OPERATION.POW:
                        Operations.Power(A, 3, g, gr);
                        break;
                 
                }
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;


            if (null != B)
            {
                Plot.MakeSystem(g, gr);
                Function.Calc(ref B);
                Plot.MakeGraphic(g, gr, B, Pens.Pink);
                Plot.AlfaCut(g, gr, B, alfa);
                MotionGraphic(g);
            }
        }


        private void pictureBox3_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;

            if (null != ANon && 0 == op)
            {
                Plot.MakeSystem(g, grN);
                Function.Calc(ref ANon);
                Plot.MakeGraphic(g, grN, ANon, Pens.Blue);
                Plot.AlfaCut(g, grN, ANon, alfaN);
            }
            if (null != ANon && null != BNon && 0 != op)
            {
                switch (op)
                {
                    case OPERATION.SUM:
                        Operations.Sum(ANon, BNon, g, grN, alfaN);
                        break;
                    case OPERATION.SUB:
                        Operations.Sub(ANon, BNon, g, grN, alfaN);
                        break;
                    case OPERATION.MUL:
                        Operations.Mul(ANon, BNon, g, grN, alfaN);
                        break;
                    case OPERATION.DIV:
                        Operations.Div(ANon, BNon, g, grN, alfaN);
                        break;
                    case OPERATION.UNI:
                        Operations.Reu(ANon, BNon, g, grN);
                        break;
                    case OPERATION.POW:
                        Operations.Power(ANon, 3, g, grN);
                        break;
                }
            }
        }

        private void pictureBox4_Paint(object sender, PaintEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            Graphics g = e.Graphics;


            if (null != BNon)
            {
                Plot.MakeSystem(g, grN);
                Function.Calc(ref BNon);
                Plot.MakeGraphic(g, grN, BNon, Pens.Pink);
                Plot.AlfaCut(g, gr, BNon, alfaN);
                MotionGraphic(g);
            }
        }
        private void buttonSum_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            CountDown(this.pictureBox4.CreateGraphics());
            op = OPERATION.SUM;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonSub_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            CountDown(this.pictureBox4.CreateGraphics());
            op = OPERATION.SUB;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonMul_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            CountDown(this.pictureBox4.CreateGraphics());
            op = OPERATION.MUL;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);

        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            CountDown(this.pictureBox4.CreateGraphics());
            op = OPERATION.DIV;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonInt_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            CountDown(this.pictureBox4.CreateGraphics());
            op = OPERATION.INT;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonUni_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            CountDown(this.pictureBox4.CreateGraphics());
            op = OPERATION.UNI;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

 

        private void button2_Click(object sender, EventArgs e)
        {
            Motion();
            CountDown(this.pictureBox2.CreateGraphics());
            op = OPERATION.POW;
            button10.Enabled = true;
            buttonSwitch_Click(button10, null);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            pictureBox1.Location = pos1;
            pictureBox2.Location = pos2;
            op = 0;
            button10.Enabled = false;
            pictureBox2.BringToFront();
            isInFront = false;
            Refresh();
        }

        private void validateButtons(bool valid)
        {
            button3.Enabled = valid;
            button4.Enabled = valid;
            button5.Enabled = valid;
            button6.Enabled = valid;
            button7.Enabled = valid;
            button8.Enabled = valid;
            button9.Enabled = valid;
        }

        private void CountDown(Graphics g)
        {
            for (int i = 6; i > 0; --i)
            {
                g.FillPie(Brushes.White, gr.RenderW(gr.Max), gr.RenderH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 0, 90);
                g.FillPie(Brushes.Gray, gr.RenderW(gr.Max), gr.RenderH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 90, 90);
                g.FillPie(Brushes.White, gr.RenderW(gr.Max), gr.RenderH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 180, 90);
                g.FillPie(Brushes.Gray, gr.RenderW(gr.Max), gr.RenderH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 270, 90);
                g.DrawString(i.ToString(), new Font("Console", gr.Offset + 5), Brushes.Gold, gr.RenderW(gr.Max) - 5, gr.RenderH(1) - gr.Offset);
                for (int j = 0; j < 360; ++j)
                {
                    g.FillPie(Brushes.Aqua, gr.RenderW(gr.Max), gr.RenderH(1) - gr.Offset, gr.Offset, 2 * gr.Offset, 0, j);
                    
                }

                 g.FillPie(Brushes.White, grN.RenderW(grN.Max), grN.RenderH(1) - grN.Offset, grN.Offset, 2 * grN.Offset, 0, 90);
                 g.FillPie(Brushes.Gray, grN.RenderW(gr.Max), grN.RenderH(1) - grN.Offset, grN.Offset, 2 * grN.Offset, 90, 90);
                 g.FillPie(Brushes.White, grN.RenderW(gr.Max), grN.RenderH(1) - grN.Offset, grN.Offset, 2 * grN.Offset, 180, 90);
                 g.FillPie(Brushes.Gray, grN.RenderW(gr.Max), grN.RenderH(1) - grN.Offset, grN.Offset, 2 * grN.Offset, 270, 90);
                 g.DrawString(i.ToString(), new Font("Console", grN.Offset + 5), Brushes.Gold, grN.RenderW(gr.Max) - 5, grN.RenderH(1) - grN.Offset);
                 for (int j = 0; j < 360; ++j)
                 {
                    g.FillPie(Brushes.Aqua, grN.RenderW(grN.Max), grN.RenderH(1) - grN.Offset, grN.Offset, 2 * grN.Offset, 0, j);

                }
            }
        }

        private void MotionGraphic(Graphics g)
        {
            float m = pictureBox1.Location.X + pictureBox1.Width - pictureBox2.Location.X - gr.Offset;
            if (m > 0)
            {
                for (int i = gr.Max - 1; i > gr.Max - m / gr.WFactor && i > 1; --i)
                {
                    g.DrawLine(Pens.Green, gr.RenderW(i) - gr.RenderW(gr.Max) + m, gr.RenderH(A.vect[i]), gr.RenderW(i - 1) - gr.RenderW(gr.Max) + m, gr.RenderH(A.vect[i - 1]));
                }

                for (int i = grN.Max - 1; i > grN.Max - m / grN.WFactor && i > 1; --i)
                {
                    g.DrawLine(Pens.Green, grN.RenderW(i) - grN.RenderW(grN.Max) + m, grN.RenderH(A.vect[i]), grN.RenderW(i - 1) - grN.RenderW(grN.Max) + m, grN.RenderH(A.vect[i - 1]));
                }
            }
        }

        private void MotionDown(PictureBox b1, PictureBox b2)
        {
            while (b1.Location.X + size1.Width > b2.Location.X && b1.Location.Y < b2.Location.Y + size2.Height)
            {
                b1.Location = new Point(b1.Location.X - 1, b1.Location.Y + 1);
                b2.Location = new Point(b2.Location.X + 1, b2.Location.Y - 1);
                Refresh();
            }
            b1.BringToFront();
;
        }

        private void MotionDownNon(PictureBox b1, PictureBox b2)
        {
            while (b1.Location.X + size3.Width > b2.Location.X && b1.Location.Y < b2.Location.Y + size4.Height)
            {
                b1.Location = new Point(b1.Location.X - 1, b1.Location.Y + 1);
                b2.Location = new Point(b2.Location.X + 1, b2.Location.Y - 1);
                Refresh();
            }
            b1.BringToFront();
            ;
        }

        private void MotionUp(PictureBox b1, PictureBox b2)
        {
            while (b1.Location.X < pos2.X && b1.Location.Y > pos2.Y)
            {
                b1.Location = new Point(b1.Location.X + 1, b1.Location.Y - 1);
                b2.Location = new Point(b2.Location.X - 1, b2.Location.Y + 1);
                Refresh();
            }
            isInFront = !isInFront;
       
        }

        private void MotionUpNon(PictureBox b1, PictureBox b2)
        {
            while (b1.Location.X < pos4.X && b1.Location.Y > pos4.Y)
            {
                b1.Location = new Point(b1.Location.X + 1, b1.Location.Y - 1);
                b2.Location = new Point(b2.Location.X - 1, b2.Location.Y + 1);
                Refresh();
            }
            isInFront = !isInFront;

        }
        private void buttonSwitch_Click(object sender, EventArgs e)
        {
            if (isInFront) { 
                ((Control)sender).BeginInvoke(mySwitch, pictureBox2, pictureBox1);
                ((Control)sender).BeginInvoke(mySwitchN, pictureBox4, pictureBox3);

            }


            else { 
                ((Control)sender).BeginInvoke(mySwitch, pictureBox1, pictureBox2);
                ((Control)sender).BeginInvoke(mySwitchN, pictureBox3, pictureBox4);


                 //((Control)sender).BeginInvoke(mySwitch, pictureBox3, pictureBox4);
            }
        }

        private void textBoxAlphaN_TextChanged(object sender, EventArgs e)
        {

        }

    }

}

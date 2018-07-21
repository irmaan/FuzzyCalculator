using System;
using System.Drawing;

namespace Fuzzy
{
    class Plot
    {
        public static void VerticalLine(Render gr, Graphics g, float x, float y, Pen p)
        {
            g.DrawLine(p, gr.RenderW(x), gr.RenderH(0), gr.RenderW(x), gr.RenderH(y));
        }

        public static void HorizontalLine(Render gr, Graphics g, float x1, float x2, float y, Pen p)
        {
            g.DrawLine(p, gr.RenderW(x2), gr.RenderH(y), gr.RenderW(x1), gr.RenderH(y));
        }

        public static void MakeSystem(Graphics g, Render gr)
        {
            Plot.HorizontalLine(gr, g, gr.Min, gr.Max, 0, Pens.Black);
            Plot.VerticalLine(gr, g, 0, 1, Pens.Black);
            Font f = new Font("Tahoma", 10);
            g.DrawString("1", f, Brushes.Black, gr.RenderW(0) - 5, gr.RenderH(1) - 15);
            g.DrawString(gr.Min.ToString(), f, Brushes.Black, gr.RenderW(gr.Min) - 10, gr.RenderH(0));
            g.DrawString(gr.Max.ToString(), f, Brushes.Black, gr.RenderW(gr.Max) - 10, gr.RenderH(0));
        }

        public static void MakeGraphic(Graphics g, Render gr, FuzzyNumber N, Pen p)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            for (int i = 1; i < N.vect.Length; ++i)
            {
                g.DrawLine(p, gr.RenderW(i - 1), gr.RenderH(N.vect[i - 1]), gr.RenderW(i), gr.RenderH(N.vect[i]));
            }
            g.DrawString((N.C - N.A).ToString(), new Font("Tahoma", 10), Brushes.Black, gr.RenderW(N.C - N.A) - 10, gr.RenderH(0));
            g.DrawString((N.C + N.B).ToString(), new Font("Tahoma", 10), Brushes.Black, gr.RenderW(N.C + N.B) - 10, gr.RenderH(0));
        }

        public static void AlfaCut(Graphics g, Render gr, FuzzyNumber N, double alfa)
        {
            float x1 = 0, y1 = -1, x2 = 0, y2 = -1;
            for (int i = 0; i < N.vect.Length; ++i)
            {
                if (alfa < N.vect[i] && -1 == y1)
                {
                    x1 = i - 1;
                    y1 = N.vect[i - 1];
                }
                if (alfa > N.vect[i] && -1 != y1 && -1 == y2)
                {
                    x2 = i;
                    y2 = N.vect[i];
                }
            }
            HorizontalLine(gr, g, x1, x2, y1, Pens.Coral);
            g.DrawString(Math.Round(x1, 0).ToString(), new Font("Tahoma", 10), Brushes.Black, gr.RenderW(x1) - 25, gr.RenderH(y1));
            VerticalLine(gr, g, x1, y1, Pens.Coral);
            g.DrawString(Math.Round(x2, 0).ToString(), new Font("Tahoma", 10), Brushes.Black, gr.RenderW(x2) + 5, gr.RenderH(y2));
            VerticalLine(gr, g, x2, y1, Pens.Coral);
        }
    }
}

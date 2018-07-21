using System;
using System.Drawing;

namespace Fuzzy
{
    class Operations
    {
        public static FuzzyNumber Sum(FuzzyNumber A, FuzzyNumber B, Graphics g, Render gr, double alfa)
        {
            float ml, mr, nl, nr, ql, qr;
            bool draw = false, este = false;
            int i = 0;
            Render temp = null;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            FuzzyNumber C = null;
            while (i <= 50)
            {
                MN(out ml, out mr, out nl, out nr, A, B, i * 0.02f);

                ql = ml + nl;
                qr = mr + nr;

                if (!draw)
                {
                    temp = new Render(gr.WindowW, gr.WindowH, ql, qr, 1, gr.Offset);
                    C = new FuzzyNumber(A.C + B.C,ql,qr,gr.Max);
                    C.vect[i] = ql;
                    C.vect[99-i] = qr;
                    Plot.MakeSystem(g, temp);
                    draw = !draw;
                }
                else
                {
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(ql), temp.RenderH(i * 0.02f));
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(qr), temp.RenderH(i * 0.02f));
                    if (alfa < i * 0.02 && !este)
                    {
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH((i -1) * 0.02f), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[i - 1], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[i - 1]) - 25, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH(0), temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[100 - i], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[100 - i]) + 5, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[100 - i]), temp.RenderH(0), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        este = !este;
                    }
                }
                ++i;
            }
            return C;
        }

        public static FuzzyNumber Sub(FuzzyNumber A, FuzzyNumber B, Graphics g, Render gr, double alfa)
        {
            float ml, mr, nl, nr, ql, qr;
            bool draw = false, este = false;
            int i = 0;
            Render temp = null;
            FuzzyNumber C = null;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            while (i <= 50)
            {
                MN(out ml, out mr, out nl, out nr, A, B, i * 0.02f);

                ql = ml - nr;
                qr = mr - nl;

                if (!draw)
                {
                    temp = new Render(gr.WindowW, gr.WindowH, ql, qr, 1, gr.Offset);
                    C = new FuzzyNumber(A.C + B.C, ql, qr, gr.Max);
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    Plot.MakeSystem(g, temp);
                    draw = !draw;
                }
                else
                {
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(ql), temp.RenderH(i * 0.02f));
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(qr), temp.RenderH(i * 0.02f));
                    if (alfa < i * 0.02 && !este)
                    {
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[i - 1], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[i - 1]) - 25, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH(0), temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[100 - i], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[100 - i]) + 5, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[100 - i]), temp.RenderH(0), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        este = !este;
                    }
                }
                ++i;
            }
            return C;
        }

        public static FuzzyNumber Mul(FuzzyNumber A, FuzzyNumber B, Graphics g, Render gr, double alfa)
        {
            float ml, mr, nl, nr, ql, qr;
            bool draw = false, este = false;
            int i = 0;
            Render temp = null;
            FuzzyNumber C = null;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            while (i <= 50)
            {
                MN(out ml, out mr, out nl, out nr, A, B, i * 0.02f);

                ql = Math.Min(Math.Min(ml * nl, ml * nr), Math.Min(mr * nl, mr * nr));
                qr = Math.Max(Math.Max(ml * nl, ml * nr), Math.Max(mr * nl, mr * nr));

                if (!draw)
                {
                    temp = new Render(gr.WindowW, gr.WindowH, ql, qr, 1, gr.Offset);
                    C = new FuzzyNumber(A.C + B.C, ql, qr, gr.Max);
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    Plot.MakeSystem(g, temp);
                    draw = !draw;
                }
                else
                {
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(ql), temp.RenderH(i * 0.02f));
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(qr), temp.RenderH(i * 0.02f));
                    if (alfa < i * 0.02 && !este)
                    {
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[i - 1], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[i - 1]) - 25, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH(0), temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[100 - i], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[100 - i]) + 5, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[100 - i]), temp.RenderH(0), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        este = !este;
                    }
                }
                ++i;
            }
            return C;
        }

        public static FuzzyNumber Div(FuzzyNumber A, FuzzyNumber B, Graphics g, Render gr, double alfa)
        {
            float ml, mr, nl, nr, ql, qr;
            bool draw = false, este = false;
            int i = 0;
            Render temp = null;
            FuzzyNumber C = null;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            while (i <= 50)
            {
                MN(out ml, out mr, out nl, out nr, A, B, i * 0.02f);

                ql = Math.Min(Math.Min(ml / nl, ml / nr), Math.Min(mr / nl, mr / nr));
                qr = Math.Max(Math.Max(ml / nl, ml / nr), Math.Max(mr / nl, mr / nr));

                if (!draw)
                {
                    temp = new Render(gr.WindowW, gr.WindowH, ql, qr, 1, gr.Offset);
                    C = new FuzzyNumber(A.C + B.C, ql, qr, gr.Max);
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    Plot.MakeSystem(g, temp);
                    draw = !draw;
                }
                else
                {
                    C.vect[i] = ql;
                    C.vect[99 - i] = qr;
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(ql), temp.RenderH(i * 0.02f));
                    g.DrawLine(Pens.Aqua, temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(qr), temp.RenderH(i * 0.02f));
                    if (alfa < i * 0.02 && !este)
                    {
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[i - 1], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[i - 1]) - 25, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[i - 1]), temp.RenderH(0), temp.RenderW(C.vect[i - 1]), temp.RenderH((i - 1) * 0.02f));
                        g.DrawString(Math.Round(C.vect[100 - i], 1).ToString(), new Font("Tahoma", 10), Brushes.Black, temp.RenderW(C.vect[100 - i]) + 5, temp.RenderH((i - 1) * 0.02f));
                        g.DrawLine(Pens.YellowGreen, temp.RenderW(C.vect[100 - i]), temp.RenderH(0), temp.RenderW(C.vect[100 - i]), temp.RenderH((i - 1) * 0.02f));
                        este = !este;
                    }
                }
                ++i;
            }
            return C;
        }

        public static void Int(FuzzyNumber A, FuzzyNumber B, Graphics g, Render gr)
        {
            Plot.MakeSystem(g, gr);
            float[] vect = new float[gr.Max];
            vect[0] = Math.Max(A.vect[0], B.vect[0]);
            for (int i = 1; i < 100; ++i)
            {
                if (A.vect[i] < B.vect[i])
                {
                    vect[i] = A.vect[i];
                }
                else
                {
                    vect[i] = B.vect[i];
                }
                g.DrawLine(Pens.Aqua, gr.RenderW(i - 1), gr.RenderH(vect[i - 1]), gr.RenderW(i), gr.RenderH(vect[i]));
            }
        }

        public static void Reu(FuzzyNumber A, FuzzyNumber B, Graphics g, Render gr)
        {
            Plot.MakeSystem(g, gr);
            float[] vect = new float[gr.Max];
            vect[0] = Math.Max(A.vect[0], B.vect[0]);
            for (int i = 1; i < 100; ++i)
            {
                if (A.vect[i] > B.vect[i])
                {
                    vect[i] = A.vect[i];
                }
                else
                {
                    vect[i] = B.vect[i];
                }
                g.DrawLine(Pens.Aqua, gr.RenderW(i - 1), gr.RenderH(vect[i - 1]), gr.RenderW(i), gr.RenderH(vect[i]));
            }
        }



        public static void Power(FuzzyNumber A, double n, Graphics g, Render gr)
        {
            Plot.MakeSystem(g, gr);
            float[] vect = new float[gr.Max];
            vect[0] = (float)Math.Pow(A.vect[0],n);
            for (int i = 1; i < 100; ++i)
            {
                vect[i] = (float)Math.Pow(A.vect[i], n);
                g.DrawLine(Pens.Aqua, gr.RenderW(i - 1), gr.RenderH(vect[i - 1]), gr.RenderW(i), gr.RenderH(vect[i]));
            }
        }



        private static void MN(out float ml,out float mr,out float nl,out float nr, FuzzyNumber A, FuzzyNumber B, float a)
        {
            ml = (A.C - A.A) + A.A * a;
            mr = (A.C + A.B) - A.B * a;
            nl = (B.C - B.A) + B.A * a;
            nr = (B.C + B.B) - B.B * a;
        }
    }
}

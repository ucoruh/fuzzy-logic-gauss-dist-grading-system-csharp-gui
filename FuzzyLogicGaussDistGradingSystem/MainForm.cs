using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace FuzzyLogicGaussDistGradingSystem
{
    public partial class AnaFrm : Form
    {
        public AnaFrm()
        {
            InitializeComponent();
        }
        
        public int bir = 50;
        public int sifir = 270;
        public int kat = 5;

        string vizeDurum1 = null;
        double vizeDeger1 = 0;
        string vizeDurum2 = null;
        double vizeDeger2 = 0;
        string finalDurum1 = null;
        double finalDeger1 = 0;
        string finalDurum2 = null;
        double finalDeger2 = 0;


        Point[] vizeOP = new Point[4];
        Point[] vizeZI = new Point[4];
        Point[] finalOP = new Point[4];
        Point[] finalZI = new Point[4];

        string[] harfler = new string[] { "ZAYIF", "ORTA", "IYI", "PEKIYI" };

        int[] Vnotpozisyon = new int[] { 0, 10, 20, 30 };
        int[] Fnotpozisyon = new int[] { 0, 1, 2, 3 };

        #region  Not Grafiði için biraz daha dinamik deðiþkenler

        Point[] Fline = new Point[2];
        Point[] Cline = new Point[3];
        Point[] Bline = new Point[3];
        Point[] Aline = new Point[3];

        #endregion

        public Point Kesisim(Point line1A, Point line1B, Point line2A, Point line2B)
        {
            Point p = new Point();

            float x1 = (float)line1A.X;
            float y1 = (float)line1A.Y;

            float x2 = (float)line1B.X;
            float y2 = (float)line1B.Y;

            float x3 = (float)line2A.X;
            float y3 = (float)line2A.Y;

            float x4 = (float)line2B.X;
            float y4 = (float)line2B.Y;



            float tk = ((((y4 - y3) * (x3 - x1)) - ((y3 - y1) * (x4 - x3))) / (((y4 - y3) * (x2 - x1)) - ((y2 - y1) * (x4 - x3))));

            p.X = (int)(x1 + (x2 - x1) * tk);
            p.Y = (int)(y1 + (y2 - y1) * tk);



            return p;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tabControl1.SelectedIndex != -1)
                tabControl3.SelectedIndex = tabControl1.SelectedIndex;
            if (tabControl1.SelectedIndex == 0)
            {
                GraphVizeCiz();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                GraphFinalCiz();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                GraphNotCiz();
                Onayla();
                Doldur();
                Hesapla();
            }

            NotKesisimGoster();
           


        }

        private void tabControl3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl3.SelectedIndex != -1)
                tabControl1.SelectedIndex = tabControl3.SelectedIndex;
            if (tabControl1.SelectedIndex == 0)
            {
                GraphVizeCiz();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                GraphFinalCiz();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                GraphNotCiz();
                Onayla();
                Doldur();
                Hesapla();
              
            }

            NotKesisimGoster();
           
        }

        private void hScrollMinV_ValueChanged(object sender, EventArgs e)
        {
            if (hScrollMinV.Value >= hScrollMaxV.Value)
                hScrollMaxV.Value = hScrollMinV.Value + 1;

            GraphVizeCiz();
            NotKesisimGoster();




            lblmnV.Text = hScrollMinV.Value.ToString();


        }

        private void GraphVizeCiz()
        {
            AnaFrm.ActiveForm.Refresh();

            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);
            string s = null;


            vizeOP[0].X = 0;
            vizeOP[0].Y = sifir;

            vizeOP[1].X = hScrollMinV.Value * kat; //ORTA
            vizeOP[1].Y = bir;

            vizeOP[2].X = (hScrollMinV.Value * kat + hScrollMaxV.Value * kat) / 2;
            vizeOP[2].Y = sifir;

            vizeOP[3].X = hScrollMaxV.Value * kat;//PEK ÝYÝ
            vizeOP[3].Y = bir;

            ////////////////////////////////////////////////////////////

            vizeZI[0].X = 0;
            vizeZI[0].Y = bir;//ZA

            vizeZI[1].X = hScrollMinV.Value * kat;
            vizeZI[1].Y = sifir;

            vizeZI[2].X = (hScrollMinV.Value * kat + hScrollMaxV.Value * kat) / 2;
            vizeZI[2].Y = bir; //ÝYÝ

            vizeZI[3].X = hScrollMaxV.Value * kat;
            vizeZI[3].Y = sifir;

            Graphics grp = this.PanelGrafVize.CreateGraphics();

            grp.DrawLines(Pens.Red, vizeOP);
            grp.DrawLines(Pens.Black, vizeZI);

            s = "{ORTA}";
            grp.DrawString(s, this.Font, br, vizeOP[1].X, vizeOP[1].Y - 20);

            s = "{PEKÝYÝ}";
            grp.DrawString(s, this.Font, br, vizeOP[3].X, vizeOP[3].Y - 20);

            s = "{ZAYIF}";
            grp.DrawString(s, this.Font, br, vizeZI[0].X, vizeZI[0].Y - 20);

            s = "{ÝYÝ}";
            grp.DrawString(s, this.Font, br, vizeZI[2].X, vizeZI[2].Y - 20);

            ///////////////////////////////////////////////////////

            s = hScrollMinV.Value.ToString();
            grp.DrawString(s, this.Font, br, vizeOP[1].X, sifir);

            s = hScrollMaxV.Value.ToString();
            grp.DrawString(s, this.Font, br, vizeOP[3].X, sifir);

            s = Convert.ToString((hScrollMaxV.Value + hScrollMinV.Value) / 2);
            grp.DrawString(s, this.Font, br, vizeOP[2].X, sifir);


            ///////////////////////////////////////////////////////

            grp.DrawLine(Pens.Blue, new Point(2, 10), new Point(2, sifir));
            grp.DrawLine(Pens.Blue, new Point(2, sifir), new Point(500, sifir));


        }

        private void hScrollMaxV_ValueChanged(object sender, EventArgs e)
        {
            if (hScrollMinV.Value >= hScrollMaxV.Value)
                hScrollMaxV.Value = hScrollMinV.Value + 1;


            GraphVizeCiz();
            NotKesisimGoster();


            lblmaxV.Text = hScrollMaxV.Value.ToString();


        }

        private void hScrollMinF_ValueChanged(object sender, EventArgs e)
        {
            if (hScrollMinF.Value >= hScrollMaxF.Value)
                hScrollMaxF.Value = hScrollMinF.Value + 1;

            GraphFinalCiz();
            NotKesisimGoster();

            lblFmn.Text = hScrollMinF.Value.ToString();


        }

        private void GraphFinalCiz()
        {
            AnaFrm.ActiveForm.Refresh();

            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);
            string s = null;


            finalOP[0].X = 0;
            finalOP[0].Y = sifir;

            finalOP[1].X = hScrollMinF.Value * kat;
            finalOP[1].Y = bir;

            finalOP[2].X = (hScrollMinF.Value * kat + hScrollMaxF.Value * kat) / 2;
            finalOP[2].Y = sifir;

            finalOP[3].X = hScrollMaxF.Value * kat;
            finalOP[3].Y = bir;

            ////////////////////////////////////////////////////////////

            finalZI[0].X = 0;
            finalZI[0].Y = bir;

            finalZI[1].X = hScrollMinF.Value * kat;
            finalZI[1].Y = sifir;

            finalZI[2].X = (hScrollMinF.Value * kat + hScrollMaxF.Value * kat) / 2;
            finalZI[2].Y = bir;

            finalZI[3].X = hScrollMaxF.Value * kat;
            finalZI[3].Y = sifir;

            Graphics grp = this.panelGraphFinal.CreateGraphics();

            grp.DrawLines(Pens.Red, finalOP);
            grp.DrawLines(Pens.Black, finalZI);

            s = "{ORTA}";
            grp.DrawString(s, this.Font, br, finalOP[1].X, finalOP[1].Y - 20);

            s = "{PEKÝYÝ}";
            grp.DrawString(s, this.Font, br, finalOP[3].X, finalOP[3].Y - 20);

            s = "{ZAYIF}";
            grp.DrawString(s, this.Font, br, finalZI[0].X, finalZI[0].Y - 20);

            s = "{ÝYÝ}";
            grp.DrawString(s, this.Font, br, finalZI[2].X, finalZI[2].Y - 20);


            s = hScrollMinF.Value.ToString();
            grp.DrawString(s, this.Font, br, finalOP[1].X, sifir);

            s = hScrollMaxF.Value.ToString();
            grp.DrawString(s, this.Font, br, finalOP[3].X, sifir);

            s = Convert.ToString((hScrollMaxF.Value + hScrollMinF.Value) / 2);
            grp.DrawString(s, this.Font, br, finalOP[2].X, sifir);



            grp.DrawLine(Pens.Blue, new Point(2, 10), new Point(2, sifir));
            grp.DrawLine(Pens.Blue, new Point(2, sifir), new Point(500, sifir));

        }

        private void hScrollMaxF_ValueChanged(object sender, EventArgs e)
        {

            if (hScrollMinF.Value >= hScrollMaxF.Value)
                hScrollMaxF.Value = hScrollMinF.Value + 1;

            GraphFinalCiz();
            NotKesisimGoster();

            lblFmax.Text = hScrollMaxF.Value.ToString();
        }



        private void NotKesisimGoster()
        {
            int vizeNot = Convert.ToInt32(txtNotVize.Value);
            int finalNot = Convert.ToInt32(txtNotFinal.Value);

            Point vizeNokta = new Point(vizeNot * kat, sifir);
            Point vizeUstNokta = new Point(vizeNot * kat, bir);

            Point finalNokta = new Point(finalNot * kat, sifir);
            Point finalUstNokta = new Point(finalNot * kat, bir);

            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);
            string s = null;
            Graphics grph = this.PanelGrafVize.CreateGraphics();
            Graphics grphF = this.panelGraphFinal.CreateGraphics();

            if (tabControl1.SelectedIndex == 0)
            {
                for (int i = 0; i < vizeOP.Length - 1; i++)
                {
                    Point kesim = Kesisim(vizeOP[i], vizeOP[i + 1], vizeNokta, vizeUstNokta);


                    if (kesim.Y >= bir && kesim.Y <= sifir)
                    {
                        double aradeger = (bir - (double)kesim.Y) / Convert.ToDouble(bir - sifir);

                        aradeger = 1 - aradeger;

                        vizeDeger1 = aradeger;

                        string deger = Convert.ToString(aradeger);

                        if (deger.Length > 4)
                        {
                            deger = deger.Substring(0, 4);
                        }

                        if (kesim.X < ((hScrollMaxV.Value * kat + hScrollMinV.Value * kat) / 2))
                        {
                            vizeDurum1 = "ORTA";
                        }
                        else
                        {
                            vizeDurum1 = "PEKIYI";
                        }



                        s = "(" + deger + ")";

                        grph.DrawRectangle(Pens.Red, kesim.X, kesim.Y, 3, 3);

                        grph.DrawString(s, this.Font, br, 0, kesim.Y);

                        s = vizeNot.ToString();

                        grph.DrawString(s, this.Font, br, kesim.X, sifir);

                        grph.DrawLine(Pens.Purple, 0, kesim.Y, kesim.X, kesim.Y);
                        grph.DrawLine(Pens.Purple, kesim.X, kesim.Y, kesim.X, sifir);

                    }

                }
                //////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < vizeZI.Length - 1; i++)
                {
                    Point kesim = Kesisim(vizeZI[i], vizeZI[i + 1], vizeNokta, vizeUstNokta);


                    if (kesim.Y >= bir && kesim.Y <= sifir)
                    {
                        double aradeger = (bir - (double)kesim.Y) / Convert.ToDouble(bir - sifir);

                        aradeger = 1 - aradeger;

                        vizeDeger2 = aradeger;

                        string deger = Convert.ToString(aradeger);

                        if (deger.Length > 4)
                        {
                            deger = deger.Substring(0, 4);
                        }

                        if (kesim.X < hScrollMinV.Value * kat)
                        {
                            vizeDurum2 = "ZAYIF";
                        }
                        else
                        {
                            vizeDurum2 = "IYI";
                        }

                        s = "(" + deger + ")";

                        grph.DrawRectangle(Pens.Red, kesim.X, kesim.Y, 3, 3);

                        grph.DrawString(s, this.Font, br, 0, kesim.Y);

                        s = vizeNot.ToString();

                        grph.DrawString(s, this.Font, br, kesim.X, sifir);

                        grph.DrawLine(Pens.BlueViolet, 0, kesim.Y, kesim.X, kesim.Y);
                        grph.DrawLine(Pens.BlueViolet, kesim.X, kesim.Y, kesim.X, sifir);

                    }

                }
            }

            if (tabControl1.SelectedIndex == 1)
            {
                ///////////////////////Final Ýçin de aynýsýný yapacaðýz///////////////////////////////////////////
                for (int i = 0; i < finalOP.Length - 1; i++)
                {
                    Point kesim = Kesisim(finalOP[i], finalOP[i + 1], finalNokta, finalUstNokta);


                    if (kesim.Y >= bir && kesim.Y <= sifir)
                    {
                        double aradeger = (bir - (double)kesim.Y) / Convert.ToDouble(bir - sifir);

                        aradeger = 1 - aradeger;

                        finalDeger1 = aradeger;

                        string deger = Convert.ToString(aradeger);

                        if (deger.Length > 4)
                        {
                            deger = deger.Substring(0, 4);
                        }

                        if (kesim.X < ((hScrollMaxF.Value * kat + hScrollMinF.Value * kat) / 2))
                        {
                            finalDurum1 = "ORTA";
                        }
                        else
                        {
                            finalDurum1 = "PEKIYI";
                        }


                        s = "(" + deger + ")";

                        grphF.DrawRectangle(Pens.Red, kesim.X, kesim.Y, 3, 3);

                        grphF.DrawString(s, this.Font, br, 0, kesim.Y);

                        s = finalNot.ToString();

                        grphF.DrawString(s, this.Font, br, kesim.X, sifir);

                        grphF.DrawLine(Pens.BlueViolet, 0, kesim.Y, kesim.X, kesim.Y);
                        grphF.DrawLine(Pens.BlueViolet, kesim.X, kesim.Y, kesim.X, sifir);

                    }

                }
                /////////////////////////////////////////////////////////////////////////////////////
                for (int i = 0; i < finalZI.Length - 1; i++)
                {
                    Point kesim = Kesisim(finalZI[i], finalZI[i + 1], finalNokta, finalUstNokta);


                    if (kesim.Y >= bir && kesim.Y <= sifir)
                    {
                        double aradeger = (bir - (double)kesim.Y) / Convert.ToDouble(bir - sifir);

                        aradeger = 1 - aradeger;

                        finalDeger2 = aradeger;

                        string deger = Convert.ToString(aradeger);

                        if (deger.Length > 4)
                        {
                            deger = deger.Substring(0, 4);
                        }

                        if (kesim.X < hScrollMinF.Value * kat)
                        {
                            finalDurum2 = "ZAYIF";
                        }
                        else
                        {
                            finalDurum2 = "IYI";
                        }

                        s = "(" + deger + ")";

                        grphF.DrawRectangle(Pens.Red, kesim.X, kesim.Y, 3, 3);

                        grphF.DrawString(s, this.Font, br, 0, kesim.Y);

                        s = finalNot.ToString();

                        grphF.DrawString(s, this.Font, br, kesim.X, sifir);

                        grphF.DrawLine(Pens.BlueViolet, 0, kesim.Y, kesim.X, kesim.Y);
                        grphF.DrawLine(Pens.BlueViolet, kesim.X, kesim.Y, kesim.X, sifir);

                    }

                }
                /////////////////////////////////////////////////////////////////////////////////
            }

            lblfinaldurum.Text = finalDurum1 + "," + finalDurum2;
            lblvizedurum.Text = vizeDurum1 + "," + vizeDurum2;
        }

        private void txtNotVize_ValueChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 0)
                tabControl1.SelectedIndex = 0;

            if (tabControl1.SelectedIndex == 0)
            {
                GraphVizeCiz();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                GraphFinalCiz();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                GraphNotCiz();
                Onayla();
                Doldur();
                Hesapla();
              
            }

            NotKesisimGoster();
          
        }

        private void txtNotFinal_ValueChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex != 1)
                tabControl1.SelectedIndex = 1;

            if (tabControl1.SelectedIndex == 0)
            {
                GraphVizeCiz();
            }
            if (tabControl1.SelectedIndex == 1)
            {
                GraphFinalCiz();
            }
            if (tabControl1.SelectedIndex == 2)
            {
                GraphNotCiz();
                Onayla();
                Doldur();
                Hesapla();
            }

            NotKesisimGoster();
           
        }



        private void GraphNotCiz()
        {


            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);
            string s = null;


            Fline[0].X = 0;
            Fline[0].Y = bir;

            Fline[1].X = Convert.ToInt32(txtFmax.Value * kat);
            Fline[1].Y = sifir;

            Cline[0].X = Convert.ToInt32(txtCmin.Value * kat);
            Cline[0].Y = sifir;

            Cline[1].X = Convert.ToInt32((txtCmax.Value * kat + txtCmin.Value * kat) / 2);
            Cline[1].Y = bir;

            Cline[2].X = Convert.ToInt32(txtCmax.Value * kat);
            Cline[2].Y = sifir;

            Bline[0].X = Convert.ToInt32(txtBmin.Value * kat);
            Bline[0].Y = sifir;

            Bline[1].X = Convert.ToInt32((txtBmax.Value * kat + txtBmin.Value * kat) / 2);
            Bline[1].Y = bir;

            Bline[2].X = Convert.ToInt32(txtBmax.Value * kat);
            Bline[2].Y = sifir;

            Aline[0].X = Convert.ToInt32(txtAmin.Value * kat);
            Aline[0].Y = sifir;

            Aline[1].X = Convert.ToInt32((txtAmax.Value * kat + txtAmin.Value * kat) / 2);
            Aline[1].Y = bir;

            Aline[2].X = Convert.ToInt32(txtAmax.Value * kat);
            Aline[2].Y = sifir;


            AnaFrm.ActiveForm.Refresh();

            Graphics grp = this.panelGraphNot.CreateGraphics();

            grp.DrawLines(Pens.Red, Fline);
            grp.DrawLines(Pens.BurlyWood, Cline);
            grp.DrawLines(Pens.Chocolate, Bline);
            grp.DrawLines(Pens.Coral, Aline);



            s = "{F}";
            grp.DrawString(s, this.Font, br, Fline[0].X, Fline[0].Y - 20);

            s = "{C}";
            grp.DrawString(s, this.Font, br, Cline[1].X, Cline[1].Y - 20);

            s = "{B}";
            grp.DrawString(s, this.Font, br, Bline[1].X, Bline[1].Y - 20);

            s = "{A}";
            grp.DrawString(s, this.Font, br, Aline[1].X, Aline[1].Y - 20);


            s = txtCmin.Value.ToString();
            grp.DrawString(s, this.Font, br, Cline[0].X, sifir);

            s = txtCmax.Value.ToString();
            grp.DrawString(s, this.Font, br, Cline[2].X, sifir);

            s = txtFmax.Value.ToString();
            grp.DrawString(s, this.Font, br, Fline[1].X, sifir);

            s = txtBmin.Value.ToString();
            grp.DrawString(s, this.Font, br, Bline[0].X, sifir);

            s = txtBmax.Value.ToString();
            grp.DrawString(s, this.Font, br, Bline[2].X, sifir);

            s = txtAmin.Value.ToString();
            grp.DrawString(s, this.Font, br, Aline[0].X, sifir);

            s = txtAmax.Value.ToString();
            grp.DrawString(s, this.Font, br, Aline[2].X, sifir);




            grp.DrawLine(Pens.Blue, new Point(2, 10), new Point(2, sifir));
            grp.DrawLine(Pens.Blue, new Point(2, sifir), new Point(500, sifir));
        }

        private void txtFmax_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
           
        }

        private void txtCmin_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
          
        }

        private void txtCmax_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
          
        }

        private void txtBmin_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
          
        }

        private void txtBmax_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
           
        }

        private void txtAmin_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
           
        }

        private void txtAmax_ValueChanged(object sender, EventArgs e)
        {
            GraphNotCiz();
            Onayla();
            Doldur();
            Hesapla();
           
        }


        private void NotKesisim(Point[] cizgi, double deger, string tip)
        {

            double anaDeger = deger;

            deger = 1 - deger;

            deger = (sifir - bir) * deger;

            double gosterDeger = deger;



            int notDeger = Convert.ToInt32(gosterDeger);

            Point notNoktasi1 = new Point(500, notDeger);
            Point notNoktasi2 = new Point(0, notDeger);



            SolidBrush br = new SolidBrush(System.Drawing.SystemColors.ControlText);
            string s = null;
            Graphics grph = this.panelGraphNot.CreateGraphics();



            for (int i = 0; i < cizgi.Length - 1; i++)
            {
                Point kesim = Kesisim(cizgi[i], cizgi[i + 1], notNoktasi1, notNoktasi2);


                if (kesim.Y >= bir && kesim.Y <= sifir)
                {

                    grph.DrawRectangle(Pens.Red, kesim.X, kesim.Y, 3, 3);

                    s = Convert.ToString(anaDeger);

                    if (s.Length > 4)
                    {
                        s = s.Substring(0, 4);
                    }

                    grph.DrawString(s, this.Font, br, 0, kesim.Y);

                    s = Convert.ToString(kesim.X / kat);

                    if (s.Length > 4)
                    {
                        s = s.Substring(0, 4);
                    }

                    grph.DrawString(s, this.Font, br, kesim.X, sifir + 20);


                    grph.DrawLine(Pens.Purple, 0, kesim.Y, kesim.X, kesim.Y);
                    grph.DrawLine(Pens.Purple, kesim.X, kesim.Y, kesim.X, sifir);

                    listKesisimler.Items.Add(tip + " " + Convert.ToString(kesim.X / kat) + " " + kesim.X.ToString() + " " + kesim.Y.ToString() + " " + anaDeger.ToString());


                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;

        }

        private void Onayla()
        {
            listKesisimler.Items.Clear();

            foreach (Control dt in tabControl2.TabPages[0].Controls)
            {
                if (dt is TextBox)
                {
                    TextBox txtdf = (TextBox)dt;

                    txtdf.BackColor = Color.White;

                }
            }

            int ind1 = 0;
            double deger1 = 0;
            int ind2 = 0;
            double deger2 = 0;
            int ind3 = 0;
            double deger3 = 0;
            int ind4 = 0;
            double deger4 = 0;

            string harfNot1 = "";
            string harfNot2 = "";
            string harfNot3 = "";
            string harfNot4 = "";


            for (int i = 0; i < harfler.Length; i++)
            {
                if (vizeDurum1 == harfler[i])
                {
                    ind1 = i;
                    break;
                }
            }
            for (int i = 0; i < harfler.Length; i++)
            {
                if (vizeDurum2 == harfler[i])
                {
                    ind2 = i;
                    break;
                }
            }
            for (int i = 0; i < harfler.Length; i++)
            {
                if (finalDurum1 == harfler[i])
                {
                    ind3 = i;
                    break;
                }
            }
            for (int i = 0; i < harfler.Length; i++)
            {
                if (finalDurum2 == harfler[i])
                {
                    ind4 = i;
                    break;
                }
            }

            int noktaBir = Vnotpozisyon[ind1] + Fnotpozisyon[ind3];

            if (vizeDeger1 > finalDeger1)
            {
                deger1 = finalDeger1;
            }
            if (vizeDeger1 <= finalDeger1)
            {
                deger1 = vizeDeger1;
            }

            int noktaIki = Vnotpozisyon[ind1] + Fnotpozisyon[ind4];

            if (vizeDeger1 > finalDeger2)
            {
                deger2 = finalDeger2;
            }
            if (vizeDeger1 <= finalDeger2)
            {
                deger2 = vizeDeger1;
            }

            int noktaUc = Vnotpozisyon[ind2] + Fnotpozisyon[ind3];

            if (vizeDeger2 > finalDeger1)
            {
                deger3 = finalDeger1;
            }
            if (vizeDeger2 <= finalDeger1)
            {
                deger3 = vizeDeger2;
            }

            int noktaDort = Vnotpozisyon[ind2] + Fnotpozisyon[ind4];

            if (vizeDeger2 > finalDeger2)
            {
                deger4 = finalDeger2;
            }
            if (vizeDeger2 <= finalDeger2)
            {
                deger4 = vizeDeger2;
            }


            string Snokta1 = noktaBir.ToString();
            string Snokta2 = noktaIki.ToString();
            string Snokta3 = noktaUc.ToString();
            string Snokta4 = noktaDort.ToString();

            if (Snokta1.Length == 1)
            {
                Snokta1 = "0" + Snokta1;
            }
            if (Snokta2.Length == 1)
            {
                Snokta2 = "0" + Snokta2;
            }
            if (Snokta3.Length == 1)
            {
                Snokta3 = "0" + Snokta3;
            }
            if (Snokta4.Length == 1)
            {
                Snokta4 = "0" + Snokta4;
            }





            foreach (Control conn in tabControl2.TabPages[0].Controls)
            {
                if (conn is TextBox)
                {
                    TextBox txt = (TextBox)conn;

                    if (txt.Name == "txt" + Snokta1)
                    {
                        string durum = txt.Text;
                        if (durum != "")
                        {
                            harfNot1 = txt.Text;
                            txt.BackColor = Color.Pink;
                        }
                        else
                        {

                            harfNot1 = "";
                        }
                    }
                    else if (txt.Name == "txt" + Snokta2)
                    {
                        string durum = txt.Text;
                        if (durum != "")
                        {
                            harfNot2 = txt.Text;
                            txt.BackColor = Color.Pink;
                        }
                        else
                        {
                            harfNot2 = "";
                        }
                    }
                    else if (txt.Name == "txt" + Snokta3)
                    {
                        string durum = txt.Text;
                        if (durum != "")
                        {
                            harfNot3 = txt.Text;
                            txt.BackColor = Color.Pink;
                        }
                        else
                        {
                            harfNot3 = "";
                        }
                    }
                    else if (txt.Name == "txt" + Snokta4)
                    {
                        string durum = txt.Text;
                        if (durum != "")
                        {
                            harfNot4 = txt.Text;
                            txt.BackColor = Color.Pink;
                        }
                        else
                        {
                            harfNot4 = "";
                        }
                    }
                }
            }

            ////////////////////////////////////////////////////////////////

            switch (harfNot1)
            {
                case "A": NotKesisim(Aline, deger1, "A"); break;
                case "B": NotKesisim(Bline, deger1, "B"); break;
                case "C": NotKesisim(Cline, deger1, "C"); break;
                case "F": NotKesisim(Fline, deger1, "F"); break;

            }

            switch (harfNot2)
            {
                case "A": NotKesisim(Aline, deger2, "A"); break;
                case "B": NotKesisim(Bline, deger2, "B"); break;
                case "C": NotKesisim(Cline, deger2, "C"); break;
                case "F": NotKesisim(Fline, deger2, "F"); break;

            }

            switch (harfNot3)
            {
                case "A": NotKesisim(Aline, deger3, "A"); break;
                case "B": NotKesisim(Bline, deger3, "B"); break;
                case "C": NotKesisim(Cline, deger3, "C"); break;
                case "F": NotKesisim(Fline, deger3, "F"); break;

            }

            switch (harfNot4)
            {
                case "A": NotKesisim(Aline, deger4, "A"); break;
                case "B": NotKesisim(Bline, deger4, "B"); break;
                case "C": NotKesisim(Cline, deger4, "C"); break;
                case "F": NotKesisim(Fline, deger4, "F"); break;

            }
        }


        public Point[] noktaDon(ArrayList liste)
        {
            Point[] noktalar = new Point[liste.Count];

            for (int i = 0; i < liste.Count; i++)
            {
                noktalar[i] = (Point)liste[i];

            }

            return noktalar;
        }

        public void bosalt(ArrayList dizi)
        {
            for (int i = dizi.Count - 1; i >= 0; i--)
            {
                dizi.RemoveAt(i);
            }
        }

       
        private void Doldur()
        {
            listAgirlik.Items.Clear();

            ArrayList Akesim = new ArrayList();
            ArrayList Bkesim = new ArrayList();
            ArrayList Ckesim = new ArrayList();
            ArrayList Fkesim = new ArrayList();

            Graphics grph = this.panelGraphNot.CreateGraphics();

            for (int i = 0; i < listKesisimler.Items.Count; i++)
            {
                string[] tempS = listKesisimler.Items[i].ToString().Split(' ');

                if (tempS[0] == "A")
                {
                    bosalt(Akesim);

                    Akesim.Add(Aline[0]);
                    Akesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    i++;
                    if (i >= listKesisimler.Items.Count)
                        break;
                    tempS = listKesisimler.Items[i].ToString().Split(' ');
                    Akesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    Akesim.Add(Aline[2]);

                    Point[] noktalar = noktaDon(Akesim);

                    grph.FillPolygon(Brushes.Green, noktalar, System.Drawing.Drawing2D.FillMode.Winding);

                    //Alaný hesaplayýp aðýrlýk merkezi ile birlikte listbox a yazdýracaðýz

                    int Ataban = Aline[2].X - Aline[0].X;
                    int AtabanKesim = noktalar[3].X - noktalar[1].X;

                    int Ayukseklik = sifir - bir;

                    double Alan = (Ataban * Ayukseklik / 2) * (Math.Sqrt(Ataban) - Math.Sqrt(AtabanKesim)) / (Math.Sqrt(Ataban));

                    int Agirlik = (Aline[0].X + Ataban / 2) / kat;

                    listAgirlik.Items.Add(Agirlik.ToString() + " " + Alan.ToString());




                }
                if (tempS[0] == "B")
                {
                    bosalt(Bkesim);

                    Bkesim.Add(Bline[0]);
                    Bkesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    i++;
                    if (i >= listKesisimler.Items.Count)
                        break;
                    tempS = listKesisimler.Items[i].ToString().Split(' ');
                    Bkesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    Bkesim.Add(Bline[2]);

                    Point[] noktalar = noktaDon(Bkesim);

                    grph.FillPolygon(Brushes.Yellow, noktalar, System.Drawing.Drawing2D.FillMode.Winding);

                    int Btaban = Bline[2].X - Bline[0].X;
                    int BtabanKesim = noktalar[3].X - noktalar[1].X;

                    int Byukseklik = sifir - bir;

                    double Alan = (Btaban * Byukseklik / 2) * (Math.Sqrt(Btaban) - Math.Sqrt(BtabanKesim)) / (Math.Sqrt(Btaban));

                    int Agirlik = (Bline[0].X + Btaban / 2) / kat;

                    listAgirlik.Items.Add(Agirlik.ToString() + " " + Alan.ToString());

                }
                if (tempS[0] == "C")
                {
                    bosalt(Ckesim);

                    Ckesim.Add(Cline[0]);
                    Ckesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    i++;
                    if (i >= listKesisimler.Items.Count)
                        break;
                    tempS = listKesisimler.Items[i].ToString().Split(' ');
                    Ckesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    Ckesim.Add(Cline[2]);

                    Point[] noktalar = noktaDon(Ckesim);

                    grph.FillPolygon(Brushes.LightSalmon, noktalar, System.Drawing.Drawing2D.FillMode.Winding);

                    int Ctaban = Cline[2].X - Cline[0].X;
                    int CtabanKesim = noktalar[3].X - noktalar[1].X;

                    int Cyukseklik = sifir - bir;

                    double Alan = (Ctaban * Cyukseklik / 2) * (Math.Sqrt(Ctaban) - Math.Sqrt(CtabanKesim)) / (Math.Sqrt(Ctaban));

                    int Agirlik = (Cline[0].X + Ctaban / 2) / kat;

                    listAgirlik.Items.Add(Agirlik.ToString() + " " + Alan.ToString());

                }
                if (tempS[0] == "F")
                {
                    bosalt(Fkesim);

                    Fkesim.Add(new Point(0, sifir));
                    Fkesim.Add(new Point(0,Convert.ToInt32(tempS[3])));      
                    Fkesim.Add(new Point(Convert.ToInt32(tempS[2]), Convert.ToInt32(tempS[3])));
                    Fkesim.Add(Fline[1]);

                    Point[] noktalar = noktaDon(Fkesim);

                    grph.FillPolygon(Brushes.Red, noktalar, System.Drawing.Drawing2D.FillMode.Winding);

                    int Ftaban = Fline[1].X;
                    int FtabanKesim = noktalar[3].X - noktalar[1].X;

                    int Fyukseklik = sifir - bir;

                    double Alan = (Ftaban * Fyukseklik / 2) * (Math.Sqrt(Ftaban) - Math.Sqrt(FtabanKesim)) / (Math.Sqrt(Ftaban));

                    int Agirlik = (Fline[0].X + Ftaban / 2) / kat;

                    listAgirlik.Items.Add(Agirlik.ToString() + " " + Alan.ToString());

                }
            }
        }

        private void btnHesapla_Click(object sender, EventArgs e)
        {
            Hesapla();
        }

        private void Hesapla()
        {
            double ToplamMoment = 0;
            double ToplamAlan = 0;

            for (int i = 0; i < listAgirlik.Items.Count; i++)
            {
                string[] temp = listAgirlik.Items[i].ToString().Split(' ');
                double agirlik = Convert.ToDouble(temp[0]);
                double alan = Convert.ToDouble(temp[1]);
                ToplamMoment = ToplamMoment + (agirlik * alan);
                ToplamAlan = ToplamAlan + alan;
            }

            if (ToplamAlan > 0)
            {
                double sonuc = ToplamMoment / ToplamAlan;
                labelSonuc.Text = "GN: " + sonuc.ToString();

            }
        }

        private void hScrollMinV_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void AnaFrm_Load(object sender, EventArgs e)
        {

        }
    }
}
namespace practicaPowerAPI
{
    partial class fmGrafica
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lbPreuMhWAvui = new System.Windows.Forms.Label();
            this.chPreuMwHDurantAvui = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.gpTotal = new System.Windows.Forms.GroupBox();
            this.chGeneracioElectrica = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cbAnysGrafica = new System.Windows.Forms.ComboBox();
            this.lbPreuAraMateix = new System.Windows.Forms.Label();
            this.lbPreuMitja = new System.Windows.Forms.Label();
            this.pbGIFEspera = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.chPreuMwHDurantAvui)).BeginInit();
            this.gpTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chGeneracioElectrica)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGIFEspera)).BeginInit();
            this.SuspendLayout();
            // 
            // lbPreuMhWAvui
            // 
            this.lbPreuMhWAvui.AutoSize = true;
            this.lbPreuMhWAvui.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPreuMhWAvui.Location = new System.Drawing.Point(17, 28);
            this.lbPreuMhWAvui.Name = "lbPreuMhWAvui";
            this.lbPreuMhWAvui.Size = new System.Drawing.Size(298, 25);
            this.lbPreuMhWAvui.TabIndex = 0;
            this.lbPreuMhWAvui.Text = "Preu del Mwh durant el dia d\'avui";
            // 
            // chPreuMwHDurantAvui
            // 
            chartArea1.Name = "PreuElectricitat";
            this.chPreuMwHDurantAvui.ChartAreas.Add(chartArea1);
            this.chPreuMwHDurantAvui.Location = new System.Drawing.Point(22, 70);
            this.chPreuMwHDurantAvui.Name = "chPreuMwHDurantAvui";
            series1.ChartArea = "PreuElectricitat";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Point;
            series1.Name = "PreuLlum";
            this.chPreuMwHDurantAvui.Series.Add(series1);
            this.chPreuMwHDurantAvui.Size = new System.Drawing.Size(685, 304);
            this.chPreuMwHDurantAvui.TabIndex = 1;
            this.chPreuMwHDurantAvui.Text = "chart1";
            // 
            // gpTotal
            // 
            this.gpTotal.Controls.Add(this.chGeneracioElectrica);
            this.gpTotal.Controls.Add(this.cbAnysGrafica);
            this.gpTotal.Controls.Add(this.lbPreuAraMateix);
            this.gpTotal.Controls.Add(this.lbPreuMitja);
            this.gpTotal.Controls.Add(this.chPreuMwHDurantAvui);
            this.gpTotal.Controls.Add(this.lbPreuMhWAvui);
            this.gpTotal.Location = new System.Drawing.Point(26, 12);
            this.gpTotal.Name = "gpTotal";
            this.gpTotal.Size = new System.Drawing.Size(1319, 904);
            this.gpTotal.TabIndex = 2;
            this.gpTotal.TabStop = false;
            // 
            // chGeneracioElectrica
            // 
            chartArea2.Name = "ChartArea1";
            this.chGeneracioElectrica.ChartAreas.Add(chartArea2);
            legend1.Name = "Legend1";
            this.chGeneracioElectrica.Legends.Add(legend1);
            this.chGeneracioElectrica.Location = new System.Drawing.Point(22, 452);
            this.chGeneracioElectrica.Name = "chGeneracioElectrica";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.StackedColumn100;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chGeneracioElectrica.Series.Add(series2);
            this.chGeneracioElectrica.Size = new System.Drawing.Size(1342, 419);
            this.chGeneracioElectrica.TabIndex = 5;
            this.chGeneracioElectrica.Text = "chart1";
            // 
            // cbAnysGrafica
            // 
            this.cbAnysGrafica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbAnysGrafica.FormattingEnabled = true;
            this.cbAnysGrafica.Items.AddRange(new object[] {
            "2007",
            "2008",
            "2009",
            "2010",
            "2011",
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017",
            "2018",
            "2019",
            "2020",
            "2021"});
            this.cbAnysGrafica.Location = new System.Drawing.Point(22, 402);
            this.cbAnysGrafica.Name = "cbAnysGrafica";
            this.cbAnysGrafica.Size = new System.Drawing.Size(121, 24);
            this.cbAnysGrafica.TabIndex = 4;
            this.cbAnysGrafica.Tag = "";
            this.cbAnysGrafica.SelectedIndexChanged += new System.EventHandler(this.cbAnysGrafica_SelectedIndexChanged);
            // 
            // lbPreuAraMateix
            // 
            this.lbPreuAraMateix.AutoSize = true;
            this.lbPreuAraMateix.Location = new System.Drawing.Point(751, 141);
            this.lbPreuAraMateix.Name = "lbPreuAraMateix";
            this.lbPreuAraMateix.Size = new System.Drawing.Size(44, 16);
            this.lbPreuAraMateix.TabIndex = 3;
            this.lbPreuAraMateix.Text = "label1";
            // 
            // lbPreuMitja
            // 
            this.lbPreuMitja.AutoSize = true;
            this.lbPreuMitja.Location = new System.Drawing.Point(751, 91);
            this.lbPreuMitja.Name = "lbPreuMitja";
            this.lbPreuMitja.Size = new System.Drawing.Size(44, 16);
            this.lbPreuMitja.TabIndex = 2;
            this.lbPreuMitja.Text = "label1";
            // 
            // pbGIFEspera
            // 
            this.pbGIFEspera.Image = global::practicaPowerAPI.Properties.Resources.wait;
            this.pbGIFEspera.Location = new System.Drawing.Point(1334, 286);
            this.pbGIFEspera.Name = "pbGIFEspera";
            this.pbGIFEspera.Size = new System.Drawing.Size(56, 55);
            this.pbGIFEspera.TabIndex = 3;
            this.pbGIFEspera.TabStop = false;
            this.pbGIFEspera.Visible = false;
            // 
            // fmGrafica
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1402, 940);
            this.Controls.Add(this.pbGIFEspera);
            this.Controls.Add(this.gpTotal);
            this.KeyPreview = true;
            this.Name = "fmGrafica";
            this.Text = "PowerAPI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.fmGrafica_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chPreuMwHDurantAvui)).EndInit();
            this.gpTotal.ResumeLayout(false);
            this.gpTotal.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chGeneracioElectrica)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGIFEspera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbPreuMhWAvui;
        private System.Windows.Forms.DataVisualization.Charting.Chart chPreuMwHDurantAvui;
        private System.Windows.Forms.GroupBox gpTotal;
        private System.Windows.Forms.PictureBox pbGIFEspera;
        private System.Windows.Forms.Label lbPreuMitja;
        private System.Windows.Forms.Label lbPreuAraMateix;
        private System.Windows.Forms.ComboBox cbAnysGrafica;
        private System.Windows.Forms.DataVisualization.Charting.Chart chGeneracioElectrica;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}


namespace Gtp.Global.ICM.Ui
{
    partial class BasketOrders
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.efg = new System.Windows.Forms.RadioButton();
            this.kie = new System.Windows.Forms.RadioButton();
            this.gunluk = new System.Windows.Forms.RadioButton();
            this.denge = new System.Windows.Forms.RadioButton();
            this.piyasadanlimite = new System.Windows.Forms.RadioButton();
            this.piyasa = new System.Windows.Forms.RadioButton();
            this.musteri = new Gtp.Framework.ControlLibrary.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musteri.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.simpleButton1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.musteri);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(506, 93);
            this.panel1.TabIndex = 0;
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(375, 20);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(119, 37);
            this.simpleButton1.TabIndex = 23;
            this.simpleButton1.Text = "Toplu Giriş \r\n( .csv Dosyasından )";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.denge);
            this.groupBox1.Controls.Add(this.piyasadanlimite);
            this.groupBox1.Controls.Add(this.piyasa);
            this.groupBox1.Location = new System.Drawing.Point(141, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(228, 75);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Info;
            this.groupBox2.Controls.Add(this.efg);
            this.groupBox2.Controls.Add(this.kie);
            this.groupBox2.Controls.Add(this.gunluk);
            this.groupBox2.Location = new System.Drawing.Point(123, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(97, 61);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Visible = false;
            // 
            // efg
            // 
            this.efg.AutoSize = true;
            this.efg.Location = new System.Drawing.Point(7, 42);
            this.efg.Name = "efg";
            this.efg.Size = new System.Drawing.Size(44, 17);
            this.efg.TabIndex = 3;
            this.efg.TabStop = true;
            this.efg.Text = "EFG";
            this.efg.UseVisualStyleBackColor = true;
            // 
            // kie
            // 
            this.kie.AutoSize = true;
            this.kie.Location = new System.Drawing.Point(7, 26);
            this.kie.Name = "kie";
            this.kie.Size = new System.Drawing.Size(41, 17);
            this.kie.TabIndex = 2;
            this.kie.TabStop = true;
            this.kie.Text = "KİE";
            this.kie.UseVisualStyleBackColor = true;
            // 
            // gunluk
            // 
            this.gunluk.AutoSize = true;
            this.gunluk.Location = new System.Drawing.Point(7, 10);
            this.gunluk.Name = "gunluk";
            this.gunluk.Size = new System.Drawing.Size(57, 17);
            this.gunluk.TabIndex = 1;
            this.gunluk.TabStop = true;
            this.gunluk.Text = "Günlük";
            this.gunluk.UseVisualStyleBackColor = true;
            // 
            // denge
            // 
            this.denge.AutoSize = true;
            this.denge.Location = new System.Drawing.Point(7, 49);
            this.denge.Name = "denge";
            this.denge.Size = new System.Drawing.Size(56, 17);
            this.denge.TabIndex = 2;
            this.denge.TabStop = true;
            this.denge.Text = "Denge";
            this.denge.UseVisualStyleBackColor = true;
            this.denge.CheckedChanged += new System.EventHandler(this.denge_CheckedChanged);
            // 
            // piyasadanlimite
            // 
            this.piyasadanlimite.AutoSize = true;
            this.piyasadanlimite.Location = new System.Drawing.Point(7, 32);
            this.piyasadanlimite.Name = "piyasadanlimite";
            this.piyasadanlimite.Size = new System.Drawing.Size(104, 17);
            this.piyasadanlimite.TabIndex = 1;
            this.piyasadanlimite.TabStop = true;
            this.piyasadanlimite.Text = "Piyasadan Limite";
            this.piyasadanlimite.UseVisualStyleBackColor = true;
            this.piyasadanlimite.CheckedChanged += new System.EventHandler(this.piyasadanlimite_CheckedChanged);
            // 
            // piyasa
            // 
            this.piyasa.AutoSize = true;
            this.piyasa.Location = new System.Drawing.Point(7, 14);
            this.piyasa.Name = "piyasa";
            this.piyasa.Size = new System.Drawing.Size(56, 17);
            this.piyasa.TabIndex = 0;
            this.piyasa.TabStop = true;
            this.piyasa.Text = "Piyasa";
            this.piyasa.UseVisualStyleBackColor = true;
            this.piyasa.CheckedChanged += new System.EventHandler(this.piyasa_CheckedChanged);
            // 
            // musteri
            // 
            this.musteri.Location = new System.Drawing.Point(12, 30);
            this.musteri.Name = "musteri";
            this.musteri.Properties.Appearance.BackColor = System.Drawing.Color.White;
            this.musteri.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(31)))), ((int)(((byte)(53)))));
            this.musteri.Properties.Appearance.Options.UseBackColor = true;
            this.musteri.Properties.Appearance.Options.UseForeColor = true;
            this.musteri.Properties.AppearanceDisabled.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.musteri.Properties.AppearanceDisabled.Options.UseBackColor = true;
            this.musteri.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.musteri.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.musteri.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.musteri.Properties.ButtonsStyle = DevExpress.XtraEditors.Controls.BorderStyles.Flat;
            this.musteri.Properties.DropDownRows = 15;
            this.musteri.Properties.NullText = "";
            this.musteri.Size = new System.Drawing.Size(123, 22);
            this.musteri.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 20;
            this.label1.Text = "Müşteri";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.simpleButton2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 358);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(506, 76);
            this.panel2.TabIndex = 1;
            // 
            // simpleButton2
            // 
            this.simpleButton2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButton2.Appearance.Options.UseFont = true;
            this.simpleButton2.Location = new System.Drawing.Point(194, 18);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(131, 42);
            this.simpleButton2.TabIndex = 24;
            this.simpleButton2.Text = "Aktarım Yap";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.gridControl1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 93);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(506, 265);
            this.panel3.TabIndex = 2;
            // 
            // gridControl1
            // 
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(506, 265);
            this.gridControl1.TabIndex = 14;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.CustomDrawCell += new DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventHandler(this.gridView1_CustomDrawCell);
            this.gridView1.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(145, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 14);
            this.label2.TabIndex = 24;
            this.label2.Text = "İşlem yapılıyor , lütfen bekleyiniz . . .";
            this.label2.Visible = false;
            // 
            // BasketOrders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 434);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "BasketOrders";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Basket Orders";
            this.Load += new System.EventHandler(this.BasketOrders_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musteri.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public Framework.ControlLibrary.LookUpEdit musteri;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton denge;
        private System.Windows.Forms.RadioButton piyasadanlimite;
        private System.Windows.Forms.RadioButton piyasa;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton efg;
        private System.Windows.Forms.RadioButton kie;
        private System.Windows.Forms.RadioButton gunluk;
        private System.Windows.Forms.Label label2;
    }
}
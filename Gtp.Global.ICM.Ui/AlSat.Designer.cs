namespace Gtp.Global.ICM.Ui
{
    partial class AlSat
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
            this.panel1 = new Gtp.Framework.ControlLibrary.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tip = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label5 = new System.Windows.Forms.Label();
            this.gecerlilik = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.lot = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.fiyat = new DevExpress.XtraEditors.ComboBoxEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.menkul = new DevExpress.XtraEditors.ComboBoxEdit();
            this.musteri = new Gtp.Framework.ControlLibrary.LookUpEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tip.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gecerlilik.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lot.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fiyat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menkul.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musteri.Properties)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(242, 212);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.tip);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.gecerlilik);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lot);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.fiyat);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.menkul);
            this.panel3.Controls.Add(this.musteri);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 24);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(242, 151);
            this.panel3.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(6, 127);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 29;
            this.label6.Text = "Tip";
            // 
            // tip
            // 
            this.tip.EditValue = "Limit";
            this.tip.Location = new System.Drawing.Point(67, 123);
            this.tip.Name = "tip";
            this.tip.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tip.Properties.Appearance.Options.UseFont = true;
            this.tip.Properties.Appearance.Options.UseTextOptions = true;
            this.tip.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.tip.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.tip.Properties.Items.AddRange(new object[] {
            "Limit",
            "Piyasa",
            "Piyasadan Limite",
            "Denge"});
            this.tip.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.tip.Size = new System.Drawing.Size(120, 21);
            this.tip.TabIndex = 28;
            this.tip.EditValueChanged += new System.EventHandler(this.tip_EditValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(6, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 27;
            this.label5.Text = "Geçerlilik";
            // 
            // gecerlilik
            // 
            this.gecerlilik.EditValue = "Gün";
            this.gecerlilik.Location = new System.Drawing.Point(67, 99);
            this.gecerlilik.Name = "gecerlilik";
            this.gecerlilik.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.gecerlilik.Properties.Appearance.Options.UseFont = true;
            this.gecerlilik.Properties.Appearance.Options.UseTextOptions = true;
            this.gecerlilik.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.gecerlilik.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gecerlilik.Properties.Items.AddRange(new object[] {
            "Gün",
            "KİE"});
            this.gecerlilik.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.gecerlilik.Size = new System.Drawing.Size(59, 21);
            this.gecerlilik.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Lot";
            // 
            // lot
            // 
            this.lot.Location = new System.Drawing.Point(67, 53);
            this.lot.Name = "lot";
            this.lot.Properties.Appearance.Options.UseTextOptions = true;
            this.lot.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lot.Size = new System.Drawing.Size(99, 20);
            this.lot.TabIndex = 21;
            this.lot.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lot_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Fiyat";
            // 
            // fiyat
            // 
            this.fiyat.Location = new System.Drawing.Point(67, 76);
            this.fiyat.Name = "fiyat";
            this.fiyat.Properties.Appearance.Options.UseTextOptions = true;
            this.fiyat.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.fiyat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.fiyat.Size = new System.Drawing.Size(99, 20);
            this.fiyat.TabIndex = 22;
            this.fiyat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.fiyat_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Hisseler";
            // 
            // menkul
            // 
            this.menkul.Location = new System.Drawing.Point(67, 29);
            this.menkul.Name = "menkul";
            this.menkul.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.menkul.Properties.Appearance.Options.UseFont = true;
            this.menkul.Properties.Appearance.Options.UseTextOptions = true;
            this.menkul.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.menkul.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.menkul.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.menkul.Size = new System.Drawing.Size(99, 21);
            this.menkul.TabIndex = 20;
            this.menkul.Leave += new System.EventHandler(this.menkul_Leave);
            // 
            // musteri
            // 
            this.musteri.Location = new System.Drawing.Point(67, 4);
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
            this.musteri.Size = new System.Drawing.Size(151, 22);
            this.musteri.TabIndex = 19;
            this.musteri.EditValueChanged += new System.EventHandler(this.musteri_EditValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Reference Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 15);
            this.label1.TabIndex = 18;
            this.label1.Text = "Müşteri";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.Window;
            this.panel2.Controls.Add(this.simpleButton3);
            this.panel2.Controls.Add(this.simpleButton1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(242, 37);
            this.panel2.TabIndex = 3;
            // 
            // simpleButton3
            // 
            this.simpleButton3.Location = new System.Drawing.Point(141, 6);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(60, 25);
            this.simpleButton3.TabIndex = 2;
            this.simpleButton3.Text = "Çıkış";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.Location = new System.Drawing.Point(39, 6);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(74, 25);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "ONAY";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabControl1.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tabControl1.HotTrack = true;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(242, 24);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Click += new System.EventHandler(this.tabControl1_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.DarkRed;
            this.tabPage1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(234, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "               Alış              ";
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(234, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "             Satış              ";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // AlSat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 212);
            this.Controls.Add(this.panel1);
            this.KeyPreview = false;
            this.MaximizeBox = false;
            this.Name = "AlSat";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ALIŞ";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.AlSat_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tip.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gecerlilik.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lot.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fiyat.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menkul.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musteri.Properties)).EndInit();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Framework.ControlLibrary.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.ComboBoxEdit fiyat;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.ComboBoxEdit menkul;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.TextEdit lot;
        public System.Windows.Forms.TabControl tabControl1;
        public System.Windows.Forms.Panel panel3;
        public Framework.ControlLibrary.LookUpEdit musteri;
        private System.Windows.Forms.Label label6;
        private DevExpress.XtraEditors.ComboBoxEdit tip;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.ComboBoxEdit gecerlilik;

    }
}
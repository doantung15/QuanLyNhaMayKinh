
namespace DoAn1ngay
{
    partial class MainWindow
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.thoigiannhapnhay = new System.Windows.Forms.TextBox();
            this.solannhapnhay = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbx_TuanTu = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.cbx_ID = new System.Windows.Forms.ComboBox();
            this.flpStorage = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.lsvStorage = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnXuatkho = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox1);
            this.tabPage4.Controls.Add(this.cbx_TuanTu);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(951, 490);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Hien Thi";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.thoigiannhapnhay);
            this.groupBox1.Controls.Add(this.solannhapnhay);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 37);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(194, 81);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // thoigiannhapnhay
            // 
            this.thoigiannhapnhay.Location = new System.Drawing.Point(129, 19);
            this.thoigiannhapnhay.Name = "thoigiannhapnhay";
            this.thoigiannhapnhay.Size = new System.Drawing.Size(45, 20);
            this.thoigiannhapnhay.TabIndex = 0;
            this.thoigiannhapnhay.Text = "10";
            // 
            // solannhapnhay
            // 
            this.solannhapnhay.Location = new System.Drawing.Point(129, 45);
            this.solannhapnhay.Name = "solannhapnhay";
            this.solannhapnhay.Size = new System.Drawing.Size(45, 20);
            this.solannhapnhay.TabIndex = 0;
            this.solannhapnhay.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Số lần";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Thời gian";
            // 
            // cbx_TuanTu
            // 
            this.cbx_TuanTu.AutoSize = true;
            this.cbx_TuanTu.Location = new System.Drawing.Point(22, 14);
            this.cbx_TuanTu.Name = "cbx_TuanTu";
            this.cbx_TuanTu.Size = new System.Drawing.Size(67, 17);
            this.cbx_TuanTu.TabIndex = 3;
            this.cbx_TuanTu.Text = "Tuần Tự";
            this.cbx_TuanTu.UseVisualStyleBackColor = true;
            this.cbx_TuanTu.CheckedChanged += new System.EventHandler(this.cbx_TuanTu_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.CausesValidation = false;
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1257, 490);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "KhoHang";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowDrop = true;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(1251, 484);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column1.FalseValue = "0";
            this.Column1.HeaderText = "";
            this.Column1.Name = "Column1";
            this.Column1.TrueValue = "1";
            this.Column1.Width = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDisconnect);
            this.tabPage2.Controls.Add(this.btn_Connect);
            this.tabPage2.Controls.Add(this.cbx_ID);
            this.tabPage2.Controls.Add(this.flpStorage);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(951, 490);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Export";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(793, 7);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(77, 25);
            this.btnDisconnect.TabIndex = 6;
            this.btnDisconnect.Text = "Dis";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(713, 7);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(74, 25);
            this.btn_Connect.TabIndex = 5;
            this.btn_Connect.Text = "Con";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // cbx_ID
            // 
            this.cbx_ID.FormattingEnabled = true;
            this.cbx_ID.Location = new System.Drawing.Point(608, 10);
            this.cbx_ID.Name = "cbx_ID";
            this.cbx_ID.Size = new System.Drawing.Size(89, 21);
            this.cbx_ID.TabIndex = 2;
            // 
            // flpStorage
            // 
            this.flpStorage.AutoScroll = true;
            this.flpStorage.Location = new System.Drawing.Point(2, 3);
            this.flpStorage.Name = "flpStorage";
            this.flpStorage.Size = new System.Drawing.Size(600, 480);
            this.flpStorage.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.lsvStorage);
            this.groupBox4.Controls.Add(this.progressBar1);
            this.groupBox4.Controls.Add(this.btnXuatkho);
            this.groupBox4.Location = new System.Drawing.Point(608, 48);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(339, 446);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "View";
            // 
            // lsvStorage
            // 
            this.lsvStorage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lsvStorage.HideSelection = false;
            this.lsvStorage.Location = new System.Drawing.Point(6, 48);
            this.lsvStorage.Name = "lsvStorage";
            this.lsvStorage.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lsvStorage.Size = new System.Drawing.Size(314, 351);
            this.lsvStorage.TabIndex = 4;
            this.lsvStorage.UseCompatibleStateImageBehavior = false;
            this.lsvStorage.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 50;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "BarCode";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 210;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Vị Trí";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 50;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 19);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(314, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // btnXuatkho
            // 
            this.btnXuatkho.BackColor = System.Drawing.Color.Transparent;
            this.btnXuatkho.Location = new System.Drawing.Point(199, 405);
            this.btnXuatkho.Name = "btnXuatkho";
            this.btnXuatkho.Size = new System.Drawing.Size(121, 34);
            this.btnXuatkho.TabIndex = 2;
            this.btnXuatkho.Text = "Xuât kho";
            this.btnXuatkho.UseVisualStyleBackColor = false;
            this.btnXuatkho.Click += new System.EventHandler(this.btnXuatkho_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(6, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(959, 516);
            this.tabControl1.TabIndex = 0;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(965, 532);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.ComboBox cbx_ID;
        private System.Windows.Forms.FlowLayoutPanel flpStorage;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListView lsvStorage;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnXuatkho;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox solannhapnhay;
        private System.Windows.Forms.TextBox thoigiannhapnhay;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cbx_TuanTu;
    }
}


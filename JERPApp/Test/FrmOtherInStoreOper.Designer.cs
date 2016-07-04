namespace JERPApp.Test
{
    partial class FrmOtherInStoreOper
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mdgrdv = new JCommon.MyDataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCapReturnNoteCode = new System.Windows.Forms.Label();
            this.txtNoteCode = new System.Windows.Forms.TextBox();
            this.ctrlUserPsn1 = new JERPApp.Define.Hr.CtrlUserPsn();
            this.ctrlSupplier2 = new JERPApp.Define.General.CtrlSupplier();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpDateNote = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.libTitle = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPrd = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.rchMemo = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ctrlSupplier1 = new JERPApp.Define.General.CtrlSupplier();
            this.ctrlPersonnel1 = new JERPApp.Define.Hr.CtrlPersonnel();
            this.ColumnBranchStoreID = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.colInPerson = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PrdCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mdgrdv)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mdgrdv
            // 
            this.mdgrdv.AllowUserToDeleteRows = false;
            this.mdgrdv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mdgrdv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnBranchStoreID,
            this.colInPerson,
            this.PrdCode,
            this.ColProName,
            this.ColProType,
            this.ColNum,
            this.ColUnit,
            this.ColMemo});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mdgrdv.DefaultCellStyle = dataGridViewCellStyle1;
            this.mdgrdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdgrdv.Location = new System.Drawing.Point(0, 90);
            this.mdgrdv.Name = "mdgrdv";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mdgrdv.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.mdgrdv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.mdgrdv.RowTemplate.Height = 23;
            this.mdgrdv.Size = new System.Drawing.Size(998, 468);
            this.mdgrdv.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCapReturnNoteCode);
            this.panel1.Controls.Add(this.txtNoteCode);
            this.panel1.Controls.Add(this.ctrlUserPsn1);
            this.panel1.Controls.Add(this.ctrlSupplier2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dtpDateNote);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.libTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 90);
            this.panel1.TabIndex = 2;
            // 
            // lblCapReturnNoteCode
            // 
            this.lblCapReturnNoteCode.AutoSize = true;
            this.lblCapReturnNoteCode.Location = new System.Drawing.Point(719, 19);
            this.lblCapReturnNoteCode.Name = "lblCapReturnNoteCode";
            this.lblCapReturnNoteCode.Size = new System.Drawing.Size(53, 12);
            this.lblCapReturnNoteCode.TabIndex = 101;
            this.lblCapReturnNoteCode.Text = "入库单号";
            // 
            // txtNoteCode
            // 
            this.txtNoteCode.Location = new System.Drawing.Point(789, 13);
            this.txtNoteCode.Name = "txtNoteCode";
            this.txtNoteCode.ReadOnly = true;
            this.txtNoteCode.Size = new System.Drawing.Size(119, 21);
            this.txtNoteCode.TabIndex = 102;
            // 
            // ctrlUserPsn1
            // 
            this.ctrlUserPsn1.AutoSize = true;
            this.ctrlUserPsn1.Location = new System.Drawing.Point(404, 46);
            this.ctrlUserPsn1.Name = "ctrlUserPsn1";
            this.ctrlUserPsn1.PsnID = 1;
            this.ctrlUserPsn1.Size = new System.Drawing.Size(218, 23);
            this.ctrlUserPsn1.TabIndex = 14;
            // 
            // ctrlSupplier2
            // 
            this.ctrlSupplier2.CompanyID = 1;
            this.ctrlSupplier2.Location = new System.Drawing.Point(95, 46);
            this.ctrlSupplier2.Name = "ctrlSupplier2";
            this.ctrlSupplier2.Size = new System.Drawing.Size(163, 23);
            this.ctrlSupplier2.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 12;
            this.label2.Text = "采购员";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(37, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "供应商";
            // 
            // dtpDateNote
            // 
            this.dtpDateNote.Location = new System.Drawing.Point(96, 10);
            this.dtpDateNote.Name = "dtpDateNote";
            this.dtpDateNote.Size = new System.Drawing.Size(124, 21);
            this.dtpDateNote.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(37, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "制单日期";
            // 
            // libTitle
            // 
            this.libTitle.AutoSize = true;
            this.libTitle.Font = new System.Drawing.Font("宋体", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.libTitle.Location = new System.Drawing.Point(412, 8);
            this.libTitle.Name = "libTitle";
            this.libTitle.Size = new System.Drawing.Size(105, 13);
            this.libTitle.TabIndex = 0;
            this.libTitle.Text = "其它采购入库单";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPrd);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.rchMemo);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 558);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(998, 35);
            this.panel2.TabIndex = 4;
            // 
            // btnPrd
            // 
            this.btnPrd.Location = new System.Drawing.Point(507, 6);
            this.btnPrd.Name = "btnPrd";
            this.btnPrd.Size = new System.Drawing.Size(75, 23);
            this.btnPrd.TabIndex = 5;
            this.btnPrd.Text = "+产品";
            this.btnPrd.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(725, 7);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 4;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(599, 7);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(92, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存入账";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // rchMemo
            // 
            this.rchMemo.Location = new System.Drawing.Point(43, 6);
            this.rchMemo.Name = "rchMemo";
            this.rchMemo.Size = new System.Drawing.Size(437, 24);
            this.rchMemo.TabIndex = 1;
            this.rchMemo.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "备注";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ctrlSupplier1
            // 
            this.ctrlSupplier1.CompanyID = 1;
            this.ctrlSupplier1.Location = new System.Drawing.Point(87, 46);
            this.ctrlSupplier1.Name = "ctrlSupplier1";
            this.ctrlSupplier1.Size = new System.Drawing.Size(136, 23);
            this.ctrlSupplier1.TabIndex = 9;
            // 
            // ctrlPersonnel1
            // 
            this.ctrlPersonnel1.AutoSize = true;
            this.ctrlPersonnel1.Location = new System.Drawing.Point(405, 46);
            this.ctrlPersonnel1.Name = "ctrlPersonnel1";
            this.ctrlPersonnel1.PsnID = -1;
            this.ctrlPersonnel1.Size = new System.Drawing.Size(173, 23);
            this.ctrlPersonnel1.TabIndex = 11;
            // 
            // ColumnBranchStoreID
            // 
            this.ColumnBranchStoreID.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
            this.ColumnBranchStoreID.HeaderText = "仓库";
            this.ColumnBranchStoreID.Name = "ColumnBranchStoreID";
            this.ColumnBranchStoreID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnBranchStoreID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.ColumnBranchStoreID.Width = 60;
            // 
            // colInPerson
            // 
            this.colInPerson.HeaderText = "入库员";
            this.colInPerson.Name = "colInPerson";
            this.colInPerson.Width = 80;
            // 
            // PrdCode
            // 
            this.PrdCode.DataPropertyName = "PrdCode";
            this.PrdCode.HeaderText = "产品编号";
            this.PrdCode.Name = "PrdCode";
            // 
            // ColProName
            // 
            this.ColProName.DataPropertyName = "PrdName";
            this.ColProName.HeaderText = "产品名称";
            this.ColProName.Name = "ColProName";
            // 
            // ColProType
            // 
            this.ColProType.DataPropertyName = "PrdSpec";
            this.ColProType.HeaderText = "产品规格";
            this.ColProType.Name = "ColProType";
            // 
            // ColNum
            // 
            this.ColNum.DataPropertyName = "Quantity";
            this.ColNum.HeaderText = "数量";
            this.ColNum.Name = "ColNum";
            this.ColNum.Width = 60;
            // 
            // ColUnit
            // 
            this.ColUnit.DataPropertyName = "UnitName";
            this.ColUnit.HeaderText = "单位";
            this.ColUnit.Name = "ColUnit";
            this.ColUnit.Width = 54;
            // 
            // ColMemo
            // 
            this.ColMemo.DataPropertyName = "Memo";
            this.ColMemo.HeaderText = "备注";
            this.ColMemo.Name = "ColMemo";
            // 
            // FrmOtherInStoreOper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 593);
            this.Controls.Add(this.mdgrdv);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmOtherInStoreOper";
            this.Text = "FrmOtherInStoreOper";
            ((System.ComponentModel.ISupportInitialize)(this.mdgrdv)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private JCommon.MyDataGridView mdgrdv;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label libTitle;
        private System.Windows.Forms.DateTimePicker dtpDateNote;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnPrd;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.RichTextBox rchMemo;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private Define.Hr.CtrlPersonnel ctrlPersonnel1;
        private System.Windows.Forms.Label label1;
        private Define.General.CtrlSupplier ctrlSupplier1;
        private Define.Hr.CtrlUserPsn ctrlUserPsn1;
        private Define.General.CtrlSupplier ctrlSupplier2;
        private System.Windows.Forms.Label lblCapReturnNoteCode;
        private System.Windows.Forms.TextBox txtNoteCode;
        private System.Windows.Forms.DataGridViewComboBoxColumn ColumnBranchStoreID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colInPerson;
        private System.Windows.Forms.DataGridViewTextBoxColumn PrdCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMemo;
    }
}
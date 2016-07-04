﻿namespace JERPApp.Supply.OutSrc
{
    partial class FrmOutSrcPriceOper
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlOrderTypeID = new JERPApp.Define.Manufacture.CtrlOutSrcOrderType();
            this.ctrlPriceTypeID = new JERPApp.Define.Finance.CtrlPriceType();
            this.ctrlSettleTypeID = new JERPApp.Define.Finance.CtrlSettleType();
            this.ctrlMoneyTypeID = new JERPApp.Define.Finance.CtrlMoneyType();
            this.dpkDateNote = new System.Windows.Forms.DateTimePicker();
            this.ctrlCompanyID = new JERPApp.Define.General.CtrlSupplierForOutSrc();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNoteCode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ctrlQFind = new JCommon.CtrlGridQuickFind();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.rchMemo = new System.Windows.Forms.RichTextBox();
            this.dgrdv = new JCommon.MyDataGridView();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctrlFileBrowse = new JCommon.CtrlFileBrowse();
            this.ColumnPrdCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrdSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnModel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrdStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnManufProcessID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctrlOrderTypeID);
            this.panel1.Controls.Add(this.ctrlPriceTypeID);
            this.panel1.Controls.Add(this.ctrlSettleTypeID);
            this.panel1.Controls.Add(this.ctrlMoneyTypeID);
            this.panel1.Controls.Add(this.dpkDateNote);
            this.panel1.Controls.Add(this.ctrlCompanyID);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.txtNoteCode);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(924, 56);
            this.panel1.TabIndex = 0;
            // 
            // ctrlOrderTypeID
            // 
            this.ctrlOrderTypeID.Location = new System.Drawing.Point(253, 29);
            this.ctrlOrderTypeID.Name = "ctrlOrderTypeID";
            this.ctrlOrderTypeID.OrderTypeID = 1;
            this.ctrlOrderTypeID.Size = new System.Drawing.Size(93, 23);
            this.ctrlOrderTypeID.TabIndex = 29;
            // 
            // ctrlPriceTypeID
            // 
            this.ctrlPriceTypeID.Location = new System.Drawing.Point(720, 28);
            this.ctrlPriceTypeID.Name = "ctrlPriceTypeID";
            this.ctrlPriceTypeID.PriceTypeID = 1;
            this.ctrlPriceTypeID.Size = new System.Drawing.Size(118, 23);
            this.ctrlPriceTypeID.TabIndex = 27;
            // 
            // ctrlSettleTypeID
            // 
            this.ctrlSettleTypeID.Location = new System.Drawing.Point(534, 29);
            this.ctrlSettleTypeID.Name = "ctrlSettleTypeID";
            this.ctrlSettleTypeID.SettleTypeID = 1;
            this.ctrlSettleTypeID.Size = new System.Drawing.Size(118, 23);
            this.ctrlSettleTypeID.TabIndex = 26;
            // 
            // ctrlMoneyTypeID
            // 
            this.ctrlMoneyTypeID.AutoSize = true;
            this.ctrlMoneyTypeID.Location = new System.Drawing.Point(386, 29);
            this.ctrlMoneyTypeID.MoneyTypeID = 1;
            this.ctrlMoneyTypeID.Name = "ctrlMoneyTypeID";
            this.ctrlMoneyTypeID.Size = new System.Drawing.Size(86, 23);
            this.ctrlMoneyTypeID.TabIndex = 25;
            // 
            // dpkDateNote
            // 
            this.dpkDateNote.Location = new System.Drawing.Point(707, 3);
            this.dpkDateNote.Name = "dpkDateNote";
            this.dpkDateNote.Size = new System.Drawing.Size(131, 21);
            this.dpkDateNote.TabIndex = 23;
            // 
            // ctrlCompanyID
            // 
            this.ctrlCompanyID.CompanyID = -1;
            this.ctrlCompanyID.Location = new System.Drawing.Point(57, 30);
            this.ctrlCompanyID.Name = "ctrlCompanyID";
            this.ctrlCompanyID.Size = new System.Drawing.Size(135, 23);
            this.ctrlCompanyID.TabIndex = 22;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(194, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 20;
            this.label9.Text = "订单类型";
            // 
            // txtNoteCode
            // 
            this.txtNoteCode.Location = new System.Drawing.Point(56, 7);
            this.txtNoteCode.Name = "txtNoteCode";
            this.txtNoteCode.Size = new System.Drawing.Size(125, 21);
            this.txtNoteCode.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(4, 13);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "报价单号";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(661, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "单价类型";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(478, 39);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "结算方式";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(351, 39);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "币种";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "供应商";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(648, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "生效日期";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(403, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "产品委外报价单";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ctrlQFind);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnNew);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 527);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(924, 32);
            this.panel2.TabIndex = 1;
            // 
            // ctrlQFind
            // 
            this.ctrlQFind.Location = new System.Drawing.Point(12, 6);
            this.ctrlQFind.Name = "ctrlQFind";
            this.ctrlQFind.SeachGridView = null;
            this.ctrlQFind.Size = new System.Drawing.Size(286, 21);
            this.ctrlQFind.TabIndex = 9;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(424, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "+项";
            this.btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(773, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(71, 23);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.Text = "删除";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(701, 3);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(71, 23);
            this.btnNew.TabIndex = 5;
            this.btnNew.Text = "新增";
            this.btnNew.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(532, 3);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // rchMemo
            // 
            this.rchMemo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rchMemo.Location = new System.Drawing.Point(3, 17);
            this.rchMemo.Name = "rchMemo";
            this.rchMemo.Size = new System.Drawing.Size(904, 277);
            this.rchMemo.TabIndex = 4;
            this.rchMemo.Text = "";
            // 
            // dgrdv
            // 
            this.dgrdv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnPrdCode,
            this.ColumnPrdName,
            this.ColumnPrdSpec,
            this.ColumnModel,
            this.ColumnPrdStatus,
            this.ColumnUnitName,
            this.ColumnPrice,
            this.ColumnMemo,
            this.ColumnManufProcessID});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrdv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdv.Location = new System.Drawing.Point(3, 3);
            this.dgrdv.Name = "dgrdv";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrdv.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.dgrdv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgrdv.RowTemplate.Height = 23;
            this.dgrdv.Size = new System.Drawing.Size(910, 439);
            this.dgrdv.TabIndex = 2;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPage1);
            this.tabMain.Controls.Add(this.tabPage2);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 56);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(924, 471);
            this.tabMain.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgrdv);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(916, 445);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "报价明细";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox2);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(916, 445);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "备注/文档";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rchMemo);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(910, 297);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "备注";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ctrlFileBrowse);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(3, 300);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(910, 142);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "文档";
            // 
            // ctrlFileBrowse
            // 
            this.ctrlFileBrowse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlFileBrowse.Location = new System.Drawing.Point(3, 17);
            this.ctrlFileBrowse.Name = "ctrlFileBrowse";
            this.ctrlFileBrowse.ReadOnly = false;
            this.ctrlFileBrowse.Size = new System.Drawing.Size(904, 122);
            this.ctrlFileBrowse.TabIndex = 0;
            // 
            // ColumnPrdCode
            // 
            this.ColumnPrdCode.DataPropertyName = "PrdCode";
            this.ColumnPrdCode.HeaderText = "产品编号";
            this.ColumnPrdCode.Name = "ColumnPrdCode";
            this.ColumnPrdCode.ReadOnly = true;
            this.ColumnPrdCode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnPrdCode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnPrdName
            // 
            this.ColumnPrdName.DataPropertyName = "PrdName";
            this.ColumnPrdName.HeaderText = "产品名称";
            this.ColumnPrdName.Name = "ColumnPrdName";
            this.ColumnPrdName.ReadOnly = true;
            this.ColumnPrdName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnPrdName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnPrdSpec
            // 
            this.ColumnPrdSpec.DataPropertyName = "PrdSpec";
            this.ColumnPrdSpec.HeaderText = "产品规格";
            this.ColumnPrdSpec.Name = "ColumnPrdSpec";
            this.ColumnPrdSpec.ReadOnly = true;
            this.ColumnPrdSpec.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnPrdSpec.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ColumnPrdSpec.Width = 120;
            // 
            // ColumnModel
            // 
            this.ColumnModel.DataPropertyName = "Model";
            this.ColumnModel.HeaderText = "机型";
            this.ColumnModel.Name = "ColumnModel";
            this.ColumnModel.ReadOnly = true;
            this.ColumnModel.Width = 80;
            // 
            // ColumnPrdStatus
            // 
            this.ColumnPrdStatus.DataPropertyName = "PrdStatus";
            this.ColumnPrdStatus.HeaderText = "工序";
            this.ColumnPrdStatus.Name = "ColumnPrdStatus";
            this.ColumnPrdStatus.ReadOnly = true;
            this.ColumnPrdStatus.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ColumnPrdStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ColumnUnitName
            // 
            this.ColumnUnitName.DataPropertyName = "UnitName";
            this.ColumnUnitName.HeaderText = "单位";
            this.ColumnUnitName.Name = "ColumnUnitName";
            this.ColumnUnitName.ReadOnly = true;
            this.ColumnUnitName.Width = 54;
            // 
            // ColumnPrice
            // 
            this.ColumnPrice.DataPropertyName = "Price";
            this.ColumnPrice.HeaderText = "报价";
            this.ColumnPrice.Name = "ColumnPrice";
            this.ColumnPrice.Width = 66;
            // 
            // ColumnMemo
            // 
            this.ColumnMemo.DataPropertyName = "Memo";
            this.ColumnMemo.HeaderText = "备注";
            this.ColumnMemo.Name = "ColumnMemo";
            this.ColumnMemo.Width = 140;
            // 
            // ColumnManufProcessID
            // 
            this.ColumnManufProcessID.DataPropertyName = "ManufProcessID";
            this.ColumnManufProcessID.HeaderText = "ManufProcessID";
            this.ColumnManufProcessID.Name = "ColumnManufProcessID";
            this.ColumnManufProcessID.Visible = false;
            // 
            // FrmOutSrcPriceOper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(924, 559);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmOutSrcPriceOper";
            this.Text = "产品委外报价单";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSave;
        private JCommon.MyDataGridView dgrdv;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rchMemo;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.TextBox txtNoteCode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private JCommon.CtrlFileBrowse ctrlFileBrowse;
        private JCommon.CtrlGridQuickFind ctrlQFind;
        private System.Windows.Forms.Label label9;
        private JERPApp.Define.General.CtrlSupplierForOutSrc ctrlCompanyID;
        private System.Windows.Forms.DateTimePicker dpkDateNote;
        private JERPApp.Define.Finance.CtrlMoneyType ctrlMoneyTypeID;
        private JERPApp.Define.Finance.CtrlSettleType ctrlSettleTypeID;
        private JERPApp.Define.Finance.CtrlPriceType ctrlPriceTypeID;
        private JERPApp.Define.Manufacture.CtrlOutSrcOrderType ctrlOrderTypeID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrdCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrdSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnModel;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrdStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMemo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnManufProcessID;
    }
}
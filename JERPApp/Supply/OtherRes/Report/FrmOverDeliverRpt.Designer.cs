﻿namespace JERPApp.Supply.OtherRes.Report
{
    partial class FrmOverDeliverRpt
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlSupplierID = new JERPApp.Define.General.CtrlSupplierForOtherRes();
            this.ckbSupplier = new System.Windows.Forms.CheckBox();
            this.ctrlMonth = new JCommon.CtrlMonth();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrlYear = new JCommon.CtrlYear();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExplore = new System.Windows.Forms.Button();
            this.ctrlGridQFind = new JCommon.CtrlGridQuickFind();
            this.dgrdv = new JCommon.MyDataGridView();
            this.ColumnCompanyAbbName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPONo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrdName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPrdSpec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnUnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDeliverQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOverDeliverQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOverDeliverRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnOverDeliverAMT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctrlSupplierID);
            this.panel1.Controls.Add(this.ckbSupplier);
            this.panel1.Controls.Add(this.ctrlMonth);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.ctrlYear);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1004, 31);
            this.panel1.TabIndex = 3;
            // 
            // ctrlSupplierID
            // 
            this.ctrlSupplierID.CompanyID = -1;
            this.ctrlSupplierID.Location = new System.Drawing.Point(279, 6);
            this.ctrlSupplierID.Name = "ctrlSupplierID";
            this.ctrlSupplierID.Size = new System.Drawing.Size(140, 23);
            this.ctrlSupplierID.TabIndex = 11;
            // 
            // ckbSupplier
            // 
            this.ckbSupplier.AutoSize = true;
            this.ckbSupplier.Location = new System.Drawing.Point(212, 9);
            this.ckbSupplier.Name = "ckbSupplier";
            this.ckbSupplier.Size = new System.Drawing.Size(60, 16);
            this.ckbSupplier.TabIndex = 10;
            this.ckbSupplier.Text = "供应商";
            this.ckbSupplier.UseVisualStyleBackColor = true;
            // 
            // ctrlMonth
            // 
            this.ctrlMonth.AutoSize = true;
            this.ctrlMonth.Location = new System.Drawing.Point(163, 7);
            this.ctrlMonth.Month = 4;
            this.ctrlMonth.Name = "ctrlMonth";
            this.ctrlMonth.Size = new System.Drawing.Size(42, 21);
            this.ctrlMonth.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(127, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "月份";
            // 
            // ctrlYear
            // 
            this.ctrlYear.AutoSize = true;
            this.ctrlYear.Location = new System.Drawing.Point(41, 6);
            this.ctrlYear.Name = "ctrlYear";
            this.ctrlYear.Size = new System.Drawing.Size(79, 21);
            this.ctrlYear.TabIndex = 6;
            this.ctrlYear.Year = 2011;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "年份";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(453, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "耗材采购超送报告";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnExplore);
            this.panel2.Controls.Add(this.ctrlGridQFind);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 524);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1004, 31);
            this.panel2.TabIndex = 4;
            // 
            // btnExplore
            // 
            this.btnExplore.Location = new System.Drawing.Point(256, 3);
            this.btnExplore.Name = "btnExplore";
            this.btnExplore.Size = new System.Drawing.Size(77, 25);
            this.btnExplore.TabIndex = 6;
            this.btnExplore.Text = "输出打印";
            this.btnExplore.UseVisualStyleBackColor = true;
            // 
            // ctrlGridQFind
            // 
            this.ctrlGridQFind.Location = new System.Drawing.Point(6, 6);
            this.ctrlGridQFind.Name = "ctrlGridQFind";
            this.ctrlGridQFind.SeachGridView = null;
            this.ctrlGridQFind.Size = new System.Drawing.Size(244, 21);
            this.ctrlGridQFind.TabIndex = 0;
            // 
            // dgrdv
            // 
            this.dgrdv.AllowUserToAddRows = false;
            this.dgrdv.AllowUserToDeleteRows = false;
            this.dgrdv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnCompanyAbbName,
            this.ColumnPONo,
            this.ColumnPrdName,
            this.ColumnPrdSpec,
            this.ColumnUnitName,
            this.ColumnQuantity,
            this.ColumnDeliverQty,
            this.ColumnOverDeliverQty,
            this.ColumnOverDeliverRate,
            this.ColumnOverDeliverAMT});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrdv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgrdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdv.Location = new System.Drawing.Point(0, 31);
            this.dgrdv.Name = "dgrdv";
            this.dgrdv.ReadOnly = true;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrdv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            this.dgrdv.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgrdv.RowTemplate.Height = 23;
            this.dgrdv.Size = new System.Drawing.Size(1004, 493);
            this.dgrdv.TabIndex = 5;
            // 
            // ColumnCompanyAbbName
            // 
            this.ColumnCompanyAbbName.DataPropertyName = "CompanyAbbName";
            this.ColumnCompanyAbbName.Frozen = true;
            this.ColumnCompanyAbbName.HeaderText = "供应商";
            this.ColumnCompanyAbbName.Name = "ColumnCompanyAbbName";
            this.ColumnCompanyAbbName.ReadOnly = true;
            this.ColumnCompanyAbbName.Width = 78;
            // 
            // ColumnPONo
            // 
            this.ColumnPONo.DataPropertyName = "PONo";
            this.ColumnPONo.Frozen = true;
            this.ColumnPONo.HeaderText = "订单编号";
            this.ColumnPONo.Name = "ColumnPONo";
            this.ColumnPONo.ReadOnly = true;
            this.ColumnPONo.Width = 80;
            // 
            // ColumnPrdName
            // 
            this.ColumnPrdName.DataPropertyName = "PrdName";
            this.ColumnPrdName.Frozen = true;
            this.ColumnPrdName.HeaderText = "产品名称";
            this.ColumnPrdName.Name = "ColumnPrdName";
            this.ColumnPrdName.ReadOnly = true;
            // 
            // ColumnPrdSpec
            // 
            this.ColumnPrdSpec.DataPropertyName = "PrdSpec";
            this.ColumnPrdSpec.HeaderText = "型号及规格";
            this.ColumnPrdSpec.Name = "ColumnPrdSpec";
            this.ColumnPrdSpec.ReadOnly = true;
            // 
            // ColumnUnitName
            // 
            this.ColumnUnitName.DataPropertyName = "UnitName";
            this.ColumnUnitName.HeaderText = "单位";
            this.ColumnUnitName.Name = "ColumnUnitName";
            this.ColumnUnitName.ReadOnly = true;
            this.ColumnUnitName.Width = 54;
            // 
            // ColumnQuantity
            // 
            this.ColumnQuantity.DataPropertyName = "Quantity";
            this.ColumnQuantity.HeaderText = "P/O数";
            this.ColumnQuantity.Name = "ColumnQuantity";
            this.ColumnQuantity.ReadOnly = true;
            this.ColumnQuantity.Width = 66;
            // 
            // ColumnDeliverQty
            // 
            this.ColumnDeliverQty.DataPropertyName = "DeliverQty";
            this.ColumnDeliverQty.HeaderText = "送货数";
            this.ColumnDeliverQty.Name = "ColumnDeliverQty";
            this.ColumnDeliverQty.ReadOnly = true;
            this.ColumnDeliverQty.Width = 66;
            // 
            // ColumnOverDeliverQty
            // 
            this.ColumnOverDeliverQty.DataPropertyName = "OverDeliverQty";
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Red;
            this.ColumnOverDeliverQty.DefaultCellStyle = dataGridViewCellStyle1;
            this.ColumnOverDeliverQty.HeaderText = "超送数";
            this.ColumnOverDeliverQty.Name = "ColumnOverDeliverQty";
            this.ColumnOverDeliverQty.ReadOnly = true;
            this.ColumnOverDeliverQty.Width = 66;
            // 
            // ColumnOverDeliverRate
            // 
            this.ColumnOverDeliverRate.DataPropertyName = "OverDeliverRate";
            dataGridViewCellStyle2.Format = "0.##%";
            this.ColumnOverDeliverRate.DefaultCellStyle = dataGridViewCellStyle2;
            this.ColumnOverDeliverRate.HeaderText = "超送比";
            this.ColumnOverDeliverRate.Name = "ColumnOverDeliverRate";
            this.ColumnOverDeliverRate.ReadOnly = true;
            this.ColumnOverDeliverRate.Width = 66;
            // 
            // ColumnOverDeliverAMT
            // 
            this.ColumnOverDeliverAMT.DataPropertyName = "OverDeliverAMT";
            this.ColumnOverDeliverAMT.HeaderText = "超送额";
            this.ColumnOverDeliverAMT.Name = "ColumnOverDeliverAMT";
            this.ColumnOverDeliverAMT.ReadOnly = true;
            this.ColumnOverDeliverAMT.Width = 66;
            // 
            // FrmOverDeliverRpt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 555);
            this.Controls.Add(this.dgrdv);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FrmOverDeliverRpt";
            this.Text = "FrmOverDeliverRpt";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private JCommon.CtrlGridQuickFind ctrlGridQFind;
        private JCommon.MyDataGridView dgrdv;
        private System.Windows.Forms.Button btnExplore;
        private JCommon.CtrlYear ctrlYear;
        private System.Windows.Forms.Label label2;
        private JCommon.CtrlMonth ctrlMonth;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox ckbSupplier;
        private JERPApp.Define.General.CtrlSupplierForOtherRes ctrlSupplierID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompanyAbbName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPONo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrdName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPrdSpec;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnUnitName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeliverQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOverDeliverQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOverDeliverRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnOverDeliverAMT;
    }
}
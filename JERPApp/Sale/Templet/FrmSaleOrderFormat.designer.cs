﻿namespace JERPApp.Sale.Templet
{
    partial class FrmSaleOrderFormat
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lnkUpload = new System.Windows.Forms.LinkLabel();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.lnkNew = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.dgrdv = new JCommon.MyDataGridView();
            this.cMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mItemRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ColumnBtnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnBtnCopy = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColumnTmpSheetName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnPONoCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCompanyNameCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDateNoteCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDeliverAddressCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnFinanceAddressCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemRowIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnSalePsnCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnMakerPsnCellName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnItemRowCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).BeginInit();
            this.cMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lnkUpload);
            this.panel1.Controls.Add(this.lnkDownload);
            this.panel1.Controls.Add(this.lnkNew);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(785, 37);
            this.panel1.TabIndex = 1;
            // 
            // lnkUpload
            // 
            this.lnkUpload.AutoSize = true;
            this.lnkUpload.Location = new System.Drawing.Point(197, 22);
            this.lnkUpload.Name = "lnkUpload";
            this.lnkUpload.Size = new System.Drawing.Size(77, 12);
            this.lnkUpload.TabIndex = 12;
            this.lnkUpload.TabStop = true;
            this.lnkUpload.Text = "格式文档上载";
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.Location = new System.Drawing.Point(83, 22);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(77, 12);
            this.lnkDownload.TabIndex = 11;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "格式文档下载";
            // 
            // lnkNew
            // 
            this.lnkNew.AutoSize = true;
            this.lnkNew.Location = new System.Drawing.Point(6, 22);
            this.lnkNew.Name = "lnkNew";
            this.lnkNew.Size = new System.Drawing.Size(53, 12);
            this.lnkNew.TabIndex = 5;
            this.lnkNew.TabStop = true;
            this.lnkNew.Text = "新增格式";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(368, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "销售订单格式";
            // 
            // dgrdv
            // 
            this.dgrdv.AllowUserToAddRows = false;
            this.dgrdv.AllowUserToDeleteRows = false;
            this.dgrdv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnBtnEdit,
            this.ColumnBtnCopy,
            this.ColumnTmpSheetName,
            this.ColumnPONoCellName,
            this.ColumnCompanyNameCellName,
            this.ColumnDateNoteCellName,
            this.ColumnDeliverAddressCellName,
            this.ColumnFinanceAddressCellName,
            this.ColumnItemRowIndex,
            this.ColumnSalePsnCellName,
            this.ColumnMakerPsnCellName,
            this.ColumnItemRowCount});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrdv.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdv.Location = new System.Drawing.Point(0, 37);
            this.dgrdv.Name = "dgrdv";
            this.dgrdv.ReadOnly = true;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgrdv.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.SkyBlue;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.dgrdv.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dgrdv.RowTemplate.Height = 23;
            this.dgrdv.Size = new System.Drawing.Size(785, 417);
            this.dgrdv.TabIndex = 2;
            // 
            // cMenu
            // 
            this.cMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mItemRefresh});
            this.cMenu.Name = "cMenu";
            this.cMenu.Size = new System.Drawing.Size(99, 26);
            // 
            // mItemRefresh
            // 
            this.mItemRefresh.Name = "mItemRefresh";
            this.mItemRefresh.Size = new System.Drawing.Size(98, 22);
            this.mItemRefresh.Text = "刷新";
            // 
            // ColumnBtnEdit
            // 
            this.ColumnBtnEdit.HeaderText = "变更";
            this.ColumnBtnEdit.Name = "ColumnBtnEdit";
            this.ColumnBtnEdit.ReadOnly = true;
            this.ColumnBtnEdit.Text = "变更";
            this.ColumnBtnEdit.UseColumnTextForButtonValue = true;
            this.ColumnBtnEdit.Width = 60;
            // 
            // ColumnBtnCopy
            // 
            this.ColumnBtnCopy.HeaderText = "复制";
            this.ColumnBtnCopy.Name = "ColumnBtnCopy";
            this.ColumnBtnCopy.ReadOnly = true;
            this.ColumnBtnCopy.Text = "复制";
            this.ColumnBtnCopy.UseColumnTextForButtonValue = true;
            this.ColumnBtnCopy.Width = 54;
            // 
            // ColumnTmpSheetName
            // 
            this.ColumnTmpSheetName.DataPropertyName = "TmpSheetName";
            this.ColumnTmpSheetName.HeaderText = "模版名称";
            this.ColumnTmpSheetName.Name = "ColumnTmpSheetName";
            this.ColumnTmpSheetName.ReadOnly = true;
            this.ColumnTmpSheetName.Width = 78;
            // 
            // ColumnPONoCellName
            // 
            this.ColumnPONoCellName.DataPropertyName = "PONoCellName";
            this.ColumnPONoCellName.HeaderText = "订单号栏";
            this.ColumnPONoCellName.Name = "ColumnPONoCellName";
            this.ColumnPONoCellName.ReadOnly = true;
            this.ColumnPONoCellName.Width = 90;
            // 
            // ColumnCompanyNameCellName
            // 
            this.ColumnCompanyNameCellName.DataPropertyName = "CompanyNameCellName";
            this.ColumnCompanyNameCellName.HeaderText = "客户名称栏";
            this.ColumnCompanyNameCellName.Name = "ColumnCompanyNameCellName";
            this.ColumnCompanyNameCellName.ReadOnly = true;
            this.ColumnCompanyNameCellName.Width = 90;
            // 
            // ColumnDateNoteCellName
            // 
            this.ColumnDateNoteCellName.DataPropertyName = "DateNoteCellName";
            this.ColumnDateNoteCellName.HeaderText = "日期栏";
            this.ColumnDateNoteCellName.Name = "ColumnDateNoteCellName";
            this.ColumnDateNoteCellName.ReadOnly = true;
            this.ColumnDateNoteCellName.Width = 66;
            // 
            // ColumnDeliverAddressCellName
            // 
            this.ColumnDeliverAddressCellName.DataPropertyName = "DeliverAddressCellName";
            this.ColumnDeliverAddressCellName.HeaderText = "送货地栏";
            this.ColumnDeliverAddressCellName.Name = "ColumnDeliverAddressCellName";
            this.ColumnDeliverAddressCellName.ReadOnly = true;
            this.ColumnDeliverAddressCellName.Width = 78;
            // 
            // ColumnFinanceAddressCellName
            // 
            this.ColumnFinanceAddressCellName.DataPropertyName = "FinanceAddressCellName";
            this.ColumnFinanceAddressCellName.HeaderText = "结算地栏";
            this.ColumnFinanceAddressCellName.Name = "ColumnFinanceAddressCellName";
            this.ColumnFinanceAddressCellName.ReadOnly = true;
            this.ColumnFinanceAddressCellName.Width = 80;
            // 
            // ColumnItemRowIndex
            // 
            this.ColumnItemRowIndex.DataPropertyName = "ItemRowIndex";
            this.ColumnItemRowIndex.HeaderText = "明细开始行";
            this.ColumnItemRowIndex.Name = "ColumnItemRowIndex";
            this.ColumnItemRowIndex.ReadOnly = true;
            this.ColumnItemRowIndex.Width = 90;
            // 
            // ColumnSalePsnCellName
            // 
            this.ColumnSalePsnCellName.DataPropertyName = "SalePsnCellName";
            this.ColumnSalePsnCellName.HeaderText = "业务员栏";
            this.ColumnSalePsnCellName.Name = "ColumnSalePsnCellName";
            this.ColumnSalePsnCellName.ReadOnly = true;
            this.ColumnSalePsnCellName.Width = 80;
            // 
            // ColumnMakerPsnCellName
            // 
            this.ColumnMakerPsnCellName.DataPropertyName = "MakerPsnCellName";
            this.ColumnMakerPsnCellName.HeaderText = "制单员";
            this.ColumnMakerPsnCellName.Name = "ColumnMakerPsnCellName";
            this.ColumnMakerPsnCellName.ReadOnly = true;
            this.ColumnMakerPsnCellName.Width = 66;
            // 
            // ColumnItemRowCount
            // 
            this.ColumnItemRowCount.DataPropertyName = "ItemRowCount";
            this.ColumnItemRowCount.HeaderText = "页明细行数";
            this.ColumnItemRowCount.Name = "ColumnItemRowCount";
            this.ColumnItemRowCount.ReadOnly = true;
            this.ColumnItemRowCount.Width = 90;
            // 
            // FrmSaleOrderFormat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(785, 454);
            this.Controls.Add(this.dgrdv);
            this.Controls.Add(this.panel1);
            this.Name = "FrmSaleOrderFormat";
            this.Text = "FrmSaleOrderFormat";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).EndInit();
            this.cMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel lnkNew;
        private System.Windows.Forms.Label label3;
        private JCommon.MyDataGridView dgrdv;
        private System.Windows.Forms.ContextMenuStrip cMenu;
        private System.Windows.Forms.ToolStripMenuItem mItemRefresh;
        private System.Windows.Forms.LinkLabel lnkUpload;
        private System.Windows.Forms.LinkLabel lnkDownload;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnBtnEdit;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnBtnCopy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTmpSheetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnPONoCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCompanyNameCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDateNoteCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeliverAddressCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnFinanceAddressCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemRowIndex;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnSalePsnCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnMakerPsnCellName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnItemRowCount;
    }
}
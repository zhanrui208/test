﻿namespace JERPApp.Define.General
{
    partial class FrmDeliverType
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放客供资源，为 true；否则为 false。</param>
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
        /// 使用代码变更器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgrdv = new JCommon.MyDataGridView();
            this.ColumnDeliverTypeCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDeliverTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgrdv
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.dgrdv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgrdv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgrdv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnDeliverTypeCode,
            this.ColumnDeliverTypeName});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(165)))), ((int)(((byte)(198)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgrdv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgrdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgrdv.Location = new System.Drawing.Point(0, 0);
            this.dgrdv.Name = "dgrdv";
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dgrdv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.dgrdv.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgrdv.RowTemplate.Height = 23;
            this.dgrdv.Size = new System.Drawing.Size(251, 368);
            this.dgrdv.TabIndex = 1;
            // 
            // ColumnDeliverTypeCode
            // 
            this.ColumnDeliverTypeCode.DataPropertyName = "DeliverTypeCode";
            this.ColumnDeliverTypeCode.HeaderText = "方式编码";
            this.ColumnDeliverTypeCode.Name = "ColumnDeliverTypeCode";
            this.ColumnDeliverTypeCode.Width = 80;
            // 
            // ColumnDeliverTypeName
            // 
            this.ColumnDeliverTypeName.DataPropertyName = "DeliverTypeName";
            this.ColumnDeliverTypeName.HeaderText = "方式名称";
            this.ColumnDeliverTypeName.Name = "ColumnDeliverTypeName";
            this.ColumnDeliverTypeName.Width = 120;
            // 
            // FrmDeliverType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(251, 368);
            this.Controls.Add(this.dgrdv);
            this.Name = "FrmDeliverType";
            this.Text = "送货方式";
            ((System.ComponentModel.ISupportInitialize)(this.dgrdv)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private JCommon.MyDataGridView dgrdv;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeliverTypeCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDeliverTypeName;
    }
}
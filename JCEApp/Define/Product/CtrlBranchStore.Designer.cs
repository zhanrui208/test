﻿namespace JCEApp.Define.Product
{
    partial class CtrlBranchStore
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbItem = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbItem
            // 
            this.cmbItem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cmbItem.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.cmbItem.Location = new System.Drawing.Point(0, 0);
            this.cmbItem.Name = "cmbItem";
            this.cmbItem.Size = new System.Drawing.Size(213, 21);
            this.cmbItem.TabIndex = 0;
            // 
            // CtrlBranchStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cmbItem);
            this.Name = "CtrlBranchStore";
            this.Size = new System.Drawing.Size(213, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbItem;
    }
}

﻿namespace JERPApp.Define.Tool
{
    partial class CtrlProduct
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSelect = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPrdCode = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrdName = new System.Windows.Forms.TextBox();
            this.txtPrdSpec = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(591, 3);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(37, 23);
            this.btnSelect.TabIndex = 20;
            this.btnSelect.Text = "...";
            this.btnSelect.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(290, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 27;
            this.label6.Text = "规格";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 13);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 23;
            this.label5.Text = "编号";
            // 
            // txtPrdCode
            // 
            this.txtPrdCode.Location = new System.Drawing.Point(36, 3);
            this.txtPrdCode.Name = "txtPrdCode";
            this.txtPrdCode.ReadOnly = true;
            this.txtPrdCode.Size = new System.Drawing.Size(99, 21);
            this.txtPrdCode.TabIndex = 24;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(141, 12);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 25;
            this.label7.Text = "名称";
            // 
            // txtPrdName
            // 
            this.txtPrdName.Location = new System.Drawing.Point(172, 4);
            this.txtPrdName.Name = "txtPrdName";
            this.txtPrdName.ReadOnly = true;
            this.txtPrdName.Size = new System.Drawing.Size(115, 21);
            this.txtPrdName.TabIndex = 26;
            // 
            // txtPrdSpec
            // 
            this.txtPrdSpec.Location = new System.Drawing.Point(319, 4);
            this.txtPrdSpec.Name = "txtPrdSpec";
            this.txtPrdSpec.ReadOnly = true;
            this.txtPrdSpec.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtPrdSpec.Size = new System.Drawing.Size(254, 21);
            this.txtPrdSpec.TabIndex = 28;
            // 
            // CtrlProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPrdCode);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtPrdName);
            this.Controls.Add(this.txtPrdSpec);
            this.Controls.Add(this.btnSelect);
            this.Name = "CtrlProduct";
            this.Size = new System.Drawing.Size(633, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPrdCode;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtPrdName;
        private System.Windows.Forms.TextBox txtPrdSpec;

    }
}

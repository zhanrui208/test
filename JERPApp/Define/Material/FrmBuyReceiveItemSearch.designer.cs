﻿namespace JERPApp.Define.Material
{
    partial class FrmBuyReceiveItemSearch
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
        this.ckbCapPONo = new System.Windows.Forms.CheckBox();
        this.txtPONo = new System.Windows.Forms.TextBox();
        this.ckbCapPrdID = new System.Windows.Forms.CheckBox();
        this.ckbCapCompanyID = new System.Windows.Forms.CheckBox();
        this.ckbCapNoteCode = new System.Windows.Forms.CheckBox();
        this.txtNoteCode = new System.Windows.Forms.TextBox();
        this.ckbCapDateNote = new System.Windows.Forms.CheckBox();
        this.tpkDateNoteBegin = new System.Windows.Forms.DateTimePicker();
        this.lblCapDateNote = new System.Windows.Forms.Label();
        this.tpkDateNoteEnd = new System.Windows.Forms.DateTimePicker();
        this.ctrlPrdID = new JERPApp.Define.Material.CtrlProduct();
        this.btnSearch = new System.Windows.Forms.Button();
        this.ctrlSupplierID = new JERPApp.Define.General.CtrlSupplierForMaterial();
        this.SuspendLayout();
        // 
        // ckbCapPONo
        // 
        this.ckbCapPONo.AutoSize = true;
        this.ckbCapPONo.Location = new System.Drawing.Point(18, 14);
        this.ckbCapPONo.Name = "ckbCapPONo";
        this.ckbCapPONo.Size = new System.Drawing.Size(72, 16);
        this.ckbCapPONo.TabIndex = 0;
        this.ckbCapPONo.Text = "订单编号";
        // 
        // txtPONo
        // 
        this.txtPONo.Location = new System.Drawing.Point(108, 9);
        this.txtPONo.Name = "txtPONo";
        this.txtPONo.Size = new System.Drawing.Size(120, 21);
        this.txtPONo.TabIndex = 1;
        // 
        // ckbCapPrdID
        // 
        this.ckbCapPrdID.AutoSize = true;
        this.ckbCapPrdID.Location = new System.Drawing.Point(18, 49);
        this.ckbCapPrdID.Name = "ckbCapPrdID";
        this.ckbCapPrdID.Size = new System.Drawing.Size(15, 14);
        this.ckbCapPrdID.TabIndex = 6;
        // 
        // ckbCapCompanyID
        // 
        this.ckbCapCompanyID.AutoSize = true;
        this.ckbCapCompanyID.Location = new System.Drawing.Point(18, 76);
        this.ckbCapCompanyID.Name = "ckbCapCompanyID";
        this.ckbCapCompanyID.Size = new System.Drawing.Size(60, 16);
        this.ckbCapCompanyID.TabIndex = 9;
        this.ckbCapCompanyID.Text = "供应商";
        // 
        // ckbCapNoteCode
        // 
        this.ckbCapNoteCode.AutoSize = true;
        this.ckbCapNoteCode.Location = new System.Drawing.Point(285, 75);
        this.ckbCapNoteCode.Name = "ckbCapNoteCode";
        this.ckbCapNoteCode.Size = new System.Drawing.Size(72, 16);
        this.ckbCapNoteCode.TabIndex = 12;
        this.ckbCapNoteCode.Text = "收货单号";
        // 
        // txtNoteCode
        // 
        this.txtNoteCode.Location = new System.Drawing.Point(375, 70);
        this.txtNoteCode.Name = "txtNoteCode";
        this.txtNoteCode.Size = new System.Drawing.Size(120, 21);
        this.txtNoteCode.TabIndex = 13;
        // 
        // ckbCapDateNote
        // 
        this.ckbCapDateNote.AutoSize = true;
        this.ckbCapDateNote.Location = new System.Drawing.Point(18, 112);
        this.ckbCapDateNote.Name = "ckbCapDateNote";
        this.ckbCapDateNote.Size = new System.Drawing.Size(72, 16);
        this.ckbCapDateNote.TabIndex = 15;
        this.ckbCapDateNote.Text = "收货日期";
        // 
        // tpkDateNoteBegin
        // 
        this.tpkDateNoteBegin.Location = new System.Drawing.Point(108, 107);
        this.tpkDateNoteBegin.Name = "tpkDateNoteBegin";
        this.tpkDateNoteBegin.Size = new System.Drawing.Size(120, 21);
        this.tpkDateNoteBegin.TabIndex = 16;
        // 
        // lblCapDateNote
        // 
        this.lblCapDateNote.AutoSize = true;
        this.lblCapDateNote.Location = new System.Drawing.Point(238, 112);
        this.lblCapDateNote.Name = "lblCapDateNote";
        this.lblCapDateNote.Size = new System.Drawing.Size(17, 12);
        this.lblCapDateNote.TabIndex = 17;
        this.lblCapDateNote.Text = "—";
        // 
        // tpkDateNoteEnd
        // 
        this.tpkDateNoteEnd.Location = new System.Drawing.Point(268, 107);
        this.tpkDateNoteEnd.Name = "tpkDateNoteEnd";
        this.tpkDateNoteEnd.Size = new System.Drawing.Size(120, 21);
        this.tpkDateNoteEnd.TabIndex = 18;
        // 
        // ctrlPrdID
        // 
        this.ctrlPrdID.AutoSize = true;
        this.ctrlPrdID.Location = new System.Drawing.Point(39, 39);
        this.ctrlPrdID.Name = "ctrlPrdID";
        this.ctrlPrdID.PrdID = -1;
        this.ctrlPrdID.Size = new System.Drawing.Size(631, 30);
        this.ctrlPrdID.TabIndex = 19;
        // 
        // btnSearch
        // 
        this.btnSearch.Location = new System.Drawing.Point(18, 148);
        this.btnSearch.Name = "btnSearch";
        this.btnSearch.Size = new System.Drawing.Size(75, 23);
        this.btnSearch.TabIndex = 20;
        this.btnSearch.Text = "检索";
        this.btnSearch.UseVisualStyleBackColor = true;
        // 
        // ctrlSupplierID
        // 
        this.ctrlSupplierID.CompanyID = 1;
        this.ctrlSupplierID.Location = new System.Drawing.Point(108, 70);
        this.ctrlSupplierID.Name = "ctrlSupplierID";
        this.ctrlSupplierID.Size = new System.Drawing.Size(147, 23);
        this.ctrlSupplierID.TabIndex = 21;
        // 
        // FrmBuyReceiveItemSearch
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.AutoSize = true;
        this.ClientSize = new System.Drawing.Size(774, 175);
        this.Controls.Add(this.ctrlSupplierID);
        this.Controls.Add(this.btnSearch);
        this.Controls.Add(this.ctrlPrdID);
        this.Controls.Add(this.ckbCapPONo);
        this.Controls.Add(this.txtPONo);
        this.Controls.Add(this.ckbCapPrdID);
        this.Controls.Add(this.ckbCapCompanyID);
        this.Controls.Add(this.ckbCapNoteCode);
        this.Controls.Add(this.txtNoteCode);
        this.Controls.Add(this.ckbCapDateNote);
        this.Controls.Add(this.tpkDateNoteBegin);
        this.Controls.Add(this.lblCapDateNote);
        this.Controls.Add(this.tpkDateNoteEnd);
        this.Name = "FrmBuyReceiveItemSearch";
        this.Text = "检索窗体";
        this.ResumeLayout(false);
        this.PerformLayout();

	}
	#endregion
	private System.Windows.Forms.CheckBox ckbCapPONo;
    private System.Windows.Forms.TextBox txtPONo;
    private System.Windows.Forms.CheckBox ckbCapPrdID;
        private System.Windows.Forms.CheckBox ckbCapCompanyID;
	private System.Windows.Forms.CheckBox ckbCapNoteCode;
	private System.Windows.Forms.TextBox txtNoteCode;
	private System.Windows.Forms.CheckBox ckbCapDateNote;
	private System.Windows.Forms.DateTimePicker tpkDateNoteBegin;
	private System.Windows.Forms.Label lblCapDateNote;
	private System.Windows.Forms.DateTimePicker tpkDateNoteEnd;
    private JERPApp.Define.Material .CtrlProduct  ctrlPrdID;
    private System.Windows.Forms.Button btnSearch;
        private JERPApp.Define.General.CtrlSupplierForMaterial ctrlSupplierID;
    }
}
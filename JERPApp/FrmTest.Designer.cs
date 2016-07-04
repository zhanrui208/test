namespace JERPApp
{
    partial class FrmTest
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
            this.ctrlSaleDeliverDayRpt1 = new JERPApp.Store.Product.Report.CtrlSaleDeliverDayRpt();
            this.SuspendLayout();
            // 
            // ctrlSaleDeliverDayRpt1
            // 
            this.ctrlSaleDeliverDayRpt1.Location = new System.Drawing.Point(40, 89);
            this.ctrlSaleDeliverDayRpt1.Name = "ctrlSaleDeliverDayRpt1";
            this.ctrlSaleDeliverDayRpt1.Size = new System.Drawing.Size(592, 372);
            this.ctrlSaleDeliverDayRpt1.TabIndex = 0;
            // 
            // FrmTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 493);
            this.Controls.Add(this.ctrlSaleDeliverDayRpt1);
            this.Name = "FrmTest";
            this.Text = "FrmTest";
            this.ResumeLayout(false);

        }

        #endregion

        private Store.Product.Report.CtrlSaleDeliverDayRpt ctrlSaleDeliverDayRpt1;
    }
}
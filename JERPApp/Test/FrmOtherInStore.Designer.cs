namespace JERPApp.Test
{
    partial class FrmOtherInStore
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrlNoteSearch = new JCommon.ctrlNoteSearch();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.libTitle = new System.Windows.Forms.Label();
            this.mdgrdv = new JCommon.MyDataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbar = new JCommon.PagebreakNav();
            this.ColumnbtnDetail = new System.Windows.Forms.DataGridViewButtonColumn();
            this.ColuDateNote = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColProNO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColMemo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdgrdv)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ctrlNoteSearch);
            this.panel1.Controls.Add(this.linkLabel1);
            this.panel1.Controls.Add(this.libTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(992, 66);
            this.panel1.TabIndex = 0;
            // 
            // ctrlNoteSearch
            // 
            this.ctrlNoteSearch.DateNoteFieldName = "DateNote";
            this.ctrlNoteSearch.Location = new System.Drawing.Point(71, 32);
            this.ctrlNoteSearch.Name = "ctrlNoteSearch";
            this.ctrlNoteSearch.NoteCodeFieldName = "NoteCode";
            this.ctrlNoteSearch.Size = new System.Drawing.Size(550, 28);
            this.ctrlNoteSearch.TabIndex = 19;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(7, 43);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(53, 12);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "新增购单";
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
            // mdgrdv
            // 
            this.mdgrdv.AllowUserToAddRows = false;
            this.mdgrdv.AllowUserToDeleteRows = false;
            this.mdgrdv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mdgrdv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnbtnDetail,
            this.ColuDateNote,
            this.ColProNO,
            this.ColUnit,
            this.ColMemo});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.mdgrdv.DefaultCellStyle = dataGridViewCellStyle4;
            this.mdgrdv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mdgrdv.Location = new System.Drawing.Point(0, 66);
            this.mdgrdv.Name = "mdgrdv";
            this.mdgrdv.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.mdgrdv.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            this.mdgrdv.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.mdgrdv.RowTemplate.Height = 23;
            this.mdgrdv.Size = new System.Drawing.Size(992, 513);
            this.mdgrdv.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pbar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 579);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(992, 41);
            this.panel2.TabIndex = 2;
            // 
            // pbar
            // 
            this.pbar.AutoSize = true;
            this.pbar.Location = new System.Drawing.Point(11, 10);
            this.pbar.Name = "pbar";
            this.pbar.PageInBlock = 10;
            this.pbar.PageIndex = 0;
            this.pbar.PageSize = 30;
            this.pbar.Size = new System.Drawing.Size(406, 27);
            this.pbar.TabIndex = 2;
            // 
            // ColumnbtnDetail
            // 
            this.ColumnbtnDetail.HeaderText = "详情";
            this.ColumnbtnDetail.Name = "ColumnbtnDetail";
            this.ColumnbtnDetail.ReadOnly = true;
            this.ColumnbtnDetail.Text = "详情";
            this.ColumnbtnDetail.UseColumnTextForButtonValue = true;
            this.ColumnbtnDetail.Width = 60;
            // 
            // ColuDateNote
            // 
            this.ColuDateNote.DataPropertyName = "DateNote";
            this.ColuDateNote.HeaderText = "日期";
            this.ColuDateNote.Name = "ColuDateNote";
            this.ColuDateNote.ReadOnly = true;
            // 
            // ColProNO
            // 
            this.ColProNO.DataPropertyName = "NoteCode";
            this.ColProNO.HeaderText = "单据号";
            this.ColProNO.Name = "ColProNO";
            this.ColProNO.ReadOnly = true;
            // 
            // ColUnit
            // 
            this.ColUnit.DataPropertyName = "MakerPsn";
            this.ColUnit.HeaderText = "制单";
            this.ColUnit.Name = "ColUnit";
            this.ColUnit.ReadOnly = true;
            // 
            // ColMemo
            // 
            this.ColMemo.DataPropertyName = "Memo";
            this.ColMemo.HeaderText = "备注";
            this.ColMemo.Name = "ColMemo";
            this.ColMemo.ReadOnly = true;
            // 
            // FrmOtherInStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(992, 620);
            this.Controls.Add(this.mdgrdv);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "FrmOtherInStore";
            this.Text = "JERPApp";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mdgrdv)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label libTitle;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private JCommon.MyDataGridView mdgrdv;
        private System.Windows.Forms.Panel panel2;
        private JCommon.PagebreakNav pbar;
        private JCommon.ctrlNoteSearch ctrlNoteSearch;
        private System.Windows.Forms.DataGridViewButtonColumn ColumnbtnDetail;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColuDateNote;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColProNO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMemo;
    }
}
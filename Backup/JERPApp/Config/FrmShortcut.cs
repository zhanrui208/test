using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Config
{
    public partial class FrmShortcut : Form
    {
        public FrmShortcut(DataTable dtblTreeMenuf)
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accShortcutFrms = new JERPData.Frame.ShortcutForms();
            this.dgrdv.RowValidated += new DataGridViewCellEventHandler(dgrdv_RowValidated);
            this.FormClosed += new FormClosedEventHandler(FrmShortcut_FormClosed);
            this.dgrdv.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dgrdv_DataBindingComplete);
            this.LoadData(dtblTreeMenuf);

           
        }

        private  void LoadData(DataTable dtblTreeMenuf)
        {
            if (this.dtblShortcuts == null)
            {
                this.dtblShortcuts = dtblTreeMenuf.Copy();
                this.dtblShortcuts.Columns.Add("ModifierCode", typeof(string));
                this.dtblShortcuts.Columns.Add("KeyCode", typeof(string));
            }
            DataTable dtblShort = this.accShortcutFrms.GetDataShortcutFormsByUser(
                JERPBiz.Frame.UserBiz.UserID).Tables[0];
            foreach (DataRow drow in dtblShort.Rows)
            {
                int FormID = (int)drow["FormID"];
                DataRow[] rows =this.dtblShortcuts .Select("FormID=" + FormID.ToString()+" and NameSpace is not null");
                if (rows.Length > 0)
                {
                    rows[0]["ModifierCode"] = drow["ModifierCode"];
                    rows[0]["KeyCode"] = drow["KeyCode"];
                }
            }
            this.dgrdv.DataSource = this.dtblShortcuts;
        }

        

        void FrmShortcut_FormClosed(object sender, FormClosedEventArgs e)
        {
            ShortcutHelper.dtblUserShortFrms = this.accShortcutFrms.GetDataShortcutFormsByUser(JERPBiz.Frame.UserBiz.UserID).Tables[0];
        }
     
        private JERPData.Frame.ShortcutForms accShortcutFrms;
        private DataTable dtblShortcuts;        
    
        void dgrdv_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            for (int i = 0; i < this.dtblShortcuts.Rows.Count; i++)
            {
                bool flag = (this.dtblShortcuts.Rows[i]["NameSpace"] == DBNull.Value);
                this.dgrdv[this.ColumnModifierCode.Name, i].ReadOnly = flag;
                this.dgrdv[this.ColumnKeyCode.Name, i].ReadOnly = flag;
                
            }
        }
        void dgrdv_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            int irow = e.RowIndex;
            int icol = e.ColumnIndex;
            if ((irow == -1) || (icol == -1)) return;
            DataRow drow = this.dtblShortcuts.DefaultView[irow].Row;
            if (drow.RowState == DataRowState.Unchanged) return;
            bool flag = false;
            string errormsg = string.Empty;
            if (drow["FormID"] == DBNull.Value) return;
            if (drow["KeyCode"] == DBNull.Value)
            {
                flag = this.accShortcutFrms.DeleteShortcutForms(ref errormsg, JERPBiz.Frame.UserBiz.UserID,
                    drow["FormID"]);
            }
            else
            {
                flag = this.accShortcutFrms.SaveShortcutForms(ref errormsg, JERPBiz.Frame.UserBiz.UserID,
                    drow["FormID"],drow["ModifierCode"],drow["KeyCode"]);
            }
        }
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("正在生成打印文档，请稍候......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder  + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "系统快捷方式");
            int rowIndex = 3;
            int rowcount = this.dgrdv.Rows.Count;
            int columnCount = this.dgrdv.Columns.Count;

            //设置页头
            for (int i = 0; i < columnCount; i++)
            {

                excel.SetCellVal(rowIndex, i + 1, this.dgrdv.Columns[i].HeaderText);
            }
            for (int i = 0; i < rowcount; i++)
            {
                rowIndex++;
                for (int j = 0; j < columnCount; j++)
                {
                    excel.SetCellVal(rowIndex, j + 1, "'" + this.dgrdv[j, i].FormattedValue);
                }
            }

            excel.SetRangeInnerBorder(3, 1, rowIndex, columnCount);
            excel.SetRangeAutoFit(3, 1, rowIndex, columnCount, true, false);           
            excel.Show();
            FrmMsg.Hide();
        }
    }
}
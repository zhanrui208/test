using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Define.Technology
{
    public partial class CtrlDevelopScheduleOperForPrd : UserControl
    {
        public CtrlDevelopScheduleOperForPrd()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.dtpDateSchedule.Value = DateTime.Now.Date;
            this.accSchedule = new JERPData.Technology.DevelopSchedules();
            this.PrdEntity = new JERPBiz.Product.ProductEntity();
            this.btnAddItem.Click += new EventHandler(btnAddItem_Click);
            this.btnAddDate.Click += new EventHandler(btnAddDate_Click);
            this.dgrdv.UserDeletingRow += new DataGridViewRowCancelEventHandler(dgrdv_UserDeletingRow);
            this.btnExport .Click +=new EventHandler(btnExport_Click);
        }

      
        private int DataScheduleIndex = 5;//����������
        private int GridScheduleIndex = 3;
        private JERPData.Technology.DevelopSchedules accSchedule;
        private JERPBiz.Product.ProductEntity PrdEntity;
        private DataTable dtblSchedule;      
        private int PrdID=-1;
        public void ScheduleOper(int PrdID)
        {
            this.PrdID = PrdID ;
            this.PrdEntity .LoadData(PrdID);
            this.txtPrdCode.Text = this.PrdEntity.PrdCode;
            this.txtPrdName.Text = this.PrdEntity.PrdName;
            this.txtPrdSpec.Text = this.PrdEntity.PrdSpec;
            this.ctrlDevelopProcessID.LoadData(PrdID);
            this.dtblSchedule = this.accSchedule.GetDataDevelopSchedulesForPrdSetting(PrdID).Tables[0];
            while (this.dgrdv.Columns.Count > GridScheduleIndex)
            {
                this.dgrdv.Columns.RemoveAt(GridScheduleIndex);
            }
            DataGridViewCheckBoxColumn  ckbcol;
            string columnname = string.Empty;
            for (int j = DataScheduleIndex; j < this.dtblSchedule.Columns.Count; j++)
            {
                columnname = this.dtblSchedule.Columns[j].ColumnName;
                ckbcol = new DataGridViewCheckBoxColumn();
                ckbcol.DataPropertyName = columnname;
                ckbcol.Width = 66;
                ckbcol.HeaderText = columnname.Remove(0, 5);
                if (DateTime.Parse(columnname).DayOfWeek == 0)
                {
                    ckbcol.Width = 70;
                    ckbcol.HeaderText = ckbcol.HeaderText + "��";
                }
                this.dgrdv.Columns.Add(ckbcol);
            }
            this.dgrdv.DataSource = this.dtblSchedule;
        }

     

     
        void btnAddItem_Click(object sender, EventArgs e)
        {
            if (this.ctrlDevelopProcessID.DevelopProcessID == -1)
            {
                MessageBox.Show("���̲���Ϊ��");
                return;
            }


            DataRow[] drows = this.dtblSchedule.Select("DevelopProcessID=" + this.ctrlDevelopProcessID.DevelopProcessID.ToString() + " and " +
                " PsnID=" + this.ctrlPsnID.PsnID.ToString(), "", DataViewRowState.CurrentRows);
            if (drows.Length > 0)
            {
                MessageBox.Show("�Ѵ��ڵ�ǰ��");
                return;
            }
            DataRow drowNew = this.dtblSchedule.NewRow();
            drowNew["DevelopProcessID"] = this.ctrlDevelopProcessID.DevelopProcessID;
            drowNew["PrdStatus"] = this.ctrlDevelopProcessID.PrdStatus;
            drowNew["DateTarget"] = this.ctrlDevelopProcessID.DateTarget;
            drowNew["PsnID"] = this.ctrlPsnID.PsnID;
            drowNew["PsnName"] = this.ctrlPsnID.PsnName;           
            this.dtblSchedule.Rows.Add(drowNew);
        }
        void btnAddDate_Click(object sender, EventArgs e)
        {
            string columnname = string.Empty;
            string str_date = this.dtpDateSchedule.Value.ToShortDateString();
            for (int j = this.DataScheduleIndex; j < this.dtblSchedule.Columns.Count; j++)
            {
                columnname = this.dtblSchedule.Columns[j].ColumnName;
                if (DateTime.Parse(columnname).Date == this.dtpDateSchedule.Value.Date)
                {
                    MessageBox.Show("�������Ѽ���");
                    return;
                }
            }
            columnname = this.dtpDateSchedule.Value.ToShortDateString();
            this.dtblSchedule.Columns.Add(columnname, typeof(bool));
            DataGridViewCheckBoxColumn ckbcol = new DataGridViewCheckBoxColumn();
            ckbcol.DataPropertyName = columnname;
            ckbcol.Width = 66;
            ckbcol.HeaderText = columnname.Remove(0, 5);
            if (DateTime.Parse(columnname).DayOfWeek == 0)
            {
                ckbcol.Width = 70;
                ckbcol.HeaderText = ckbcol.HeaderText + "��";
            }
            this.dgrdv.Columns.Add(ckbcol);
        }
        void dgrdv_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            int irow = e.Row .Index;
            DataRow drow = this.dtblSchedule.DefaultView[irow].Row;
            if (drow != null)
            {
                string errormsg=string.Empty ;
                this.accSchedule.DeleteDevelopSchedules (ref errormsg,                  
                    drow["DevelopProcessID"],
                    drow["PsnID"]);
            }
        }
        public void Save()
        {
            string errormsg=string.Empty ;
            foreach (DataRow drow in this.dtblSchedule.Rows)
            {
                if (drow.RowState == DataRowState.Deleted) continue;
                if (drow.RowState == DataRowState.Unchanged) continue;          
                for (int j = this.DataScheduleIndex; j < this.dtblSchedule.Columns.Count; j++)
                {
                    if (drow[j] == DBNull.Value) continue;
                    this.accSchedule.SaveDevelopSchedules(ref errormsg,
                        drow["DevelopProcessID"],
                        drow["PsnID"],
                        this.dtblSchedule.Columns[j].ColumnName,
                        drow[j]);
                }
                drow.AcceptChanges();
            }
        }
     


        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��������");
            excel.SetCellVal("A2", "��Ʒ���:" + this.PrdEntity .PrdCode
                + "     ��Ʒ����:" + this.PrdEntity.PrdName
                + "     ��Ʒ���:" + this.PrdEntity.PrdSpec);           
            int rowIndex = 3;
            int colIndex = 1;
            excel.ImportGridData(dgrdv, ref rowIndex, ref colIndex, true, true);
            excel.SetRangeInnerBorder(3, 1, rowIndex, colIndex);
            for (int i = 4; i <=rowIndex; i++)
            {

                //��
                for (int j = this.GridScheduleIndex +2; j <= colIndex; j++)
                {
                    if (excel.GetCellVal(i, j).ToString() == "��")
                    {                     
                        excel.SetCellColor(Color.White, Color.Red, i, j);
                    } 
                    excel.SetCellVal(i, j, null);
                }
            }
            FrmMsg.Hide();
            excel.Show();
        }

    }
}

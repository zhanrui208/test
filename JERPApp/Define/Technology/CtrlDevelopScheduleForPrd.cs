using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace JERPApp.Define.Technology
{
    public partial class CtrlDevelopScheduleForPrd : UserControl
    {
        public CtrlDevelopScheduleForPrd()
        {
            InitializeComponent();
            this.dgrdv.AutoGenerateColumns = false;
            this.accSchedule = new JERPData.Technology.DevelopSchedules();
            this.PrdEntity = new JERPBiz.Product.ProductEntity();
            this.btnExport .Click +=new EventHandler(btnExport_Click);
        }

        private int DataScheduleIndex =5;//����������
        private int GridScheduleIndex = 3;
        private JERPData.Technology.DevelopSchedules accSchedule;
        private JERPBiz.Product .ProductEntity  PrdEntity;
        private DataTable dtblSchedule;      
        private int PrdID=-1;
        public void Schedule(int PrdID)
        {
            this.PrdID = PrdID;
            this.PrdEntity.LoadData(PrdID);           
            this.txtPrdCode.Text = this.PrdEntity.PrdCode;
            this.txtPrdName.Text = this.PrdEntity.PrdName;
            this.txtPrdSpec.Text = this.PrdEntity.PrdSpec;

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
            this.ctrlQFind.SeachGridView = this.dgrdv;
        }         
        void btnExport_Click(object sender, EventArgs e)
        {
            FrmMsg.Show("�������ɴ�ӡ�ĵ������Ժ�......");
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + @"GeneralShowSheet.xlt");
            excel.SetCellVal("D1", "��Ʒ��������");
            excel.SetCellVal("A2","      ��Ʒ���:"+this.PrdEntity .PrdCode 
                +"      ��Ʒ����:"+this.PrdEntity .PrdName
                +"      ��Ʒ���:" + this.PrdEntity.PrdSpec
                );
                  
            int rowIndex =3;
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

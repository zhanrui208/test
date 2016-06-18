using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Material
{
    public class OutStoreNotePrintHelper
    {
        public OutStoreNotePrintHelper()
        { 
            this.accFormatTitle = new JERPData.Material.OutStoreFieldTitle(); 
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "MtrOutStoreNote.xlt";
         
            
        } 
        private JERPData.Material.OutStoreFieldTitle accFormatTitle; 
        private string TmpFileName = string.Empty; 
        public void ExportToExcel(DataRow[] drowItems)
        {
            DataTable dtblColumns = this.accFormatTitle.GetDataOutStoreFieldTitle ().Tables[0];
            if (dtblColumns.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κζ�����ʽ");
                return;
            } 
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(this.TmpFileName); 
              
            int rowIndex = 3 ;
            if (drowItems.Length >1)
            {
                excel.InsertRows(rowIndex, drowItems.Length -1, true);
            }
           
            int No = 0;
            for (int irow = 0; irow <drowItems .Length ; irow++)
            {
                DataRow drow = drowItems[irow];
                No++;
                foreach (DataRow column in dtblColumns.Rows)
                {
                    string columnName = column["ColumnName"].ToString();
                    //���û�У����ù���
                    string value = string.Empty;
                    string[] values;
                    //���
                    if ((bool)column["SerialNoFlag"])
                    {
                        value = No.ToString();
                    }
                    else
                    {
                        if (column["Fieldlist"].ToString() == string.Empty) continue;
                        values = column["Fieldlist"].ToString().Split('+');
                        for (int i = 0; i < values.Length; i++)
                        {
                            if (i == 0)
                            {
                                value = drow[values[i]].ToString();
                            }
                            else
                            {
                                value += "\n" + drow[values[i]].ToString();
                            }
                        }
                    }
                    excel.SetCellVal(columnName + rowIndex.ToString(), value);
                }   
                rowIndex++;
            }
            excel.Printer = Config.ConfigInfo.GetNotePrinter();
            DialogResult rlt = MessageBox.Show("ֱ�Ӵ�ӡ��\n�ǣ�ȷ�ϣ���ȡ��!", "����ȷ��",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rlt == DialogResult.Yes)
            {
                excel.PrintCurSheet();
                excel.Dispose();
            }
            else
            {
                excel.Show();
            }
        }

    }

}

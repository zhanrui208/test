using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Material
{
    public class OutSrcLossSupplyNotePrintHelper
    {
        public OutSrcLossSupplyNotePrintHelper()
        {
            this.NoteEntity = new OutSrcLossSupplyNoteEntity();
            this.SupplierEntity = new JERPBiz.General.SupplierEntity();
            this.accFormat = new JERPData.Material.OutSrcLossSupplyFormat();
            this.accFormatTitle = new JERPData.Material.OutSrcLossSupplyFieldTitle();
            this.FormatEntity = new OutSrcLossSupplyFormatEntity();
            this.accItems = new JERPData.Material.OutSrcLossSupplyItems(); 
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "MtrOutSrcLossSupplyNote.xlt";
         
            
        }
        private JERPBiz.Material .OutSrcLossSupplyNoteEntity   NoteEntity;
        private JERPBiz.General.SupplierEntity SupplierEntity; 
        private JERPData.Material.OutSrcLossSupplyFormat accFormat;
        private JERPData.Material.OutSrcLossSupplyFieldTitle accFormatTitle;
        private JERPData.Material.OutSrcLossSupplyItems accItems; 
        private OutSrcLossSupplyFormatEntity FormatEntity;
        private string TmpFileName = string.Empty;
        
        public void ExportToExcel(long NoteID)
        {
            DataTable dtblFormat = this.accFormat.GetDataOutSrcLossSupplyFormat().Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κζ�����ʽ");
                return;
            }
            int FormatID=General .FrmFormat .GetFormatID (dtblFormat );          
            this.FormatEntity.LoadData(FormatID);
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(this.TmpFileName);
            this.NoteEntity.LoadData(NoteID); 
            this.SupplierEntity.LoadData(this.NoteEntity.CompanyID   );
            //�Ȱ��ܱߵĸ㶨
            for (int i = 1; i <= excel.GetSheetsCount(); i++)
            {
                if (excel.GetSheetName(i).Trim() != FormatEntity.TmpSheetName)
                {
                    excel.DeleteSheet(i);
                    i--;
                }
            }
            excel.SetCurSheet(FormatEntity.TmpSheetName);
            if (this.FormatEntity.NoteCodeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.NoteCodeCellName, this.NoteEntity.NoteCode);
            }
            if (this.FormatEntity.DateNoteCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName, this.NoteEntity.DateNote.ToShortDateString());
            }
            if (this.FormatEntity.SupplierCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.SupplierCellName, this.SupplierEntity.CompanyAllName  );
            }
            if (this.FormatEntity.SupplierAddressCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.SupplierAddressCellName, this.NoteEntity.SupplierAddress);
            }
            if (this.FormatEntity.LinkmanCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.LinkmanCellName, this.SupplierEntity.Linkman);
            }
            if (this.FormatEntity.TelephoneCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.TelephoneCellName, "'" + this.SupplierEntity.Telephone);
            }
            if (this.FormatEntity.FaxCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.FaxCellName, "'" + this.SupplierEntity.Fax);
            }
            if (this.FormatEntity.MakerPsnCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MakerPsnCellName, this.NoteEntity.MakerPsn);
            }     
            if (this.FormatEntity.MemoCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MemoCellName, this.NoteEntity.Memo);
            }
            //��ʼ����ϸ��ѽ
            DataTable dtblItems = this.accItems.GetDataOutSrcLossSupplyItemsByNoteID(NoteID).Tables[0];
           
            int rowIndex = this.FormatEntity.ItemRowIndex ;
            if (dtblItems.Rows.Count > this.FormatEntity.ItemRowCount)
            {
                excel.InsertRows(rowIndex, dtblItems.Rows.Count - this.FormatEntity.ItemRowCount , true);
            }
            DataTable dtblColumns = this.accFormatTitle .GetDataOutSrcLossSupplyFieldTitleByFormatID (FormatID).Tables[0];
           
            int No = 0;
            for (int irow = 0; irow < dtblItems.Rows.Count; irow++)
            {
                DataRow drow = dtblItems.Rows[irow];
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
            excel.Show();
        }

    }

}

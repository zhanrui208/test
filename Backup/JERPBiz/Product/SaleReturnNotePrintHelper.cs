using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Product
{
    public class SaleReturnNotePrintHelper
    {
        public SaleReturnNotePrintHelper()
        {
            this.NoteEntity = new SaleReturnNoteEntity();
            this.accItems = new JERPData.Product.SaleReturnItems();
            this.SupplierEntity = new JERPBiz.General.SupplierEntity();
            this.accFormat = new JERPData.Product.SaleReturnFormat();
            this.accFormatTitle = new JERPData.Product.SaleReturnFieldTitle();
            this.FormatEntity = new SaleReturnFormatEntity();
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "SaleReturnNote.xlt";
         
            
        }
        private JERPBiz.Product .SaleReturnNoteEntity   NoteEntity;
        private JERPData.Product.SaleReturnItems accItems;
        private JERPBiz.General.SupplierEntity SupplierEntity; 
        private JERPData.Product.SaleReturnFormat accFormat;
        private JERPData.Product.SaleReturnFieldTitle accFormatTitle;
        private SaleReturnFormatEntity FormatEntity;
        private string TmpFileName = string.Empty;
        
        public void ExportToExcel(long NoteID)
        {
            DataTable dtblFormat = this.accFormat.GetDataSaleReturnFormat().Tables[0];
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
            this.SupplierEntity.LoadData(this.NoteEntity.CompanyID);
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
            if (this.FormatEntity.CompanyNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName, this.SupplierEntity.CompanyAllName);
            }           
            if (this.FormatEntity.QCPsnCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.QCPsnCellName , this.NoteEntity.QCPsn );
            }
         
            if (this.FormatEntity.MakerPsnCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MakerPsnCellName, this.NoteEntity.MakerPsn);
            }
         
            if (this.FormatEntity.ReturnHandleTypeCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.ReturnHandleTypeCellName, this.NoteEntity.ReturnHandleTypeName );
            }
            if (this.FormatEntity.MoneyTypeCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MoneyTypeCellName,this.NoteEntity .MoneyTypeName );
            }
            if ((this.FormatEntity.TotalAMTCellName  != string.Empty)&&(this.NoteEntity .TotalAMT >0))
            {
                excel.SetCellVal(this.FormatEntity.TotalAMTCellName, this.NoteEntity .TotalAMT );
            }  
            if (this.FormatEntity.MemoCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MemoCellName, this.NoteEntity.Memo);
            }
            //��ʼ����ϸ��ѽ

            DataTable dtblItems = this.accItems.GetDataSaleReturnItemsByNoteID(NoteID).Tables[0];           
            int rowIndex = this.FormatEntity.ItemRowIndex ;
            if (dtblItems.Rows.Count > this.FormatEntity.ItemRowCount)
            {
                excel.InsertRows(rowIndex, dtblItems.Rows.Count - this.FormatEntity.ItemRowCount , true);
            }
            DataTable dtblColumns = this.accFormatTitle .GetDataSaleReturnFieldTitleByFormatID (FormatID).Tables[0];
           
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

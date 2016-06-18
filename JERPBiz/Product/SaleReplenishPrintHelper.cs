using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Product
{
    public class SaleReplenishPrintHelper
    {
        public SaleReplenishPrintHelper()
        {
            this.accFormat = new JERPData.Product.SaleReplenishFormat();
            this.accReplenishTitle = new JERPData.Product.SaleReplenishFieldTitle();
            this.accReplenishItems = new JERPData.Product.SaleReplenishItems();
            this.CustomerEntity = new JERPBiz.General.CustomerEntity();
            this.ReplenishNoteEntity = new SaleReplenishNoteEntity();
            this.DeliverAddressEntity = new JERPBiz.General.DeliverAddressEntity();
            this.FormatEntity = new SaleReplenishFormatEntity();
        }

        private JERPData.Product.SaleReplenishFormat accFormat;
        private JERPData.Product.SaleReplenishFieldTitle accReplenishTitle;
        private JERPData.Product.SaleReplenishItems accReplenishItems;
        private SaleReplenishNoteEntity ReplenishNoteEntity;
        private SaleReplenishFormatEntity FormatEntity;
        private JERPBiz.General.CustomerEntity CustomerEntity;
        private JERPBiz.General.DeliverAddressEntity DeliverAddressEntity;
        private Office2003Helper.Excel2003 excel;
  
        public void ExportToExcel(long  NoteID)
        {
            this.ReplenishNoteEntity.LoadData(NoteID);
            DataTable dtblFormat = this.accFormat.GetDataSaleReplenishFormatByCompanyID(this.ReplenishNoteEntity.CompanyID).Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κβ�����ʽ");
                return;
            }
            int FormatID = JERPBiz.General.FrmFormat.GetFormatID(dtblFormat);  
            this.FormatEntity.LoadData(FormatID);
            this.CustomerEntity.LoadData(this.ReplenishNoteEntity.CompanyID);
            this.DeliverAddressEntity.LoadData(this.ReplenishNoteEntity.DeliverAddressID);
            excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(JERPData.ServerParameter.TempletFolder + "SaleReplenishNote.xlt");
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
                excel.SetCellVal(this.FormatEntity.NoteCodeCellName, this.ReplenishNoteEntity.NoteCode);
            }
           
            if (this.FormatEntity.DateNoteCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName  , this.ReplenishNoteEntity.DateNote.ToShortDateString());
            }
            if (this.FormatEntity.CompanyNameCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName , this.CustomerEntity .CompanyAbbName);
            }
            if (this.FormatEntity.LinkmanCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.LinkmanCellName , "'" + this.DeliverAddressEntity .Linkman );
            }
            if (this.FormatEntity.TelephoneCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.TelephoneCellName , "'" + this.DeliverAddressEntity .Telephone );
            }
          
            if (this.FormatEntity.DeliverAddressCellName    != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DeliverAddressCellName  , this.DeliverAddressEntity.Address);
            }
            if (this.FormatEntity.DeliverPsnCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DeliverPsnCellName, this.ReplenishNoteEntity.DeliverPsn );
            }          
            if (this.FormatEntity.SalePsnCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.SalePsnCellName, this.ReplenishNoteEntity.SalePsn );
            }
            if (this.FormatEntity.MakerPsnCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MakerPsnCellName , this.ReplenishNoteEntity.MakerPsn );
            }
            if (this.FormatEntity.MemoCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MemoCellName, this.ReplenishNoteEntity.Memo);
            }

            //����һ�����������һҳ��Ҫcopy��ʽ
            DataTable dtblItems = this.accReplenishItems.GetDataSaleReplenishItemsByNoteID(NoteID).Tables[0];   
             DataTable dtblColumns = this.accReplenishTitle.GetDataSaleReplenishFieldTitleByFormatID(FormatID).Tables[0];
            int RowCount = dtblItems.Rows.Count;
            int ItemRowCount = this.FormatEntity.ItemRowCount;
            int rowIndex = this.FormatEntity.ItemRowIndex;
            if (RowCount > ItemRowCount)
            {
                excel.InsertRows(rowIndex, RowCount - ItemRowCount, true);
            }
            /////��ʼ����ϸ��ѽ           
            int No = 0;
            for (int irow = 0; irow <dtblItems .Rows .Count ; irow++)
            {
                DataRow drow = dtblItems.Rows[irow] ;             
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

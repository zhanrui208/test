using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Product
{
    public class RepairInvoicePrintHelper
    {
        public RepairInvoicePrintHelper()
        {
            this.accInvoice = new JERPData.Product.RepairInvoices();
            this.accFormat = new JERPData.Product.RepairInvoiceFormat();
            this.accInvoiceTitle = new JERPData.Product.RepairInvoiceFieldTitle();
            this.accDeliverItems = new JERPData.Product.RepairDeliverItems();
            this.custEntity = new JERPBiz.General.CustomerEntity();
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "RepairInvoice.xlt";
         
            
        }
        private JERPData.Product.RepairInvoiceFormat accFormat;
        private JERPData.Product.RepairInvoiceFieldTitle accInvoiceTitle;
        private JERPData.Product.RepairDeliverItems accDeliverItems;
        private JERPBiz.General.CustomerEntity custEntity;
        private JERPData.Product.RepairInvoices accInvoice;
        private RepairInvoiceEntity InvoiceEntity = new RepairInvoiceEntity();
        private RepairInvoiceFormatEntity FormatEntity = new RepairInvoiceFormatEntity();
        private string TmpFileName = string.Empty;     
      
       
        public void ExportToExcel(long[] InvoiceIDArray)
        {
            Office2003Helper.Excel2003  excel = new Office2003Helper.Excel2003();
            excel.New();
            for (int i = 0; i < InvoiceIDArray.Length; i++)
            {
                this.PrintExport(excel,InvoiceIDArray[i],i);
            }
            if (excel.GetSheetsCount() > 1)
            {
                excel.DeleteSheet(1);
            }

            excel.SetCurSheet(1);
            excel.Show();
        }
        private  void PrintExport(Office2003Helper.Excel2003  excel ,long InvoiceID,int SerialNo)
        {
            this.InvoiceEntity.LoadData(InvoiceID);
            DataTable dtblFormat = this.accFormat.GetDataRepairInvoiceFormatByCompanyID (this.InvoiceEntity.CompanyID).Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κη�Ʊ��ʽ");
                return;
            }
            int FormatID;
            if (dtblFormat.Rows.Count > 1)
            {
                FormatID=General.FrmFormat.GetFormatID(dtblFormat);
            }
            else
            {
                FormatID = (int)dtblFormat.Rows[0]["FormatID"];
            }
            this.FormatEntity.LoadData(FormatID);
            this.custEntity.LoadData(this.InvoiceEntity.CompanyID);
            if (excel.NewSheetByCopyOhterFileSheet (FormatEntity.TmpSheetName,this.TmpFileName)==false) return;
            excel.SetCurSheetName((SerialNo + 1).ToString() + InvoiceEntity.InvoiceCode);
            
            //Ū��ͷ
            if (this.FormatEntity.InvoiceNameCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.InvoiceNameCellName ,  this.InvoiceEntity.InvoiceName  );
            }
            //InvoiceCode
            if (this.FormatEntity.InvoiceCodeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.InvoiceCodeCellName, "'" + this.InvoiceEntity.InvoiceCode);
            }
            //�ͻ�����
            if (this.FormatEntity.CompanyNameCellName!= string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName , "'" + this.InvoiceEntity.CompanyAllName );
            }
            //�����
            if (this.FormatEntity.FinanceAddressCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.FinanceAddressCellName,  this.InvoiceEntity.FinanceAddress );
            }
            //����
            if (this.FormatEntity.MoneyTypeCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MoneyTypeCellName,  this.InvoiceEntity.MoneyTypeName );
            }
            //��Ʊ����
            if (this.FormatEntity.InvoiceTypeCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.InvoiceTypeCellName,  this.InvoiceEntity.InvoiceTypeName);
            }
            //����
            if (this.FormatEntity.DateNoteCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName , this.InvoiceEntity.DateNote );
            }
          
            //��ϵ��
            if (this.FormatEntity.LinkmanCellName    != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.LinkmanCellName , this.custEntity.Linkman );
            }
            //�绰
            if (this.FormatEntity.TelephoneCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.TelephoneCellName  ,"'"+ this.custEntity.Telephone );
            }  
            //�绰
            if (this.FormatEntity.FaxCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.FaxCellName ,"'"+ this.custEntity.Fax );
            }
            //��ʼ���ϼ�
            excel.SetCellVal(this.FormatEntity.TotalAMTCellName, string.Format("{0:#,##0.####}", this.InvoiceEntity .TotalAMT));
            excel.SetCellVal(this.FormatEntity.TaxAMTCellName , string.Format("{0:#,##0.####}", this.InvoiceEntity.TaxAMT));
            DataTable dtblColumns = this.accInvoiceTitle.GetDataRepairInvoiceFieldTitleByFormatID(FormatID).Tables[0];
            string outputlist = string.Empty;
            string grouplist=string.Empty ;
            DataTable dtblItems=this.accDeliverItems .GetDataRepairDeliverItemsByInvoiceID (InvoiceID ).Tables [0];
            if (this.FormatEntity.TotalQtyCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.TotalQtyCellName, dtblItems.Compute("SUM(Quantity)", ""));
            }
            if (this.FormatEntity.FieldGroupFlag)
            {
                foreach (DataRow drowColumn in dtblColumns.Rows)
                {
                    if (drowColumn["Fieldlist"].ToString() == string.Empty) continue;
                    if ((bool)drowColumn["SumFlag"])
                    {
                        outputlist += ",SUM(" + drowColumn["Fieldlist"].ToString() + ") as "+drowColumn["Fieldlist"].ToString();
                    }
                    if((bool)drowColumn ["GroupFlag"])
                    {
                        outputlist += "," + drowColumn["Fieldlist"].ToString();
                        grouplist += "," + drowColumn["Fieldlist"].ToString();
                    }
                    
                }
                outputlist = outputlist.Remove(0, 1);
                grouplist = grouplist.Remove(0, 1);
                dtblItems = this.accDeliverItems.GetDataRepairDeliverItemsGroupByInvoiceID(InvoiceID, outputlist, grouplist).Tables[0];
                
            }            
            int RowCount = dtblItems.Rows.Count;          
            if (RowCount >1)
            {
                excel.InsertRows(this.FormatEntity.ItemBeginRowIndex, RowCount-1, true);
            }
            int rowIndex = this.FormatEntity.ItemBeginRowIndex;
            int No = 0;
            string SortInfor = "NoteID";
            DataView dv = new DataView(dtblItems, "", SortInfor, DataViewRowState.CurrentRows);
            for (int irow = 0; irow <dv.Count; irow++)
            {
                DataRow drow = dv[irow].Row;              
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
        
        }
    }

}

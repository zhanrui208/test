using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Product
{
    public class ExpressReconciliationPrintHelper
    {
        public ExpressReconciliationPrintHelper()
        {
            this.accReconciliationFormat = new JERPData.Product.ExpressReconciliationFormat();
            this.accReconciliationTitle = new JERPData.Product.ExpressReconciliationFieldTitle();
            this.accReconconciliations = new JERPData.Product.ExpressReconciliations();
            this.accFormat = new JERPData.Product.ExpressReconciliationFormat();
            this.accRecords = new JERPData.Product.ExpressReceiptNotes();
            this.accSaleReceiptNote = new JERPData.Product.SaleReceiptNotes();
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "ExpressReconciliation.xlt";
            this.ReconciliationEntity = new ExpressReconciliationEntity();
            this.FormatEntity = new ExpressReconciliationFormatEntity();
        }

        private JERPData.Product.ExpressReconciliationFormat accReconciliationFormat;
        private JERPData.Product.ExpressReconciliationFieldTitle accReconciliationTitle;
        private JERPData.Product.SaleReceiptNotes accSaleReceiptNote;
        private JERPData.Product.ExpressReconciliations accReconconciliations;
        private JERPData.Product.ExpressReconciliationFormat accFormat;
        private JERPData.Product.ExpressReceiptNotes  accRecords;
        private ExpressReconciliationEntity ReconciliationEntity;
        private ExpressReconciliationFormatEntity FormatEntity;
        private string TmpFileName = string.Empty;
        public void ExportToExcel(long[] ReconciliationIDArray)
        {
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.New();
            for (int i = 0; i < ReconciliationIDArray.Length; i++)
            {
                this.PrintExport(excel, ReconciliationIDArray[i], i);
            }
            if (excel.GetSheetsCount() >1)
            {
                excel.DeleteSheet(1);
            }
            excel.SetCurSheet(1);
            excel.Show();
        }
        private void PrintExport(Office2003Helper.Excel2003 excel, long ReconciliationID, int SerialNo)
        {
            this.ReconciliationEntity.LoadData(ReconciliationID);
            DataTable dtblFormat = this.accFormat.GetDataExpressReconciliationFormat().Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κζ��˸�ʽ");
                return;
            }
            int FormatID = General.FrmFormat.GetFormatID(dtblFormat);          
            this.FormatEntity.LoadData(FormatID);
            if (excel.NewSheetByCopyOhterFileSheet(FormatEntity.TmpSheetName, this.TmpFileName) == false) return;
            excel.SetCurSheetName((SerialNo + 1).ToString() + ReconciliationEntity.MoneyTypeName + this.ReconciliationEntity.DateNote.Day.ToString());
           
            //Ū��ͷ
            //����
            if (this.FormatEntity.ReconciliationNameCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.ReconciliationNameCellName,this.ReconciliationEntity.ReconciliationName);
            }
            //���˵���
            if (this.FormatEntity.ReconciliationCodeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.ReconciliationCodeCellName, this.ReconciliationEntity.ReconciliationCode);
            }
            //������˾
            if (this.FormatEntity.CompanyNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName, this.ReconciliationEntity.CompanyName );
            }
            //����
            if (this.FormatEntity.DateNoteCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName , this.ReconciliationEntity .DateNote  );
            }
          
            //����
            if (this.FormatEntity.MoneyTypeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MoneyTypeCellName, this.ReconciliationEntity.MoneyTypeName);
            }

            //�ϼƽ��
            if (this.FormatEntity.TotalAMTCellName  != string.Empty)
            {
                if (this.ReconciliationEntity.TotalAMT  > 0)
                {
                    excel.SetCellVal(this.FormatEntity.TotalAMTCellName, this.ReconciliationEntity.TotalAMT );
                }
            }   
         
            //��ʼ����ϸ��ѽ
            //�ȸ㸶����ϸ
            int rowIndex = this.FormatEntity.RecordBeginRowIndex;
            if (this.FormatEntity.RecordBeginRowIndex >-1)
            {
                DataTable dtblRecord = this.accRecords.GetDataExpressReceiptNotesReconciliationSettleRecord(ReconciliationID).Tables[0];
                if (dtblRecord.Rows.Count > 1)
                {
                    excel.InsertRows(rowIndex, rowIndex, dtblRecord.Rows.Count - 1);
                }
                foreach (DataRow drow in dtblRecord.Rows)
                {
                    if (this.FormatEntity.RecordDateColumnName != string.Empty)
                    {
                        excel.SetCellVal(this.FormatEntity.RecordDateColumnName + rowIndex.ToString(), drow["DateNote"]);
                    }
                    if (this.FormatEntity.RecordCodeColumnName != string.Empty)
                    {
                        excel.SetCellVal(this.FormatEntity.RecordCodeColumnName + rowIndex.ToString(), drow["NoteCode"]);
                    }
                    if (this.FormatEntity.RecordPsnColumnName  != string.Empty)
                    {
                        excel.SetCellVal(this.FormatEntity.RecordPsnColumnName + rowIndex.ToString(), drow["SettlePsn"]);
                    }
                    if (this.FormatEntity.RecordAMTColumnName  != string.Empty)
                    {
                        excel.SetCellVal(this.FormatEntity.RecordAMTColumnName + rowIndex.ToString(), drow["TotalAMT"]);
                    }
                    rowIndex++;
                }
            }
            //�ͻ�
            DataTable dtblReciptNotes= this.accSaleReceiptNote.GetDataSaleReceiptNotesByExpressReconciliationID (ReconciliationID).Tables[0];
          
        
            DataTable dtblColumns = this.accReconciliationTitle.GetDataExpressReconciliationFieldTitleByFormatID(FormatID).Tables[0];
            rowIndex = this.FormatEntity.ItemBeginRowIndex;
            if (dtblReciptNotes.Rows.Count > 1)
            {
                excel.InsertRows(rowIndex, dtblReciptNotes.Rows.Count - 1, true);
            }
            int No = 0;
            for (int irow = 0; irow < dtblReciptNotes.Rows.Count; irow++)
            {
                DataRow drow = dtblReciptNotes.Rows[irow];
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

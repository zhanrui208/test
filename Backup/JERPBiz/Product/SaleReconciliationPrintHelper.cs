using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Product
{
    public class SaleReconciliationPrintHelper
    {
        public SaleReconciliationPrintHelper()
        {
            this.accReconciliationFormat = new JERPData.Product.SaleReconciliationFormat();
            this.accReconciliationTitle = new JERPData.Product.SaleReconciliationFieldTitle();
            this.accDeliverItems = new JERPData.Product.SaleDeliverItems();
            this.accRepairItems = new JERPData.Product.RepairDeliverItems();
            this.accReturnItems = new JERPData.Product.SaleReturnItems();
            this.accReconconciliations = new JERPData.Product.SaleReconciliations();
            this.accFormat = new JERPData.Product.SaleReconciliationFormat();
            this.accRecords = new JERPData.Product.SaleReceiptNotes();
            this.FinanceAddressEntity = new JERPBiz.General.FinanceAddressEntity();
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "SaleReconciliation.xlt";
            this.ReconciliationEntity = new SaleReconciliationEntity();
            this.FormatEntity = new SaleReconciliationFormatEntity();
            this.CustomerEntity = new JERPBiz.General.CustomerEntity();
        }

        private JERPData.Product.SaleReconciliationFormat accReconciliationFormat;
        private JERPData.Product.SaleReconciliationFieldTitle accReconciliationTitle;
        private JERPData.Product.SaleDeliverItems accDeliverItems;
        private JERPData.Product.RepairDeliverItems accRepairItems;
        private JERPData.Product.SaleReturnItems accReturnItems;
        private JERPData.Product.SaleReconciliations accReconconciliations;
        private JERPData.Product.SaleReconciliationFormat accFormat;
        private JERPData.Product.SaleReceiptNotes  accRecords;
        private SaleReconciliationEntity ReconciliationEntity;
        private SaleReconciliationFormatEntity FormatEntity;
        private JERPBiz.General.CustomerEntity CustomerEntity;
        private JERPBiz.General.FinanceAddressEntity FinanceAddressEntity;
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
            DataTable dtblFormat = this.accFormat.GetDataSaleReconciliationFormatByCompanyID  (this.ReconciliationEntity .CompanyID ).Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κζ��˸�ʽ");
                return;
            }
            int FormatID = General.FrmFormat.GetFormatID(dtblFormat);          
            this.FormatEntity.LoadData(FormatID);
            if (excel.NewSheetByCopyOhterFileSheet(FormatEntity.TmpSheetName, this.TmpFileName) == false) return;
            excel.SetCurSheetName((SerialNo + 1).ToString() + ReconciliationEntity.MoneyTypeName + this.ReconciliationEntity.DateNote.Day.ToString());
            this.CustomerEntity.LoadData(this.ReconciliationEntity.CompanyID);
            this.FinanceAddressEntity.LoadData(this.ReconciliationEntity.FinanceAddressID);
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
            //����
            if (this.FormatEntity.DateNoteCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName , this.ReconciliationEntity .DateNote  );
            }
            //������
            if (this.FormatEntity.DateSettleCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateSettleCellName , this.ReconciliationEntity.DateSettle );
            }
            //���ܶ����
            //�ͻ�����
            if (this.FormatEntity.CompanyNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName, "'" + this.CustomerEntity .CompanyAllName);
            }
          
            //�����
            if (this.FormatEntity.FinanceAddressCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.FinanceAddressCellName, "'" + this.FinanceAddressEntity.Address  );
            }
            //��ϵ��
            if (this.FormatEntity.LinkmanCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.LinkmanCellName, "'" + this.FinanceAddressEntity .Linkman);
            }
            //�绰
            if (this.FormatEntity.TelephoneCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.TelephoneCellName, "'" + this.FinanceAddressEntity .Telephone);
            }
            //����
            if (this.FormatEntity.FaxCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.FaxCellName, "'" + this.FinanceAddressEntity .Fax );
            } 
            //����
            if (this.FormatEntity.MoneyTypeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MoneyTypeCellName, this.ReconciliationEntity.MoneyTypeName);
            }

            //���㷽ʽ
            if (this.FormatEntity.SettleTypeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.SettleTypeCellName, this.ReconciliationEntity.SettleTypeName);
            }
         
            //�ͻ����
            if (this.FormatEntity.DeliverAMTCellName != string.Empty)
            {
                if (this.ReconciliationEntity.DeliverAMT > 0)
                {
                    excel.SetCellVal(this.FormatEntity.DeliverAMTCellName, this.ReconciliationEntity.DeliverAMT);
                }
            }
            //ά�޽��
            if (this.FormatEntity.RepairAMTCellName  != string.Empty)
            {
                if (this.ReconciliationEntity.RepairAMT  > 0)
                {
                    excel.SetCellVal(this.FormatEntity.RepairAMTCellName , this.ReconciliationEntity.RepairAMT );
                }
            }
            //�����˷�
            if (this.FormatEntity.ReplaceExpressAMTCellName  != string.Empty)
            {
                if (this.ReconciliationEntity.ReplaceExpressAMT  > 0)
                {
                    excel.SetCellVal(this.FormatEntity.ReplaceExpressAMTCellName, this.ReconciliationEntity.ReplaceExpressAMT);
                }
            }
            //�˻����
            if (this.FormatEntity.ReturnAMTCellName != string.Empty)
            {
                if (this.ReconciliationEntity.ReturnAMT > 0)
                {
                    excel.SetCellVal(this.FormatEntity.ReturnAMTCellName, this.ReconciliationEntity.ReturnAMT);
                }
            }          
           
            //�ۿ���
            if (this.FormatEntity.FineAMTCellName != string.Empty)
            {
                if (this.ReconciliationEntity.FineAMT > 0)
                {
                    excel.SetCellVal(this.FormatEntity.FineAMTCellName, this.ReconciliationEntity.FineAMT);
                }
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
                DataTable dtblRecord = this.accRecords.GetDataSaleReceiptNotesReconciliationSettleRecord(ReconciliationID).Tables[0];
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
            DataTable dtblDeliverItems = this.accDeliverItems.GetDataSaleDeliverItemsByReconciliationID(ReconciliationID).Tables[0];
            dtblDeliverItems.Columns.Add("TypeID", typeof(int));
            foreach (DataRow drow in dtblDeliverItems.Rows)
            {
                drow["TypeID"] = 1;
            }
            //ά��
            DataTable dtblRepairItems = this.accRepairItems.GetDataRepairDeliverItemsByReconciliationID (ReconciliationID).Tables[0];
            dtblRepairItems.Columns.Add("TypeID", typeof(int));
            foreach (DataRow drow in dtblRepairItems.Rows)
            {
                drow["TypeID"] = 2;
            }
            //�˻�
            DataTable dtblReturnItems = this.accReturnItems.GetDataSaleReturnItemsByReconciliationID(ReconciliationID).Tables[0];
            dtblReturnItems.Columns.Add("TypeID", typeof(int));
            foreach (DataRow drow in dtblReturnItems.Rows)
            {
                drow["TypeID"] = 3;
                drow["ItemAMT"] = -(decimal)drow["ItemAMT"];
            }
            dtblDeliverItems.Merge(dtblRepairItems); 
            dtblDeliverItems.Merge(dtblReturnItems);
            if (this.FormatEntity.TotalQtyCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.TotalQtyCellName, dtblDeliverItems.Compute("SUM(Quantity)", ""));
            }
            //�ͻ�
            string sort = "DateNote,TypeID,NoteID";
            DataView dv = new DataView(dtblDeliverItems , "", sort, DataViewRowState.CurrentRows);
            DataTable dtblColumns = this.accReconciliationTitle.GetDataSaleReconciliationFieldTitleByFormatID(FormatID).Tables[0];
            rowIndex = this.FormatEntity.ItemBeginRowIndex;
            if (dtblDeliverItems.Rows.Count > 1)
            {
                excel.InsertRows(rowIndex, dtblDeliverItems.Rows.Count - 1, true);
            }
            int No = 0;
           
            for (int irow = 0; irow < dv.Count; irow++)
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

using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Product
{
    public class ExpressReceiptPrintHelper
    {
        public ExpressReceiptPrintHelper()
        {
            this.accFormat = new JERPData.Product.ExpressReceiptFormat();
            this.ReceiptNoteEntity = new ExpressReceiptNoteEntity();
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "ExpressReceiptNote.xlt";
            this.FormatEntity = new ExpressReceiptFormatEntity();
            
        }
        private JERPData.Product.ExpressReceiptFormat accFormat;
        private ExpressReceiptNoteEntity ReceiptNoteEntity;
        private ExpressReceiptFormatEntity FormatEntity;
        private string TmpFileName = string.Empty;
        private Office2003Helper.Excel2003 excel;
        public void ExportToExcel(long NoteID)
        {
         
            DataTable dtblFormat = this.accFormat.GetDataExpressReceiptFormat ().Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κ��ͻ���ʽ");
                return;
            }
            int FormatID = General.FrmFormat.GetFormatID(dtblFormat);
            this.FormatEntity.LoadData(FormatID);        
            excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(this.TmpFileName);
            for (int i = 1; i <= excel.GetSheetsCount(); i++)
            {
                if (excel.GetSheetName(i).Trim() != FormatEntity.TmpSheetName)
                {
                    excel.DeleteSheet(i);
                    i--;
                }
            }
            excel.SetCurSheet(FormatEntity.TmpSheetName);
            this.ReceiptNoteEntity.LoadData(NoteID);
            if (this.FormatEntity.NoteCodeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.NoteCodeCellName, this.ReceiptNoteEntity.NoteCode);
            }

            if (this.FormatEntity.DateNoteCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName, this.ReceiptNoteEntity.DateNote);
            }
          
            if (this.FormatEntity.CompanyNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName, this.ReceiptNoteEntity.CompanyName);
            }
            if (this.FormatEntity.MoneyTypeCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MoneyTypeCellName, "'" + this.ReceiptNoteEntity.MoneyTypeName);
            }  
            if (this.FormatEntity.ReconciliationCodeCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.ReconciliationCodeCellName, "'"+this.ReceiptNoteEntity.ReconciliationCode  );
            }
            if (this.FormatEntity.AmountCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.AmountCellName , this.ReceiptNoteEntity.Amount);
            }           
            if (this.FormatEntity.MakerPsnCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MakerPsnCellName, this.ReceiptNoteEntity.MakerPsn );
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
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
namespace JERPBiz.Manufacture
{
    public class WorkingSheetPrintHelper
    {
        public WorkingSheetPrintHelper()
        {
            this.WrkNoteEntity = new WorkingSheetEntity(); 
            this.accFormat = new JERPData.Manufacture.WorkingSheetFormat();
            this.accFormatTitle = new JERPData.Manufacture.WorkingSheetFieldTitle();
            this.FormatEntity = new WorkingSheetFormatEntity();
            this.accParmValues = new JERPData.Product.ParmValues();
            this.accSchedules = new JERPData.Manufacture.ManufSchedules();
            this.TmpFileName = JERPData.ServerParameter.TempletFolder + "WorkingSheet.xlt";
         
            
        }
        private JERPBiz.Manufacture .WorkingSheetEntity    WrkNoteEntity; 
        private JERPData.Manufacture.ManufSchedules accSchedules;
        private JERPData.Manufacture.WorkingSheetFormat accFormat;
        private JERPData.Manufacture.WorkingSheetFieldTitle accFormatTitle;
        private JERPData.Product.ParmValues accParmValues;
        private WorkingSheetFormatEntity FormatEntity;
        private string TmpFileName = string.Empty;
        public void ExportToExcel(long WorkingSheetID)
        {
            DataTable dtblFormat = this.accFormat.GetDataWorkingSheetFormat ().Tables[0];
            if (dtblFormat.Rows.Count == 0)
            {
                MessageBox.Show("�Բ���δ�����κζ�����ʽ");
                return;
            }
            int FormatID = General.FrmFormat.GetFormatID(dtblFormat);          
            this.FormatEntity.LoadData(FormatID);
            Office2003Helper.Excel2003 excel = new Office2003Helper.Excel2003();
            excel.NewFromTemp(this.TmpFileName);
            this.WrkNoteEntity.LoadData(WorkingSheetID);         
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
            if (this.FormatEntity.WorkingSheetCodeCellName!= string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.WorkingSheetCodeCellName  , this.WrkNoteEntity.WorkingSheetCode );
            }
            if (this.FormatEntity.DateNoteCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateNoteCellName, this.WrkNoteEntity.DateNote.ToShortDateString());
            }
         
            if (this.FormatEntity.CompanyNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.CompanyNameCellName,"'"+ this.WrkNoteEntity.CompanyCode);
            }         
            if (this.FormatEntity.PrdCodeCellName   != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.PrdCodeCellName ,"'"+ this.WrkNoteEntity.PrdCode );
            }
            if (this.FormatEntity.PrdNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.PrdNameCellName, "'" + this.WrkNoteEntity.PrdName);
            } 
            if (this.FormatEntity.ModelCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.ModelCellName, "'" + this.WrkNoteEntity.Model);
            }
            if (this.FormatEntity.PrdSpecCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.PrdSpecCellName, "'" + this.WrkNoteEntity.PrdSpec);
            }        
            if (this.FormatEntity.ManufImgCellName != string.Empty)
            {
                //ͼƬ·��
                string dir = JERPData.ServerParameter.ERPFileFolder + @"\Engineer\ManufImg\" + this.WrkNoteEntity .PrdID .ToString ();
                if (System.IO.Directory.Exists(dir))
                {
                    string[] AllFiles = System.IO.Directory.GetFiles(dir);
                    if (AllFiles.Length > 0)
                    {
                        excel.InsertPicture(this.FormatEntity.ManufImgCellName,
                            AllFiles[0]);
                    }
                }
            }
            if (this.FormatEntity.QuantityCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.QuantityCellName, this.WrkNoteEntity.Quantity);
            }
            if (this.FormatEntity.UnitNameCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.UnitNameCellName, this.WrkNoteEntity.UnitName);
            }
            if (this.FormatEntity.DateTargetCellName  != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.DateTargetCellName, this.WrkNoteEntity.DateTarget );
            }
            if (this.FormatEntity.StoreTypeCellName != string.Empty)
            {
                string StoreType = string.Empty;
                if (this.WrkNoteEntity.PrdStoreFlag)
                {
                    StoreType = "��Ʒ��";
                }
                if (this.WrkNoteEntity.MtrStoreFlag)
                {
                    StoreType = "ԭ�ϲ�";
                }
                excel.SetCellVal(this.FormatEntity.StoreTypeCellName , StoreType );
            } 
            if (this.FormatEntity.MakerPsnCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MakerPsnCellName, this.WrkNoteEntity.MakerPsn);
            }
            
            if (this.FormatEntity.MemoCellName != string.Empty)
            {
                excel.SetCellVal(this.FormatEntity.MemoCellName, this.WrkNoteEntity.Memo);
            }
            if (this.FormatEntity.ParmsCellName != string.Empty)
            {
                //��������
                DataTable dtblParmValues = this.accParmValues.GetDataParmValuesByPrdID(
                    this.WrkNoteEntity .PrdID).Tables[0];
                string Parms = string.Empty;
                foreach (DataRow drow in dtblParmValues.Rows)
                {
                    Parms += @"
            " + drow["ParmTypeName"] + ":" + drow["ParmValue"].ToString();
                }
                if (Parms != string.Empty)
                {
                    Parms = Parms.Remove(0, 2);
                }
                excel.SetCellVal(this.FormatEntity .ParmsCellName, Parms);
            }
        
            //��ʼ����ϸ��ѽ

            DataTable dtblManufSchedules = this.accSchedules .GetDataManufSchedulesByWorkingSheetID  (WorkingSheetID ).Tables[0];
            int rowIndex = this.FormatEntity.ItemRowIndex ;
            if (dtblManufSchedules.Rows.Count > this.FormatEntity.ItemRowCount)
            {
                excel.InsertRows(rowIndex, dtblManufSchedules.Rows.Count - this.FormatEntity.ItemRowCount , true);
            }
            DataTable dtblColumns = this.accFormatTitle.GetDataWorkingSheetFieldTitleByFormatID  (FormatID).Tables[0];
            int No = 0;
            for (int irow = 0; irow < dtblManufSchedules.Rows.Count; irow++)
            {
                DataRow drow = dtblManufSchedules.Rows[irow];
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

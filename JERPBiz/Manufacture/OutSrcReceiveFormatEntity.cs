/*
$Header$
$Author$
$Date$
$Revision$
*/
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
namespace JERPBiz.Manufacture
{
    /// <����>
    /// ��[OutSrcReceiveFormat]����ʵ����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/7/19 14:12:47
    ///</ʱ��>  
    public class OutSrcReceiveFormatEntity
    {
        public OutSrcReceiveFormatEntity()
        {
            this.accData = new JERPData.Manufacture.OutSrcReceiveFormat();
        }
        private JERPData.Manufacture.OutSrcReceiveFormat accData;
        public int FormatID = -1;
        public string TmpSheetName = string.Empty;
        public string NoteCodeCellName = string.Empty;
        public string DateNoteCellName = string.Empty;
        public string PONoCellName = string.Empty;
        public string CompanyNameCellName = string.Empty;
        public string SettleTypeCellName = string.Empty;
        public string MoneyTypeCellName = string.Empty;
        public string PriceTypeCellName = string.Empty;
        public string InvoiceTypeCellName = string.Empty;
        public string TotalAMTCellName = string.Empty;
        public string QCPsnCellName = string.Empty;
        public string MakerPsnCellName = string.Empty;
        public string MemoCellName = string.Empty;
        public int ItemRowIndex = -1;
        public int ItemRowCount = -1;
        public void LoadData(int FormatID)
        {
            this.FormatID = FormatID;
            DataTable dtbl = this.accData.GetDataOutSrcReceiveFormatByFormatID(FormatID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.TmpSheetName = string.Empty;
                this.NoteCodeCellName = string.Empty;
                this.DateNoteCellName = string.Empty;
                this.PONoCellName = string.Empty;
                this.CompanyNameCellName = string.Empty;
                this.SettleTypeCellName = string.Empty;
                this.MoneyTypeCellName = string.Empty;
                this.PriceTypeCellName = string.Empty;
                this.InvoiceTypeCellName = string.Empty;
                this.TotalAMTCellName = string.Empty;
                this.QCPsnCellName = string.Empty;
                this.MakerPsnCellName = string.Empty;
                this.MemoCellName = string.Empty;
                this.ItemRowIndex = -1;
                this.ItemRowCount = -1;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["TmpSheetName"] == DBNull.Value)
            {
                this.TmpSheetName = string.Empty;
            }
            else
            {
                this.TmpSheetName = drow["TmpSheetName"].ToString();
            }
            if (drow["NoteCodeCellName"] == DBNull.Value)
            {
                this.NoteCodeCellName = string.Empty;
            }
            else
            {
                this.NoteCodeCellName = drow["NoteCodeCellName"].ToString();
            }
            if (drow["DateNoteCellName"] == DBNull.Value)
            {
                this.DateNoteCellName = string.Empty;
            }
            else
            {
                this.DateNoteCellName = drow["DateNoteCellName"].ToString();
            }
            if (drow["PONoCellName"] == DBNull.Value)
            {
                this.PONoCellName = string.Empty;
            }
            else
            {
                this.PONoCellName = drow["PONoCellName"].ToString();
            }
            if (drow["CompanyNameCellName"] == DBNull.Value)
            {
                this.CompanyNameCellName = string.Empty;
            }
            else
            {
                this.CompanyNameCellName = drow["CompanyNameCellName"].ToString();
            }
            if (drow["SettleTypeCellName"] == DBNull.Value)
            {
                this.SettleTypeCellName = string.Empty;
            }
            else
            {
                this.SettleTypeCellName = drow["SettleTypeCellName"].ToString();
            }
            if (drow["MoneyTypeCellName"] == DBNull.Value)
            {
                this.MoneyTypeCellName = string.Empty;
            }
            else
            {
                this.MoneyTypeCellName = drow["MoneyTypeCellName"].ToString();
            }
            if (drow["PriceTypeCellName"] == DBNull.Value)
            {
                this.PriceTypeCellName = string.Empty;
            }
            else
            {
                this.PriceTypeCellName = drow["PriceTypeCellName"].ToString();
            }
            if (drow["InvoiceTypeCellName"] == DBNull.Value)
            {
                this.InvoiceTypeCellName = string.Empty;
            }
            else
            {
                this.InvoiceTypeCellName = drow["InvoiceTypeCellName"].ToString();
            }
            if (drow["TotalAMTCellName"] == DBNull.Value)
            {
                this.TotalAMTCellName = string.Empty;
            }
            else
            {
                this.TotalAMTCellName = drow["TotalAMTCellName"].ToString();
            }
            if (drow["QCPsnCellName"] == DBNull.Value)
            {
                this.QCPsnCellName = string.Empty;
            }
            else
            {
                this.QCPsnCellName = drow["QCPsnCellName"].ToString();
            }
            if (drow["MakerPsnCellName"] == DBNull.Value)
            {
                this.MakerPsnCellName = string.Empty;
            }
            else
            {
                this.MakerPsnCellName = drow["MakerPsnCellName"].ToString();
            }
            if (drow["MemoCellName"] == DBNull.Value)
            {
                this.MemoCellName = string.Empty;
            }
            else
            {
                this.MemoCellName = drow["MemoCellName"].ToString();
            }
            if (drow["ItemRowIndex"] == DBNull.Value)
            {
                this.ItemRowIndex = -1;
            }
            else
            {
                this.ItemRowIndex = (int)drow["ItemRowIndex"];
            }
            if (drow["ItemRowCount"] == DBNull.Value)
            {
                this.ItemRowCount = -1;
            }
            else
            {
                this.ItemRowCount = (int)drow["ItemRowCount"];
            }
        }
    }
}
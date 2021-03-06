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
namespace JERPBiz.Product
{
    /// <描述>
    /// 表[SaleOrderFormat]数据实体类
    ///</描述> 
    ///<作者> 
    /// 金优富
    ///</作者> 
    ///<时间> 
    /// 2015/7/21 21:15:33
    ///</时间>  
    public class SaleOrderFormatEntity
    {
        public SaleOrderFormatEntity()
        {
            this.accData = new JERPData.Product.SaleOrderFormat();
        }
        private JERPData.Product.SaleOrderFormat accData;
        public int FormatID = -1;
        public string TmpSheetName = string.Empty;
        public string NoteCodeCellName = string.Empty;
        public string PONoCellName = string.Empty;
        public string DateNoteCellName = string.Empty;
        public string CompanyNameCellName = string.Empty;
        public string LegalPersonCellName = string.Empty;
        public string LinkmanCellName = string.Empty;
        public string TelephoneCellName = string.Empty;
        public string FaxCellName = string.Empty;
        public string DeliverAddressCellName = string.Empty;
        public string FinanceAddressCellName = string.Empty;
        public string MakerPsnCellName = string.Empty;
        public string SalePsnCellName = string.Empty;
        public string DeliverTypeCellName = string.Empty;
        public string ExpressRequireCellName = string.Empty;
        public string MoneyTypeCellName = string.Empty;
        public string SettleTypeCellName = string.Empty;
        public string PriceTypeCellName = string.Empty;
        public string InvoiceTypeCellName = string.Empty;
        public string MemoCellName = string.Empty;
        public string TotalAMTCellName = string.Empty;
        public int ItemRowIndex = -1;
        public int ItemRowCount = -1;
        public void LoadData(int FormatID)
        {
            this.FormatID = FormatID;
            DataTable dtbl = this.accData.GetDataSaleOrderFormatByFormatID(FormatID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.TmpSheetName = string.Empty;
                this.NoteCodeCellName = string.Empty;
                this.PONoCellName = string.Empty;
                this.DateNoteCellName = string.Empty;
                this.CompanyNameCellName = string.Empty;
                this.LegalPersonCellName = string.Empty;
                this.LinkmanCellName = string.Empty;
                this.TelephoneCellName = string.Empty;
                this.FaxCellName = string.Empty;
                this.DeliverAddressCellName = string.Empty;
                this.FinanceAddressCellName = string.Empty;
                this.MakerPsnCellName = string.Empty;
                this.SalePsnCellName = string.Empty;
                this.DeliverTypeCellName = string.Empty;
                this.ExpressRequireCellName = string.Empty;
                this.MoneyTypeCellName = string.Empty;
                this.SettleTypeCellName = string.Empty;
                this.PriceTypeCellName = string.Empty;
                this.InvoiceTypeCellName = string.Empty;
                this.MemoCellName = string.Empty;
                this.TotalAMTCellName = string.Empty;
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
            if (drow["PONoCellName"] == DBNull.Value)
            {
                this.PONoCellName = string.Empty;
            }
            else
            {
                this.PONoCellName = drow["PONoCellName"].ToString();
            }
            if (drow["DateNoteCellName"] == DBNull.Value)
            {
                this.DateNoteCellName = string.Empty;
            }
            else
            {
                this.DateNoteCellName = drow["DateNoteCellName"].ToString();
            }
            if (drow["CompanyNameCellName"] == DBNull.Value)
            {
                this.CompanyNameCellName = string.Empty;
            }
            else
            {
                this.CompanyNameCellName = drow["CompanyNameCellName"].ToString();
            }
            if (drow["LegalPersonCellName"] == DBNull.Value)
            {
                this.LegalPersonCellName = string.Empty;
            }
            else
            {
                this.LegalPersonCellName = drow["LegalPersonCellName"].ToString();
            }
            if (drow["LinkmanCellName"] == DBNull.Value)
            {
                this.LinkmanCellName = string.Empty;
            }
            else
            {
                this.LinkmanCellName = drow["LinkmanCellName"].ToString();
            }
            if (drow["TelephoneCellName"] == DBNull.Value)
            {
                this.TelephoneCellName = string.Empty;
            }
            else
            {
                this.TelephoneCellName = drow["TelephoneCellName"].ToString();
            }
            if (drow["FaxCellName"] == DBNull.Value)
            {
                this.FaxCellName = string.Empty;
            }
            else
            {
                this.FaxCellName = drow["FaxCellName"].ToString();
            }
            if (drow["DeliverAddressCellName"] == DBNull.Value)
            {
                this.DeliverAddressCellName = string.Empty;
            }
            else
            {
                this.DeliverAddressCellName = drow["DeliverAddressCellName"].ToString();
            }
            if (drow["FinanceAddressCellName"] == DBNull.Value)
            {
                this.FinanceAddressCellName = string.Empty;
            }
            else
            {
                this.FinanceAddressCellName = drow["FinanceAddressCellName"].ToString();
            }
            if (drow["MakerPsnCellName"] == DBNull.Value)
            {
                this.MakerPsnCellName = string.Empty;
            }
            else
            {
                this.MakerPsnCellName = drow["MakerPsnCellName"].ToString();
            }
            if (drow["SalePsnCellName"] == DBNull.Value)
            {
                this.SalePsnCellName = string.Empty;
            }
            else
            {
                this.SalePsnCellName = drow["SalePsnCellName"].ToString();
            }
            if (drow["DeliverTypeCellName"] == DBNull.Value)
            {
                this.DeliverTypeCellName = string.Empty;
            }
            else
            {
                this.DeliverTypeCellName = drow["DeliverTypeCellName"].ToString();
            }
            if (drow["ExpressRequireCellName"] == DBNull.Value)
            {
                this.ExpressRequireCellName = string.Empty;
            }
            else
            {
                this.ExpressRequireCellName = drow["ExpressRequireCellName"].ToString();
            }
            if (drow["MoneyTypeCellName"] == DBNull.Value)
            {
                this.MoneyTypeCellName = string.Empty;
            }
            else
            {
                this.MoneyTypeCellName = drow["MoneyTypeCellName"].ToString();
            }
            if (drow["SettleTypeCellName"] == DBNull.Value)
            {
                this.SettleTypeCellName = string.Empty;
            }
            else
            {
                this.SettleTypeCellName = drow["SettleTypeCellName"].ToString();
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
            if (drow["MemoCellName"] == DBNull.Value)
            {
                this.MemoCellName = string.Empty;
            }
            else
            {
                this.MemoCellName = drow["MemoCellName"].ToString();
            }
            if (drow["TotalAMTCellName"] == DBNull.Value)
            {
                this.TotalAMTCellName = string.Empty;
            }
            else
            {
                this.TotalAMTCellName = drow["TotalAMTCellName"].ToString();
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
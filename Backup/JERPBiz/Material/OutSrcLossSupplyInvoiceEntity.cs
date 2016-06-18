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
namespace JERPBiz.Material
{
    /// <����>
    /// ��[OutSrcLossSupplyInvoices]����ʵ����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2016/5/8 9:42:14
    ///</ʱ��>  
    public class OutSrcLossSupplyInvoiceEntity
    {
        public OutSrcLossSupplyInvoiceEntity()
        {
            this.accData = new JERPData.Material.OutSrcLossSupplyInvoices();
        }
        private JERPData.Material.OutSrcLossSupplyInvoices accData;
        public long InvoiceID = -1;
        public string InvoiceCode = string.Empty;
        public string InvoiceName = string.Empty;
        public int Year = -1;
        public int Month = -1;
        public DateTime DateNote = DateTime.MinValue;
        public int CompanyID = -1;
        public int MoneyTypeID = -1;
        public decimal TotalAMT = 0;
        public int MakerPsnID = -1;
        public int ConfirmPsnID = -1;
        public string CompanyAbbName = string.Empty;
        public string CompanyAllName = string.Empty;
        public string MoneyTypeName = string.Empty;
        public string MakerPsn = string.Empty;
        public string ConfirmPsn = string.Empty;
        public void LoadData(long InvoiceID)
        {
            this.InvoiceID = InvoiceID;
            DataTable dtbl = this.accData.GetDataOutSrcLossSupplyInvoicesByInvoiceID(InvoiceID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.InvoiceCode = string.Empty;
                this.InvoiceName = string.Empty;
                this.Year = -1;
                this.Month = -1;
                this.DateNote = DateTime.MinValue;
                this.CompanyID = -1;
                this.MoneyTypeID = -1;
                this.TotalAMT = 0;
                this.MakerPsnID = -1;
                this.ConfirmPsnID = -1;
                this.CompanyAbbName = string.Empty;
                this.CompanyAllName = string.Empty;
                this.MoneyTypeName = string.Empty;
                this.MakerPsn = string.Empty;
                this.ConfirmPsn = string.Empty;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["InvoiceCode"] == DBNull.Value)
            {
                this.InvoiceCode = string.Empty;
            }
            else
            {
                this.InvoiceCode = drow["InvoiceCode"].ToString();
            }
            if (drow["InvoiceName"] == DBNull.Value)
            {
                this.InvoiceName = string.Empty;
            }
            else
            {
                this.InvoiceName = drow["InvoiceName"].ToString();
            }
            if (drow["Year"] == DBNull.Value)
            {
                this.Year = -1;
            }
            else
            {
                this.Year = (int)drow["Year"];
            }
            if (drow["Month"] == DBNull.Value)
            {
                this.Month = -1;
            }
            else
            {
                this.Month = (int)drow["Month"];
            }
            if (drow["DateNote"] == DBNull.Value)
            {
                this.DateNote = DateTime.MinValue;
            }
            else
            {
                this.DateNote = (DateTime)drow["DateNote"];
            }
            if (drow["CompanyID"] == DBNull.Value)
            {
                this.CompanyID = -1;
            }
            else
            {
                this.CompanyID = (int)drow["CompanyID"];
            }
            if (drow["MoneyTypeID"] == DBNull.Value)
            {
                this.MoneyTypeID = -1;
            }
            else
            {
                this.MoneyTypeID = (int)drow["MoneyTypeID"];
            }
            if (drow["TotalAMT"] == DBNull.Value)
            {
                this.TotalAMT = 0;
            }
            else
            {
                this.TotalAMT = (decimal)drow["TotalAMT"];
            }
            if (drow["MakerPsnID"] == DBNull.Value)
            {
                this.MakerPsnID = -1;
            }
            else
            {
                this.MakerPsnID = (int)drow["MakerPsnID"];
            }
            if (drow["ConfirmPsnID"] == DBNull.Value)
            {
                this.ConfirmPsnID = -1;
            }
            else
            {
                this.ConfirmPsnID = (int)drow["ConfirmPsnID"];
            }
            if (drow["CompanyAbbName"] == DBNull.Value)
            {
                this.CompanyAbbName = string.Empty;
            }
            else
            {
                this.CompanyAbbName = drow["CompanyAbbName"].ToString();
            }
            if (drow["CompanyAllName"] == DBNull.Value)
            {
                this.CompanyAllName = string.Empty;
            }
            else
            {
                this.CompanyAllName = drow["CompanyAllName"].ToString();
            }
            if (drow["MoneyTypeName"] == DBNull.Value)
            {
                this.MoneyTypeName = string.Empty;
            }
            else
            {
                this.MoneyTypeName = drow["MoneyTypeName"].ToString();
            }
            if (drow["MakerPsn"] == DBNull.Value)
            {
                this.MakerPsn = string.Empty;
            }
            else
            {
                this.MakerPsn = drow["MakerPsn"].ToString();
            }
            if (drow["ConfirmPsn"] == DBNull.Value)
            {
                this.ConfirmPsn = string.Empty;
            }
            else
            {
                this.ConfirmPsn = drow["ConfirmPsn"].ToString();
            }
        }
    }
}
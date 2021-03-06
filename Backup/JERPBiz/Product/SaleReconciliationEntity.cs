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
    /// 表[SaleReconciliations]数据实体类
    ///</描述> 
    ///<作者> 
    /// 金优富
    ///</作者> 
    ///<时间> 
    /// 2016/1/18 22:47:49
    ///</时间>  
    public class SaleReconciliationEntity
    {
        public SaleReconciliationEntity()
        {
            this.accData = new JERPData.Product.SaleReconciliations();
        }
        private JERPData.Product.SaleReconciliations accData;
        public long ReconciliationID = -1;
        public string ReconciliationCode = string.Empty;
        public string ReconciliationName = string.Empty;
        public int SerialNo = -1;
        public int Year = -1;
        public int Month = -1;
        public DateTime DateNote = DateTime.MinValue;
        public int CompanyID = -1;
        public int FinanceAddressID = -1;
        public long SaleOrderNoteID = -1;
        public int SaleOrderSerialNo = -1;
        public string PONo = string.Empty;
        public bool CashSettleFlag = false;
        public int MoneyTypeID = -1;
        public int SettleTypeID = -1;
        public DateTime DateSettle = DateTime.MinValue;
        public int MakerPsnID = -1;
        public decimal DeliverAMT = 0;
        public decimal RepairAMT = 0;
        public decimal ReplaceExpressAMT = 0;
        public decimal ReturnAMT = 0;
        public decimal FineAMT = 0;
        public decimal TotalAMT = 0;
        public decimal FinishedAMT = 0;
        public decimal NonFinishedAMT = 0;
        public string CompanyAbbName = string.Empty;
        public string CompanyAllName = string.Empty;
        public string FinanceAddress = string.Empty;
        public string MakerPsn = string.Empty;
        public string MoneyTypeName = string.Empty;
        public string SettleTypeName = string.Empty;
        public void LoadData(long ReconciliationID)
        {
            this.ReconciliationID = ReconciliationID;
            DataTable dtbl = this.accData.GetDataSaleReconciliationsByReconciliationID(ReconciliationID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.ReconciliationCode = string.Empty;
                this.ReconciliationName = string.Empty;
                this.SerialNo = -1;
                this.Year = -1;
                this.Month = -1;
                this.DateNote = DateTime.MinValue;
                this.CompanyID = -1;
                this.FinanceAddressID = -1;
                this.SaleOrderNoteID = -1;
                this.SaleOrderSerialNo = -1;
                this.PONo = string.Empty;
                this.CashSettleFlag = false;
                this.MoneyTypeID = -1;
                this.SettleTypeID = -1;
                this.DateSettle = DateTime.MinValue;
                this.MakerPsnID = -1;
                this.DeliverAMT = 0;
                this.RepairAMT = 0;
                this.ReplaceExpressAMT = 0;
                this.ReturnAMT = 0;
                this.FineAMT = 0;
                this.TotalAMT = 0;
                this.FinishedAMT = 0;
                this.NonFinishedAMT = 0;
                this.CompanyAbbName = string.Empty;
                this.CompanyAllName = string.Empty;
                this.FinanceAddress = string.Empty;
                this.MakerPsn = string.Empty;
                this.MoneyTypeName = string.Empty;
                this.SettleTypeName = string.Empty;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["ReconciliationCode"] == DBNull.Value)
            {
                this.ReconciliationCode = string.Empty;
            }
            else
            {
                this.ReconciliationCode = drow["ReconciliationCode"].ToString();
            }
            if (drow["ReconciliationName"] == DBNull.Value)
            {
                this.ReconciliationName = string.Empty;
            }
            else
            {
                this.ReconciliationName = drow["ReconciliationName"].ToString();
            }
            if (drow["SerialNo"] == DBNull.Value)
            {
                this.SerialNo = -1;
            }
            else
            {
                this.SerialNo = (int)drow["SerialNo"];
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
            if (drow["FinanceAddressID"] == DBNull.Value)
            {
                this.FinanceAddressID = -1;
            }
            else
            {
                this.FinanceAddressID = (int)drow["FinanceAddressID"];
            }
            if (drow["SaleOrderNoteID"] == DBNull.Value)
            {
                this.SaleOrderNoteID = -1;
            }
            else
            {
                this.SaleOrderNoteID = (long)drow["SaleOrderNoteID"];
            }
            if (drow["SaleOrderSerialNo"] == DBNull.Value)
            {
                this.SaleOrderSerialNo = -1;
            }
            else
            {
                this.SaleOrderSerialNo = (int)drow["SaleOrderSerialNo"];
            }
            if (drow["PONo"] == DBNull.Value)
            {
                this.PONo = string.Empty;
            }
            else
            {
                this.PONo = drow["PONo"].ToString();
            }
            if (drow["CashSettleFlag"] == DBNull.Value)
            {
                this.CashSettleFlag = false;
            }
            else
            {
                this.CashSettleFlag = (bool)drow["CashSettleFlag"];
            }
            if (drow["MoneyTypeID"] == DBNull.Value)
            {
                this.MoneyTypeID = -1;
            }
            else
            {
                this.MoneyTypeID = (int)drow["MoneyTypeID"];
            }
            if (drow["SettleTypeID"] == DBNull.Value)
            {
                this.SettleTypeID = -1;
            }
            else
            {
                this.SettleTypeID = (int)drow["SettleTypeID"];
            }
            if (drow["DateSettle"] == DBNull.Value)
            {
                this.DateSettle = DateTime.MinValue;
            }
            else
            {
                this.DateSettle = (DateTime)drow["DateSettle"];
            }
            if (drow["MakerPsnID"] == DBNull.Value)
            {
                this.MakerPsnID = -1;
            }
            else
            {
                this.MakerPsnID = (int)drow["MakerPsnID"];
            }
            if (drow["DeliverAMT"] == DBNull.Value)
            {
                this.DeliverAMT = 0;
            }
            else
            {
                this.DeliverAMT = (decimal)drow["DeliverAMT"];
            }
            if (drow["RepairAMT"] == DBNull.Value)
            {
                this.RepairAMT = 0;
            }
            else
            {
                this.RepairAMT = (decimal)drow["RepairAMT"];
            }
            if (drow["ReplaceExpressAMT"] == DBNull.Value)
            {
                this.ReplaceExpressAMT = 0;
            }
            else
            {
                this.ReplaceExpressAMT = (decimal)drow["ReplaceExpressAMT"];
            }
            if (drow["ReturnAMT"] == DBNull.Value)
            {
                this.ReturnAMT = 0;
            }
            else
            {
                this.ReturnAMT = (decimal)drow["ReturnAMT"];
            }
            if (drow["FineAMT"] == DBNull.Value)
            {
                this.FineAMT = 0;
            }
            else
            {
                this.FineAMT = (decimal)drow["FineAMT"];
            }
            if (drow["TotalAMT"] == DBNull.Value)
            {
                this.TotalAMT = 0;
            }
            else
            {
                this.TotalAMT = (decimal)drow["TotalAMT"];
            }
            if (drow["FinishedAMT"] == DBNull.Value)
            {
                this.FinishedAMT = 0;
            }
            else
            {
                this.FinishedAMT = (decimal)drow["FinishedAMT"];
            }
            if (drow["NonFinishedAMT"] == DBNull.Value)
            {
                this.NonFinishedAMT = 0;
            }
            else
            {
                this.NonFinishedAMT = (decimal)drow["NonFinishedAMT"];
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
            if (drow["FinanceAddress"] == DBNull.Value)
            {
                this.FinanceAddress = string.Empty;
            }
            else
            {
                this.FinanceAddress = drow["FinanceAddress"].ToString();
            }
            if (drow["MakerPsn"] == DBNull.Value)
            {
                this.MakerPsn = string.Empty;
            }
            else
            {
                this.MakerPsn = drow["MakerPsn"].ToString();
            }
            if (drow["MoneyTypeName"] == DBNull.Value)
            {
                this.MoneyTypeName = string.Empty;
            }
            else
            {
                this.MoneyTypeName = drow["MoneyTypeName"].ToString();
            }
            if (drow["SettleTypeName"] == DBNull.Value)
            {
                this.SettleTypeName = string.Empty;
            }
            else
            {
                this.SettleTypeName = drow["SettleTypeName"].ToString();
            }
        }
    }
}
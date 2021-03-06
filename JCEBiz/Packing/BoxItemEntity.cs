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
namespace JCEBiz.Packing
{
    /// <描述>
    /// 表[BoxItems]数据实体类
    ///</描述> 
    ///<作者> 
    /// 金优富
    ///</作者> 
    ///<时间> 
    /// 2015/10/11 10:01:20
    ///</时间>  
    public class BoxItemEntity
    {
        public BoxItemEntity()
        {
            this.accData = new JCEData.Packing.BoxItems();
        }
        private JCEData.Packing.BoxItems accData;
        public string Barcode = string.Empty;
        public int SerialNo = -1;
        public int PrdID = -1;
        public long WorkingSheetID = -1;
        public string WorkingSheetCode = string.Empty;
        public string ChipsetCode = string.Empty;
        public string BoxCode = string.Empty;
        public string PrdCode = string.Empty;
        public string PrdName = string.Empty;
        public string PrdSpec = string.Empty;
        public string Manufacturer = string.Empty;
        public string Model = string.Empty;
        public void LoadData(string Barcode)
        {
            this.Barcode = Barcode;
            DataTable dtbl = this.accData.GetDataBoxItemsByBarcode(Barcode).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.SerialNo = -1;
                this.PrdID = -1;
                this.WorkingSheetID = -1;
                this.WorkingSheetCode = string.Empty;
                this.ChipsetCode = string.Empty;
                this.BoxCode = string.Empty;
                this.PrdCode = string.Empty;
                this.PrdName = string.Empty;
                this.PrdSpec = string.Empty;
                this.Manufacturer = string.Empty;
                this.Model = string.Empty;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["SerialNo"] == DBNull.Value)
            {
                this.SerialNo = -1;
            }
            else
            {
                this.SerialNo = (int)drow["SerialNo"];
            }
            if (drow["PrdID"] == DBNull.Value)
            {
                this.PrdID = -1;
            }
            else
            {
                this.PrdID = (int)drow["PrdID"];
            }
            if (drow["WorkingSheetID"] == DBNull.Value)
            {
                this.WorkingSheetID = -1;
            }
            else
            {
                this.WorkingSheetID = (long)drow["WorkingSheetID"];
            }
            if (drow["WorkingSheetCode"] == DBNull.Value)
            {
                this.WorkingSheetCode = string.Empty;
            }
            else
            {
                this.WorkingSheetCode = drow["WorkingSheetCode"].ToString();
            }
            if (drow["ChipsetCode"] == DBNull.Value)
            {
                this.ChipsetCode = string.Empty;
            }
            else
            {
                this.ChipsetCode = drow["ChipsetCode"].ToString();
            }
            if (drow["BoxCode"] == DBNull.Value)
            {
                this.BoxCode = string.Empty;
            }
            else
            {
                this.BoxCode = drow["BoxCode"].ToString();
            }
            if (drow["PrdCode"] == DBNull.Value)
            {
                this.PrdCode = string.Empty;
            }
            else
            {
                this.PrdCode = drow["PrdCode"].ToString();
            }
            if (drow["PrdName"] == DBNull.Value)
            {
                this.PrdName = string.Empty;
            }
            else
            {
                this.PrdName = drow["PrdName"].ToString();
            }
            if (drow["PrdSpec"] == DBNull.Value)
            {
                this.PrdSpec = string.Empty;
            }
            else
            {
                this.PrdSpec = drow["PrdSpec"].ToString();
            }
            if (drow["Manufacturer"] == DBNull.Value)
            {
                this.Manufacturer = string.Empty;
            }
            else
            {
                this.Manufacturer = drow["Manufacturer"].ToString();
            }
            if (drow["Model"] == DBNull.Value)
            {
                this.Model = string.Empty;
            }
            else
            {
                this.Model = drow["Model"].ToString();
            }
        }
    }
}
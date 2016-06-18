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
namespace JERPBiz.General
{
    /// <����>
    /// ��[FinanceAddress]����ʵ����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/4/14 15:53:28
    ///</ʱ��>  
    public class FinanceAddressEntity
    {
        public FinanceAddressEntity()
        {
            this.accData = new JERPData.General.FinanceAddress();
        }
        private JERPData.General.FinanceAddress accData;
        public int FinanceAddressID = -1;
        public int CompanyID = -1;
        public string Address = string.Empty;
        public string Linkman = string.Empty;
        public string Telephone = string.Empty;
        public string Fax = string.Empty;
        public void LoadData(int FinanceAddressID)
        {
            this.FinanceAddressID = FinanceAddressID;
            DataTable dtbl = this.accData.GetDataFinanceAddressByFinanceAddressID(FinanceAddressID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.CompanyID = -1;
                this.Address = string.Empty;
                this.Linkman = string.Empty;
                this.Telephone = string.Empty;
                this.Fax = string.Empty;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["CompanyID"] == DBNull.Value)
            {
                this.CompanyID = -1;
            }
            else
            {
                this.CompanyID = (int)drow["CompanyID"];
            }
            if (drow["Address"] == DBNull.Value)
            {
                this.Address = string.Empty;
            }
            else
            {
                this.Address = drow["Address"].ToString();
            }
            if (drow["Linkman"] == DBNull.Value)
            {
                this.Linkman = string.Empty;
            }
            else
            {
                this.Linkman = drow["Linkman"].ToString();
            }
            if (drow["Telephone"] == DBNull.Value)
            {
                this.Telephone = string.Empty;
            }
            else
            {
                this.Telephone = drow["Telephone"].ToString();
            }
            if (drow["Fax"] == DBNull.Value)
            {
                this.Fax = string.Empty;
            }
            else
            {
                this.Fax = drow["Fax"].ToString();
            }
        }
    }
}
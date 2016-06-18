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
namespace JERPBiz.Finance
{
    /// <����>
    /// ��[MoneyType]����ʵ����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2012-07-21 16:35:16
    ///</ʱ��>  
    public class MoneyTypeEntity
    {
        public MoneyTypeEntity()
        {
            this.accData = new JERPData.Finance.MoneyType();
        }
        private JERPData.Finance.MoneyType accData;
        public int MoneyTypeID = -1;
        public string MoneyTypeCode = string.Empty;
        public string MoneyTypeName = string.Empty;
        public bool MainMoneyFlag = false;
        public decimal ExchangeRate = -1;
        public void LoadData(int MoneyTypeID)
        {
            this.MoneyTypeID = MoneyTypeID;
            DataTable dtbl = this.accData.GetDataMoneyTypeByMoneyTypeID(MoneyTypeID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.MoneyTypeCode = string.Empty;
                this.MoneyTypeName = string.Empty;
                this.MainMoneyFlag = false;
                this.ExchangeRate = -1;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["MoneyTypeCode"] == DBNull.Value)
            {
                this.MoneyTypeCode = string.Empty;
            }
            else
            {
                this.MoneyTypeCode = drow["MoneyTypeCode"].ToString();
            }
            if (drow["MoneyTypeName"] == DBNull.Value)
            {
                this.MoneyTypeName = string.Empty;
            }
            else
            {
                this.MoneyTypeName = drow["MoneyTypeName"].ToString();
            }
            if (drow["MainMoneyFlag"] == DBNull.Value)
            {
                this.MainMoneyFlag = false;
            }
            else
            {
                this.MainMoneyFlag = (bool)drow["MainMoneyFlag"];
            }
            if (drow["ExchangeRate"] == DBNull.Value)
            {
                this.ExchangeRate = -1;
            }
            else
            {
                this.ExchangeRate = (decimal)drow["ExchangeRate"];
            }
        }
    }
}
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
    /// ��[WorkingReport]����ʵ����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014-11-26 09:47:50
    ///</ʱ��>  
    public class WorkingReportEntity
    {
        public WorkingReportEntity()
        {
            this.accData = new JERPData.General.WorkingReports();
        }
        private JERPData.General.WorkingReports accData;
        public long ReportID = -1;
        public DateTime DateReport = DateTime.MinValue;
        public string ReportTitle = string.Empty;
        public int PsnID = -1;
        public string ReportContent = string.Empty;
        public int ConfirmPsnID = -1;
        public string PsnCode = string.Empty;
        public string PsnName = string.Empty;
        public string ConfirmPsn = string.Empty;
        public void LoadData(long ReportID)
        {
            this.ReportID = ReportID;
            DataTable dtbl = this.accData.GetDataWorkingReportsByReportID(ReportID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.DateReport = DateTime.MinValue;
                this.ReportTitle = string.Empty;
                this.PsnID = -1;
                this.ReportContent = string.Empty;
                this.ConfirmPsnID = -1;
                this.PsnCode = string.Empty;
                this.PsnName = string.Empty;
                this.ConfirmPsn = string.Empty;
                return;
            }
            DataRow drow = dtbl.Rows[0];
            if (drow["DateReport"] == DBNull.Value)
            {
                this.DateReport = DateTime.MinValue;
            }
            else
            {
                this.DateReport = (DateTime)drow["DateReport"];
            }
            if (drow["ReportTitle"] == DBNull.Value)
            {
                this.ReportTitle = string.Empty;
            }
            else
            {
                this.ReportTitle = drow["ReportTitle"].ToString();
            }
            if (drow["PsnID"] == DBNull.Value)
            {
                this.PsnID = -1;
            }
            else
            {
                this.PsnID = (int)drow["PsnID"];
            }
            if (drow["ReportContent"] == DBNull.Value)
            {
                this.ReportContent = string.Empty;
            }
            else
            {
                this.ReportContent = drow["ReportContent"].ToString();
            }
            if (drow["ConfirmPsnID"] == DBNull.Value)
            {
                this.ConfirmPsnID = -1;
            }
            else
            {
                this.ConfirmPsnID = (int)drow["ConfirmPsnID"];
            }
            if (drow["PsnCode"] == DBNull.Value)
            {
                this.PsnCode = string.Empty;
            }
            else
            {
                this.PsnCode = drow["PsnCode"].ToString();
            }
            if (drow["PsnName"] == DBNull.Value)
            {
                this.PsnName = string.Empty;
            }
            else
            {
                this.PsnName = drow["PsnName"].ToString();
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
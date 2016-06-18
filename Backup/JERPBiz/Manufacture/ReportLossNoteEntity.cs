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
    /// ��[ReportLossNote]����ʵ����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/4/23 15:01:31
    ///</ʱ��>  
    public class ReportLossNoteEntity
    {
        public ReportLossNoteEntity()
        {
            this.accData = new JERPData.Manufacture.ReportLossNotes();
        }
        private JERPData.Manufacture.ReportLossNotes accData;
        public long NoteID = -1;
        public int SerialNo = -1;
        public DateTime DateNote = DateTime.MinValue;
        public string NoteCode = string.Empty;
        public int MakerPsnID = -1;
        public string QC = string.Empty;
        public string ConfirmPsns = string.Empty;
        public string Memo = string.Empty;
        public string MakerPsn = string.Empty;
        public void LoadData(long NoteID)
        {
            this.NoteID = NoteID;
            DataTable dtbl = this.accData.GetDataReportLossNotesByNoteID(NoteID).Tables[0];
            if (dtbl.Rows.Count == 0)
            {
                this.SerialNo = -1;
                this.DateNote = DateTime.MinValue;
                this.NoteCode = string.Empty;
                this.MakerPsnID = -1;
                this.QC = string.Empty;
                this.ConfirmPsns = string.Empty;
                this.Memo = string.Empty;
                this.MakerPsn = string.Empty;
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
            if (drow["DateNote"] == DBNull.Value)
            {
                this.DateNote = DateTime.MinValue;
            }
            else
            {
                this.DateNote = (DateTime)drow["DateNote"];
            }
            if (drow["NoteCode"] == DBNull.Value)
            {
                this.NoteCode = string.Empty;
            }
            else
            {
                this.NoteCode = drow["NoteCode"].ToString();
            }
            if (drow["MakerPsnID"] == DBNull.Value)
            {
                this.MakerPsnID = -1;
            }
            else
            {
                this.MakerPsnID = (int)drow["MakerPsnID"];
            }
            if (drow["QC"] == DBNull.Value)
            {
                this.QC = string.Empty;
            }
            else
            {
                this.QC = drow["QC"].ToString();
            }
            if (drow["ConfirmPsns"] == DBNull.Value)
            {
                this.ConfirmPsns = string.Empty;
            }
            else
            {
                this.ConfirmPsns = drow["ConfirmPsns"].ToString();
            }
            if (drow["Memo"] == DBNull.Value)
            {
                this.Memo = string.Empty;
            }
            else
            {
                this.Memo = drow["Memo"].ToString();
            }
            if (drow["MakerPsn"] == DBNull.Value)
            {
                this.MakerPsn = string.Empty;
            }
            else
            {
                this.MakerPsn = drow["MakerPsn"].ToString();
            }
        }
    }
}
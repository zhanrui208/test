/*
$Header$
$Author$
$Date$
$Revision$
*/
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
namespace JERPData.Product
{
    /// <����>
    /// ��[prd.SaleFineAMTNotes]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2013-12-27 15:37:05
    ///</ʱ��>  
    public class SaleFineAMTNotes
    {
        private SqlConnection sqlConn;
        public SaleFineAMTNotes()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteSaleFineAMTNotes(ref string ErrorMsg, object NoteID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteSaleFineAMTNotes", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //���ش�����Ϣ
                flag = false;
                DBTransaction.Rollback();//--��������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
        public DataSet GetDataSaleFineAMTNotesByNoteID(long NoteID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleFineAMTNotesByNoteID", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --������������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public DataSet GetDataSaleFineAMTNotesByReconciliationID(long ReconciliationID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@ReconciliationID", SqlDbType.BigInt);
            arParams[0].Value = ReconciliationID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleFineAMTNotesByReconciliationID", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --������������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public DataSet GetDataSaleFineAMTNotesForReconciliation(long ReconciliationID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@ReconciliationID", SqlDbType.BigInt);
            arParams[0].Value = ReconciliationID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleFineAMTNotesForReconciliation", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --������������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public DataSet GetDataSaleFineAMTNotesPivotMonth(int Year)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@Year", SqlDbType.Int);
            arParams[0].Value = Year;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleFineAMTNotesPivotMonth", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --������������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public DataSet GetDataSaleFineAMTNotesMonthRecord(int Year, int Month, int CompanyID, int MoneyTypeID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@Year", SqlDbType.Int);
            arParams[1] = new SqlParameter("@Month", SqlDbType.Int);
            arParams[2] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[3] = new SqlParameter("@MoneyTypeID", SqlDbType.Int);
            arParams[0].Value = Year;
            arParams[1].Value = Month;
            arParams[2].Value = CompanyID;
            arParams[3].Value = MoneyTypeID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleFineAMTNotesMonthRecord", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --������������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public bool GetParmSaleFineAMTNotesCountForReconciliation(long ReconciliationID, ref int Count)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@ReconciliationID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@Count", SqlDbType.Int);
            arParams[1].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = ReconciliationID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "prd.GetParmSaleFineAMTNotesCountForReconciliation", arParams);
                Count = (int)arParams[1].Value;
                flag = true;
            }
            catch//(SqlException ex)
            {
                flag = false;
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }

        public DataSet GetDataSaleFineAMTNotesLastRecord(int RecordCount)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@RecordCount", SqlDbType.Int);
            arParams[0].Value = RecordCount;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleFineAMTNotesLastRecord", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --������������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public bool InsertSaleFineAMTNotes(ref string ErrorMsg, ref object NoteID, object NoteCode, object DateNote, object CompanyID, object MoneyTypeID, object CashSettleFlag, object Amount, object Summary, object MakerPsnID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[9];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@NoteCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@DateNote", SqlDbType.DateTime);
            arParams[3] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@MoneyTypeID", SqlDbType.Int);
            arParams[5] = new SqlParameter("@CashSettleFlag", SqlDbType.Bit);
            arParams[6] = new SqlParameter("@Amount", SqlDbType.Money);
            arParams[7] = new SqlParameter("@Summary", SqlDbType.VarChar);
            arParams[7].Size = -1;
            arParams[8] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[1].Value = NoteCode;
            arParams[2].Value = DateNote;
            arParams[3].Value = CompanyID;
            arParams[4].Value = MoneyTypeID;
            arParams[5].Value = CashSettleFlag;
            arParams[6].Value = Amount;
            arParams[7].Value = Summary;
            arParams[8].Value = MakerPsnID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertSaleFineAMTNotes", arParams);
                NoteID = arParams[0].Value;
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //���ش�����Ϣ
                flag = false;
                DBTransaction.Rollback();//--��������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
        public bool UpdateSaleFineAMTNotes(ref string ErrorMsg, object NoteID, object NoteCode, object DateNote, object CompanyID, object MoneyTypeID, object Amount, object Summary, object MakerPsnID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[8];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@NoteCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@DateNote", SqlDbType.DateTime);
            arParams[3] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@MoneyTypeID", SqlDbType.Int);
            arParams[5] = new SqlParameter("@Amount", SqlDbType.Money);
            arParams[6] = new SqlParameter("@Summary", SqlDbType.VarChar);
            arParams[6].Size = -1;
            arParams[7] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[0].Value = NoteID;
            arParams[1].Value = NoteCode;
            arParams[2].Value = DateNote;
            arParams[3].Value = CompanyID;
            arParams[4].Value = MoneyTypeID;
            arParams[5].Value = Amount;
            arParams[6].Value = Summary;
            arParams[7].Value = MakerPsnID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateSaleFineAMTNotes", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //���ش�����Ϣ
                flag = false;
                DBTransaction.Rollback();//--��������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
        public bool UpdateSaleFineAMTNotesCancelReconciliation(ref string ErrorMsg, object NoteID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateSaleFineAMTNotesCancelReconciliation", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //���ش�����Ϣ
                flag = false;
                DBTransaction.Rollback();//--��������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
        public bool UpdateSaleFineAMTNotesForReconciliation(ref string ErrorMsg, object NoteID, object ReconciliationID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@ReconciliationID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            arParams[1].Value = ReconciliationID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateSaleFineAMTNotesForReconciliation", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //���ش�����Ϣ
                flag = false;
                DBTransaction.Rollback();//--��������
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }

    }
}
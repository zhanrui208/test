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
namespace JERPData.Material
{
    /// <����>
    /// ��[mtr.OEMStoreCheckNotes]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/5/2 12:04:07
    ///</ʱ��>  
    public class OEMStoreCheckNotes
    {
        private SqlConnection sqlConn;
        public OEMStoreCheckNotes()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }
        public bool DeleteOEMStoreCheckNotes(ref string ErrorMsg, object NoteID)
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
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.DeleteOEMStoreCheckNotes", arParams);
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
        public DataSet GetDataOEMStoreCheckNotesByNoteID(long NoteID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "mtr.GetDataOEMStoreCheckNotesByNoteID", arParams);
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
        public DataSet GetDataOEMStoreCheckNotesDescPagesFreeSearch(int PageIndex, int PageSize, ref int RecordCount, string WhereClause)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@PageIndex", SqlDbType.Int);
            arParams[1] = new SqlParameter("@PageSize", SqlDbType.Int);
            arParams[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            arParams[2].Direction = ParameterDirection.InputOutput;
            arParams[3] = new SqlParameter("@WhereClause", SqlDbType.NVarChar);
            arParams[3].Size = -1;
            arParams[0].Value = PageIndex;
            arParams[1].Value = PageSize;
            arParams[2].Value = RecordCount;
            arParams[3].Value = WhereClause;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "mtr.GetDataOEMStoreCheckNotesDescPagesFreeSearch", arParams);
                RecordCount = (int)arParams[2].Value;
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

        public DataSet GetDataOEMStoreCheckNotesNonConfirm()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "mtr.GetDataOEMStoreCheckNotesNonConfirm");
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
        public bool InsertOEMStoreCheckNotes(ref string ErrorMsg, ref object NoteID, ref object NoteCode, object DateNote, object CompanyID, object MakerPsnID, object CheckPersons, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[7];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@NoteCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[1].Direction = ParameterDirection.InputOutput;
            arParams[2] = new SqlParameter("@DateNote", SqlDbType.DateTime);
            arParams[3] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[5] = new SqlParameter("@CheckPersons", SqlDbType.VarChar);
            arParams[5].Size = 100;
            arParams[6] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[6].Size = 200;
            arParams[2].Value = DateNote;
            arParams[3].Value = CompanyID;
            arParams[4].Value = MakerPsnID;
            arParams[5].Value = CheckPersons;
            arParams[6].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.InsertOEMStoreCheckNotes", arParams);
                NoteID = arParams[0].Value;
                NoteCode = arParams[1].Value;
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

        public bool UpdateOEMStoreCheckNotes(ref string ErrorMsg, object NoteID, object MakerPsnID, object CheckPersons, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@CheckPersons", SqlDbType.VarChar);
            arParams[2].Size = 100;
            arParams[3] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[3].Size = 200;
            arParams[0].Value = NoteID;
            arParams[1].Value = MakerPsnID;
            arParams[2].Value = CheckPersons;
            arParams[3].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.UpdateOEMStoreCheckNotes", arParams);
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
        public bool UpdateOEMStoreCheckNotesForConfirm(ref string ErrorMsg, object NoteID, object ConfirmPsnID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@ConfirmPsnID", SqlDbType.Int);
            arParams[0].Value = NoteID;
            arParams[1].Value = ConfirmPsnID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.UpdateOEMStoreCheckNotesForConfirm", arParams);
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
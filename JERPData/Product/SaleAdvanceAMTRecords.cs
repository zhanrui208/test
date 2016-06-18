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
    /// ��[prd.SaleAdvanceAMTRecords]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/10/5 11:57:54
    ///</ʱ��>  
    public class SaleAdvanceAMTRecords
    {
        private SqlConnection sqlConn;
        public SaleAdvanceAMTRecords()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }


        public DataSet GetDataSaleAdvanceAMTRecordsByReceiptNoteID(long ReceiptNoteID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@ReceiptNoteID", SqlDbType.BigInt);
            arParams[0].Value = ReceiptNoteID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleAdvanceAMTRecordsByReceiptNoteID", arParams);
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
        public DataSet GetDataSaleAdvanceAMTRecordsDescPagesFreeSearch(int PageIndex, int PageSize, ref int RecordCount, string WhereClause)
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
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleAdvanceAMTRecordsDescPagesFreeSearch", arParams);
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
        public DataSet GetDataSaleAdvanceAMTRecordsForReceipt(int CompanyID, int MoneyTypeID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@MoneyTypeID", SqlDbType.Int);
            arParams[0].Value = CompanyID;
            arParams[1].Value = MoneyTypeID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleAdvanceAMTRecordsForReceipt", arParams);
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

        public bool GetParmSaleAdvanceAMTRecordsReceiptFlag(int CompanyID, int MoneyTypeID, ref bool Flag)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@MoneyTypeID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@Flag", SqlDbType.Bit);
            arParams[2].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = CompanyID;
            arParams[1].Value = MoneyTypeID;
            arParams[2].Value = Flag;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "prd.GetParmSaleAdvanceAMTRecordsReceiptFlag", arParams);
                Flag = (bool)arParams[2].Value;
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
        public bool InsertSaleAdvanceAMTRecords(ref string ErrorMsg, object DateRecord, object SaleOrderNoteID, object CompanyID, object MoneyTypeID, object Amount, object MakerPsnID, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[7];
            arParams[0] = new SqlParameter("@DateRecord", SqlDbType.DateTime);
            arParams[1] = new SqlParameter("@SaleOrderNoteID", SqlDbType.BigInt);
            arParams[2] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[3] = new SqlParameter("@MoneyTypeID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@Amount", SqlDbType.Money);
            arParams[5] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[6] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[6].Size = 100;
            arParams[0].Value = DateRecord;
            arParams[1].Value = SaleOrderNoteID;
            arParams[2].Value = CompanyID;
            arParams[3].Value = MoneyTypeID;
            arParams[4].Value = Amount;
            arParams[5].Value = MakerPsnID;
            arParams[6].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertSaleAdvanceAMTRecords", arParams);
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
        public bool UpdateSaleAdvanceAMTRecordsForReceipt(ref string ErrorMsg, object RecordID, object ReceiptNoteID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@RecordID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@ReceiptNoteID", SqlDbType.BigInt);
            arParams[0].Value = RecordID;
            arParams[1].Value = ReceiptNoteID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateSaleAdvanceAMTRecordsForReceipt", arParams);
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
        public bool UpdateSaleAdvanceAMTRecordsCancelReceipt(ref string ErrorMsg, object RecordID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@RecordID", SqlDbType.BigInt);
            arParams[0].Value = RecordID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateSaleAdvanceAMTRecordsCancelReceipt", arParams);
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
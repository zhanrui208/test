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
namespace JERPData.Asset
{
    /// <����>
    /// ��[asset.PrdRepairRecord]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014-12-22 11:48:49
    ///</ʱ��>  
    public class PrdRepairRecord
    {
        private SqlConnection sqlConn;
        public PrdRepairRecord()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeletePrdRepairRecord(ref string ErrorMsg, object RecordID)
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
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "asset.DeletePrdRepairRecord", arParams);
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
        public DataSet GetDataPrdRepairRecordByPrdID(int PrdID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[0].Value = PrdID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "asset.GetDataPrdRepairRecordByPrdID", arParams);
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
        public bool InsertPrdRepairRecord(ref string ErrorMsg, ref object RecordID, object PrdID, object DateRepair, object Remark)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@RecordID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@DateRepair", SqlDbType.DateTime);
            arParams[3] = new SqlParameter("@Remark", SqlDbType.VarChar);
            arParams[3].Size = 100;
            arParams[1].Value = PrdID;
            arParams[2].Value = DateRepair;
            arParams[3].Value = Remark;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "asset.InsertPrdRepairRecord", arParams);
                RecordID = arParams[0].Value;
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
        public bool UpdatePrdRepairRecord(ref string ErrorMsg, object RecordID, object DateRepair, object Remark)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@RecordID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@DateRepair", SqlDbType.DateTime);
            arParams[2] = new SqlParameter("@Remark", SqlDbType.VarChar);
            arParams[2].Size = 100;
            arParams[0].Value = RecordID;
            arParams[1].Value = DateRepair;
            arParams[2].Value = Remark;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "asset.UpdatePrdRepairRecord", arParams);
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
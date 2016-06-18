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
    /// ��[prd.ParmTempleValue]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/3/3 17:30:35
    ///</ʱ��>  
    public class ParmTempletValues
    {
        private SqlConnection sqlConn;
        public ParmTempletValues()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteParmTempletValues(ref string ErrorMsg, object ParmID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@ParmID", SqlDbType.BigInt);
            arParams[0].Value = ParmID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteParmTempletValues", arParams);
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
        public DataSet GetDataParmTempletValuesByTempletID(int TempletID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@TempletID", SqlDbType.Int);
            arParams[0].Value = TempletID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataParmTempletValuesByTempletID", arParams);
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
        public bool InsertParmTempletValues(ref string ErrorMsg, ref object ParmID, object TempletID, object ParmTypeID, object ParmValue)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@ParmID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@TempletID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@ParmTypeID", SqlDbType.Int);
            arParams[3] = new SqlParameter("@ParmValue", SqlDbType.VarChar);
            arParams[3].Size = -1;
            arParams[1].Value = TempletID;
            arParams[2].Value = ParmTypeID;
            arParams[3].Value = ParmValue;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertParmTempletValues", arParams);
                ParmID = arParams[0].Value;
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
        public bool UpdateParmTempletValues(ref string ErrorMsg, object ParmID, object ParmTypeID, object ParmValue)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@ParmID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@ParmTypeID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@ParmValue", SqlDbType.VarChar);
            arParams[2].Size = -1;
            arParams[0].Value = ParmID;
            arParams[1].Value = ParmTypeID;
            arParams[2].Value = ParmValue;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateParmTempletValues", arParams);
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
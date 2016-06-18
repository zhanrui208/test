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
    /// ��[prd.ParmType]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/3/3 17:31:05
    ///</ʱ��>  
    public class ParmType
    {
        private SqlConnection sqlConn;
        public ParmType()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteParmType(ref string ErrorMsg, object ParmTypeID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@ParmTypeID", SqlDbType.Int);
            arParams[0].Value = ParmTypeID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteParmType", arParams);
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
        public DataSet GetDataParmType()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "prd.GetDataParmType");
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

        public bool InsertParmType(ref string ErrorMsg, ref object ParmTypeID, object ParmTypeName, object DefaultValue, object ItemValues)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@ParmTypeID", SqlDbType.Int);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@ParmTypeName", SqlDbType.VarChar);
            arParams[1].Size = 100;
            arParams[2] = new SqlParameter("@DefaultValue", SqlDbType.VarChar);
            arParams[2].Size = -1;
            arParams[3] = new SqlParameter("@ItemValues", SqlDbType.VarChar);
            arParams[3].Size = -1;
            arParams[1].Value = ParmTypeName;
            arParams[2].Value = DefaultValue;
            arParams[3].Value = ItemValues;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertParmType", arParams);
                ParmTypeID = arParams[0].Value;
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
        public bool UpdateParmType(ref string ErrorMsg, object ParmTypeID, object ParmTypeName, object DefaultValue, object ItemValues)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@ParmTypeID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@ParmTypeName", SqlDbType.VarChar);
            arParams[1].Size = 100;
            arParams[2] = new SqlParameter("@DefaultValue", SqlDbType.VarChar);
            arParams[2].Size = -1;
            arParams[3] = new SqlParameter("@ItemValues", SqlDbType.VarChar);
            arParams[3].Size = -1;
            arParams[0].Value = ParmTypeID;
            arParams[1].Value = ParmTypeName;
            arParams[2].Value = DefaultValue;
            arParams[3].Value = ItemValues;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateParmType", arParams);
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
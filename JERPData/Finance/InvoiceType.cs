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
namespace JERPData.Finance
{
    /// <����>
    /// ��[finance.InvoiceType]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/6/30 6:36:23
    ///</ʱ��>  
    public class InvoiceType
    {
        private SqlConnection sqlConn;
        public InvoiceType()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }


        public bool DeleteInvoiceType(ref string ErrorMsg, object InvoiceTypeID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@InvoiceTypeID", SqlDbType.Int);
            arParams[0].Value = InvoiceTypeID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.DeleteInvoiceType", arParams);
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
        public DataSet GetDataInvoiceType()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "finance.GetDataInvoiceType");
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

        public bool InsertInvoiceType(ref string ErrorMsg, ref object InvoiceTypeID, object InvoiceTypeCode, object InvoiceTypeName)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@InvoiceTypeID", SqlDbType.Int);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@InvoiceTypeCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@InvoiceTypeName", SqlDbType.VarChar);
            arParams[2].Size = 50;
            arParams[1].Value = InvoiceTypeCode;
            arParams[2].Value = InvoiceTypeName;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.InsertInvoiceType", arParams);
                InvoiceTypeID = arParams[0].Value;
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
        public bool UpdateInvoiceType(ref string ErrorMsg, object InvoiceTypeID, object InvoiceTypeCode, object InvoiceTypeName)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@InvoiceTypeID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@InvoiceTypeCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@InvoiceTypeName", SqlDbType.VarChar);
            arParams[2].Size = 50;
            arParams[0].Value = InvoiceTypeID;
            arParams[1].Value = InvoiceTypeCode;
            arParams[2].Value = InvoiceTypeName;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.UpdateInvoiceType", arParams);
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
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
    /// ��[prd.SaleReconciliationFormatXCustomer]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/1/4 0:29:48
    ///</ʱ��>  
    public class SaleReconciliationFormatXCustomer
    {
        private SqlConnection sqlConn;
        public SaleReconciliationFormatXCustomer()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }


        public bool DeleteSaleReconciliationFormatXCustomer(ref string ErrorMsg, object FormatID, object CompanyID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[0].Value = FormatID;
            arParams[1].Value = CompanyID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteSaleReconciliationFormatXCustomer", arParams);
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
        public DataSet GetDataSaleReconciliationFormatXCustomer()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "prd.GetDataSaleReconciliationFormatXCustomer");
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
        public bool SaveSaleReconciliationFormatXCustomer(ref string ErrorMsg, object FormatID, object CompanyID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@CompanyID", SqlDbType.Int);
            arParams[0].Value = FormatID;
            arParams[1].Value = CompanyID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.SaveSaleReconciliationFormatXCustomer", arParams);
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
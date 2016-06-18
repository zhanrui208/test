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
    /// ��[prd.SaleDeliverItemsXBox]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/9/27 8:05:14
    ///</ʱ��>  
    public class SaleDeliverItemsXBox
    {
        private SqlConnection sqlConn;
        public SaleDeliverItemsXBox()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }


        public bool DeleteSaleDeliverItemsXBoxBatch(ref string ErrorMsg, object SaleDeliverItemID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@SaleDeliverItemID", SqlDbType.BigInt);
            arParams[0].Value = SaleDeliverItemID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteSaleDeliverItemsXBoxBatch", arParams);
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

        public bool SaveSaleDeliverItemsXBox(ref string ErrorMsg, object SaleDeliverItemID, object BoxCode)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@SaleDeliverItemID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@BoxCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[0].Value = SaleDeliverItemID;
            arParams[1].Value = BoxCode;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.SaveSaleDeliverItemsXBox", arParams);
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
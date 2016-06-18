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
    /// ��[finance.PieceworkPrice]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2013-6-21 16:33:58
    ///</ʱ��>  
    public class PieceworkPrice
    {
        private SqlConnection sqlConn;
        public PieceworkPrice()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeletePieceworkPrice(ref string ErrorMsg, object ProcessTypeID, object PrdTypeID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@ProcessTypeID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@PrdTypeID", SqlDbType.Int);
            arParams[0].Value = ProcessTypeID;
            arParams[1].Value = PrdTypeID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.DeletePieceworkPrice", arParams);
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
        public DataSet GetDataPieceworkPriceForSetting()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "finance.GetDataPieceworkPriceForSetting");
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
        public bool SavePieceworkPrice(ref string ErrorMsg, object ProcessTypeID, object PrdTypeID, object LaborPrice)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@ProcessTypeID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@PrdTypeID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@LaborPrice", SqlDbType.Money);
            arParams[0].Value = ProcessTypeID;
            arParams[1].Value = PrdTypeID;
            arParams[2].Value = LaborPrice;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.SavePieceworkPrice", arParams);
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
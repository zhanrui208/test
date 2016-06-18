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
    /// ��[finance.WageOtherItems]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2014/5/7 15:30:06
    ///</ʱ��>  
    public class WageOtherItems
    {
        private SqlConnection sqlConn;
        public WageOtherItems()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }
        public bool DeleteWageOtherItems(ref string ErrorMsg, object WageItemID, object WageTypeID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@WageItemID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@WageTypeID", SqlDbType.Int);
            arParams[0].Value = WageItemID;
            arParams[1].Value = WageTypeID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.DeleteWageOtherItems", arParams);
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
        public bool SaveWageOtherItems(ref string ErrorMsg, object WageItemID, object WageTypeID, object OtherWage)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@WageItemID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@WageTypeID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@OtherWage", SqlDbType.Money);
            arParams[0].Value = WageItemID;
            arParams[1].Value = WageTypeID;
            arParams[2].Value = OtherWage;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "finance.SaveWageOtherItems", arParams);
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
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
    /// ��[mtr.StoreWorkingSheetReserve]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/9/15 19:17:39
    ///</ʱ��>  
    public class StoreWorkingSheetReserve
    {
        private SqlConnection sqlConn;
        public StoreWorkingSheetReserve()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool GetParmStoreWorkingSheetReserveQuantity(int PrdID, ref decimal Quantity)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[1].Precision = 18;
            arParams[1].Scale = 4;
            arParams[1].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = PrdID;
            arParams[1].Value = Quantity;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "mtr.GetParmStoreWorkingSheetReserveQuantity", arParams);
                Quantity = (decimal)arParams[1].Value;
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
        public DataSet GetDataStoreWorkingSheetReserve()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "mtr.GetDataStoreWorkingSheetReserve");
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
        public bool SaveStoreWorkingSheetReserveForAppReserve(ref string ErrorMsg, object PrdID, object Quantity)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[1].Precision = 18;
            arParams[1].Scale = 4;
            arParams[0].Value = PrdID;
            arParams[1].Value = Quantity;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.SaveStoreWorkingSheetReserveForAppReserve", arParams);
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
        public bool SaveStoreWorkingSheetReserveForCheck(ref string ErrorMsg, object PrdID, object Quantity)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[1].Precision = 18;
            arParams[1].Scale = 4;
            arParams[0].Value = PrdID;
            arParams[1].Value = Quantity;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.SaveStoreWorkingSheetReserveForCheck", arParams);
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
        public bool SaveStoreWorkingSheetReserveForSubReserve(ref string ErrorMsg, object PrdID, object Quantity)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[1].Precision = 18;
            arParams[1].Scale = 4;
            arParams[0].Value = PrdID;
            arParams[1].Value = Quantity;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.SaveStoreWorkingSheetReserveForSubReserve", arParams);
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

        public bool UpdateStoreWorkingSheetReserveForAdjustStore(ref string ErrorMsg, object PrdID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[0].Value = PrdID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.UpdateStoreWorkingSheetReserveForAdjustStore", arParams);
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

using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
namespace JERPData.Product
{
    /// <描述>
    /// 表[prd.StoreItemsOther]数据访问类
    ///</描述> 
    ///<作者> 
    /// 金优富
    ///</作者> 
    ///<时间> 
    /// 2016/6/20 17:06:23
    ///</时间>  
    public class StoreItemsOther
    {
        private SqlConnection sqlConn;
        public StoreItemsOther()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public DataSet GetDataStoreItemsOtherByNoteID(long NoteID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataStoreItemsOtherByNoteID", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --这里作调试用
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }


        public bool InsertStoreItemsOther(ref string ErrorMsg,object NoteID, object BranchStoreID, object ValidFlag, object PrdID, object IntoQty, object IntoPrice, object InventoryQty, object InventoryPrice, object InventoryAMT, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[10];

            arParams[0] = new SqlParameter("@NoteID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@BranchStoreID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@ValidFlag", SqlDbType.Bit);
            arParams[3] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@IntoQty", SqlDbType.Decimal);
            arParams[4].Precision = 18;
            arParams[4].Scale = 4;
            arParams[5] = new SqlParameter("@IntoPrice", SqlDbType.Money);
            arParams[6] = new SqlParameter("@InventoryQty", SqlDbType.Decimal);
            arParams[6].Precision = 18;
            arParams[6].Scale = 4;
            arParams[7] = new SqlParameter("@InventoryPrice", SqlDbType.Money);
            arParams[8] = new SqlParameter("@InventoryAMT", SqlDbType.Money);
            arParams[9] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[9].Size = 100;
            arParams[0].Value = NoteID;
            arParams[1].Value = BranchStoreID;
            arParams[2].Value = ValidFlag;
            arParams[3].Value = PrdID;
            arParams[4].Value = IntoQty;
            arParams[5].Value = IntoPrice;
            arParams[6].Value = InventoryQty;
            arParams[7].Value = InventoryPrice;
            arParams[8].Value = InventoryAMT;
            arParams[9].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertStoreItemsOther", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }

        public bool UpdateStoreItemsOther(ref string ErrorMsg, object DateRegister, object BranchStoreID, object ValidFlag, object PrdID, object IntoQty, object IntoPrice, object InventoryQty, object InventoryPrice, object InventoryAMT, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[10];
            arParams[0] = new SqlParameter("@DateRegister", SqlDbType.DateTime);
            arParams[1] = new SqlParameter("@BranchStoreID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@ValidFlag", SqlDbType.Bit);
            arParams[3] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@IntoQty", SqlDbType.Decimal);
            arParams[4].Precision = 18;
            arParams[4].Scale = 4;
            arParams[5] = new SqlParameter("@IntoPrice", SqlDbType.Money);
            arParams[6] = new SqlParameter("@InventoryQty", SqlDbType.Decimal);
            arParams[6].Precision = 18;
            arParams[6].Scale = 4;
            arParams[7] = new SqlParameter("@InventoryPrice", SqlDbType.Money);
            arParams[8] = new SqlParameter("@InventoryAMT", SqlDbType.Money);
            arParams[9] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[9].Size = 100;
            arParams[0].Value = DateRegister;
            arParams[1].Value = BranchStoreID;
            arParams[2].Value = ValidFlag;
            arParams[3].Value = PrdID;
            arParams[4].Value = IntoQty;
            arParams[5].Value = IntoPrice;
            arParams[6].Value = InventoryQty;
            arParams[7].Value = InventoryPrice;
            arParams[8].Value = InventoryAMT;
            arParams[9].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateStoreItemsOther", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }


        public bool DeleteStoreItemsOther(ref string ErrorMsg, object RegisterID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@RegisterID", SqlDbType.BigInt);
            arParams[0].Value = RegisterID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteStoreItemsOther", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
    }
}
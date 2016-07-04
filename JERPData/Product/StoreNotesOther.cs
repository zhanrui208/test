using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
namespace JERPData.Product
{
    /// <描述>
    /// 表[prd.StoreNotesOther]数据访问类
    ///</描述> 
    ///<作者> 
    /// 金优富
    ///</作者> 
    ///<时间> 
    /// 2016/6/20 17:11:20
    ///</时间>  
    public class StoreNotesOther
    {
        private SqlConnection sqlConn;
        public StoreNotesOther()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public DataSet GetDataStoreNotesOtherDescPagesFreeSearch(int PageIndex, int PageSize, ref int RecordCount, string WhereClause)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@PageIndex", SqlDbType.Int);
            arParams[1] = new SqlParameter("@PageSize", SqlDbType.Int);
            arParams[2] = new SqlParameter("@RecordCount", SqlDbType.Int);
            arParams[2].Direction = ParameterDirection.InputOutput;
            arParams[3] = new SqlParameter("@WhereClause", SqlDbType.NVarChar);
            arParams[3].Size = -1;
            arParams[0].Value = PageIndex;
            arParams[1].Value = PageSize;
            arParams[2].Value = RecordCount;
            arParams[3].Value = WhereClause;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open(); 
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataStoreNotesOtherDescPagesFreeSearch", arParams);
                RecordCount = (int)arParams[2].Value;
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

        public bool InsertIntoStoreNotesOther(ref string ErrorMsg, ref object NoteID, ref object NoteCode, object DateNote,object MakerPsnID, object SupplyID, object OrderPers, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[7];

            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;

            arParams[1] = new SqlParameter("@NoteCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[1].Direction = ParameterDirection.InputOutput;

            arParams[2] = new SqlParameter("@DateNote", SqlDbType.DateTime);


            arParams[3] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@SupplyID", SqlDbType.Int);
            arParams[5] = new SqlParameter("@OrderPers", SqlDbType.Int);
            arParams[6] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[6].Size = 100;

            arParams[2].Value = DateNote;
            arParams[3].Value = MakerPsnID;
            arParams[4].Value = SupplyID;
            arParams[5].Value = OrderPers;
            arParams[6].Value = Memo;

            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertIntoStoreNotesOther", arParams);
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

        public bool UpdateStoreNotesOther(ref string ErrorMsg, object NoteID, object SerialNo, object DateNote, object NoteCode, object MakerPsnID, object SupplyID, object OrderPers, object ConfirmPsnID, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[9];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@SerialNo", SqlDbType.Int);
            arParams[2] = new SqlParameter("@DateNote", SqlDbType.DateTime);
            arParams[3] = new SqlParameter("@NoteCode", SqlDbType.VarChar);
            arParams[3].Size = 50;
            arParams[4] = new SqlParameter("@MakerPsnID", SqlDbType.Int);
            arParams[5] = new SqlParameter("@SupplyID", SqlDbType.Int);
            arParams[6] = new SqlParameter("@OrderPers", SqlDbType.Int);
            arParams[7] = new SqlParameter("@ConfirmPsnID", SqlDbType.Int);
            arParams[8] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[8].Size = 100;
            arParams[0].Value = NoteID;
            arParams[1].Value = SerialNo;
            arParams[2].Value = DateNote;
            arParams[3].Value = NoteCode;
            arParams[4].Value = MakerPsnID;
            arParams[5].Value = SupplyID;
            arParams[6].Value = OrderPers;
            arParams[7].Value = ConfirmPsnID;
            arParams[8].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateStoreNotesOther", arParams);
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


        public bool DeleteStoreNotesOther(ref string ErrorMsg, object NoteID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteStoreNotesOther", arParams);
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
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
    /// ��[mtr.OtherOutStoreItems]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/12/13 16:48:14
    ///</ʱ��>  
    public class OtherOutStoreItems
    {
        private SqlConnection sqlConn;
        public OtherOutStoreItems()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public DataSet GetDataOtherOutStoreItemsByNoteID(long NoteID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "mtr.GetDataOtherOutStoreItemsByNoteID", arParams);
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
        public DataSet GetDataOtherOutStoreItemsPivotMonth(int Year, int DeptID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@Year", SqlDbType.Int);
            arParams[1] = new SqlParameter("@DeptID", SqlDbType.Int);
            arParams[0].Value = Year;
            arParams[1].Value = DeptID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "mtr.GetDataOtherOutStoreItemsPivotMonth", arParams);
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
        public bool InsertOtherOutStoreItems(ref string ErrorMsg, object NoteID, object PrdID, object Quantity, object BranchStoreID, object CostPrice)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[2].Precision = 18;
            arParams[2].Scale = 4;
            arParams[3] = new SqlParameter("@BranchStoreID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@CostPrice", SqlDbType.Money);
            arParams[0].Value = NoteID;
            arParams[1].Value = PrdID;
            arParams[2].Value = Quantity;
            arParams[3].Value = BranchStoreID;
            arParams[4].Value = CostPrice;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "mtr.InsertOtherOutStoreItems", arParams);
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
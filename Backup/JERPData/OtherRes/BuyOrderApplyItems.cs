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
namespace JERPData.OtherRes
{
    /// <����>
    /// ��[otherRes.BuyOrderApplyItems]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2016/6/6 21:02:38
    ///</ʱ��>  
    public class BuyOrderApplyItems
    {
        private SqlConnection sqlConn;
        public BuyOrderApplyItems()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteBuyOrderApplyItems(ref string ErrorMsg, object ItemID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
            arParams[0].Value = ItemID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "otherRes.DeleteBuyOrderApplyItems", arParams);
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



        public DataSet GetDataBuyOrderApplyItemsByNoteID(long NoteID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[0].Value = NoteID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "otherRes.GetDataBuyOrderApplyItemsByNoteID", arParams);
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

        public bool InsertBuyOrderApplyItems(ref string ErrorMsg, ref object ItemID, object NoteID, object PrdID, object Quantity, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[5];
            arParams[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@NoteID", SqlDbType.BigInt);
            arParams[2] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[3] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[3].Precision = 18;
            arParams[3].Scale = 4;
            arParams[4] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[4].Size = 50;
            arParams[0].Value = ItemID;
            arParams[1].Value = NoteID;
            arParams[2].Value = PrdID;
            arParams[3].Value = Quantity;
            arParams[4].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "otherRes.InsertBuyOrderApplyItems", arParams);
                ItemID = arParams[0].Value;
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
        public bool UpdateBuyOrderApplyItems(ref string ErrorMsg, object ItemID, object PrdID, object Quantity, object Memo)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[4];
            arParams[0] = new SqlParameter("@ItemID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[2].Precision = 18;
            arParams[2].Scale = 4;
            arParams[3] = new SqlParameter("@Memo", SqlDbType.VarChar);
            arParams[3].Size = 50;
            arParams[0].Value = ItemID;
            arParams[1].Value = PrdID;
            arParams[2].Value = Quantity;
            arParams[3].Value = Memo;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "otherRes.UpdateBuyOrderApplyItems", arParams);
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
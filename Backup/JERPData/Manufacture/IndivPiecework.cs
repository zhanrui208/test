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
namespace JERPData.Manufacture
{
    /// <����>
    /// ��[manuf.IndivPiecework]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015-01-18 11:22:22
    ///</ʱ��>  
    public class IndivPiecework
    {
        private SqlConnection sqlConn;
        public IndivPiecework()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }


        public bool DeleteIndivPiecework(ref string ErrorMsg, object PieceworkID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@PieceworkID", SqlDbType.BigInt);
            arParams[0].Value = PieceworkID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.DeleteIndivPiecework", arParams);
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
        public DataSet GetDataIndivPieceworkByDateManuf(DateTime DateManuf)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@DateManuf", SqlDbType.DateTime);
            arParams[0].Value = DateManuf;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataIndivPieceworkByDateManuf", arParams);
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
        public DataSet GetDataIndivPieceworkLaborWagePivotProcessType(DateTime DateBegin, DateTime DateEnd)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@DateBegin", SqlDbType.DateTime);
            arParams[1] = new SqlParameter("@DateEnd", SqlDbType.DateTime);
            arParams[0].Value = DateBegin;
            arParams[1].Value = DateEnd;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataIndivPieceworkLaborWagePivotProcessType", arParams);
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
        public DataSet GetDataIndivPieceworkQuantityPivotProcessType(DateTime DateBegin, DateTime DateEnd)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@DateBegin", SqlDbType.DateTime);
            arParams[1] = new SqlParameter("@DateEnd", SqlDbType.DateTime);
            arParams[0].Value = DateBegin;
            arParams[1].Value = DateEnd;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataIndivPieceworkQuantityPivotProcessType", arParams);
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

        public DataSet GetDataIndivPieceworkFreeSearch(string WhereClause)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@WhereClause", SqlDbType.VarChar);
            arParams[0].Size = -1;
            arParams[0].Value = WhereClause;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataIndivPieceworkFreeSearch", arParams);
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
        public DataSet GetDataIndivPieceworkPsnBadRatePivotProcessType(DateTime DateBegin, DateTime DateEnd)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@DateBegin", SqlDbType.DateTime);
            arParams[1] = new SqlParameter("@DateEnd", SqlDbType.DateTime);
            arParams[0].Value = DateBegin;
            arParams[1].Value = DateEnd;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataIndivPieceworkPsnBadRatePivotProcessType", arParams);
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
        public DataSet GetDataIndivPieceworkPsnRecord(int PsnID, DateTime DateBegin, DateTime DateEnd)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@PsnID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@DateBegin", SqlDbType.DateTime);
            arParams[2] = new SqlParameter("@DateEnd", SqlDbType.DateTime);
            arParams[0].Value = PsnID;
            arParams[1].Value = DateBegin;
            arParams[2].Value = DateEnd;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataIndivPieceworkPsnRecord", arParams);
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

        public bool InsertIndivPiecework(ref string ErrorMsg, ref object PieceworkID, object DateManuf, object ProcessTypeID, object PsnID, object PrdID, object ReworkFlag, object Quantity, object BadQty, object PriceQty)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[9];
            arParams[0] = new SqlParameter("@PieceworkID", SqlDbType.BigInt);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@DateManuf", SqlDbType.DateTime);
            arParams[2] = new SqlParameter("@ProcessTypeID", SqlDbType.Int);
            arParams[3] = new SqlParameter("@PsnID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[5] = new SqlParameter("@ReworkFlag", SqlDbType.Bit);
            arParams[6] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[6].Precision = 18;
            arParams[6].Scale = 4;
            arParams[7] = new SqlParameter("@BadQty", SqlDbType.Decimal);
            arParams[7].Precision = 18;
            arParams[7].Scale = 4;
            arParams[8] = new SqlParameter("@PriceQty", SqlDbType.Decimal);
            arParams[8].Precision = 18;
            arParams[8].Scale = 4;
            arParams[1].Value = DateManuf;
            arParams[2].Value = ProcessTypeID;
            arParams[3].Value = PsnID;
            arParams[4].Value = PrdID;
            arParams[5].Value = ReworkFlag;
            arParams[6].Value = Quantity;
            arParams[7].Value = BadQty;
            arParams[8].Value = PriceQty;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.InsertIndivPiecework", arParams);
                PieceworkID = arParams[0].Value;
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
        public bool UpdateIndivPiecework(ref string ErrorMsg, object PieceworkID, object ProcessTypeID, object PsnID, object PrdID, object ReworkFlag, object Quantity, object BadQty, object PriceQty)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[8];
            arParams[0] = new SqlParameter("@PieceworkID", SqlDbType.BigInt);
            arParams[1] = new SqlParameter("@ProcessTypeID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@PsnID", SqlDbType.Int);
            arParams[3] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[4] = new SqlParameter("@ReworkFlag", SqlDbType.Bit);
            arParams[5] = new SqlParameter("@Quantity", SqlDbType.Decimal);
            arParams[5].Precision = 18;
            arParams[5].Scale = 4;
            arParams[6] = new SqlParameter("@BadQty", SqlDbType.Decimal);
            arParams[6].Precision = 18;
            arParams[6].Scale = 4;
            arParams[7] = new SqlParameter("@PriceQty", SqlDbType.Decimal);
            arParams[7].Precision = 18;
            arParams[7].Scale = 4;
            arParams[0].Value = PieceworkID;
            arParams[1].Value = ProcessTypeID;
            arParams[2].Value = PsnID;
            arParams[3].Value = PrdID;
            arParams[4].Value = ReworkFlag;
            arParams[5].Value = Quantity;
            arParams[6].Value = BadQty;
            arParams[7].Value = PriceQty;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.UpdateIndivPiecework", arParams);
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
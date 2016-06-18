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
namespace JCEData.Packing
{
    /// <����>
    /// ��[packing.BoxItems]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/9/24 9:49:40
    ///</ʱ��>  
    public class BoxItems
    {
        private SqlConnection sqlConn;
        public BoxItems()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public DataSet GetDataBoxItemsByBarcode(string Barcode)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@Barcode", SqlDbType.VarChar);
            arParams[0].Size = 200;
            arParams[0].Value = Barcode;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "packing.GetDataBoxItemsByBarcode", arParams);
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
        public DataSet GetDataBoxItemsByBoxCode(string BoxCode)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@BoxCode", SqlDbType.VarChar);
            arParams[0].Size = 50;
            arParams[0].Value = BoxCode;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "packing.GetDataBoxItemsByBoxCode", arParams);
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
        public bool GetParmBoxItemsBoxingMatch(string Barcode, int PrdID, ref bool MatchFlag)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@Barcode", SqlDbType.VarChar);
            arParams[0].Size = 200;
            arParams[1] = new SqlParameter("@PrdID", SqlDbType.Int);
            arParams[2] = new SqlParameter("@MatchFlag", SqlDbType.Bit);
            arParams[2].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = Barcode;
            arParams[1].Value = PrdID;
            arParams[2].Value = MatchFlag;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "packing.GetParmBoxItemsBoxingMatch", arParams);
                MatchFlag = (bool)arParams[2].Value;
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
        public bool GetParmBoxItemsExistFlag(string Barcode, ref bool ExistFlag)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@Barcode", SqlDbType.VarChar);
            arParams[0].Size = 200;
            arParams[1] = new SqlParameter("@ExistFlag", SqlDbType.Bit);
            arParams[1].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = Barcode;
            arParams[1].Value = ExistFlag;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "packing.GetParmBoxItemsExistFlag", arParams);
                ExistFlag = (bool)arParams[1].Value;
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
        public bool UpdateBoxItemsForBoxCode(ref string ErrorMsg, object Barcode, object BoxCode)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@Barcode", SqlDbType.VarChar);
            arParams[0].Size = 200;
            arParams[1] = new SqlParameter("@BoxCode", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[0].Value = Barcode;
            arParams[1].Value = BoxCode;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "packing.UpdateBoxItemsForBoxCode", arParams);
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
        public bool UpdateBoxItemsForChipsetCode(ref string ErrorMsg, object Barcode, object ChipsetCode)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@Barcode", SqlDbType.VarChar);
            arParams[0].Size = 200;
            arParams[1] = new SqlParameter("@ChipsetCode", SqlDbType.VarChar);
            arParams[1].Size = 200;
            arParams[0].Value = Barcode;
            arParams[1].Value = ChipsetCode;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "packing.UpdateBoxItemsForChipsetCode", arParams);
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
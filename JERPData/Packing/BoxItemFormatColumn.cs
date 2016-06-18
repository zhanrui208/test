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
namespace JERPData.Packing
{
    /// <����>
    /// ��[packing.BoxItemFormatColumn]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015/11/29 15:12:53
    ///</ʱ��>  
    public class BoxItemFormatColumn
    {
        private SqlConnection sqlConn;
        public BoxItemFormatColumn()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteBoxItemFormatColumn(ref string ErrorMsg, object FormatID)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[0].Value = FormatID;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "packing.DeleteBoxItemFormatColumn", arParams);
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
        public DataSet GetDataBoxItemFormatColumn()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "packing.GetDataBoxItemFormatColumn");
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
        public bool InsertBoxItemFormatColumn(ref string ErrorMsg, ref object FormatID, object BarcodeX, object BarcodeY, object CaptionX, object CaptionY, object CaptionWidth)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[6];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@BarcodeX", SqlDbType.Int);
            arParams[2] = new SqlParameter("@BarcodeY", SqlDbType.Int);
            arParams[3] = new SqlParameter("@CaptionX", SqlDbType.Int);
            arParams[4] = new SqlParameter("@CaptionY", SqlDbType.Int);
            arParams[5] = new SqlParameter("@CaptionWidth", SqlDbType.Int);
            arParams[0].Value = FormatID;
            arParams[1].Value = BarcodeX;
            arParams[2].Value = BarcodeY;
            arParams[3].Value = CaptionX;
            arParams[4].Value = CaptionY;
            arParams[5].Value = CaptionWidth;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "packing.InsertBoxItemFormatColumn", arParams);
                FormatID = arParams[0].Value;
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
        public bool UpdateBoxItemFormatColumn(ref string ErrorMsg, object FormatID, object BarcodeX, object BarcodeY, object CaptionX, object CaptionY, object CaptionWidth)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[6];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@BarcodeX", SqlDbType.Int);
            arParams[2] = new SqlParameter("@BarcodeY", SqlDbType.Int);
            arParams[3] = new SqlParameter("@CaptionX", SqlDbType.Int);
            arParams[4] = new SqlParameter("@CaptionY", SqlDbType.Int);
            arParams[5] = new SqlParameter("@CaptionWidth", SqlDbType.Int);
            arParams[0].Value = FormatID;
            arParams[1].Value = BarcodeX;
            arParams[2].Value = BarcodeY;
            arParams[3].Value = CaptionX;
            arParams[4].Value = CaptionY;
            arParams[5].Value = CaptionWidth;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "packing.UpdateBoxItemFormatColumn", arParams);
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
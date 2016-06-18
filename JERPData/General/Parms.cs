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
namespace JERPData.General
{
    /// <����>
    /// ��[general.Parms]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015-04-27 15:08:43
    ///</ʱ��>  
    public class Parms
    {
        private SqlConnection sqlConn;
        public Parms()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public DataSet GetDataParms()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "general.GetDataParms");
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
        public bool GetParmParmsParmValue(int ParmID, ref string ParmValue)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@ParmID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@ParmValue", SqlDbType.VarChar);
            arParams[1].Size = 200;
            arParams[1].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = ParmID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "general.GetParmParmsParmValue", arParams);
                ParmValue = (string)arParams[1].Value;
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
        public bool UpdateParms(ref string ErrorMsg, object ParmID, object ParmValue)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@ParmID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@ParmValue", SqlDbType.VarChar);
            arParams[1].Size = 200;
            arParams[0].Value = ParmID;
            arParams[1].Value = ParmValue;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "general.UpdateParms", arParams);
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
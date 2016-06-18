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
namespace JERPData.Product
{
    /// <����>
    /// ��[prd.SourceType]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2013-12-9 13:22:11
    ///</ʱ��>  
    public class SourceType
    {
        private SqlConnection sqlConn;
        public SourceType()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool GetParmSourceTypeSourceTypeID(string SourceTypeName, ref int SourceTypeID)
        {
            bool flag = false;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@SourceTypeName", SqlDbType.VarChar);
            arParams[0].Size = 50;
            arParams[1] = new SqlParameter("@SourceTypeID", SqlDbType.Int);
            arParams[1].Direction = ParameterDirection.InputOutput;
            arParams[0].Value = SourceTypeName;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                SqlHelper.ExecuteNonQuery(this.sqlConn, CommandType.StoredProcedure, "prd.GetParmSourceTypeSourceTypeID", arParams);
                SourceTypeID = (int)arParams[1].Value;
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
        public DataSet GetDataSourceType()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "prd.GetDataSourceType");
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




    }
}
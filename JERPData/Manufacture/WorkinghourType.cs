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
    /// ��[manuf.WorkinghourType]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2015-01-18 12:10:36
    ///</ʱ��>  
    public class WorkinghourType
    {
        private SqlConnection sqlConn;
        public WorkinghourType()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public DataSet GetDataWorkinghourType()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "manuf.GetDataWorkinghourType");
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


        public bool InsertWorkinghourType(ref string ErrorMsg, ref object WorkinghourTypeID, object WorkinghourTypeName, object LaborPrice)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@WorkinghourTypeID", SqlDbType.Int);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@WorkinghourTypeName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@LaborPrice", SqlDbType.Money);
            arParams[1].Value = WorkinghourTypeName;
            arParams[2].Value = LaborPrice;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.InsertWorkinghourType", arParams);
                WorkinghourTypeID = arParams[0].Value;
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
        public bool UpdateWorkinghourType(ref string ErrorMsg, object WorkinghourTypeID, object WorkinghourTypeName, object LaborPrice)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[3];
            arParams[0] = new SqlParameter("@WorkinghourTypeID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@WorkinghourTypeName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@LaborPrice", SqlDbType.Money);
            arParams[0].Value = WorkinghourTypeID;
            arParams[1].Value = WorkinghourTypeName;
            arParams[2].Value = LaborPrice;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.UpdateWorkinghourType", arParams);
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
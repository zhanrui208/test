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

namespace JCEData.Hr
{
    /// <����>
    /// ��[hr.Personnels]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2011-1-27 16:14:35
    ///</ʱ��>  
    public class Personnel
    {
        private SqlConnection sqlConn;
        public Personnel()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }
      
        public DataSet GetDataPersonnel()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "hr.GetDataPersonnel");
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
/*
$Header$
$Author$
$Date$
$Revision$
*/
using System;
using System.Data ; 
using System.Data .SqlClient ;
using System.Text;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
namespace JERPData.Finance
{  
	/// <����>
	/// ��[finance.Bank]���ݷ�����
	///</����> 
	///<����> 
	/// ���Ÿ�
	///</����> 
	///<ʱ��> 
	/// 2015-02-12 18:58:29
	///</ʱ��>  
	public class AccountBook
	{
		private SqlConnection sqlConn;
        public AccountBook()
		{
			this.sqlConn=DBConnection.JSqlDBConn;
		}
        public DataSet GetDataAccountBookBalance()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "finance.GetDataAccountBookBalance");
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
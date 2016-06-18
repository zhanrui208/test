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
    /// <描述>
    /// 表[manuf.OutSrcOrderFormat]数据访问类
    ///</描述> 
    ///<作者> 
    /// 金优富
    ///</作者> 
    ///<时间> 
    /// 2013-12-20 22:02:36
    ///</时间>  
    public class OutSrcOrderFormat
    {
        private SqlConnection sqlConn;
        public OutSrcOrderFormat()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteOutSrcOrderFormat(ref string ErrorMsg, object FormatID)
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
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.DeleteOutSrcOrderFormat", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
        public DataSet GetDataOutSrcOrderFormat()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "manuf.GetDataOutSrcOrderFormat");
            }
            catch//(SqlException ex)
            {
                // ex.Message --这里作调试用
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }
        public DataSet GetDataOutSrcOrderFormatByFormatID(int FormatID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[0].Value = FormatID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "manuf.GetDataOutSrcOrderFormatByFormatID", arParams);
            }
            catch//(SqlException ex)
            {
                // ex.Message --这里作调试用
            }
            finally
            {
                this.sqlConn.Close();
            }
            return ds;
        }

        public bool InsertOutSrcOrderFormat(ref string ErrorMsg, ref object FormatID, object TmpSheetName, object NoteCodeCellName, object DateNoteCellName, object CompanyNameCellName, object OrderTypeCellName, object LinkmanCellName, object TelephoneCellName, object SupplierAddressCellName, object DeliverAddressCellName, object DeliverTypeCellName, object ExpressRequireCellName, object MoneyTypeCellName, object SettleTypeCellName, object PriceTypeCellName, object InvoiceTypeCellName, object MakerPsnCellName, object MemoCellName, object ItemRowIndex, object ItemRowCount)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[20];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@TmpSheetName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@NoteCodeCellName", SqlDbType.VarChar);
            arParams[2].Size = 50;
            arParams[3] = new SqlParameter("@DateNoteCellName", SqlDbType.VarChar);
            arParams[3].Size = 50;
            arParams[4] = new SqlParameter("@CompanyNameCellName", SqlDbType.VarChar);
            arParams[4].Size = 50;
            arParams[5] = new SqlParameter("@OrderTypeCellName", SqlDbType.VarChar);
            arParams[5].Size = 50;
            arParams[6] = new SqlParameter("@LinkmanCellName", SqlDbType.VarChar);
            arParams[6].Size = 50;
            arParams[7] = new SqlParameter("@TelephoneCellName", SqlDbType.VarChar);
            arParams[7].Size = 50;
            arParams[8] = new SqlParameter("@SupplierAddressCellName", SqlDbType.VarChar);
            arParams[8].Size = 50;
            arParams[9] = new SqlParameter("@DeliverAddressCellName", SqlDbType.VarChar);
            arParams[9].Size = 50;
            arParams[10] = new SqlParameter("@DeliverTypeCellName", SqlDbType.VarChar);
            arParams[10].Size = 50;
            arParams[11] = new SqlParameter("@ExpressRequireCellName", SqlDbType.VarChar);
            arParams[11].Size = 50;
            arParams[12] = new SqlParameter("@MoneyTypeCellName", SqlDbType.VarChar);
            arParams[12].Size = 50;
            arParams[13] = new SqlParameter("@SettleTypeCellName", SqlDbType.VarChar);
            arParams[13].Size = 50;
            arParams[14] = new SqlParameter("@PriceTypeCellName", SqlDbType.VarChar);
            arParams[14].Size = 50;
            arParams[15] = new SqlParameter("@InvoiceTypeCellName", SqlDbType.VarChar);
            arParams[15].Size = 50;
            arParams[16] = new SqlParameter("@MakerPsnCellName", SqlDbType.VarChar);
            arParams[16].Size = 50;
            arParams[17] = new SqlParameter("@MemoCellName", SqlDbType.VarChar);
            arParams[17].Size = 50;
            arParams[18] = new SqlParameter("@ItemRowIndex", SqlDbType.Int);
            arParams[19] = new SqlParameter("@ItemRowCount", SqlDbType.Int);
            arParams[1].Value = TmpSheetName;
            arParams[2].Value = NoteCodeCellName;
            arParams[3].Value = DateNoteCellName;
            arParams[4].Value = CompanyNameCellName;
            arParams[5].Value = OrderTypeCellName;
            arParams[6].Value = LinkmanCellName;
            arParams[7].Value = TelephoneCellName;
            arParams[8].Value = SupplierAddressCellName;
            arParams[9].Value = DeliverAddressCellName;
            arParams[10].Value = DeliverTypeCellName;
            arParams[11].Value = ExpressRequireCellName;
            arParams[12].Value = MoneyTypeCellName;
            arParams[13].Value = SettleTypeCellName;
            arParams[14].Value = PriceTypeCellName;
            arParams[15].Value = InvoiceTypeCellName;
            arParams[16].Value = MakerPsnCellName;
            arParams[17].Value = MemoCellName;
            arParams[18].Value = ItemRowIndex;
            arParams[19].Value = ItemRowCount;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.InsertOutSrcOrderFormat", arParams);
                FormatID = arParams[0].Value;
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
        public bool InsertOutSrcOrderFormatFromCopy(ref string ErrorMsg, object OldFormatID, object TmpSheetName)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[2];
            arParams[0] = new SqlParameter("@OldFormatID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@TmpSheetName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[0].Value = OldFormatID;
            arParams[1].Value = TmpSheetName;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.InsertOutSrcOrderFormatFromCopy", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }

        public bool UpdateOutSrcOrderFormat(ref string ErrorMsg, object FormatID, object TmpSheetName, object NoteCodeCellName, object DateNoteCellName, object CompanyNameCellName, object OrderTypeCellName, object SupplierAddressCellName, object DeliverAddressCellName, object DeliverTypeCellName, object ExpressRequireCellName, object LinkmanCellName, object TelephoneCellName, object MoneyTypeCellName, object SettleTypeCellName, object PriceTypeCellName, object InvoiceTypeCellName, object MakerPsnCellName, object MemoCellName, object ItemRowIndex, object ItemRowCount)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[20];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@TmpSheetName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@NoteCodeCellName", SqlDbType.VarChar);
            arParams[2].Size = 50;
            arParams[3] = new SqlParameter("@DateNoteCellName", SqlDbType.VarChar);
            arParams[3].Size = 50;
            arParams[4] = new SqlParameter("@CompanyNameCellName", SqlDbType.VarChar);
            arParams[4].Size = 50;
            arParams[5] = new SqlParameter("@OrderTypeCellName", SqlDbType.VarChar);
            arParams[5].Size = 50;
            arParams[6] = new SqlParameter("@SupplierAddressCellName", SqlDbType.VarChar);
            arParams[6].Size = 50;
            arParams[7] = new SqlParameter("@DeliverAddressCellName", SqlDbType.VarChar);
            arParams[7].Size = 50;
            arParams[8] = new SqlParameter("@DeliverTypeCellName", SqlDbType.VarChar);
            arParams[8].Size = 50;
            arParams[9] = new SqlParameter("@ExpressRequireCellName", SqlDbType.VarChar);
            arParams[9].Size = 50;
            arParams[10] = new SqlParameter("@LinkmanCellName", SqlDbType.VarChar);
            arParams[10].Size = 50;
            arParams[11] = new SqlParameter("@TelephoneCellName", SqlDbType.VarChar);
            arParams[11].Size = 50;
            arParams[12] = new SqlParameter("@MoneyTypeCellName", SqlDbType.VarChar);
            arParams[12].Size = 50;
            arParams[13] = new SqlParameter("@SettleTypeCellName", SqlDbType.VarChar);
            arParams[13].Size = 50;
            arParams[14] = new SqlParameter("@PriceTypeCellName", SqlDbType.VarChar);
            arParams[14].Size = 50;
            arParams[15] = new SqlParameter("@InvoiceTypeCellName", SqlDbType.VarChar);
            arParams[15].Size = 50;
            arParams[16] = new SqlParameter("@MakerPsnCellName", SqlDbType.VarChar);
            arParams[16].Size = 50;
            arParams[17] = new SqlParameter("@MemoCellName", SqlDbType.VarChar);
            arParams[17].Size = 50;
            arParams[18] = new SqlParameter("@ItemRowIndex", SqlDbType.Int);
            arParams[19] = new SqlParameter("@ItemRowCount", SqlDbType.Int);
            arParams[0].Value = FormatID;
            arParams[1].Value = TmpSheetName;
            arParams[2].Value = NoteCodeCellName;
            arParams[3].Value = DateNoteCellName;
            arParams[4].Value = CompanyNameCellName;
            arParams[5].Value = OrderTypeCellName;
            arParams[6].Value = SupplierAddressCellName;
            arParams[7].Value = DeliverAddressCellName;
            arParams[8].Value = DeliverTypeCellName;
            arParams[9].Value = ExpressRequireCellName;
            arParams[10].Value = LinkmanCellName;
            arParams[11].Value = TelephoneCellName;
            arParams[12].Value = MoneyTypeCellName;
            arParams[13].Value = SettleTypeCellName;
            arParams[14].Value = PriceTypeCellName;
            arParams[15].Value = InvoiceTypeCellName;
            arParams[16].Value = MakerPsnCellName;
            arParams[17].Value = MemoCellName;
            arParams[18].Value = ItemRowIndex;
            arParams[19].Value = ItemRowCount;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "manuf.UpdateOutSrcOrderFormat", arParams);
                DBTransaction.Commit();
                flag = true;
            }
            catch (SqlException ex)
            {
                ErrorMsg = ex.Message; //返回错误信息
                flag = false;
                DBTransaction.Rollback();//--回退事务
            }
            finally
            {
                this.sqlConn.Close();
            }
            return flag;
        }
    }
}
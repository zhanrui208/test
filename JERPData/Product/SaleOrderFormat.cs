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
    /// ��[prd.SaleOrderFormat]���ݷ�����
    ///</����> 
    ///<����> 
    /// ���Ÿ�
    ///</����> 
    ///<ʱ��> 
    /// 2013-12-20 22:02:36
    ///</ʱ��>  
    public class SaleOrderFormat
    {
        private SqlConnection sqlConn;
        public SaleOrderFormat()
        {
            this.sqlConn = DBConnection.JSqlDBConn;
        }

        public bool DeleteSaleOrderFormat(ref string ErrorMsg, object FormatID)
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
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.DeleteSaleOrderFormat", arParams);
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
        public DataSet GetDataSaleOrderFormat()
        {
            DataSet ds = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, "prd.GetDataSaleOrderFormat");
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
        public DataSet GetDataSaleOrderFormatByFormatID(int FormatID)
        {
            DataSet ds = null;
            SqlParameter[] arParams = new SqlParameter[1];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[0].Value = FormatID;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                ds = SqlHelper.ExecuteDataset(sqlConn, CommandType.StoredProcedure, "prd.GetDataSaleOrderFormatByFormatID", arParams);
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


        public bool InsertSaleOrderFormat(ref string ErrorMsg, ref object FormatID, object TmpSheetName, object NoteCodeCellName, object PONoCellName, object DateNoteCellName, object CompanyNameCellName, object LegalPersonCellName, object LinkmanCellName, object TelephoneCellName, object FaxCellName, object DeliverAddressCellName, object FinanceAddressCellName, object DeliverTypeCellName, object ExpressRequireCellName, object MoneyTypeCellName, object SettleTypeCellName, object PriceTypeCellName, object InvoiceTypeCellName, object MakerPsnCellName, object SalePsnCellName, object TotalAMTCellName, object MemoCellName, object ItemRowIndex, object ItemRowCount)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[24];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[0].Direction = ParameterDirection.InputOutput;
            arParams[1] = new SqlParameter("@TmpSheetName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@NoteCodeCellName", SqlDbType.VarChar);
            arParams[2].Size = 50;
            arParams[3] = new SqlParameter("@PONoCellName", SqlDbType.VarChar);
            arParams[3].Size = 50;
            arParams[4] = new SqlParameter("@DateNoteCellName", SqlDbType.VarChar);
            arParams[4].Size = 50;
            arParams[5] = new SqlParameter("@CompanyNameCellName", SqlDbType.VarChar);
            arParams[5].Size = 50;
            arParams[6] = new SqlParameter("@LegalPersonCellName", SqlDbType.VarChar);
            arParams[6].Size = 50;
            arParams[7] = new SqlParameter("@LinkmanCellName", SqlDbType.VarChar);
            arParams[7].Size = 50;
            arParams[8] = new SqlParameter("@TelephoneCellName", SqlDbType.VarChar);
            arParams[8].Size = 50;
            arParams[9] = new SqlParameter("@FaxCellName", SqlDbType.VarChar);
            arParams[9].Size = 50;
            arParams[10] = new SqlParameter("@DeliverAddressCellName", SqlDbType.VarChar);
            arParams[10].Size = 50;
            arParams[11] = new SqlParameter("@FinanceAddressCellName", SqlDbType.VarChar);
            arParams[11].Size = 50;
            arParams[12] = new SqlParameter("@DeliverTypeCellName", SqlDbType.VarChar);
            arParams[12].Size = 50;
            arParams[13] = new SqlParameter("@ExpressRequireCellName", SqlDbType.VarChar);
            arParams[13].Size = 50;
            arParams[14] = new SqlParameter("@MoneyTypeCellName", SqlDbType.VarChar);
            arParams[14].Size = 50;
            arParams[15] = new SqlParameter("@SettleTypeCellName", SqlDbType.VarChar);
            arParams[15].Size = 50;
            arParams[16] = new SqlParameter("@PriceTypeCellName", SqlDbType.VarChar);
            arParams[16].Size = 50;
            arParams[17] = new SqlParameter("@InvoiceTypeCellName", SqlDbType.VarChar);
            arParams[17].Size = 50;
            arParams[18] = new SqlParameter("@MakerPsnCellName", SqlDbType.VarChar);
            arParams[18].Size = 50;
            arParams[19] = new SqlParameter("@SalePsnCellName", SqlDbType.VarChar);
            arParams[19].Size = 50;
            arParams[20] = new SqlParameter("@TotalAMTCellName", SqlDbType.VarChar);
            arParams[20].Size = 50;
            arParams[21] = new SqlParameter("@MemoCellName", SqlDbType.VarChar);
            arParams[21].Size = 50;
            arParams[22] = new SqlParameter("@ItemRowIndex", SqlDbType.Int);
            arParams[23] = new SqlParameter("@ItemRowCount", SqlDbType.Int);
            arParams[1].Value = TmpSheetName;
            arParams[2].Value = NoteCodeCellName;
            arParams[3].Value = PONoCellName;
            arParams[4].Value = DateNoteCellName;
            arParams[5].Value = CompanyNameCellName;
            arParams[6].Value = LegalPersonCellName;
            arParams[7].Value = LinkmanCellName;
            arParams[8].Value = TelephoneCellName;
            arParams[9].Value = FaxCellName;
            arParams[10].Value = DeliverAddressCellName;
            arParams[11].Value = FinanceAddressCellName;
            arParams[12].Value = DeliverTypeCellName;
            arParams[13].Value = ExpressRequireCellName;
            arParams[14].Value = MoneyTypeCellName;
            arParams[15].Value = SettleTypeCellName;
            arParams[16].Value = PriceTypeCellName;
            arParams[17].Value = InvoiceTypeCellName;
            arParams[18].Value = MakerPsnCellName;
            arParams[19].Value = SalePsnCellName;
            arParams[20].Value = TotalAMTCellName;
            arParams[21].Value = MemoCellName;
            arParams[22].Value = ItemRowIndex;
            arParams[23].Value = ItemRowCount;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertSaleOrderFormat", arParams);
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
        public bool InsertSaleOrderFormatFromCopy(ref string ErrorMsg, object OldFormatID, object TmpSheetName)
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
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.InsertSaleOrderFormatFromCopy", arParams);
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
        public bool UpdateSaleOrderFormat(ref string ErrorMsg, object FormatID, object TmpSheetName, object NoteCodeCellName, object PONoCellName, object DateNoteCellName, object CompanyNameCellName, object LegalPersonCellName, object LinkmanCellName, object TelephoneCellName, object FaxCellName, object DeliverAddressCellName, object FinanceAddressCellName, object DeliverTypeCellName, object ExpressRequireCellName, object MoneyTypeCellName, object SettleTypeCellName, object PriceTypeCellName, object InvoiceTypeCellName, object MakerPsnCellName, object SalePsnCellName, object TotalAMTCellName, object MemoCellName, object ItemRowIndex, object ItemRowCount)
        {
            bool flag = false;
            ErrorMsg = string.Empty;
            SqlParameter[] arParams = new SqlParameter[24];
            arParams[0] = new SqlParameter("@FormatID", SqlDbType.Int);
            arParams[1] = new SqlParameter("@TmpSheetName", SqlDbType.VarChar);
            arParams[1].Size = 50;
            arParams[2] = new SqlParameter("@NoteCodeCellName", SqlDbType.VarChar);
            arParams[2].Size = 50;
            arParams[3] = new SqlParameter("@PONoCellName", SqlDbType.VarChar);
            arParams[3].Size = 50;
            arParams[4] = new SqlParameter("@DateNoteCellName", SqlDbType.VarChar);
            arParams[4].Size = 50;
            arParams[5] = new SqlParameter("@CompanyNameCellName", SqlDbType.VarChar);
            arParams[5].Size = 50;
            arParams[6] = new SqlParameter("@LegalPersonCellName", SqlDbType.VarChar);
            arParams[6].Size = 50;
            arParams[7] = new SqlParameter("@LinkmanCellName", SqlDbType.VarChar);
            arParams[7].Size = 50;
            arParams[8] = new SqlParameter("@TelephoneCellName", SqlDbType.VarChar);
            arParams[8].Size = 50;
            arParams[9] = new SqlParameter("@FaxCellName", SqlDbType.VarChar);
            arParams[9].Size = 50;
            arParams[10] = new SqlParameter("@DeliverAddressCellName", SqlDbType.VarChar);
            arParams[10].Size = 50;
            arParams[11] = new SqlParameter("@FinanceAddressCellName", SqlDbType.VarChar);
            arParams[11].Size = 50;
            arParams[12] = new SqlParameter("@DeliverTypeCellName", SqlDbType.VarChar);
            arParams[12].Size = 50;
            arParams[13] = new SqlParameter("@ExpressRequireCellName", SqlDbType.VarChar);
            arParams[13].Size = 50;
            arParams[14] = new SqlParameter("@MoneyTypeCellName", SqlDbType.VarChar);
            arParams[14].Size = 50;
            arParams[15] = new SqlParameter("@SettleTypeCellName", SqlDbType.VarChar);
            arParams[15].Size = 50;
            arParams[16] = new SqlParameter("@PriceTypeCellName", SqlDbType.VarChar);
            arParams[16].Size = 50;
            arParams[17] = new SqlParameter("@InvoiceTypeCellName", SqlDbType.VarChar);
            arParams[17].Size = 50;
            arParams[18] = new SqlParameter("@MakerPsnCellName", SqlDbType.VarChar);
            arParams[18].Size = 50;
            arParams[19] = new SqlParameter("@SalePsnCellName", SqlDbType.VarChar);
            arParams[19].Size = 50;
            arParams[20] = new SqlParameter("@TotalAMTCellName", SqlDbType.VarChar);
            arParams[20].Size = 50;
            arParams[21] = new SqlParameter("@MemoCellName", SqlDbType.VarChar);
            arParams[21].Size = 50;
            arParams[22] = new SqlParameter("@ItemRowIndex", SqlDbType.Int);
            arParams[23] = new SqlParameter("@ItemRowCount", SqlDbType.Int);
            arParams[0].Value = FormatID;
            arParams[1].Value = TmpSheetName;
            arParams[2].Value = NoteCodeCellName;
            arParams[3].Value = PONoCellName;
            arParams[4].Value = DateNoteCellName;
            arParams[5].Value = CompanyNameCellName;
            arParams[6].Value = LegalPersonCellName;
            arParams[7].Value = LinkmanCellName;
            arParams[8].Value = TelephoneCellName;
            arParams[9].Value = FaxCellName;
            arParams[10].Value = DeliverAddressCellName;
            arParams[11].Value = FinanceAddressCellName;
            arParams[12].Value = DeliverTypeCellName;
            arParams[13].Value = ExpressRequireCellName;
            arParams[14].Value = MoneyTypeCellName;
            arParams[15].Value = SettleTypeCellName;
            arParams[16].Value = PriceTypeCellName;
            arParams[17].Value = InvoiceTypeCellName;
            arParams[18].Value = MakerPsnCellName;
            arParams[19].Value = SalePsnCellName;
            arParams[20].Value = TotalAMTCellName;
            arParams[21].Value = MemoCellName;
            arParams[22].Value = ItemRowIndex;
            arParams[23].Value = ItemRowCount;
            SqlTransaction DBTransaction = null;
            try
            {
                if (this.sqlConn.State == System.Data.ConnectionState.Closed) this.sqlConn.Open();
                DBTransaction = this.sqlConn.BeginTransaction();
                SqlHelper.ExecuteNonQuery(DBTransaction, CommandType.StoredProcedure, "prd.UpdateSaleOrderFormat", arParams);
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